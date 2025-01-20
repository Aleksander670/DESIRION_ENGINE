namespace LauncherSettings
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            label1 = new Label();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            label5 = new Label();
            label6 = new Label();
            label7 = new Label();
            label8 = new Label();
            MonitorComboBox = new ComboBox();
            WindowsizeComboBox = new ComboBox();
            FullScreenCheckBox = new CheckBox();
            VsynsCheckBox = new CheckBox();
            FPSComboBox = new ComboBox();
            MaxMusicTrackBar = new TrackBar();
            MaxSoundTrackBar = new TrackBar();
            BrightnessTrackBar = new TrackBar();
            SaveChangeBtn = new Button();
            MaximumMusicLabel = new Label();
            MaximumSoundLabel = new Label();
            BrightnessLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)MaxMusicTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)MaxSoundTrackBar).BeginInit();
            ((System.ComponentModel.ISupportInitialize)BrightnessTrackBar).BeginInit();
            SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new Point(15, 22);
            label1.Name = "label1";
            label1.Size = new Size(53, 15);
            label1.TabIndex = 0;
            label1.Text = "Monitor:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new Point(15, 58);
            label2.Name = "label2";
            label2.Size = new Size(76, 15);
            label2.TabIndex = 1;
            label2.Text = "Window size:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new Point(12, 96);
            label3.Name = "label3";
            label3.Size = new Size(75, 15);
            label3.TabIndex = 2;
            label3.Text = "Is full screen:";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Location = new Point(15, 129);
            label4.Name = "label4";
            label4.Size = new Size(41, 15);
            label4.TabIndex = 3;
            label4.Text = "VSyns:";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Location = new Point(15, 166);
            label5.Name = "label5";
            label5.Size = new Size(56, 15);
            label5.TabIndex = 4;
            label5.Text = "FPS limit:";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Location = new Point(15, 203);
            label6.Name = "label6";
            label6.Size = new Size(143, 15);
            label6.TabIndex = 5;
            label6.Text = "Maximum music volume:";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Location = new Point(15, 243);
            label7.Name = "label7";
            label7.Size = new Size(144, 15);
            label7.TabIndex = 6;
            label7.Text = "Maximum sound volume:";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Location = new Point(15, 285);
            label8.Name = "label8";
            label8.Size = new Size(65, 15);
            label8.TabIndex = 7;
            label8.Text = "Brightness:";
            // 
            // MonitorComboBox
            // 
            MonitorComboBox.FormattingEnabled = true;
            MonitorComboBox.Location = new Point(109, 19);
            MonitorComboBox.Name = "MonitorComboBox";
            MonitorComboBox.Size = new Size(274, 23);
            MonitorComboBox.TabIndex = 8;
            MonitorComboBox.SelectedIndexChanged += MonitorComboBox_SelectedIndexChanged;
            // 
            // WindowsizeComboBox
            // 
            WindowsizeComboBox.FormattingEnabled = true;
            WindowsizeComboBox.Location = new Point(109, 55);
            WindowsizeComboBox.Name = "WindowsizeComboBox";
            WindowsizeComboBox.Size = new Size(274, 23);
            WindowsizeComboBox.TabIndex = 9;
            WindowsizeComboBox.SelectedIndexChanged += WindowsizeComboBox_SelectedIndexChanged;
            // 
            // FullScreenCheckBox
            // 
            FullScreenCheckBox.AutoSize = true;
            FullScreenCheckBox.Location = new Point(109, 97);
            FullScreenCheckBox.Name = "FullScreenCheckBox";
            FullScreenCheckBox.Size = new Size(15, 14);
            FullScreenCheckBox.TabIndex = 10;
            FullScreenCheckBox.UseVisualStyleBackColor = true;
            FullScreenCheckBox.CheckedChanged += FullScreenCheckBox_CheckedChanged;
            // 
            // VsynsCheckBox
            // 
            VsynsCheckBox.AutoSize = true;
            VsynsCheckBox.Location = new Point(109, 130);
            VsynsCheckBox.Name = "VsynsCheckBox";
            VsynsCheckBox.Size = new Size(15, 14);
            VsynsCheckBox.TabIndex = 11;
            VsynsCheckBox.UseVisualStyleBackColor = true;
            VsynsCheckBox.CheckedChanged += VsynsCheckBox_CheckedChanged;
            // 
            // FPSComboBox
            // 
            FPSComboBox.FormattingEnabled = true;
            FPSComboBox.Location = new Point(109, 158);
            FPSComboBox.Name = "FPSComboBox";
            FPSComboBox.Size = new Size(77, 23);
            FPSComboBox.TabIndex = 12;
            // 
            // MaxMusicTrackBar
            // 
            MaxMusicTrackBar.Location = new Point(164, 203);
            MaxMusicTrackBar.Maximum = 100;
            MaxMusicTrackBar.Name = "MaxMusicTrackBar";
            MaxMusicTrackBar.Size = new Size(193, 45);
            MaxMusicTrackBar.TabIndex = 13;
            MaxMusicTrackBar.Scroll += MaxMusicTrackBar_Scroll;
            // 
            // MaxSoundTrackBar
            // 
            MaxSoundTrackBar.Location = new Point(165, 243);
            MaxSoundTrackBar.Maximum = 100;
            MaxSoundTrackBar.Name = "MaxSoundTrackBar";
            MaxSoundTrackBar.Size = new Size(192, 45);
            MaxSoundTrackBar.TabIndex = 14;
            MaxSoundTrackBar.Scroll += MaxSoundTrackBar_Scroll;
            // 
            // BrightnessTrackBar
            // 
            BrightnessTrackBar.Location = new Point(165, 285);
            BrightnessTrackBar.Maximum = 100;
            BrightnessTrackBar.Name = "BrightnessTrackBar";
            BrightnessTrackBar.Size = new Size(192, 45);
            BrightnessTrackBar.TabIndex = 15;
            BrightnessTrackBar.Scroll += BrightnessTrackBar_Scroll;
            // 
            // SaveChangeBtn
            // 
            SaveChangeBtn.Location = new Point(257, 336);
            SaveChangeBtn.Name = "SaveChangeBtn";
            SaveChangeBtn.Size = new Size(126, 23);
            SaveChangeBtn.TabIndex = 16;
            SaveChangeBtn.Text = "Save changes";
            SaveChangeBtn.UseVisualStyleBackColor = true;
            SaveChangeBtn.Click += SaveChangeBtn_Click;
            // 
            // MaximumMusicLabel
            // 
            MaximumMusicLabel.AutoSize = true;
            MaximumMusicLabel.Location = new Point(363, 203);
            MaximumMusicLabel.Name = "MaximumMusicLabel";
            MaximumMusicLabel.Size = new Size(17, 15);
            MaximumMusicLabel.TabIndex = 17;
            MaximumMusicLabel.Text = "%";
            // 
            // MaximumSoundLabel
            // 
            MaximumSoundLabel.AutoSize = true;
            MaximumSoundLabel.Location = new Point(363, 243);
            MaximumSoundLabel.Name = "MaximumSoundLabel";
            MaximumSoundLabel.Size = new Size(17, 15);
            MaximumSoundLabel.TabIndex = 18;
            MaximumSoundLabel.Text = "%";
            // 
            // BrightnessLabel
            // 
            BrightnessLabel.AutoSize = true;
            BrightnessLabel.Location = new Point(363, 285);
            BrightnessLabel.Name = "BrightnessLabel";
            BrightnessLabel.Size = new Size(17, 15);
            BrightnessLabel.TabIndex = 19;
            BrightnessLabel.Text = "%";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(395, 364);
            Controls.Add(BrightnessLabel);
            Controls.Add(MaximumSoundLabel);
            Controls.Add(MaximumMusicLabel);
            Controls.Add(SaveChangeBtn);
            Controls.Add(BrightnessTrackBar);
            Controls.Add(MaxSoundTrackBar);
            Controls.Add(MaxMusicTrackBar);
            Controls.Add(FPSComboBox);
            Controls.Add(VsynsCheckBox);
            Controls.Add(FullScreenCheckBox);
            Controls.Add(WindowsizeComboBox);
            Controls.Add(MonitorComboBox);
            Controls.Add(label8);
            Controls.Add(label7);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(label1);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            MaximizeBox = false;
            MinimizeBox = false;
            Name = "Form1";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "SETTINGSAPP";
            ((System.ComponentModel.ISupportInitialize)MaxMusicTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)MaxSoundTrackBar).EndInit();
            ((System.ComponentModel.ISupportInitialize)BrightnessTrackBar).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Label label1;
        private Label label2;
        private Label label3;
        private Label label4;
        private Label label5;
        private Label label6;
        private Label label7;
        private Label label8;
        private ComboBox MonitorComboBox;
        private ComboBox WindowsizeComboBox;
        private CheckBox FullScreenCheckBox;
        private CheckBox VsynsCheckBox;
        private ComboBox FPSComboBox;
        private TrackBar MaxMusicTrackBar;
        private TrackBar MaxSoundTrackBar;
        private TrackBar BrightnessTrackBar;
        private Button SaveChangeBtn;
        private Label MaximumMusicLabel;
        private Label MaximumSoundLabel;
        private Label BrightnessLabel;
    }
}
