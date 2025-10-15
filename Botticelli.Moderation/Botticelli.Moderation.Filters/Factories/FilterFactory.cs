using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Filters.Factories;

/// <summary>
///     Factory class for creating filters.
/// </summary>
public static class FilterFactory
{
    /// <summary>
    ///     Creates a function-based filter.
    /// </summary>
    /// <param name="action">The function to be used as the filter action.</param>
    /// <returns>An instance of FuncBasedFilter.</returns>
    public static FuncFilter CreateFuncBasedFilter(Func<Message, IFilterResult> action)
    {
        if (action == null) throw new ArgumentNullException(nameof(action), "Filter action cannot be null.");

        return new FuncFilter(action);
    }
}