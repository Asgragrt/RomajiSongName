using HarmonyLib;
using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.UI.Panels;
using Il2CppPeroPeroGames.GlobalDefines;
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
            if (_lastIndex == i) return;
            _lastIndex = i;

            var dbMusicTag = GlobalDataBase.dbMusicTag;
            if (_lastIndex == dbMusicTag.stageShowMusicCount - 1) return;

            var musicInfo = dbMusicTag.GetMusicInfoFromShowMusicUids(i);
            //musicInfo?.GetLocal(Language.english);
            musicInfo?.GetLocal(Language.LanguageToIndex(DataHelper.userLanguage));
            __instance.musicFancyScrollView.onItemIndexChange?.Invoke(i);
        });
    }
}