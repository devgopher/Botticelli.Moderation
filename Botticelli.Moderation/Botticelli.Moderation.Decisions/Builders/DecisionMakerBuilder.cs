namespace Botticelli.Moderation.Decisions.Builders;

/// <summary>
///     <inheritdoc />
/// </summary>
public class DecisionMakerBuilder : IDecisionMakerBuilder
{
    public IDecisionMakerBuilder WithRule(IRule rule)
    {
        throw new NotImplementedException();
    }

    public IDecisionMakerBuilder WithRule<TRule>() where TRule : IRule, new()
    {
        throw new NotImplementedException();
    }

    public bool HasRules { get; }

    public IDecisionMaker Build()
    {
        throw new NotImplementedException();
    }
}