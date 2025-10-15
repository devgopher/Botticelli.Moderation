using Botticelli.Moderation.Filters.Factories;
using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Filters.Builders;

/// <summary>
///     Builder for creating filters
/// </summary>
public class FilterChainBuilder : IFilterChainBuilder
{
    private readonly List<IFilter> _filters = [];

    public FilterChainBuilder()
    {
    }

    public FilterChainBuilder(ICollection<IFilter>? filters)
    {
        _filters.AddRange(filters ?? []);
    }

    /// <summary>
    ///     Adds a func-based filter to the chain
    /// </summary>
    /// <returns>Current builder instance</returns>
    public IFilterChainBuilder WithFuncFilter(Func<Message, IFilterResult> action)
    {
        _filters.Add(FilterFactory.CreateFuncBasedFilter(action));

        return this;
    }

    /// <summary>
    ///     Adds a filter to the chain
    /// </summary>
    /// <param name="filter">Filter instance</param>
    /// <returns>Current builder instance</returns>
    public IFilterChainBuilder WithFilter(IFilter filter)
    {
        _filters.Add(filter);
        
        return this;
    }
    
    /// <summary>
    ///     Adds a filter to the chain by creating its instance
    /// </summary>
    /// <typeparam name="TFilter">Type of filter</typeparam>
    /// <returns>Current builder instance</returns>
    public IFilterChainBuilder WithFilter<TFilter>() where TFilter : IFilter, new()
    {
        _filters.Add(new TFilter());

        return this;
    }

    /// <summary>
    ///     Has initialized filters or not
    /// </summary>
    public bool HasFilters => _filters.Count != 0;

    /// <summary>
    ///     Creates a collection of filters
    /// </summary>
    /// <returns>List of created filters</returns>
    public IFilter Build() => new FilterChain(_filters.AsReadOnly());
}