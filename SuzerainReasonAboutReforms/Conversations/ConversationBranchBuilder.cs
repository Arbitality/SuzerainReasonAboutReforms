using SuzerainModdingKit.Character;
using SuzerainModdingKit.StoryFragments.Conversation;
using SuzerainModdingKit.StoryFragments.Conversation.NodeSelectors;

namespace SuzerainReasonAboutReforms.Conversations;

internal sealed class ConversationBranchBuilder
{
    private readonly ConversationInjection _injection;
    private readonly string _nodePrefix;
    private readonly string _speakerName;
    private readonly HashSet<string> _nodeNames = new(StringComparer.Ordinal);

    internal ConversationBranchBuilder(
        ConversationInjection injection,
        string nodePrefix,
        string speakerName)
    {
        ArgumentNullException.ThrowIfNull(injection);
        EnsureNotBlank(nodePrefix, nameof(nodePrefix));
        EnsureNotBlank(speakerName, nameof(speakerName));

        _injection = injection;
        _nodePrefix = nodePrefix;
        _speakerName = speakerName;
    }

    internal void AddConversation(
        string name,
        string hookArticyId,
        string openingText,
        IReadOnlyList<ConversationChoice> choices)
    {
        EnsureNotBlank(name, nameof(name));
        EnsureNotBlank(hookArticyId, nameof(hookArticyId));
        EnsureNotBlank(openingText, nameof(openingText));
        ArgumentNullException.ThrowIfNull(choices);

        if (choices.Count < 2)
        {
            throw new ArgumentException(
                "Injected player prompts must provide at least two choices.",
                nameof(choices));
        }

        ValidateChoices(choices);

        string branchPrefix = $"{_nodePrefix}.{name}";
        string openingNodeName = $"{branchPrefix}.Opening";
        ConversationNodeModdedNameSelector[] playerNodes =
            new ConversationNodeModdedNameSelector[choices.Count];

        ReserveNodeName(openingNodeName);
        for (int index = 0; index < choices.Count; index++)
        {
            string choicePrefix = $"{branchPrefix}.Choice{index + 1}";
            string playerNodeName = $"{choicePrefix}.Player";
            ReserveNodeName(playerNodeName);
            playerNodes[index] = new ConversationNodeModdedNameSelector(
                playerNodeName,
                conversationName: null);

            if (choices[index].ResponseText is not null)
            {
                ReserveNodeName($"{choicePrefix}.Response");
            }
        }

        _ = _injection.AddNode(new ConversationNode(
            name: openingNodeName,
            text: Quote(openingText),
            hooks: new[]
            {
                new ConversationNodeHook(
                    selector: new ConversationNodeArticyIDSelector(
                        hookArticyId,
                        conversationName: null),
                    mode: ConversationNodeHook.HookMode.Override),
            },
            nextNodes: playerNodes,
            speakerSelector: CreateSpeakerSelector()));

        for (int index = 0; index < choices.Count; index++)
        {
            AddChoice(branchPrefix, index, choices[index]);
        }
    }

    private void AddChoice(string branchPrefix, int index, ConversationChoice choice)
    {
        string choicePrefix = $"{branchPrefix}.Choice{index + 1}";
        string playerNodeName = $"{choicePrefix}.Player";
        string responseNodeName = $"{choicePrefix}.Response";
        ConversationNodeSelector playerNextNode = choice.ResponseText is null
            ? new ConversationNodeArticyIDSelector(
                choice.NextArticyId,
                conversationName: null)
            : new ConversationNodeModdedNameSelector(
                responseNodeName,
                conversationName: null);

        _ = _injection.AddNode(new ConversationNode(
            name: playerNodeName,
            text: Quote(choice.PlayerText),
            nextNodes: new[]
            {
                playerNextNode,
            },
            speakerSelector: new CharacterNameSelector("Player")));

        if (choice.ResponseText is null)
        {
            return;
        }

        _ = _injection.AddNode(new ConversationNode(
            name: responseNodeName,
            text: Quote(choice.ResponseText),
            nextNodes: new[]
            {
                new ConversationNodeArticyIDSelector(
                    choice.NextArticyId,
                    conversationName: null),
            },
            speakerSelector: CreateSpeakerSelector()));
    }

    private static void ValidateChoices(IReadOnlyList<ConversationChoice> choices)
    {
        for (int index = 0; index < choices.Count; index++)
        {
            ConversationChoice? choice = choices[index];
            if (choice is null)
            {
                throw new ArgumentException(
                    $"Conversation choice {index + 1} is null.",
                    nameof(choices));
            }

            EnsureNotBlank(choice.PlayerText, nameof(choice.PlayerText));
            EnsureNotBlank(choice.NextArticyId, nameof(choice.NextArticyId));
            if (choice.ResponseText is not null)
            {
                EnsureNotBlank(choice.ResponseText, nameof(choice.ResponseText));
            }
        }
    }

    private static void EnsureNotBlank(string value, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(value))
        {
            throw new ArgumentException("Value cannot be blank.", parameterName);
        }
    }

    private static string Quote(string text)
    {
        return $"\"{text}\"";
    }

    private CharacterNameSelector CreateSpeakerSelector()
    {
        return new CharacterNameSelector(_speakerName);
    }

    private void ReserveNodeName(string nodeName)
    {
        if (!_nodeNames.Add(nodeName))
        {
            throw new ArgumentException(
                $"Generated conversation node name '{nodeName}' is not unique.",
                nameof(nodeName));
        }
    }
}
