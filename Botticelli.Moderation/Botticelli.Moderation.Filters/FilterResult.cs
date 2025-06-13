using Botticelli.Moderation.Integration.Telegram.Interfaces;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Filters;

/// <summary>
/// Message filtering result
/// </summary>
public class FilterResult : IFilterResult
{
    private bool _passed;
    
    /// <inheritdoc/>
    public required string MessageId { get; set; }

    /// <inheritdoc/>
    public required Message Message { get; set; }

    /// <inheritdoc/>
    public bool Passed {
        get => Errors.Length == 0 && _passed;
        set => _passed = value; 
    }

    /// <inheritdoc/>
    public string[] Errors { get; set; } = [];
}