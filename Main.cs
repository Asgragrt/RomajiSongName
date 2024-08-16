using MelonLoader;
using RomajiSongName.Managers;
using RomajiSongName.Properties;

namespace RomajiSongName;

public class Main : MelonMod
{
    public override void OnInitializeMelon()
    {
        SettingsManager.Load();
        ModManager.LoadCustomNames();
        LoggerInstance.Msg($"{MelonBuildInfo.ModName} has loaded correctly!");
    }
}
