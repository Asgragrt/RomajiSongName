// cSpell:disable
using System.Text.RegularExpressions;
using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppAssets.Scripts.PeroTools.Managers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppPeroPeroGames.GlobalDefines;
using RomajiSongName.Data;
using RomajiSongName.Utils;

namespace RomajiSongName.Managers;

internal static class ModManager
{
    internal static void AddSearchTags()
    {
        var config = Singleton<ConfigManager>.instance.GetConfigObject<DBConfigMusicSearchTag>();

        foreach (var (uid, romanName) in SongNames.RomajiNames)
        {
            var splitString = SplitString(romanName);
            var joinedSpacedString = string.Join(' ', splitString);
            var joinedTightString = string.Join(null, splitString);

            List<string> tags = [romanName, joinedSpacedString, joinedTightString];
            tags.AddRange(splitString);

            if (config.m_Dictionary.TryGetValue(uid, out var tagInfo))
            {
                var oldTags = new List<string>(tagInfo.tag);
                oldTags.AddRange(tags);
                tagInfo.tag = new Il2CppStringArray([.. oldTags]);
            }
            else
            {
                var searchTag = new MusicSearchTagInfo
                {
                    uid = uid,
                    listIndex = config.count,
                    tag = new Il2CppStringArray([.. tags])
                };

                config.m_Dictionary.Add(searchTag.uid, searchTag);
                config.list.Add(searchTag);
            }
        }
    }

    internal static void ReplaceLocalName()
    {
        var musicTag = GlobalDataBase.dbMusicTag;
        foreach (var (uid, romanName) in SongNames.RomajiNames)
        {
            var curMusic = musicTag.GetMusicInfoFromAll(uid);
            if (curMusic is null)
            {
                Logger.Msg($"Failed to find a chart with the following uid: {uid}");
                continue;
            }

            var localInfo = curMusic.GetLocal(Language.english);
            localInfo.name = romanName;
        }
    }

    private static List<string> SplitString(string s)
    {
        // Split string by non alphanumerical characters, sanitize from empty or space only strings and return a list
        return Regex
            .Split(s.Trim(), @"[^a-zA-Z0-9]")
            .Select(word => word.Trim('\'').ToLower())
            .Where(word => word.Any())
            .ToList();
    }
}
