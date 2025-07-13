namespace Botticelli.Moderation.Shared;

/// <summary>
///     Represents a decision with associated comments, reasons, and additional parameters.
/// </summary>
public sealed class RuleDecision : DecisionBase
{
    /// <summary>
    ///     Decision type (Warn, ban...)
    /// </summary>
    public DecisionType? Type { get; set; }
}