using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Moderation.Shared;

namespace Botticelli.Moderation.Decisions;

/// <summary>
///     An abstract base class for making decisions based on filter results, with support for rules.
/// </summary>
public class BaseDecisionMaker : IDecisionMaker
{
    private readonly bool _parallelInvocation = false;
    private readonly SemaphoreSlim _semaphoreSlim = new(1, 1);

    public BaseDecisionMaker()
    {
        Rules = new List<IRule>(5);
    }

    /// <summary>
    ///     Initializes a new instance of the <see cref="rules" /> class with the specified rules.
    /// </summary>
    /// <param name="rules">The rules to initialize the decision maker with.</param>
    protected BaseDecisionMaker(IEnumerable<IRule> rules)
    {
        Rules = [..rules];
    }

    public List<IRule> Rules { get; }

    /// <summary>
    ///     Asynchronously makes a decision based on the provided filter result.
    /// </summary>
    /// <param name="filterResult">The filter result used to determine the decision.</param>
    /// <param name="cancellationToken">A cancellation token to signal the operation's cancellation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the resulting <see cref="Decision" />.</returns>
    public virtual async Task<Decision> MakeDecision(IFilterResult filterResult,
        CancellationToken cancellationToken = default)
    {
        // Implement decision-making logic based on the rules
        var decision = new Decision
        {
            Id = Guid.NewGuid().ToString(),
            Comments = string.Empty,
            Reasons = [],
            UtcDateTime = DateTime.UtcNow,
            AdditionalParams = null
        };

        if (!_parallelInvocation)
            foreach (var rule in Rules)
                await GetDecisionByRule(filterResult, cancellationToken, rule, decision).ConfigureAwait(false);
        else
            await Parallel.ForEachAsync(Rules, cancellationToken,
                async (rule, token) => await GetDecisionByRule(filterResult, token, rule, decision));

        // Return a default decision if no rules apply
        return decision;
    }

    /// <summary>
    ///     Gets decision using a particular rule
    /// </summary>
    /// <param name="filterResult"></param>
    /// <param name="cancellationToken"></param>
    /// <param name="rule"></param>
    /// <param name="decision"></param>
    private async Task GetDecisionByRule(IFilterResult filterResult, CancellationToken cancellationToken,
        IRule rule, Decision? decision)
    {
        var execute = await ApplyRule(filterResult, rule, cancellationToken);

        if (execute != null && execute.Reasons.Count != 0 && decision != null)
        {
            await _semaphoreSlim.WaitAsync(cancellationToken);
            try
            {
                decision.Comments += $"\n{execute.Comments}";
                decision.Reasons.AddRange(execute.Reasons);
                decision.AdditionalParams?.AddRange(execute.AdditionalParams ?? []);
                decision.Types ??= [];
                if (execute.Type != null) 
                    decision.Types.Add(execute.Type);
            }
            finally
            {
                _semaphoreSlim.Release();
            }
        }
    }

    /// <summary>
    ///     Applies a particular rule
    /// </summary>
    /// <param name="filterResult"></param>
    /// <param name="rule"></param>
    /// <param name="token"></param>
    /// <returns></returns>
    private static async Task<RuleDecision?> ApplyRule(IFilterResult filterResult, IRule rule, CancellationToken token)
    {
        return rule.IsApplicable(filterResult) ? await rule.Execute(filterResult, token) : null;
    }
}