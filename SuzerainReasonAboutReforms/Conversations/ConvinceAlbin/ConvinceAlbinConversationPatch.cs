using SuzerainModdingKit.StoryFragments.Conversation;

namespace SuzerainReasonAboutReforms.Conversations.ConvinceAlbin;

internal static class ConvinceAlbinConversationPatch
{
    private const string _conversationTitle = "Sordland/Turn04/A_ConvinceAlbin";
    private const string _nodePrefix = "SuzerainReasonAboutReforms.Albin";
    private const string _speakerName = "Albin Clavin";

    internal static void Register()
    {
        ConversationInjection injection = new(_conversationTitle);
        ConversationBranchBuilder builder = new(
            injection,
            _nodePrefix,
            _speakerName);

        ConvinceAlbinConversationContent.AddNodes(builder);
        injection.Register();
    }
}
