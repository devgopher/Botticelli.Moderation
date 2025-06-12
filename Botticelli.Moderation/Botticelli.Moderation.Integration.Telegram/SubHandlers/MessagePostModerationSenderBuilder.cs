using Botticelli.Framework.Builders;
using Botticelli.Framework.Exceptions;
using Botticelli.Interfaces;
using Botticelli.Moderation.Filters;

namespace Botticelli.Moderation.Api.SubHandlers;

/// <summary>
///     A builder class for MessagePostModerationSender
/// </summary>
/// <typeparam name="TBot"></typeparam>
/// <typeparam name="TBotBuilder"></typeparam>
public abstract class MessagePostModerationSenderBuilder<TBot, TBotBuilder> : BotBuilder<TBot, TBotBuilder>
    where TBot : IBot<TBot> where TBotBuilder : BotBuilder<TBot, TBotBuilder>
{
    private readonly List<IFilter> _filters = new();

    public MessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter(IFilter filter)
    {
        _filters.Add(filter);

        return this;
    }
    
    public MessagePostModerationSenderBuilder<TBot, TBotBuilder> WithFilter<TFilter>()
    where TFilter : IFilter, new()
    {
        _filters.Add(new TFilter());

        return this;
    }
    
    public override TBot? Build()
    {
        if (_filters.Count == 0)
            throw new BotLoadingException("No filters were provided!");
        
        AddOnMessageReceived((sender, args) =>
        {
            // TODO: all further business logic is here!
        });
        
        return base.Build();
    }

    protected abstract override TBot? InnerBuild();
}