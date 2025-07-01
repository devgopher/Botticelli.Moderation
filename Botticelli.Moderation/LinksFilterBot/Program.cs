using Botticelli.Framework.Telegram.Extensions;
using Botticelli.Moderation.Api.Extensions;
using Botticelli.Moderation.Decisions;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddPostModerationBot<BaseDecisionMaker>(builder.Configuration);

builder.Services.AddTelegramLayoutsSupport()
    .AddLogging(cfg => cfg.AddNLog());

await builder.Build().RunAsync();