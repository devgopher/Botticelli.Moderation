using System;
using Botticelli.Interfaces;

namespace Botticelli.Moderation.Integration.Interfaces;

public interface IMessagePostModerationSenderBuilder<TBot, TBotBuilder> where TBot : IBot<TBot>
{
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter(IFilter filter);
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter<TFilter>() where TFilter : IFilter, new();
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter(Action<IFilterChainBuilder> actionBuilder);
    
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithDecisionMaker(Action<IDecisionMakerBuilder> decisionMakerBuilder);
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithDecisionMaker(IDecisionMakerBuilder decisionMakerBuilder);

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithDecisionMaker<TDecisionMakerBuilder>()
        where TDecisionMakerBuilder : IDecisionMakerBuilder, new();
    
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithExecutor(Action<IExecutor<TBot>> actionBuilder);
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithExecutor(IExecutor<TBot> executor);
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithExecutor<TExecutor>(TExecutor executor) 
        where TExecutor : IExecutor<TBot>;
}