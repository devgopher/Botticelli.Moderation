using Botticelli.Moderation.Integration.Interfaces;

namespace Botticelli.Moderation.Decisions.Builders;

/// <summary>
///     <inheritdoc />
/// </summary>
public class DecisionMakerBuilder<TDecisionMaker> : IDecisionMakerBuilder<TDecisionMaker>
    where TDecisionMaker : IDecisionMaker, new()
{
    private readonly IList<IRule> _rules = new List<IRule>(5);

    public IDecisionMakerBuilder<TDecisionMaker> WithRule(IRule rule)
    {
        _rules.Add(rule);

        return this;
    }

    public IDecisionMakerBuilder<TDecisionMaker> WithRule<TRule>() where TRule : IRule, new()
    {
        var rule = new TRule();
        return WithRule(rule);
    }

    public bool HasRules => _rules.Count > 0;

    public TDecisionMaker Build()
    {
        var decisionMaker = new TDecisionMaker();
        decisionMaker.Rules.AddRange(_rules);

        return decisionMaker;
    }
}