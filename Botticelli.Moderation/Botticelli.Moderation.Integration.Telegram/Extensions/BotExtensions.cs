using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Builders;
using Botticelli.Moderation.Api.Builders;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Botticelli.Framework.Telegram.Extensions;

namespace Botticelli.Moderation.Api.Extensions;

public static class BotExtensions
{
    private static readonly TelegramMessagePostModerationSenderBuilder _builder;

    // public static IServiceCollection AddTelegramBot<TBotBuilder>(this IServiceCollection services,
    //     IConfiguration configuration,
    //     Action<TelegramMessagePostModerationSenderBuilder>? telegramBotBuilderFunc = null)
    // {
    //     _builder = telegramBotBuilderFunc();
    //     
    //     return services.AddTelegramBot(configuration,
    //         (Action<TelegramBotBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>>>?)telegramBotBuilderFunc);
    // }

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