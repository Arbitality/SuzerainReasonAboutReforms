using MelonLoader;
using SuzerainModdingKit;
using SuzerainReasonAboutReforms.Configuration;
using SuzerainReasonAboutReforms.Conversations.ConvinceAlbin;
using SuzerainReasonAboutReforms.Conversations.ConvinceGloria;
using SuzerainReasonAboutReforms.Conversations.ConvinceIsabel;

[assembly: MelonInfo(
    typeof(SuzerainReasonAboutReforms.Core),
    "Reason About Reforms",
    SuzerainReasonAboutReforms.ModInfo.Version,
    "Arbitality",
    null)]
[assembly: MelonGame("Torpor Games", "Suzerain")]

namespace SuzerainReasonAboutReforms;

internal sealed class Core : MelonMod
{
    public override void OnInitializeMelon()
    {
        LoggerInstance.Msg(
            $"SuzerainReasonAboutReforms version: {ModInfo.Version}, " +
            $"target SMK version: {SmkInfo.VersionStr}, " +
            $"minimum versions: Suzerain {ModInfo.MinimumSuzerainVersion}, " +
            $"SMK {ModInfo.MinimumSmkVersion}, " +
            $"MelonLoader {ModInfo.MinimumMelonLoaderVersion}.");

        FeaturePreferences? preferences = TryCreatePreferences();
        if (preferences == null)
        {
            return;
        }

        if (!preferences.GloriaEnabled)
        {
            LoggerInstance.Msg(
                "Gloria feature is disabled. No Gloria conversation injection " +
                "will be registered.");
        }
        else
        {
            TryRegisterGloriaConversation();
        }

        if (!preferences.AlbinEnabled)
        {
            LoggerInstance.Msg(
                "Albin feature is disabled. No Albin conversation injection " +
                "will be registered.");
        }
        else
        {
            TryRegisterAlbinConversation();
        }

        if (!preferences.IsabelEnabled)
        {
            LoggerInstance.Msg(
                "Isabel feature is disabled. No Isabel conversation injection " +
                "will be registered.");
        }
        else
        {
            TryRegisterIsabelConversation();
        }
    }

    private FeaturePreferences? TryCreatePreferences()
    {
        try
        {
            return new FeaturePreferences();
        }
        catch (Exception exception)
        {
            LoggerInstance.Error(
                "Feature preferences could not be loaded. " +
                $"No conversation additions will be registered: " +
                $"{exception.GetType().Name}: {exception.Message}");
            return null;
        }
    }

    private void TryRegisterGloriaConversation()
    {
        try
        {
            ConvinceGloriaConversationPatch.Register();
            LoggerInstance.Msg(
                "Gloria feature is enabled. Registered the A_ConvinceGloria " +
                "conversation injection.");
        }
        catch (Exception exception)
        {
            LoggerInstance.Error(
                "Gloria conversation registration failed. " +
                $"Other feature registrations remain independent: " +
                $"{exception.GetType().Name}: {exception.Message}");
        }
    }

    private void TryRegisterAlbinConversation()
    {
        try
        {
            ConvinceAlbinConversationPatch.Register();
            LoggerInstance.Msg(
                "Albin feature is enabled. Registered the A_ConvinceAlbin " +
                "conversation injection.");
        }
        catch (Exception exception)
        {
            LoggerInstance.Error(
                "Albin conversation registration failed. " +
                $"Other feature registrations remain independent: " +
                $"{exception.GetType().Name}: {exception.Message}");
        }
    }

    private void TryRegisterIsabelConversation()
    {
        try
        {
            ConvinceIsabelConversationPatch.Register();
            LoggerInstance.Msg(
                "Isabel feature is enabled. Registered the A_ConvinceIsabel " +
                "outcome redirect.");
        }
        catch (Exception exception)
        {
            LoggerInstance.Error(
                "Isabel conversation registration failed. " +
                $"The failure was contained to A_ConvinceIsabel: " +
                $"{exception.GetType().Name}: {exception.Message}");
        }
    }
}
