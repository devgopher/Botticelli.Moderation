using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Builders;
using Botticelli.Moderation.Integration;

namespace Botticelli.Moderation.Api.Extensions;

public static class BotExtensions
{
    private static readonly MessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>> _builder;

    public static TelegramBotBuilder<TelegramBot> AddPostModeration(this TelegramBotBuilder<TelegramBot> builder)
    {
        builder.AddOnMessageReceived((sender, args) =>
        {
            // TODO: all further business logic is here!
        });

        return builder;
    }
}