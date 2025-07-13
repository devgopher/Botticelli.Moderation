using Botticelli.Moderation.Decisions;
using Botticelli.Moderation.Integration.Interfaces;

namespace LinkFilterBot.Decisions.Extensions;

/// <summary>
///     Provides extension methods for configuring decision makers.
/// </summary>
public static class DecisionMakerExtensions
{
    /// <summary>
    ///     Adds rules to the specified decision maker builder.
    /// </summary>
    /// <param name="decisionMakerBuilder">The decision maker builder to which the rules will be added.</param>
    /// <returns>The updated decision maker builder with the added rules.</returns>
    public static IDecisionMakerBuilder<BaseDecisionMaker> AddRules(
        this IDecisionMakerBuilder<BaseDecisionMaker> decisionMakerBuilder)
    {
        return decisionMakerBuilder.WithRule<LinksRule>();
    }
}