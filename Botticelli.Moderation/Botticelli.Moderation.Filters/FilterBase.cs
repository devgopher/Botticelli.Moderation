using Botticelli.Moderation.Integration.Telegram.Interfaces;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Filters;

/// <summary>
/// Base abstract class for filters
/// </summary>
public abstract class FilterBase : IFilter
{
    /// <summary>
    /// Filtering method
    /// </summary>
    /// <param name="message"></param>
    /// <param name="cancellationToken"></param>
    /// <returns></returns>
    /// <exception cref="NotImplementedException"></exception>
    public abstract Task<IFilterResult> FilterMessageAsync(Message message, CancellationToken cancellationToken = default);

    /// <summary>
    /// Virtual method for filter initialization
    /// </summary>
    protected virtual void Initialize() { }
}