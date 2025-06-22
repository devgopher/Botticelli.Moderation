using Botticelli.Framework.Exceptions;
using Botticelli.Framework.Telegram;
using Botticelli.Framework.Telegram.Builders;
using Botticelli.Moderation.Decisions.Builders;
using Botticelli.Moderation.Filters.Builders;
using Botticelli.Moderation.Integration.Interfaces;

namespace Botticelli.Moderation.Api.Builders;

/// <summary>
///     A builder class for MessagePostModerationSender
/// </summary>
public class TelegramMessagePostModerationSenderBuilder :
    TelegramBotBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>>,
    IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>>
{
    private readonly List<IDecisionMaker> _decisionMakers = new();
    private readonly IFilterChainBuilder _filterChainBuilder = new FilterChainBuilder();
    private IDecisionMakerBuilder _decisionMakerBuilder = new DecisionMakerBuilder<>();

    private IFilter? _filterChain;

    protected TelegramMessagePostModerationSenderBuilder(bool isStandalone) : base(isStandalone)
    {
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>> WithFilter(IFilter filter)
    {
        _filterChainBuilder.WithFilter(filter);

        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>> WithFilter<TFilter>()
        where TFilter : IFilter, new()
    {
        _filterChainBuilder.WithFilter(new TFilter());

        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>> WithFilter(
        Action<IFilterChainBuilder> actionBuilder)
    {
        actionBuilder(_filterChainBuilder);

        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>> WithDecisionMaker(
        Action<IDecisionMakerBuilder> actionBuilder)
    {
        actionBuilder(_decisionMakerBuilder);

        var decisionMaker = _decisionMakerBuilder.Build();

        _decisionMakers.Add(decisionMaker);

        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>> WithDecisionMaker(
        IDecisionMakerBuilder decisionMakerBuilder)
    {
        _decisionMakerBuilder = decisionMakerBuilder;

        return this;
    }

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>> WithExecutor(
        Action<IExecutor<TelegramBot>> actionBuilder) => throw new NotImplementedException();

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>>
        WithExecutor(IExecutor<TelegramBot> executor) => throw new NotImplementedException();

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>>
        WithExecutor<TExecutor>(TExecutor executor) where TExecutor : IExecutor<TelegramBot> =>
        throw new NotImplementedException();

    public IMessagePostModerationSenderBuilder<TelegramBot, TelegramBotBuilder<TelegramBot>> WithDecisionMaker<
        TDecisionMakerBuilder>()
        where TDecisionMakerBuilder : IDecisionMakerBuilder, new()
    {
        _decisionMakerBuilder = new TDecisionMakerBuilder();

        return this;
    }

    public override TelegramBot? Build()
    {
        if (!_filterChainBuilder.HasFilters)
            throw new BotLoadingException("No filters were provided!");

        _filterChain = _filterChainBuilder.Build();

        AddOnMessageReceived((_, args) => _filterChain?.FilterMessageAsync(args.Message));

        return base.Build();
    }
}