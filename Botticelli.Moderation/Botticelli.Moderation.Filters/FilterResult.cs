namespace Botticelli.Moderation.Filters;

/// <summary>
/// Message filtering result
/// </summary>
public class FilterResult
{
    private bool _passed;
    
    /// <summary>
    /// Message ID
    /// </summary>
    public required string MessageId { get; set; }
    
    /// <summary>
    /// Passed or not
    /// </summary>
    public bool Passed {
        get => Errors.Length == 0 && _passed;
        set => _passed = value; 
    }

    /// <summary>
    /// Errors occurred during filtration
    /// </summary>
    public string[] Errors { get; set; } = [];
}