using Botticelli.Framework.Exceptions;
using Botticelli.Framework.Telegram;
using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Moderation.Shared;
using Botticelli.Moderation.Shared.Extensions;
using Botticelli.Shared.API.Client.Requests;
using Botticelli.Shared.ValueObjects;

namespace LinksFilterBot.Executors.Telegram;

/// <summary>
/// Represents an executor for handling Telegram messages in the Links Filter Bot.
/// Implements the <see cref="IExecutor{T}" /> interface for the <see cref="TelegramBot" />.
/// </summary>
public class TelegramWarnUserLinkExecutor : IExecutor<TelegramBot>
{
    /// <summary>
    /// Gets or sets the bot instance used to interact with Telegram.
    /// </summary>
    public TelegramBot? Bot { get; set; }

    /// <summary>
    /// Executes the moderation decision for a given Telegram message.
    /// </summary>
    /// <param name="message">The Telegram message to be processed.</param>
    /// <param name="decision">The moderation decision to be applied to the message.</param>
    /// <param name="cancellationToken">A cancellation token to observe while waiting for the task to complete.</param>
    /// <exception cref="NullReferenceException">Thrown when the <see cref="Bot"/> property is null.</exception>
    public async Task Execute(Message message, Decision decision, CancellationToken cancellationToken = default)
    {
        // Check if the Bot instance is null and throw an exception if it is.
        if (Bot == null)
            throw new ModerationException("Bot is null!");

        // If the decision type is to remove the message, delete it using the bot.
        if (decision.Types != null && decision.Types.Contains(DecisionTypes.Warning))
        {
            await Bot.SendMessageAsync(new SendMessageRequest(message.Uid)
            {
                Message = new Message
                {
                    Type = Message.MessageType.Messaging,
                    ChatIds = message.ChatIds,
                    Subject = string.Empty,
                    Body = $"Dear, @{message.From?.NickName}! No URLs allowed!"
                }
            }, cancellationToken);
        }
    }
}