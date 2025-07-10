using System.Threading;
using System.Threading.Tasks;
using Botticelli.Interfaces;
using Botticelli.Moderation.Shared;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Integration.Interfaces;

/// <summary>
/// Represents an executor that processes messages and makes decisions based on filtering criteria.
/// </summary>
/// <typeparam name="TBot">The type of the bot that implements the <see cref="IBot{TBot}"/> interface.</typeparam>
public interface IExecutor<TBot>
    where TBot : IBot<TBot>
{
    public TBot? Bot { get; set; }
    
    /// <summary>
    /// Executes a specified action based on the provided message and decision.
    /// </summary>
    /// <param name="message">The message to be processed.</param>
    /// <param name="decision">The decision to be executed, which may include actions such as banning a user or deleting a message.</param>
    /// <param name="cancellationToken">A cancellation token that can be used to cancel the operation. The default value is <see cref="CancellationToken.None"/>.</param>
    /// <returns>A task that represents the asynchronous operation.</returns>
    Task Execute(Message message, Decision decision, CancellationToken cancellationToken = default);
}