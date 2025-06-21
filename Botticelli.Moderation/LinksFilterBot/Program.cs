using Botticelli.Framework.Telegram.Extensions;
using Botticelli.Moderation.Api.Extensions;
using NLog.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddTelegramBot(builder.Configuration, 
//         botBuilder => botBuilder.AddPostModeration())
//     .AddTelegramLayoutsSupport()
//     .AddLogging(cfg => cfg.AddNLog());

await builder.Build().RunAsync();