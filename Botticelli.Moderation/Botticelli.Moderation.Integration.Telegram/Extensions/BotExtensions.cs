using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Builders;

namespace Botticelli.Moderation.Api.Extensions;

public static class BotExtensions
{
    public static TelegramBotBuilder<TelegramBot> AddPostModeration(this TelegramBotBuilder<TelegramBot> builder)
    {
        builder.AddOnMessageReceived((sender, args) =>
        {
            // TODO: all further business logic is here!
        });
        
        return builder;
    }
    
}