using MelonLoader;
using RomajiSongName.Properties;

namespace RomajiSongName.Managers;

using static MelonBuildInfo;

internal static class SettingsManager
{
    private const string SettingsPath = $"UserData/{ModName}.cfg";
    private static MelonPreferences_Entry<bool> _displayMusicUid;

    private static MelonPreferences_Entry<bool> _isEnabled;
    internal static bool DisplayMusicUid
    {
        get => _displayMusicUid.Value;
        set => _displayMusicUid.Value = value;
    }

    internal static bool IsEnabled
    {
        get => _isEnabled.Value;
        set => _isEnabled.Value = value;
    }

    internal static void Load()
    {
        var mainCategory = MelonPreferences.CreateCategory(ModName);
        mainCategory.SetFilePath(SettingsPath, true, false);

        _isEnabled = mainCategory.CreateEntry(nameof(IsEnabled), true);
        _displayMusicUid = mainCategory.CreateEntry(nameof(DisplayMusicUid), false);
    }
}
