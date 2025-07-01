using Botticelli.Framework.Telegram.Extensions;
using Botticelli.Moderation.Api.Builders;
using Botticelli.Moderation.Api.Extensions;
using Botticelli.Moderation.Decisions;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTelegramBot<TelegramMessagePostModerationBotBuilder<BaseDecisionMaker>>(builder.Configuration, 
        botBuilder => botBuilder.AddPostModeration())
    .AddTelegramLayoutsSupport()
    .AddLogging(cfg => cfg.AddNLog());

await builder.Build().RunAsync();