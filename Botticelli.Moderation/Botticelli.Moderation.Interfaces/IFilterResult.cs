using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Integration.Telegram.Interfaces;

public interface IFilterResult
{
    /// <summary>
    /// Message ID
    /// </summary>
    string MessageId { get; set; }

    /// <summary>
    /// Message for a further decision
    /// </summary>
    public Message Message { get; set; }
    
    /// <summary>
    /// Passed or not
    /// </summary>
    bool Passed { get; set; }

    /// <summary>
    /// Errors occurred during filtration
    /// </summary>
    string[] Errors { get; set; }
}