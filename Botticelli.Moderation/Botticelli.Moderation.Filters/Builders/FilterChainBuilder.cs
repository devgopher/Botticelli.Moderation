using Botticelli.Moderation.Integration.Telegram.Interfaces;

namespace Botticelli.Moderation.Filters.Builders;

/// <summary>
/// Builder for creating filters
/// </summary>
public class FilterChainBuilder : IFilterChainBuilder
{
    private readonly List<IFilter> _filters = [];

    public FilterChainBuilder()
    {
    }

    public FilterChainBuilder(ICollection<IFilter>? filters)
        => _filters.AddRange(filters ?? []);
    
    /// <summary>
    /// Adds a filter to the chain
    /// </summary>
    /// <param name="filter">Filter instance</param>
    /// <returns>Current builder instance</returns>
    public IFilterChainBuilder WithFilter(IFilter filter)
    {
        _filters.Add(filter);
        return this;
    }

    /// <summary>
    /// Adds a filter to the chain by creating its instance
    /// </summary>
    /// <typeparam name="TFilter">Type of filter</typeparam>
    /// <returns>Current builder instance</returns>
    public IFilterChainBuilder WithFilter<TFilter>() where TFilter : IFilter, new()
    {
        _filters.Add(new TFilter());
        
        return this;
    }
    
    /// <summary>
    /// Has initialized filters or not
    /// </summary>
    public bool HasFilters => _filters.Any();

    /// <summary>
    /// Creates a collection of filters
    /// </summary>
    /// <returns>List of created filters</returns>
    public IFilter Build() => new FilterChain(_filters.AsReadOnly());
}
