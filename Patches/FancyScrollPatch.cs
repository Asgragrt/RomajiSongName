using HarmonyLib;
using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.UI.Panels;
using RomajiSongName.Managers;

namespace RomajiSongName.Patches;

[HarmonyPatch(typeof(PnlStage), nameof(PnlStage.Awake))]
internal static class FancyScrollPatch
{
    private static int _lastIndex;

    internal static void Postfix(PnlStage __instance)
    {
        if (!SettingsManager.IsEnabled) return;

        _lastIndex = -1;

        __instance.musicFancyScrollView.onItemIndexChange += new Action<int>(i =>
        {
            // Avoid getting stuck in an infinite loop
            if (_lastIndex == i) return;
            _lastIndex = i;

            var dbMusicTag = GlobalDataBase.dbMusicTag;
            // Return if random
            if (_lastIndex == dbMusicTag.stageShowMusicCount - 1) return;

            // Force update the current song's title
            var musicInfo = dbMusicTag.GetMusicInfoFromShowMusicUids(i);
            musicInfo?.GetLocal(GlobalDataBase.s_DbUi.curLanguageIndex);
            __instance.musicFancyScrollView.onItemIndexChange?.Invoke(i);
        });
    }
}