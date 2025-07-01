using Botticelli.Framework.Exceptions;
using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Builders;
using Botticelli.Moderation.Decisions.Builders;
using Botticelli.Moderation.Filters.Builders;
using Botticelli.Moderation.Integration.Interfaces;

namespace Botticelli.Moderation.Api.Builders;

/// <summary>
///     A builder class for MessagePostModerationSender
/// </summary>
public class TelegramMessagePostModerationBotBuilder<TDecisionMaker> :
    TelegramBotBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>>,
    IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker> where TDecisionMaker : IDecisionMaker, new()
{
    private readonly List<IExecutor<TelegramBot>> _executors = new();
    private TDecisionMaker? _decisionMaker;
    private readonly IFilterChainBuilder _filterChainBuilder = new FilterChainBuilder();
    private IDecisionMakerBuilder<TDecisionMaker> _decisionMakerBuilder = new DecisionMakerBuilder<TDecisionMaker>();

    private IFilter? _filterChain;

    protected TelegramMessagePostModerationBotBuilder(bool isStandalone) : base(isStandalone)
    {
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker> WithFilter(IFilter filter)
    {
        _filterChainBuilder.WithFilter(filter);

        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker> WithFilter<TFilter>()
        where TFilter : IFilter, new()
    {
        _filterChainBuilder.WithFilter(new TFilter());

        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker> WithFilter(
        Action<IFilterChainBuilder> actionBuilder)
    {
        actionBuilder(_filterChainBuilder);

        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker>
        WithDecisionMaker(Action<IDecisionMakerBuilder<TDecisionMaker>> decisionMakerBuilder)
    {
        decisionMakerBuilder(_decisionMakerBuilder);
        
        return this;
    }
    
    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker> WithDecisionMaker(IDecisionMakerBuilder<TDecisionMaker> decisionMakerBuilder)
    {
       _decisionMakerBuilder = decisionMakerBuilder;
        
        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker> WithDecisionMaker<TDecisionMakerBuilder>()
        where TDecisionMakerBuilder : IDecisionMakerBuilder<TDecisionMaker>, new()
    {
        _decisionMakerBuilder = new DecisionMakerBuilder<TDecisionMaker>();
        
        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker>
        WithExecutor<TExecutor>(Action<IExecutor<TelegramBot>> actionBuilder)
        where TExecutor : IExecutor<TelegramBot>, new()
    {
        var executor = new TExecutor();

        actionBuilder(executor);

        WithExecutor(executor);
        
        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker> WithExecutor(IExecutor<TelegramBot> executor)
    {
        _executors.Add(executor);
        
        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>, TDecisionMaker>
        WithExecutor<TExecutor>(TExecutor executor) where TExecutor : IExecutor<TelegramBot>
    {
        WithExecutor((IExecutor<TelegramBot>)executor);

        return this;
    }

    public override TelegramBot? Build()
    {
        if (!_filterChainBuilder.HasFilters)
            throw new BotLoadingException("No filters were provided!");
        
        if (!_executors.Any())
            throw new BotLoadingException("No decision executors were provided!");      

        _filterChain ??= _filterChainBuilder.Build();
        _decisionMaker ??= _decisionMakerBuilder.Build();
        
        AddOnMessageReceived(async (_, args) =>
        {
            // Filtering
            var filterResult = await _filterChain!.FilterMessageAsync(args.Message);
            
            // Decision-making
            var decision = await _decisionMaker.MakeDecision(filterResult);

            // Decision executing
            foreach (var executor in _executors) 
                await executor.Execute(args.Message, decision);
        });

        return base.Build();
    }
}