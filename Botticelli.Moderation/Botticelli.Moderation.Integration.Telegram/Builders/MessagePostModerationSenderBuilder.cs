using Botticelli.Framework.Exceptions;
using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Builders;
using Botticelli.Moderation.Filters.Builders;
using Botticelli.Moderation.Integration;
using Botticelli.Moderation.Integration.Telegram.Interfaces;

namespace Botticelli.Moderation.Api.Builders;

/// <summary>
///     A builder class for MessagePostModerationSender
/// </summary>
public class TelegramMessagePostModerationSenderBuilder : MessagePostModerationSenderBuilder<TelegramBot,
    TelegramBotBuilder<TelegramBot>>
{
    private readonly IFilterChainBuilder _filterChainBuilder = new FilterChainBuilder();
    private IFilter? _filterChain;

    protected TelegramMessagePostModerationSenderBuilder(TelegramBotBuilder<TelegramBot> innerBotBuilder, IFilter? filterChain) : base(innerBotBuilder)
    {
        _filterChain = filterChain;
    }

    public override TelegramBot? Build()
    {
        if (!_filterChainBuilder.HasFilters)
            throw new BotLoadingException("No filters were provided!");

        _filterChain = _filterChainBuilder.Build();

        AddOnMessageReceived((_, args) => _filterChain?.FilterMessageAsync(args.Message));

        return InnerBotBuilder.Build();
    }

    protected override TelegramBot? InnerBuild() => InnerBotBuilder.Build();
}