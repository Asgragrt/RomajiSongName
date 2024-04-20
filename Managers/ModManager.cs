﻿using System.Text.RegularExpressions;
using Il2CppAssets.Scripts.Database;
using Il2CppAssets.Scripts.PeroTools.Commons;
using Il2CppAssets.Scripts.PeroTools.Managers;
using Il2CppInterop.Runtime.InteropTypes.Arrays;

namespace RomajiSongName.Managers;

internal static class ModManager
{
    // Follows https://musedash.fandom.com/wiki/Songs order
    public static readonly Dictionary<string, string> RomajiNames = new()
    {
        // Default Songs Pack
        ["0-4"] = "Dan Xiang Di Tie Feat.karin",
        ["0-6"] = "Shi Guang Tu Ya",
        ["0-7"] = "Hai Tun Yu Guang Bo feat.Uranyan",
        ["0-31"] = "Tang Guo Se Lian Ai Xue",
        ["0-10"] = "Lian Ai Yu Yin Dao Hang feat.yousa",
        ["0-42"] = "Shinsekai Yori",
        ["0-35"] = "Tu Hua",

        // Cute Is Everything Vol.2
        ["13-0"] = "Su Rong Ni Hong feat.kumako",
        ["13-1"] = "Xing Qiu Shang De Zhui Su Shi",
        ["13-2"] = "Wo Yao Mai Mai Mai",
        ["13-3"] = "Yue Hui Xuan Yan",
        ["13-4"] = "Chu Xue",
        ["13-5"] = "Xian Shang Hua Hai",

        // Cute Is Everything Vol.3
        ["15-0"] = "Mo Zhou feat.早木旋子",
        ["15-1"] = "Ban Lan Xing Cai Hui Lv Xing Shi",
        ["15-4"] = "Sheng Er Wei Ren Wo Hen Bao Qian",

        // Cute Is Everything Vol.4
        ["18-1"] = "Shen Lan Yu Ye De Hu xi",
        ["18-4"] = "Jiu Shi Bu Ting Hua",

        // Cute Is Everything Vol.5
        ["23-0"] = "Yu Hou Tian Dian",
        ["23-1"] = "Gao Bai Ying Yuan Fang Cheng Shi",
        
        // Cute Is Everything Vol.6
        ["30-5"] = "Jiao Dian feat.早木旋子",

        // Cute Is Everything Vol.7
        ["37-0"] = "Liu Li Se Qian Zou Qu",
        
        // Muse Radio FM101
        ["25-5"] = "Kimi to pool disco",
        
        // Muse Radio FM102
        ["39-0"] = "Qu Jian Hai De Ri Zi",
        ["39-6"] = "Hadaka no Summer",

        // Muse Radio FM103
        ["52-2"] = "Ornament janai(Muse Dash Mix)",
        
        // Muse Radio FM104
        ["63-1"] = "Streamers High of 100 years",
        ["63-4"] = "Yoru no Machi",

        // Let's GROOVE!
        ["29-3"] = "HG Makaizou Polyvinyl Shounen",
        ["29-4"] = "Seija no Ibuki",
        
        // Happy Otaku Pack Vol.10
        ["28-5"] = "NiNi-nini-",
        
        // Happy Otaku Pack SP
        ["35-0"] = "MuseDash wo Tsukutte Iru PeroPeroGames-san ga Tousan shi chatta yo\uff5e",
        ["35-2"] = "Boku no Hefeng Bendang Shangshou",
        
        // Happy Otaku Pack 13
        ["44-1"] = "Ying Ying Da Zuo Zhan",
        
        // Happy Otaku Pack Vol.16
        ["57-3"] = "Sore wa mo Lovechu",
        
        // Happy Otaku Pack Vol.17
        ["62-0"] = "【Dongai li Lovely】Lovely",
        ["62-1"] = "Shinkai no Fune",
        ["62-3"] = "Numatta！！",
        
        // Phigros
        ["38-1"] = "Yukifuri Merry Christmas",

        // Touhou Mugakudan -I-
        ["42-1"] = "Iro wa Nioedo Chirinuru wo",
        ["42-3"] = "Hiiro Gekka Kyoushou no Zetsu",

        // DokiDoki! Valentine!
        ["49-1"] = "Ren'ai Kaihi Izonshi",

        // Virtual Idol Production
        ["51-0"] = "Jia Mian Ri Ji",
        ["51-4"] = "Danshi in\u2606 virtual land",

        // MEGAREX THE FUTURE
        ["54-2"] = "Kongetsu no Osusume Playlist wo Kensakushimasu",

        // Touhou Mugakudan -II-
        ["55-0"] = "Tsuki ni Murakumo Hana ni Kaze",
        ["55-2"] = "Monosugoi Space Shuttle de Koishi ga Monosugoi Uta",
        ["55-3"] = "Kakoi Naki yo wa Ikki no Tsukikage",

        // Nanahira Paradise
        ["58-3"] = "Vocal ni Mucha Sasenna",

        // Ola Dash
        ["61-0"] = "MuseDash ga Nanika Chotto Okashi", // ??????????
        ["61-2"] = "Buttoba Supernova",

        // COSMIC RADIO PEROLIST
        ["64-0"] = "Sutori\uff5ema\uff5eFIRE!?!?",
        ["64-4"] = "Kawaiku Kareini Uchuukaitou",

        // Miku in Museland
        ["66-0"] = "39 Music！",
        ["66-2"] = "Cynical Night Plan",
        ["66-3"] = "Kamippoina", // Kamippoina
        ["66-4"] = "Darling Dance",
        ["66-5"] = "Hatsune Tenchi Kaibyaku Shinwa",
        ["66-6"] = "Vampire",
        ["66-7"] = "Future・Eve",
        ["66-9"] = "Shunran",

        // Touhou Mugakudan -II I-
        ["69-5"] = "Tsurupettan",

        // Rin·Len's Mirrorland
        ["70-0"] = "Rettou Joutou",
        ["70-1"] = "Telecaster b boy",
        ["70-2"] = "I\uff5eya i\uff5eya i\uff5eya",
        ["70-3"] = "Nee Nee Nee。",
        ["70-5"] = "Shikabane no Odori",
        ["70-6"] = "Bitter Choco Decoration",
        ["70-7"] = "Dance Robot Dance",

        // Valentine Stage
        ["71-4"] = "Qiangwei Lianxin feat.AKA", // Qiangwei no koigokoro

        // Legends of Muse Warriors
        ["72-0"] = "P E R O P E R O Xiong\u2730Gui\u2730Luan\u2730Wu (feat.音游部, howsoon)",
        ["72-2"] = "How To Make Otoge\uff5eKyoku!",
        ["72-6"] = "Teshikani( TESHiKANi )",

        // MD Plus Project
        ["43-3"] = "Night・of・Knights",
        ["43-10"] = "Catalyst",
        ["43-18"] = "Monosugoi Kuruttoru Fran-chan ga Monosugoi Uta",
        ["43-24"] = "Tian Ling Ling Di Ling Ling",
        ["43-29"] = "Jiu Meng",
        ["43-36"] = "Mermaid Radio",
        ["43-44"] = "Mu Se Xiao Shi"
    };

