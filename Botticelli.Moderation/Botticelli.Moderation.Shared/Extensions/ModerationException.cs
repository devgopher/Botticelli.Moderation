namespace Botticelli.Moderation.Shared.Extensions;

public class ModerationException(string? message, Exception? exception = null) : Exception(message, exception);