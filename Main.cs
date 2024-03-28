using MelonLoader;
using RomajiSongName.Managers;

namespace RomajiSongName;

public class Main : MelonMod
{
    public override void OnInitializeMelon()
    {
        SettingsManager.Load();
        LoggerInstance.Msg("RomajiSongName has loaded correctly!");
    }
}