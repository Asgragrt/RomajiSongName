using HarmonyLib;
using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.UI.Panels;
using RomajiSongName.Managers;
using RomajiSongName.Utils;

namespace RomajiSongName.Patches;

[HarmonyPatch(typeof(PnlStage), nameof(PnlStage.Awake))]
internal static class FancyScrollPatch
{
    internal static void Postfix(PnlStage __instance)
    {
        if (!SettingsManager.DisplayMusicUid)
            return;

        var musicTag = GlobalDataBase.dbMusicTag;
        __instance.musicFancyScrollView.onItemIndexChange += new Action<int>(i =>
        {
            var musicInfo = musicTag.GetMusicInfoFromShowMusicUids(i);
            if (musicInfo is null)
                return;

            Logger.Msg($"UID: {musicInfo.uid} - Name: {musicInfo.name}");
        });
    }
}
