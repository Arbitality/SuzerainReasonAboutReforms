namespace SuzerainReasonAboutReforms.Conversations.ConvinceGloria;

internal static class ConvinceGloriaConversationContent
{
    private const string _gloriaConvincedArticyId = "0x0100000300026B65";
    private const string _gloriaFailArticyId = "0x01000003000265F2";
    private const string _postBenfiAgreementArticyId = "0x01000006000B3585";
    private const string _reformContinuationArticyId = "0x0100000300026B16";

    internal static void AddNodes(ConversationBranchBuilder builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        AddThresholdThreeUltimatum(builder);
        AddThresholdOpposition(builder);
        AddMemberOfHonor(builder);
        AddWholeProposalObjection(builder);
        AddCourtAppointment(builder);
        AddExecutivePowers(builder);
        AddOtherClauseUltimatum(builder);
        AddBenfiSpeechRefusal(builder);
    }

    private static void AddThresholdThreeUltimatum(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "ThresholdThreeUltimatum",
            hookArticyId: "0x01000003000265F7",
            openingText: "One last chance, Mr. President. Tell me how does this not hand the Assembly to Ricter and the communists?",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "At ten percent they can all campaign against the system. At three, they will have to campaign against each other.",
                    ResponseText: "An attempt to trade one opposition for three smaller ones. That is the first sensible argument you've made for it.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "If a legal ballot destroys the USP, the threshold was only hiding the corpse. I don't believe our party is that weak. Do you?",
                    ResponseText: "Careful, Mr. Rayne. If I admit the USP needs protection from the ballot, I have already conceded your point.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "I won't. The threshold stays.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "I was elected to lead this party, not ask your permission to save it from itself.",
                    ResponseText: "Then save it without the conservatives, Mr. President.",
                    NextArticyId: _gloriaFailArticyId),
            });

    }

    private static void AddThresholdOpposition(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "ThresholdOpposition",
            hookArticyId: "0x01000003000265FC",
            openingText: "We have asked each other for concessions neither intends to give. What exactly is left to discuss?",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "The fact that neither of us has left the room.",
                    ResponseText: "Which means we are both more worried about failure than we care to admit...",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "You know the demand for change. When the opposition turns our failure against us into their next term victory, will your wing explain why the USP walked away?",
                    ResponseText: "You speak as though they have already won. They have not. But no, I would not give them the satisfaction.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "Then ask for a different promise. One that does not rewrite the proposal.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "Nothing. I will find the signatures without you.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "Your wing will fall in line when the vote comes, with or without your blessing.",
                    ResponseText: "Then you have no need of me. Let us see how far that confidence carries you.",
                    NextArticyId: _gloriaFailArticyId),
            });

    }

    private static void AddMemberOfHonor(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "MemberOfHonor",
            hookArticyId: "0x010000030002675A",
            openingText: "Every Sord who reads that clause will see a knife with Soll's name on it.",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "If Soll's place in history depends on a title no other Sord can claim, you think less of him than I do.",
                    ResponseText: "Careful, Mr. President. I will not have you question my regard for the Colonel. His legacy is greater than any title you could abolish.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "You once took to the streets against a king. Since when did a title become more important than the republic beneath it?",
                    ResponseText: "Do not preach my own history against me, Mr. Rayne. I remember it well enough... Perhaps better now than I have allowed.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "Call it a knife if you want. I am not removing the clause.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "Soll is an old man hiding behind a title, and you are hiding behind him.",
                    ResponseText: "You have mistaken contempt for courage. The Assembly will not.",
                    NextArticyId: _gloriaFailArticyId),
            });

    }

    private static void AddWholeProposalObjection(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "WholeProposalObjection",
            hookArticyId: "0x0100000300026B9F",
            openingText: "You are asking my wing to overlook much more than a clause.",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "You know the demand for change. You know that I will pass this. And you know you must stay close enough to contain what follows. Walking away only removes your hand from the draft.",
                    ResponseText: "Extend my support in order to restrain you? That may be the first argument today that I would like to believe.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "Half the Assembly will look to you before they look to me. Obstructing us now will lose the next election for the party, lead with me instead.",
                    ResponseText: "Flattery from you always arrives with a bill attached. Mr. Rayne, you assume too much, but you do have a point.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "Mrs. Tory, without your wing this dies before it reaches the floor. Tell me there is still something I can do.",
                    ResponseText: "There was, Mr. President. But you signed it away with this egregious draft.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "If you cannot separate the proposal from my government and my person, then there is nothing left to discuss.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "I do not owe you a defense of every decision this government makes.",
                    ResponseText: "No. Only of the proposal you brought here. You have failed even at that.",
                    NextArticyId: _gloriaFailArticyId),
            });

    }

    private static void AddCourtAppointment(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "CourtAppointment",
            hookArticyId: "0x0100000300026BF5",
            openingText: "Do not pretend you can afford to leave this office without my support.",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "No. You judged that correctly. You misjudged what I would pay for it.",
                    ResponseText: "A more honest answer than most men give in this building. Then we will leave my future out of this.",
                    NextArticyId: _gloriaConvincedArticyId),
                new ConversationChoice(
                    PlayerText: "If I put your name first today, every Justice will know what my word is worth tomorrow.",
                    ResponseText: "A semblance of integrity? You surprise me, Mr. Rayne.",
                    NextArticyId: _gloriaConvincedArticyId),
                new ConversationChoice(
                    PlayerText: "Then we have no agreement. I will find the votes elsewhere.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "I need your votes, not your ambition dressed up as public service.",
                    ResponseText: "And sitting in the Maroon Palace does not make you entitled to them, Mr. Rayne.",
                    NextArticyId: _gloriaFailArticyId),
            });

    }

    private static void AddExecutivePowers(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "ExecutivePowers",
            hookArticyId: "0x010000030002C60C",
            openingText: "Unless you would actually answer a question of mine. What restrains the President in your proposal?",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "The party that puts him in office. I cannot pass a budget, a bill or a second term with a decree.",
                    ResponseText: "So the leash belongs to the USP, not the Constitution. A dangerous answer... but one my wing knows how to use.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "You do. I may write the decree, but I cannot manufacture votes in the Assembly.",
                    ResponseText: "No. For that you need us... and I need you to remember it.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "If you cannot trust me with this, there is nothing left to discuss.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "I will not be restrained by someone who mistakes obstruction for authority.",
                    ResponseText: "And Sordland should not be ruled by a President who mistakes power for competence.",
                    NextArticyId: _gloriaFailArticyId),
            });

    }

    private static void AddOtherClauseUltimatum(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "OtherClauseUltimatum",
            hookArticyId: "0x0100002C00000A17",
            openingText: "No, Mr. President. You are trying to trade with an empty hand.",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "My hand is the vote. If I lose it, you get Ricter's constitution come next election, written without either of us.",
                    ResponseText: "You keep bringing Mr. Ricter into my office when he was not invited. Perhaps that is why I shouldn't leave you alone with him.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "Then stop counting clauses. Which leaves the USP stronger on the morning after the vote: this draft, or none at all?",
                    ResponseText: "You assume those are the only choices. Today... perhaps they are. Very well.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "Then put something on the table that I can actually give you.",
                    NextArticyId: _reformContinuationArticyId),
                new ConversationChoice(
                    PlayerText: "Then there will be no trade. The proposal goes forward as it is.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "Keep your vote. I will pass this without it and remember who stood in the way.",
                    ResponseText: "Threats will not improve your position, Mr. President.",
                    NextArticyId: _gloriaFailArticyId),
            });

    }

    private static void AddBenfiSpeechRefusal(ConversationBranchBuilder builder)
    {
        builder.AddConversation(
            name: "BenfiSpeechRefusal",
            hookArticyId: "0x0100002C00000A93",
            openingText: "If you insist keeping her on that stage, give me a reason I can take back to the conservatives.",
            choices: new[]
            {
                new ConversationChoice(
                    PlayerText: "Mayor Leste can survive losing one speech. The USP may not survive losing this amendment. Tell your wing which matters more.",
                    ResponseText: "Then you must know he will be furious, but Leste knows the difference between a personal slight and a threat to the party. Your loss, Mr. Rayne.",
                    NextArticyId: _postBenfiAgreementArticyId),
                new ConversationChoice(
                    PlayerText: "The decision is mine. Tell Leste you fought me on it and lost. He can blame the President without blaming you or the conservative wing.",
                    ResponseText: "So will I, Mr. Rayne. But you understand this. Leave me to manage Leste, that may be enough.",
                    NextArticyId: _postBenfiAgreementArticyId),
                new ConversationChoice(
                    PlayerText: "Monica will speak. That is the only answer you are getting.",
                    NextArticyId: _gloriaFailArticyId),
                new ConversationChoice(
                    PlayerText: "If your wing abandons this over a festival schedule, it was never serious to begin with.",
                    ResponseText: "Then you have badly misjudged both my wing and me, Mr. President.",
                    NextArticyId: _gloriaFailArticyId),
            });
    }
}
