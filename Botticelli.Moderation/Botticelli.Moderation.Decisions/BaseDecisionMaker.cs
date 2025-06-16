using Botticelli.Moderation.Filters;
using Botticelli.Moderation.Shared;

namespace Botticelli.Moderation.Decisions;

/// <summary>
///     An abstract base class for making decisions based on filter results, with support for rules.
/// </summary>
public abstract class BaseDecisionMaker : IDecisionMaker
{
    private readonly bool _parallelInvocation = false;
    public List<IRule> Rules { get; }


    /// <summary>
    ///     Initializes a new instance of the <see cref="rules" /> class with the specified rules.
    /// </summary>
    /// <param name="rules">The rules to initialize the decision maker with.</param>
    protected BaseDecisionMaker(IEnumerable<IRule> rules)
    {
        Rules = [..rules];
    }

    /// <summary>
    ///     Asynchronously makes a decision based on the provided filter result.
    /// </summary>
    /// <param name="filterResult">The filter result used to determine the decision.</param>
    /// <param name="cancellationToken">A cancellation token to signal the operation's cancellation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the resulting <see cref="Decision" />.</returns>
    public async Task<Decision> MakeDecision(FilterResult filterResult, CancellationToken cancellationToken)
    {
        // Implement decision-making logic based on the rules
        var decision = new Decision
        {
            Id = Guid.NewGuid()
                .ToString(),
            Comments = string.Empty,
            Reasons = [],
            UtcDateTime = DateTime.UtcNow,
            AdditionalParams = null
        };

        if (!_parallelInvocation)
            foreach (var rule in Rules)
                await GetDecisionByRule(filterResult, cancellationToken, rule, decision).ConfigureAwait(false);
        else
            await Parallel.ForEachAsync(Rules, cancellationToken, async (rule, token) =>
            {
                await GetDecisionByRule(filterResult, token, rule, decision);
            });

        // Return a default decision if no rules apply
        return decision;
    }

    private static async Task GetDecisionByRule(FilterResult filterResult, CancellationToken cancellationToken,
        IRule rule, Decision? decision)
    {
        var execute = await ApplyRule(filterResult, rule, cancellationToken);
        
        if (execute != null && execute.Reasons.Any())
        {
            
            decision?.Reasons.AddRange(execute.Reasons);
            if (decision != null)
            {
                decision.Comments = execute.Comments;
                decision.AdditionalParams = execute.AdditionalParams ?? [];
            }
        }
    }

    private static async Task<Decision?> ApplyRule(FilterResult filterResult, IRule rule, CancellationToken token) => rule.IsApplicable(filterResult) ? await rule.Execute(filterResult, token) : null;
}