using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Filters;

/// <summary>
/// Function-based filter
/// </summary>
/// <param name="action"></param>
public class FuncFilter(Func<Message, IFilterResult> action) : FilterBase
{
    public override Task<IFilterResult> FilterMessageAsync(Message message, CancellationToken cancellationToken = default)
    {
        return Task.FromResult(action.Invoke(message));   
    }
}