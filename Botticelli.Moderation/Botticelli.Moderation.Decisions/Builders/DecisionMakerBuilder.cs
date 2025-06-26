using Botticelli.Moderation.Integration.Interfaces;

namespace Botticelli.Moderation.Decisions.Builders;

/// <summary>
///     <inheritdoc />
/// </summary>
public class DecisionMakerBuilder<TDecisionMaker> : IDecisionMakerBuilder
where TDecisionMaker : IDecisionMaker, new()
{
    private readonly IList<IRule> _rules = new List<IRule>(5);
    
    public IDecisionMakerBuilder WithRule(IRule rule)
    {
        _rules.Add(rule);
        
        return this;
    }
    
    public IDecisionMakerBuilder WithRule<TRule>() where TRule : IRule, new()
    {
        var rule = new TRule();
        
        return WithRule(rule);
    }

    public bool HasRules => _rules.Count > 0;

    public IDecisionMaker Build()
    {
        var _decisionMaker = new TDecisionMaker();
        _decisionMaker.Rules.AddRange(_rules);

        return _decisionMaker;
    }
}