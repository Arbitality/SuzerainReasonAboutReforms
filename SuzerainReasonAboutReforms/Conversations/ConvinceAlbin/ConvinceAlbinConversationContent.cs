namespace SuzerainReasonAboutReforms.Conversations.ConvinceAlbin;

internal static class ConvinceAlbinConversationContent
{
    private const string _albinConvincedExitArticyId = "0x010000030002E15B";
    private const string _albinFailedExitArticyId = "0x010000030002E24E";

    internal static void AddNodes(ConversationBranchBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        AddMeetingBreakdown(builder);
        AddVicePresidencyRefusal(builder);
        AddChequeDealRefusal(builder);
    }

    private static void AddMeetingBreakdown(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "MeetingBreakdown",
            hookArticyId: "0x010000030002E162",
            openingText: "Still... I would rather not explain to my people why we left the room with nothing.",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "Then don't. Bring me the signatures and tell them you 'kept your leash on me' and a seat at the table for the future of Sordland.",
                    ResponseText: "You would offer me such a generous version of events? And with our votes, it will not be at all inaccurate. Very well.",
                    NextArticyId: _albinConvincedExitArticyId),
                new ConversationChoice(
                    PlayerText: "Why would you oppose me when you can make yourself a necessity in the Assembly of this term? I thought you preferred the latter.",
                    ResponseText: "Much as I would like to see you try without me, I do prefer being in the room.",
                    NextArticyId: _albinConvincedExitArticyId),
                new ConversationChoice(
                    PlayerText: "And I would rather not bargain over the draft any further.",
                    NextArticyId: _albinFailedExitArticyId),
                new ConversationChoice(
                    PlayerText: "You listen to me, Albin. You don't hold any cards here, not after the nomination. Sit on the sidelines for this and having you replaced will have cost me nothing.",
                    ResponseText: "There's no need for that. Forget I said anything, this stays between us.",
                    NextArticyId: _albinConvincedExitArticyId),
            });
    }

    private static void AddVicePresidencyRefusal(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "VicePresidencyRefusal",
            hookArticyId: "0x010000030002E4C2",
            openingText: "Then I misread the invitation. I thought you wanted my wing, not merely my applause.",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "Deliver the vote and the party will know who carried the proposal. You don't need my promise if you can make yourself indispensable.",
                    ResponseText: "Indispensable... That is music to my ears.",
                    NextArticyId: _albinConvincedExitArticyId),
                new ConversationChoice(
                    PlayerText: "You call yourself the face of a new generation, yet trade your vote for a chair like as if Old Guard taught you. Is that the story you want whispered through these halls?",
                    ResponseText: "Spare me the lecture, Mr. President... But no, you have made your point.",
                    NextArticyId: _albinConvincedExitArticyId),
                new ConversationChoice(
                    PlayerText: "There is no deal. The vice presidency is not for sale.",
                    NextArticyId: _albinFailedExitArticyId),
                new ConversationChoice(
                    PlayerText: "Walk out from this, and your base hears its leader priced the proposal at one chair. Back me, and that sentence never leaves this room.",
                    ResponseText: "Keep your voice down. Fine. We will not discuss this again.",
                    NextArticyId: _albinConvincedExitArticyId),
            });
    }

    private static void AddChequeDealRefusal(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "ChequeDealRefusal",
            hookArticyId: "0x010000030002E537",
            openingText: "Then put the cheque away. It seems we have mistaken each other.",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "Not at all. Instead, being the man who delivered these votes will pay in political capital from me for years.",
                    ResponseText: "Now that sounds like an investment. Very good, Mr. President.",
                    NextArticyId: _albinConvincedExitArticyId),
                new ConversationChoice(
                    PlayerText: "Take the money and you're useful once. Bring me your wing and you have my ear after the vote.",
                    ResponseText: "You are careful with your promises. But that one may be worth more. All right.",
                    NextArticyId: _albinConvincedExitArticyId),
                new ConversationChoice(
                    PlayerText: "There is no deal. Not for you. I won't pay a ren for your vote.",
                    NextArticyId: _albinFailedExitArticyId),
                new ConversationChoice(
                    PlayerText: "You named your price, Albin. Support the draft, or the next people to utter your name will be the impeachment committee.",
                    ResponseText: "You wouldn't do that... Fine. This conversation did not happen.",
                    NextArticyId: _albinConvincedExitArticyId),
            });
    }
}
