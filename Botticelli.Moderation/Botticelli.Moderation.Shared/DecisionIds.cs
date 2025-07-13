namespace Botticelli.Moderation.Shared;

/// <summary>
///     Provides a set of predefined decision identifiers for message filtering actions.
/// </summary>
public static class DecisionIds
{
    /// <summary>
    ///     Gets the identifier for the action that bans a user.
    /// </summary>
    public static string Ban => "Ban";

    /// <summary>
    ///     Gets the identifier for the action that deletes a message.
    /// </summary>
    public static string DeleteMessage => "DeleteMessage";

    /// <summary>
    ///     Gets the identifier for the action that edits a message.
    /// </summary>
    public static string EditMessage => "EditMessage";

    /// <summary>
    ///     Gets the identifier for the action that sends a message.
    /// </summary>
    public static string SendMessage => "SendMessage";

    /// <summary>
    ///     Gets the identifier for the action that warns a user.
    /// </summary>
    public static string WarnUser => "WarnUser";
}