namespace Botticelli.Moderation.Integration.Telegram.Interfaces;

public interface IFilterChainBuilder
{
    /// <summary>
    /// Adds a filter to the chain
    /// </summary>
    /// <param name="filter">Filter instance</param>
    /// <returns>Current builder instance</returns>
    IFilterChainBuilder WithFilter(IFilter filter);

    /// <summary>
    /// Adds a filter to the chain by creating its instance
    /// </summary>
    /// <typeparam name="TFilter">Type of filter</typeparam>
    /// <returns>Current builder instance</returns>
    IFilterChainBuilder WithFilter<TFilter>() where TFilter : IFilter, new();

    /// <summary>
    /// Has initialized filters or not
    /// </summary>
    public bool HasFilters { get; }

    /// <summary>
    /// Creates a collection of filters
    /// </summary>
    /// <returns>List of created filters</returns>
    IFilter? Build();
}