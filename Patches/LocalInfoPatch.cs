using HarmonyLib;
using Il2CppAssets.Scripts.Database;
using Il2CppPeroPeroGames.GlobalDefines;
using RomajiSongName.Managers;

namespace RomajiSongName.Patches;

[HarmonyPatch(typeof(MusicInfo), nameof(MusicInfo.GetLocal))]
internal static class LocalInfoPatch
{
    internal static void Postfix(MusicInfo __instance, int language, ref LocalALBUMInfo __result)
    {
        if (!SettingsManager.IsEnabled
            || language != Language.english) return;

        if (!ModManager.RomajiNames.TryGetValue(__instance.uid, out var newName)) return;

        __result.name = newName;
    }
}