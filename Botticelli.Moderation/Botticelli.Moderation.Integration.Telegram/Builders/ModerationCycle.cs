using Botticelli.Framework.Events;
using Botticelli.Framework.Telegram;
using Botticelli.Moderation.Integration.Interfaces;

namespace Botticelli.Moderation.Api.Builders;

public sealed class ModerationCycle(
    List<IExecutor<TelegramBot>> executors,
    IDecisionMaker decisionMaker,
    IFilter filterChain)
{
    public async Task Process(MessageReceivedBotEventArgs args)
    {
        // Filtering
        var filterResult = await filterChain.FilterMessageAsync(args.Message);

        // Decision-making
        var decision = await decisionMaker?.MakeDecision(filterResult)!;

        // Decision executing
        foreach (var executor in executors)
            await executor.Execute(args.Message, decision);
    }
}