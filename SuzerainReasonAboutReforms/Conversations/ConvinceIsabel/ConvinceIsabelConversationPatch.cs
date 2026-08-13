using SuzerainModdingKit.Character;
using SuzerainModdingKit.StoryFragments.Conversation;
using SuzerainModdingKit.StoryFragments.Conversation.NodeSelectors;

namespace SuzerainReasonAboutReforms.Conversations.ConvinceIsabel;

internal static class ConvinceIsabelConversationPatch
{
    private const string _conversationTitle = "Sordland/Turn07/A_ConvinceIsabel";
    private const string _failureJournalArticyId = "0x010000060009766A";
    private const string _nodeName = "SuzerainReasonAboutReforms.Isabel.ForceSuccess";
    private const string _narration =
        "After a brief pause, she continued.";
    private const string _successJournalArticyId = "0x0100000600097664";

    internal static void Register()
    {
        ConversationInjection injection = CreateInjection();
        injection.Register();
    }

    private static ConversationInjection CreateInjection()
    {
        ConversationInjection injection = new(_conversationTitle);

        _ = injection.AddNode(new ConversationNode(
            name: _nodeName,
            text: _narration,
            hooks: new[]
            {
                new ConversationNodeHook(
                    selector: new ConversationNodeArticyIDSelector(
                        _failureJournalArticyId,
                        conversationName: null),
                    mode: ConversationNodeHook.HookMode.Override),
            },
            nextNodes: new[]
            {
                new ConversationNodeArticyIDSelector(
                    _successJournalArticyId,
                    conversationName: null),
            },
            speakerSelector: new CharacterNameSelector("Narrator")));

        return injection;
    }
}
