using System.Threading;
using System.Threading.Tasks;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Integration.Telegram.Interfaces;

public interface IFilter
{
    public Task<IFilterResult> FilterMessageAsync(Message message, CancellationToken cancellationToken = default);
}