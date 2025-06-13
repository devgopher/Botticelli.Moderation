using Botticelli.Moderation.Integration.Telegram.Interfaces;
using Botticelli.Moderation.Shared;

namespace Botticelli.Moderation.Decisions;

/// <summary>
/// Defines a contract for making decisions based on filter results.
/// </summary>
public interface IDecisionMaker
{
    /// <summary>
    /// Asynchronously makes a decision based on the provided filter result.
    /// </summary>
    /// <param name="filterResult">The filter result used to determine the decision.</param>
    /// <param name="cancellationToken">A cancellation token to signal the operation's cancellation.</param>
    /// <returns>A task that represents the asynchronous operation, containing the resulting <see cref="Decision"/>.</returns>
    public Task<Decision> MakeDecision(IFilterResult filterResult, CancellationToken cancellationToken); 
}