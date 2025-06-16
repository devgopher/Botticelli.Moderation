namespace Botticelli.Moderation.Decisions.Builders;

/// <summary>
///     Defines a contract for building a decision maker with specified rules.
/// </summary>
public interface IDecisionMakerBuilder
{
    /// <summary>
    ///     Gets a value indicating whether any rules have been initialized for the decision maker.
    /// </summary>
    /// <value><c>true</c> if rules have been added; otherwise, <c>false</c>.</value>
    bool HasRules { get; }

    /// <summary>
    ///     Adds a specific rule to the decision maker.
    /// </summary>
    /// <param name="rule">The rule to add to the decision maker.</param>
    /// <returns>The current instance of the <see cref="IDecisionMakerBuilder" /> for method chaining.</returns>
    IDecisionMakerBuilder WithRule(IRule rule);

    /// <summary>
    ///     Adds a rule of the specified type to the decision maker.
    /// </summary>
    /// <typeparam name="TRule">The type of the rule to add, which must implement <see cref="IRule" />.</typeparam>
    /// <returns>The current instance of the <see cref="IDecisionMakerBuilder" /> for method chaining.</returns>
    IDecisionMakerBuilder WithRule<TRule>() where TRule : IRule, new();

    /// <summary>
    ///     Builds and returns an instance of <see cref="IDecisionMaker" /> with the configured rules.
    /// </summary>
    /// <returns>An instance of <see cref="IDecisionMaker" />.</returns>
    IDecisionMaker Build();
}