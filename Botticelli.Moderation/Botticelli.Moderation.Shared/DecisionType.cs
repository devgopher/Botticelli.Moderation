namespace Botticelli.Moderation.Shared;

/// <summary>
/// Represents a type of decision with an identifier and a name.
/// </summary>
public record DecisionType
{
    /// <summary>
    /// Initializes a new instance of the <see cref="DecisionType"/> record.
    /// </summary>
    /// <param name="decisionId">The unique identifier for the decision type.</param>
    /// <param name="decisionName">The name of the decision type.</param>
    public DecisionType(int decisionId, string decisionName)
    {
        DecisionId = decisionId;
        DecisionName = decisionName;
    }

    /// <summary>
    /// Gets the unique identifier for the decision type.
    /// </summary>
    public int DecisionId { get; }

    /// <summary>
    /// Gets the name of the decision type.
    /// </summary>
    public string DecisionName { get; }
}