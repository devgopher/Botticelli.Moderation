namespace Botticelli.Moderation.Shared;

/// <summary>
/// Provides predefined instances of <see cref="DecisionType"/> for common decision types.
/// </summary>
public static class DecisionTypes
{
    /// <summary>
    /// Gets the decision type representing a passed decision.
    /// </summary>
    public static DecisionType Passed => new DecisionType(1, "Passed");

    /// <summary>
    /// Gets the decision type representing a warning decision.
    /// </summary>
    public static DecisionType Warning => new DecisionType(2, "Warning");

    /// <summary>
    /// Gets the decision type representing a message removal decision.
    /// </summary>
    public static DecisionType RemoveMessage => new DecisionType(3, "RemoveMessage");

    /// <summary>
    /// Gets the decision type representing a ban decision.
    /// </summary>
    public static DecisionType Ban => new DecisionType(4, "Ban");
}