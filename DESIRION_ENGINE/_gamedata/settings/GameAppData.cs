using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.settings
{
    public class GameAppData
    {
        public string AppTitle { get; set; }

        public string DeveloperName { get; set; }

        public string AppVersion { get; set; }

        public int ClientStandartWidth { get; set; }

        public int ClientStandartHeight { get; set; }

        public bool isMouseVisible { get; set; }

        public bool noneBorless { get; set; }

        public string SettingsFilePath { get; set; }

        public string SaveFilePath { get; set; }

        public string SaveFileName { get; set; }

        public string ServerAddress { get; set; }

        public string LogFilePath { get; set; }

        public bool Cheats { get; set; }

        public GameAppData(string AppTitle = null, string DeveloperName = null, string AppVersion = null, int ClientStandartWidth = 800, int ClientStandertHeight = 600, bool isMouseVisible = false, bool noneBorless = false, string SettingsFilePath = null, string SaveFilePath = null, string SaveFileName = null, string ServerAddress = null, string LogFilePath = null, bool Cheats = true)//default
        {
            this.AppTitle = AppTitle;
            this.DeveloperName = DeveloperName;
            this.AppVersion = AppVersion;
            this.ClientStandartWidth = ClientStandartWidth;
            this.ClientStandartHeight = ClientStandertHeight;
            this.isMouseVisible = isMouseVisible;
            this.noneBorless = noneBorless;
            this.SettingsFilePath = SettingsFilePath;
            this.SaveFilePath = SaveFilePath;
            this.SaveFileName = SaveFileName;
            this.ServerAddress = ServerAddress;
            this.LogFilePath = LogFilePath;
            this.Cheats = Cheats;
        }
    }


}
