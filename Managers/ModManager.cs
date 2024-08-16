using System.Text.Json;
using System.Text.RegularExpressions;
using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppAssets.Scripts.PeroTools.Managers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;
using Il2CppPeroPeroGames.GlobalDefines;
using MelonLoader.Utils;
using RomajiSongName.Data;
using RomajiSongName.Utils;

namespace RomajiSongName.Managers;

internal static class ModManager
{
    private static readonly string CustomNamesFilePath = Path.Join(
        MelonEnvironment.UserDataDirectory,
        "CustomNames.json"
    );

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

    internal static void LoadCustomNames()
    {
        if (!File.Exists(CustomNamesFilePath))
        {
            Logger.Debug("Couln't find CustomNames.json");
            return;
        }

        try
        {
            Logger.Msg("Found CustomNames.json");
            using StreamReader sr = new(CustomNamesFilePath);
            var json = sr.ReadToEnd();
            var data = JsonSerializer.Deserialize<Dictionary<string, string>>(json);

            var romajiNames = SongNames.RomajiNames;

            Regex regex = new(@"^\d{1,3}-\d+$");
            Logger.Debug("Loading CustomNames.json data");

            // Validate keys
            var filteredData = data.Where(p =>
                {
                    if (regex.IsMatch(p.Key))
                    {
                        return true;
                    }

                    Logger.Error($"{p.Key} is not a valid uid.");
                    return false;
                })
                .ToDictionary(p => p.Key, p => p.Value);

            // Merging values
            foreach (var (uid, name) in filteredData)
            {
                var message = $"Loading {uid} - ";
                if (romajiNames.ContainsKey(uid))
                {
                    message += $"replacing {romajiNames[uid]} with {name}";
                    romajiNames[uid] = name;
                }
                else
                {
                    message += $"adding {name}";
                    romajiNames[uid] = name;
                }
                Logger.Debug(message);
            }

            Logger.Msg($"Successfully added {filteredData.Count} custom title(s)!");
        }
        catch (Exception e)
        {
            Logger.Error(e);
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
                Logger.Error($"Failed to find a chart with the following uid: {uid}");
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
