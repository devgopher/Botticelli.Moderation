using Botticelli.Framework.Telegram;
using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Moderation.Shared;
using Botticelli.Shared.ValueObjects;

namespace LinksFilterBot.Executors.Telegram;

/// <summary>
///     Represents an executor for handling Telegram messages in the Links Filter Bot.
///     Implements the <see cref="IExecutor{T}" /> interface for the <see cref="TelegramBot" />.
/// </summary>
public class TelegramRemoveLinkExecutor : IExecutor<TelegramBot>
{
    /// <summary>
    ///     A bot instance
    /// </summary>
    public TelegramBot? Bot { get; set; }

    /// <summary>
    ///     Executes the moderation decision for a given Telegram message.
    /// </summary>
    /// <param name="message">The Telegram message to be processed.</param>
    /// <param name="decision">The moderation decision to be applied to the message.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <exception cref="NotImplementedException">Thrown when the method is called, as the implementation is not yet provided.</exception>
    public async Task Execute(Message message, Decision decision, CancellationToken cancellationToken = default)
    {
        if (Bot == null)
            throw new NullReferenceException("Bot is null!");

        if (decision.Types != null && decision.Types.Contains(DecisionTypes.RemoveMessage))
            await Bot.DeleteMessageAsync(new DeleteMessageRequest(message.Uid, message.ChatIds[0]), cancellationToken);
    }
}