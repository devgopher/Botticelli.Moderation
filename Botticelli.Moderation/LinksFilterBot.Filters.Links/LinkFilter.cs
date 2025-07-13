using System.Text.RegularExpressions;
using Botticelli.Moderation.Integration.Interfaces;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Filters.Links;

/// <summary>
///     A filter that checks messages for the presence of HTTP or HTTPS links.
/// </summary>
public class LinkFilter : FilterBase
{
    // Regular expression pattern to match HTTP and HTTPS links.
    private const string Pattern = @"\bhttps?://\S+\b";

    /// <summary>
    ///     Filters the provided message to determine if it contains any links.
    /// </summary>
    /// <param name="message">The message to be filtered.</param>
    /// <param name="cancellationToken">A token to monitor for cancellation requests.</param>
    /// <returns>A task that represents the asynchronous operation. The task result contains the filter result.</returns>
    public override Task<IFilterResult> FilterMessageAsync(Message message,
        CancellationToken cancellationToken = default)
    {
        var result = new FilterResult
        {
            MessageId = message.Uid!,
            Message = message
        };

        // Check if the message body is null or does not contain any links.
        if (message.Body == null || !Regex.IsMatch(message.Body, Pattern))
            return Task.FromResult<IFilterResult>(result);

        // If a link is found, set the result to not passed and add an error message.
        result.Passed = false;
        result.Errors = ["An http link was found!"];

        return Task.FromResult<IFilterResult>(result);
    }
}