using Newtonsoft.Json;
using System.Collections.Generic;

namespace LauncherSettings
{
    public partial class Form1 : Form
    {
        private int MonitorIndex = 0;

        private List<Size> availableResolutions;

        private List<int> ResolutionWidth = new List<int>();
        private List<int> ResolutionHeight = new List<int>();
        private int ResolutionIndex;

        private bool isFullScreen;

        private bool isVsync;

        public Form1()
        {
            InitializeComponent();

            InitializeMonitorInfo();

            InitializeWindowSize();

            InitializeFPS();

            InitializeMaxMusic();

            InitializeMaxSound();

            InitializeBrightness();
        }

        private void InitializeMonitorInfo()
        {
            var screens = Screen.AllScreens;

            for (int i = 0; i < screens.Length; i++)
            {
                var screen = screens[i];

                MonitorComboBox.Items.Add($"Монитор {i + 1}: {screen.DeviceName}");
            }
        }

        private void InitializeWindowSize()
        {
            var screen = Screen.PrimaryScreen;

            availableResolutions = new List<Size>();

            int minWidth = 800;
            int minHeight = 600;

            var aspectRatios = new List<Tuple<int, int>>
            {
                new Tuple<int, int>(16, 9),
                new Tuple<int, int>(16, 10),
                new Tuple<int, int>(4, 3),
                new Tuple<int, int>(1, 1)
            };

            foreach (var ratio in aspectRatios)
            {
                int width = minWidth;

                while (width <= screen.Bounds.Width)
                {
                    int height = (width * ratio.Item2) / ratio.Item1;
                    if (height >= minHeight && height <= screen.Bounds.Height)
                    {
                        availableResolutions.Add(new Size(width, height));
                        WindowsizeComboBox.Items.Add($"{width} * {height}");
                        ResolutionWidth.Add(width);
                        ResolutionHeight.Add(height);
                    }
                    width += 10;
                }
            }

        }

        private void InitializeFPS()
        {
            FPSComboBox.Items.Add("30");
            FPSComboBox.Items.Add("60");
            FPSComboBox.Items.Add("120");
        }



        private void FullScreenCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isFullScreen = FullScreenCheckBox.Checked;
        }

        private void VsynsCheckBox_CheckedChanged(object sender, EventArgs e)
        {
            isVsync = VsynsCheckBox.Checked;
        }

        private void MaxMusicTrackBar_Scroll(object sender, EventArgs e)
        {
            MaximumMusicLabel.Text = MaxMusicTrackBar.Value.ToString() + "%";
        }

        private void MaxSoundTrackBar_Scroll(object sender, EventArgs e)
        {
            MaximumSoundLabel.Text = MaxSoundTrackBar.Value.ToString() + "%";
        }

        private void BrightnessTrackBar_Scroll(object sender, EventArgs e)
        {
            BrightnessLabel.Text = BrightnessTrackBar.Value.ToString() + "%";
        }

        private void InitializeMaxMusic()
        {
            MaximumMusicLabel.Text = MaxMusicTrackBar.Value.ToString() + "%";
        }

        private void InitializeMaxSound()
        {
            MaximumSoundLabel.Text = MaxSoundTrackBar.Value.ToString() + "%";
        }

        private void InitializeBrightness()
        {
            BrightnessLabel.Text = BrightnessTrackBar.Value.ToString() + "%";
        }

        private void SaveChangeBtn_Click(object sender, EventArgs e)
        {
            try
            {
                var settings = new Settings
                {
                    MonitorNumber = MonitorIndex,
                    WindowSizeWidth = ResolutionWidth[ResolutionIndex],
                    WindowSizeHeight = ResolutionHeight[ResolutionIndex],
                    isFullScreen = isFullScreen,
                    VerticalSync = isVsync,
                    FPSlimit = int.Parse(FPSComboBox.SelectedItem.ToString()),
                    AutoSave = true,
                    AutoSaveTime = 5,
                    Lang = "en",
                    MaximumMusicVolume = MaxMusicTrackBar.Value / 100f,
                    MaximumSoundVolume = MaxSoundTrackBar.Value / 100f,
                    Brightness = BrightnessTrackBar.Value / 100f
                };

                string json = JsonConvert.SerializeObject(settings, Formatting.Indented);
                File.WriteAllText("settings.json", json);

                MessageBox.Show("Settings saved!");
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error! + {ex}");
            }
        }

        private void WindowsizeComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ResolutionIndex = WindowsizeComboBox.SelectedIndex;
        }

        private void MonitorComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            MonitorIndex = MonitorComboBox.SelectedIndex;
        }

    }

    public class Settings
    {
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
    }
}
