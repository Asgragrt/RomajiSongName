using HarmonyLib;
using Il2Cpp;
using RomajiSongName.Managers;

namespace RomajiSongName.Patches;

[HarmonyPatch(typeof(MusicTagManager), nameof(MusicTagManager.InitDatas))]
internal static class MusicTagPatch
{
    internal static void Posftfix()
    {
        ModManager.AddSearchTags();
    }
}