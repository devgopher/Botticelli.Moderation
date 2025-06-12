namespace Botticelli.Moderation.Filters;

public interface IFilter
{
    public Task<FilterResult> FilterMessageAsync(string message);
}