using Botticelli.Bot.Data.Settings;
using Botticelli.Client.Analytics.Settings;
using Botticelli.Framework;
using Botticelli.Framework.Builders;
using Botticelli.Framework.Exceptions;
using Botticelli.Framework.Options;
using Botticelli.Interfaces;
using Botticelli.Moderation.Filters.Builders;
using Botticelli.Moderation.Integration.Telegram.Interfaces;
using Microsoft.Extensions.DependencyInjection;

namespace Botticelli.Moderation.Integration;

/// <summary>
///     A builder class for MessagePostModerationSender
/// </summary>
/// <typeparam name="TBot"></typeparam>
/// <typeparam name="TBotBuilder"></typeparam>
public class MessagePostModerationSenderBuilder<TBot, TBotBuilder> : BotBuilder<TBot, TBotBuilder>,
    IMessagePostModerationSenderBuilder<TBot, TBotBuilder>
    where TBot : IBot<TBot>
    where TBotBuilder : BotBuilder<TBot, TBotBuilder>
{
    private readonly IFilterChainBuilder _filterChainBuilder = new FilterChainBuilder();
    protected readonly TBotBuilder InnerBotBuilder;
    private IFilter? _filterChain;

    protected MessagePostModerationSenderBuilder(TBotBuilder innerBotBuilder) => InnerBotBuilder = innerBotBuilder;

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter(IFilter filter)
    {
        _filterChainBuilder.WithFilter(filter);

        return this;
    }

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter<TFilter>()
        where TFilter : IFilter, new()
    {
        _filterChainBuilder.WithFilter(new TFilter());

        return this;
    }

    public IMessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter(Action<IFilterChainBuilder> actionBuilder)
    {
        actionBuilder(_filterChainBuilder);

        return this;
    }

    public override TBot? Build()
    {
        if (!_filterChainBuilder.HasFilters)
            throw new BotLoadingException("No filters were provided!");

        _filterChain = _filterChainBuilder.Build();

        AddOnMessageReceived((_, args) => _filterChain?.FilterMessageAsync(args.Message));

        return InnerBotBuilder.Build();
    }

    protected override TBot? InnerBuild() => InnerBotBuilder.Build();

    public override BotBuilder<TBot, TBotBuilder> AddServices(IServiceCollection services) =>
        InnerBotBuilder.AddServices(services);

    public override BotBuilder<TBot, TBotBuilder> AddAnalyticsSettings(
        AnalyticsClientSettingsBuilder<AnalyticsClientSettings> clientSettingsBuilder) =>
        InnerBotBuilder.AddAnalyticsSettings(clientSettingsBuilder);

    protected override BotBuilder<TBot, TBotBuilder> AddServerSettings(
        ServerSettingsBuilder<ServerSettings> settingsBuilder) => this; //InnerBotBuilder.AddServerSettings(settingsBuilder);

    public override BotBuilder<TBot, TBotBuilder> AddBotDataAccessSettings(
        DataAccessSettingsBuilder<DataAccessSettings> botDataAccessBuilder) =>
        InnerBotBuilder.AddBotDataAccessSettings(botDataAccessBuilder);

    public override BotBuilder<TBot, TBotBuilder> AddOnMessageSent(BaseBot.MsgSentEventHandler handler) =>
        InnerBotBuilder.AddOnMessageSent(handler);

    public override BotBuilder<TBot, TBotBuilder> AddOnMessageReceived(BaseBot.MsgReceivedEventHandler handler) =>
        InnerBotBuilder.AddOnMessageReceived(handler);

    public override BotBuilder<TBot, TBotBuilder> AddOnMessageRemoved(BaseBot.MsgRemovedEventHandler handler) =>
        InnerBotBuilder.AddOnMessageRemoved(handler);

    public override BotBuilder<TBot, TBotBuilder> AddNewChatMembers(BaseBot.NewChatMembersEventHandler handler) =>
        InnerBotBuilder.AddNewChatMembers(handler);
}