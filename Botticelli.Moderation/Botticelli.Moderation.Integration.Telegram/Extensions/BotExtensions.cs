using Botticelli.Moderation.Api.Builders;

namespace Botticelli.Moderation.Api.Extensions;

public static class BotExtensions
{
    public static TelegramMessagePostModerationSenderBuilder AddPostModeration(
        this TelegramMessagePostModerationSenderBuilder builder)
    {
        builder.AddOnMessageReceived((sender, args) =>
        {
            // TODO: all further business logic is here!
        });

        return builder;
    }
}