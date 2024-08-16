using HarmonyLib;
using Il2Cpp;
using RomajiSongName.Managers;

namespace RomajiSongName.Patches;

[HarmonyPatch(typeof(MusicTagManager))]
internal static class MusicTagPatch
{
    [HarmonyPatch(nameof(MusicTagManager.InitDatas))]
    [HarmonyPostfix]
    internal static void InitDataPatch()
    {
        // * Replace locals after initialization
        ModManager.ReplaceLocalName();
    }

    [HarmonyPatch(nameof(MusicTagManager.InitDefaultInfo))]
    [HarmonyPostfix]
    internal static void InitDefaultInfoPatch()
    {
        // * Move to InitDefaultInfo to work with custom albums
        ModManager.AddSearchTags();
    }
}
