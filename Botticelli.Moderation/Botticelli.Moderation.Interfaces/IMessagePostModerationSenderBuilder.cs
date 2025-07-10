using System;
using Botticelli.Interfaces;

namespace Botticelli.Moderation.Integration.Interfaces;

public interface IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> where TBot : IBot<TBot>
    where TDecisionMaker : IDecisionMaker, new()
{
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> WithFilter(IFilter filter);

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> WithFilter<TFilter>()
        where TFilter : IFilter, new();

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> WithFilter(
        Action<IFilterChainBuilder> actionBuilder);

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> WithDecisionMaker(
        Action<IDecisionMakerBuilder<TDecisionMaker>> decisionMakerBuilder);

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> WithDecisionMaker(
        IDecisionMakerBuilder<TDecisionMaker> decisionMakerBuilder);

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> WithDecisionMaker<
        TDecisionMakerBuilder>()
        where TDecisionMakerBuilder : IDecisionMakerBuilder<TDecisionMaker>, new();

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> WithExecutor<TExecutor>(
        Action<IExecutor<TBot>> actionBuilder)
        where TExecutor : IExecutor<TBot>, new();

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker>
        WithExecutor(IExecutor<TBot> executor);

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder, TDecisionMaker> WithExecutor<TExecutor>(
        TExecutor executor)
        where TExecutor : IExecutor<TBot>, new();

    public TBot? Build();
}