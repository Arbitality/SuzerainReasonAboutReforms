namespace SuzerainReasonAboutReforms.Conversations;

internal sealed record ConversationChoice(
    string PlayerText,
    string NextArticyId,
    string? ResponseText = null);
