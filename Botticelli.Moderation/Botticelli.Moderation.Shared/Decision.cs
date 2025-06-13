namespace Botticelli.Moderation.Shared;

/// <summary>
/// Represents a decision with associated comments, reasons, and additional parameters.
/// </summary>
public sealed class Decision
{
    /// <summary>
    /// Gets or sets the unique identifier for the decision.
    /// </summary>
    public required string Id { get; set; }

    /// <summary>
    /// Gets or sets the comments associated with the decision.
    /// </summary>
    public required string Comments { get; set; }

    /// <summary>
    /// Gets or sets the reasons for the decision.
    /// This property can contain multiple reasons as an array of strings.
    /// </summary>
    public required string[] Reasons { get; set; }

    /// <summary>
    /// Gets or sets the date and time when the decision was made, in UTC.
    /// </summary>
    public DateTime UtcDateTime { get; set; }

    /// <summary>
    /// Gets or sets additional parameters for the decision.
    /// This can include various optional parameters, such as:
    /// <list type="bullet">
    /// <item><description>BanTerm: Represents the duration of a ban (e.g., "BanTerm" = TimeSpan.FromDays(3)).</description></item>
    /// </list>
    /// </summary>
    public string[]? AdditionalParams { get; set; }
}