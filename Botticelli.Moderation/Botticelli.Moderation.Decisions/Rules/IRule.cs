using Botticelli.Moderation.Filters;
using Botticelli.Moderation.Shared;

/// <summary>
/// Defines a contract for a rule that can evaluate filter results.
/// </summary>
public interface IRule
{
    /// <summary>
    /// Evaluates the provided filter result to determine if the rule applies.
    /// </summary>
    /// <param name="filterResult">The filter result to evaluate.</param>
    /// <returns>True if the rule applies; otherwise, false.</returns>
    bool IsApplicable(FilterResult filterResult);

    /// <summary>
    /// Executes the rule and returns a decision based on the filter result.
    /// </summary>
    /// <param name="filterResult">The filter result used to make the decision.</param>
    /// <param name="token"></param>
    /// <returns>The resulting <see cref="Decision"/> based on the rule.</returns>
    Task<Decision> Execute(FilterResult filterResult, CancellationToken token);
}