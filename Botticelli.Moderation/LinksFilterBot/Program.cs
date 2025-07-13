using Botticelli.Framework.Telegram.Extensions;
using Botticelli.Interfaces;
using Botticelli.Moderation.Api.Extensions;
using Botticelli.Moderation.Decisions;
using Botticelli.Moderation.Filters.Links;
using LinkFilterBot.Decisions;
using LinksFilterBot.Executors.Telegram;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

var bot = builder.Services
    .AddPostModerationBot<BaseDecisionMaker>(builder.Configuration)
    .WithFilter<LinkFilter>()
    .WithDecisionMaker(bm => bm
        .WithRule<LinksRemoveRule>()
        .WithRule<LinksWarnRule>())
    .WithExecutor(new TelegramRemoveLinkExecutor())
    .WithExecutor(new TelegramWarnUserLinkExecutor())
    .Build();

builder.Services.AddTelegramLayoutsSupport()
    .AddLogging(cfg => cfg.AddNLog())
    .AddSingleton<IBot>(bot);

var app = builder.Build();

await app.RunAsync();