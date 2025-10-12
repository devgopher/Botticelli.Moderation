using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Extensions;
using Botticelli.Moderation.Api.Builders;
using Botticelli.Moderation.Integration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Botticelli.Moderation.Api.Extensions;

public static class BotExtensions
{
    public static TelegramMessagePostModerationBotBuilder<TDecisionMaker> AddPostModerationBot<TDecisionMaker>(
        this IServiceCollection services, IConfiguration config)
        where TDecisionMaker : IDecisionMaker, new()
    {
        var botBuilder = services.AddTelegramBot<TelegramBot, TelegramMessagePostModerationBotBuilder<TDecisionMaker>>(
            config,
            botBuilder => botBuilder.AddServices(services));

        botBuilder.Prepare();
        
        return botBuilder;
    }
}