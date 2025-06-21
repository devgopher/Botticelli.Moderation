using System.Text.RegularExpressions;
using Botticelli.Moderation.Integration.Telegram.Interfaces;
using Botticelli.Shared.ValueObjects;

namespace Botticelli.Moderation.Filters.Links;

public class LinkFilter : FilterBase
{
    private const string Pattern = @"\bhttps?://\S+\b";

    public override Task<IFilterResult> FilterMessageAsync(Message message,
        CancellationToken cancellationToken = default)
    {
        var result = new FilterResult
        {
            MessageId = message.Uid!,
            Message = message
        };

        if (message.Body == null || !Regex.IsMatch(message.Body, Pattern))
            return Task.FromResult<IFilterResult>(result);
        
        result.Passed = false;
        result.Errors = ["An http link was found!"];

        return Task.FromResult<IFilterResult>(result);
    }
}