    internal static void AddSearchTags()
    {
        var config = Singleton<ConfigManager>.instance.GetConfigObject<DBConfigMusicSearchTag>();

        foreach (var (key, value) in RomajiNames)
        {
            var splitString = SplitString(value);
            var joinedSpacedString = string.Join(' ', splitString);
            var joinedTightString = string.Join(null, splitString);

            List<string> tags = [value, joinedSpacedString, joinedTightString];
            tags.AddRange(splitString);

            if (config.m_Dictionary.TryGetValue(key, out var tagInfo))
            {
                var oldTags = new List<string>(tagInfo.tag);
                oldTags.AddRange(tags);
                tagInfo.tag = new Il2CppStringArray(oldTags.ToArray());
            }
            else
            {
                var searchTag = new MusicSearchTagInfo
                {
                    uid = key,
                    listIndex = config.count,
                    tag = new Il2CppStringArray(tags.ToArray())
                };

                config.m_Dictionary.Add(searchTag.uid, searchTag);
                config.list.Add(searchTag);
            }
        }
    }

    private static List<string> SplitString(string s)
    {
        // Split string by non alphanumerical characters, sanitize from empty or space only strings and return a list
        return Regex.Split(s.Trim(), @"[^a-zA-Z0-9]")
            .Select(word => word.Trim('\'').ToLower())
            .Where(word => word.Any())
            .ToList();
    }
}
