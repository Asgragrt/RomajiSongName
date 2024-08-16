using MelonLoader;
using RomajiSongName.Properties;

namespace RomajiSongName.Managers;

using static MelonBuildInfo;

internal static class SettingsManager
{
    private const string SettingsPath = $"UserData/{ModName}.cfg";

    private static MelonPreferences_Entry<bool> _displayMusicUid;

    private static MelonPreferences_Entry<bool> _isEnabled;

    private static MelonPreferences_Entry<bool> _verboseLogging;

    internal static bool DisplayMusicUid => _displayMusicUid.Value;

    internal static bool IsEnabled => _isEnabled.Value;
    internal static bool VerboseLogging => _verboseLogging.Value;

    internal static void Load()
    {
        var mainCategory = MelonPreferences.CreateCategory(ModName);
        mainCategory.SetFilePath(SettingsPath, true, false);

        _isEnabled = mainCategory.CreateEntry(nameof(IsEnabled), true);
        _displayMusicUid = mainCategory.CreateEntry(nameof(DisplayMusicUid), false);
        _verboseLogging = mainCategory.CreateEntry(nameof(VerboseLogging), false);
    }
}
