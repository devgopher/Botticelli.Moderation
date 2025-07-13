using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Moderation.Shared;

namespace LinkFilterBot.Decisions;

/// <summary>
/// Represents a rule that processes filter results related to links (warn user).
/// </summary>
public class LinksWarnRule : IRule
{
    /// <summary>
    /// Determines whether the rule is applicable to the given filter result.
    /// </summary>
    /// <param name="filterResult">The filter result to evaluate for applicability.</param>
    /// <returns><c>true</c> if the rule is applicable; otherwise, <c>false</c>.</returns>
    public bool IsApplicable(IFilterResult filterResult)
    {
        // This rule is always applicable.
        return true;
    }

    /// <summary>
    /// Executes the rule on the provided filter result and returns a decision.
    /// </summary>
    /// <param name="filterResult">The filter result to process.</param>
    /// <param name="token">A cancellation token that can be used to cancel the operation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the decision made.</returns>
    public async Task<RuleDecision> Execute(IFilterResult filterResult, CancellationToken token)
    {
        // Check if the filter result indicates that the message did not pass the filter.
        if (!filterResult.Passed)
        {
            return new RuleDecision
            {
                Id = Guid.NewGuid()
                    .ToString(),
                Comments = "Link found in the message.",
                Reasons = filterResult.Errors.ToList(),
                Type = DecisionTypes.Warning,
                UtcDateTime = DateTime.UtcNow
            };
        }

        // If the message passed the filter, return a decision indicating it is acceptable.
        return new RuleDecision
        {
            Id = Guid.NewGuid().ToString(),
            Comments = "No links found in the message.",
            Reasons = filterResult.Errors.ToList(),
            Type = DecisionTypes.Passed,
            UtcDateTime = DateTime.UtcNow
        };
    }
}