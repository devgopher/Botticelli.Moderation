using Botticelli.Moderation.Integration.Telegram.Interfaces;
using Botticelli.Moderation.Shared;

namespace Botticelli.Moderation.Decisions.Rules;

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
    bool IsApplicable(IFilterResult filterResult);

    /// <summary>
    /// Executes the rule and returns a decision based on the filter result.
    /// </summary>
    /// <param name="filterResult">The filter result used to make the decision.</param>
    /// <param name="token"></param>
    /// <returns>The resulting <see cref="Decision"/> based on the rule.</returns>
    Task<Decision> Execute(IFilterResult filterResult, CancellationToken token);
}