using HarmonyLib;
using Il2CppAssets.Scripts.Database;
using Il2CppPeroPeroGames.GlobalDefines;
using RomajiSongName.Managers;

namespace RomajiSongName.Patches;

[HarmonyPatch(typeof(MusicInfo), nameof(MusicInfo.GetLocal))]
internal static class LocalInfoPatch
{
    private static HashSet<int> _supportedLanguages = [
        Language.none,
        Language.english,
    ];
    
    internal static void Postfix(int language, MusicInfo __instance, ref LocalALBUMInfo __result)
    {
        if (!SettingsManager.IsEnabled
            || !_supportedLanguages.Contains(language)) return;
 
        if (!ModManager.RomajiNames.TryGetValue(__instance.uid, out var newName)) return;
        
        __result.name = newName;
    }
}