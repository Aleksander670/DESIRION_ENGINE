using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DESIRION_ENGINE._gamedata.settings
{
    public class GameSettings
    {
        private string FileName { get; set; }
        public int MonitorNumber { get; set; }
        public int WindowSizeWidth { get; set; }
        public int WindowSizeHeight { get; set; }
        public bool isFullScreen { get; set; }

        public bool VerticalSync { get; set; }
        public int FPSlimit { get; set; }

        public bool AutoSave { get; set; }
        public int AutoSaveTime { get; set; }
        public string Lang { get; set; }

        public float MaximumMusicVolume { get; set; }
        public float MaximumSoundVolume { get; set; }

        public float Brightness { get; set; }

        public int desiredHightForCamera = 600;

        public GameSettings() { }

        public GameSettings(string fileName)
        {
            this.FileName = fileName;
            Initialize();
        }

        public void Initialize()
        {
            if (!CheckedSettingsSaveFile(FileName))
            {
                SetDefaultValues();
                CreateSettingsFile();
            }
            else
            {
                ReadSettingsFile();
            }
        }

        public bool CheckedSettingsSaveFile(string fileName)
        {
            return File.Exists(fileName);
        }

        private void SetDefaultValues()
        {
            MonitorNumber = 0;
            WindowSizeWidth = 800;
            WindowSizeHeight = 600;
            isFullScreen = false;
            VerticalSync = true;
            FPSlimit = 60;
            AutoSave = true;
            AutoSaveTime = 5;
            Lang = "en";
            MaximumMusicVolume = 1f;
            MaximumSoundVolume = 1f;
            Brightness = 1f;
        }

        public void CreateSettingsFile()
        {
            WriteSettingsToFile();
        }

        public void ReadSettingsFile()
        {
            string jsonString = File.ReadAllText(FileName);
            var settings = JsonSerializer.Deserialize<GameSettings>(jsonString);

            if (settings != null)
            {
                MonitorNumber = settings.MonitorNumber;
                WindowSizeWidth = settings.WindowSizeWidth;
                WindowSizeHeight = settings.WindowSizeHeight;
                isFullScreen = settings.isFullScreen;
                VerticalSync = settings.VerticalSync;
                FPSlimit = settings.FPSlimit;
                AutoSave = settings.AutoSave;
                AutoSaveTime = settings.AutoSaveTime;
                Lang = settings.Lang;
                MaximumMusicVolume = settings.MaximumMusicVolume;
                MaximumSoundVolume = settings.MaximumSoundVolume;
                Brightness = settings.Brightness;
            }
        }

        private void WriteSettingsToFile()
        {
            string jsonString = JsonSerializer.Serialize(this);
            File.WriteAllText(FileName, jsonString);
        }


    }
}
