using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Moderation.Shared;

namespace LinkFilterBot.Decisions;

/// <summary>
///     Represents a rule that processes filter results related to links.
/// </summary>
public class LinksRule : IRule
{
    /// <summary>
    ///     Determines whether the rule is applicable to the given filter result.
    /// </summary>
    /// <param name="filterResult">The filter result to evaluate for applicability.</param>
    /// <returns><c>true</c> if the rule is applicable; otherwise, <c>false</c>.</returns>
    public bool IsApplicable(IFilterResult filterResult)
    {
        return true;
    }

    /// <summary>
    ///     Executes the rule on the provided filter result and returns a decision.
    /// </summary>
    /// <param name="filterResult">The filter result to process.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the decision made.</returns>
    public async Task<Decision> Execute(IFilterResult filterResult, CancellationToken token)
    {
        if (!filterResult.Passed)
            return new Decision
            {
                Id = Guid.NewGuid().ToString(),
                Comments = string.Join(',', filterResult.Errors),
                Reasons = filterResult.Errors.ToList(),
                Type = DecisionTypes.RemoveMessage
            };

        return new Decision
        {
            Id = Guid.NewGuid().ToString(),
            Comments = string.Join(',', filterResult.Errors),
            Reasons = filterResult.Errors.ToList(),
            Type = DecisionTypes.Passed
        };
    }
}