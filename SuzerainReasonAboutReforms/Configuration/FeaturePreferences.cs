using MelonLoader;

namespace SuzerainReasonAboutReforms.Configuration;

internal sealed class FeaturePreferences
{
    private const bool _albinEnabledDefault = true;
    private const bool _gloriaEnabledDefault = true;
    private const bool _isabelEnabledDefault = true;

    private readonly MelonPreferences_Entry<bool> _albinEnabled;
    private readonly MelonPreferences_Entry<bool> _gloriaEnabled;
    private readonly MelonPreferences_Entry<bool> _isabelEnabled;

    internal FeaturePreferences()
    {
        MelonPreferences_Category category = MelonPreferences.CreateCategory(
            "SuzerainReasonAboutReforms",
            "Reason About Reforms");

        _gloriaEnabled = category.CreateEntry(
            "GloriaEnabled",
            _gloriaEnabledDefault,
            "GloriaEnabled",
            "Enable Gloria Tory conversation additions.",
            false,
            false,
            null);

        _albinEnabled = category.CreateEntry(
            "AlbinEnabled",
            _albinEnabledDefault,
            "AlbinEnabled",
            "Enable Albin Clavin conversation additions.",
            false,
            false,
            null);

        _isabelEnabled = category.CreateEntry(
            "IsabelEnabled",
            _isabelEnabledDefault,
            "IsabelEnabled",
            "Always convince Isabel Edmonds.",
            false,
            false,
            null);
    }

    internal bool AlbinEnabled => _albinEnabled.Value;

    internal bool GloriaEnabled => _gloriaEnabled.Value;

    internal bool IsabelEnabled => _isabelEnabled.Value;
}
