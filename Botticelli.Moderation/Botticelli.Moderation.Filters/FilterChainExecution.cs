using Botticelli.Moderation.Integration.Telegram.Interfaces;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Filters;

/// <summary>
/// Represents a chain of filters that can be executed
/// </summary>
public class FilterChain(IReadOnlyCollection<IFilter> filters) : IFilter
{
    private readonly IReadOnlyCollection<IFilter> _filters = filters ?? throw new ArgumentNullException(nameof(filters));

    /// <summary>
    /// Executes all filters in parallel and returns true only if all filters return true
    /// </summary>
    /// <returns>True if all filters pass, false otherwise</returns>
    public async Task<IFilterResult> FilterMessageAsync(Message message, CancellationToken cancellationToken = default)
    {
        try
        {
            if (_filters.Count == 0)
                return new FilterResult
                {
                    MessageId = message.Uid ?? string.Empty,
                    Message = message,
                    Passed = false,
                    Errors =
                    [
                        "No filters ia an chain!"
                    ]
                };

            var filterTasks = _filters.Select(filter => filter.FilterMessageAsync(message, cancellationToken)).ToArray();
            var results = await Task.WhenAll(filterTasks);

            var result = new FilterResult
            {
                MessageId = message.Uid ?? string.Empty,
                Message = message,
                Passed = results.All(r => r.Passed),
                Errors = results.Where(r => !r.Passed).SelectMany(r => r.Errors).ToArray()
            };

            return result;
        } catch (Exception ex)
        {
            return new FilterResult
            {
                MessageId = message.Uid ?? string.Empty,
                Message = message,
                Passed = false,
                Errors = [ex.Message]
            };
        }
    }
}