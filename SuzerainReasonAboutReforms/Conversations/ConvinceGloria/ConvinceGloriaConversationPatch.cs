using SuzerainModdingKit.StoryFragments.Conversation;

namespace SuzerainReasonAboutReforms.Conversations.ConvinceGloria;

internal static class ConvinceGloriaConversationPatch
{
    private const string _conversationTitle = "Sordland/Turn04/A_ConvinceGloria";
    private const string _nodePrefix = "SuzerainReasonAboutReforms.Gloria";
    private const string _speakerName = "Gloria Tory";

    internal static void Register()
    {
        ConversationInjection injection = new(_conversationTitle);
        ConversationBranchBuilder builder = new(
            injection,
            _nodePrefix,
            _speakerName);

        ConvinceGloriaConversationContent.AddNodes(builder);
        injection.Register();
    }
}
