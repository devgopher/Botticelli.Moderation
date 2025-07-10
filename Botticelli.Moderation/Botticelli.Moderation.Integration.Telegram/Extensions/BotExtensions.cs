using Botticelli.Framework.Builders;
using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Extensions;
using Botticelli.Moderation.Api.Builders;
using Botticelli.Moderation.Integration.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Botticelli.Moderation.Api.Extensions;

public static class BotExtensions
{
    public static TelegramMessagePostModerationBotBuilder<TDecisionMaker> AddPostModerationBot<TDecisionMaker>(this IServiceCollection services, IConfiguration config) 
        where TDecisionMaker : IDecisionMaker, new()
    {
        services.AddTelegramBot<TelegramMessagePostModerationBotBuilder<TDecisionMaker>>(config,
            botBuilder => botBuilder.AddServices(services));

        return services.BuildServiceProvider()
                .GetRequiredService<BotBuilder<TelegramBot>>()
            as TelegramMessagePostModerationBotBuilder<TDecisionMaker>;
    }
}