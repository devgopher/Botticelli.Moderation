using System;

namespace Botticelli.Moderation.Integration.Telegram.Interfaces;

public interface IMessagePostModerationSenderBuilder<TBot, TBotBuilder>
{
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter(IFilter filter);
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter<TFilter>() where TFilter : IFilter, new();
    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter(Action<IFilterChainBuilder> actionBuilder);
}