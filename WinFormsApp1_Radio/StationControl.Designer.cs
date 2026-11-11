namespace WinFormsApp1_Radio
{
    partial class StationControl
    {
        /// <summary> 
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором компонентов

        /// <summary> 
        /// Требуемый метод для поддержки конструктора — не изменяйте 
        /// содержимое этого метода с помощью редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            stationIcon = new PictureBox();
            playPauseButton = new Button();
            stationName = new Label();
            pictureBoxFlag = new PictureBox();
            labelFavorite = new Label();
            ((System.ComponentModel.ISupportInitialize)stationIcon).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFlag).BeginInit();
            SuspendLayout();
            // 
            // stationIcon
            // 
            stationIcon.Location = new Point(6, 25);
            stationIcon.Name = "stationIcon";
            stationIcon.Size = new Size(64, 64);
            stationIcon.SizeMode = PictureBoxSizeMode.Zoom;
            stationIcon.TabIndex = 0;
            stationIcon.TabStop = false;
            // 
            // playPauseButton
            // 
            playPauseButton.Location = new Point(6, 107);
            playPauseButton.Name = "playPauseButton";
            playPauseButton.Size = new Size(75, 23);
            playPauseButton.TabIndex = 1;
            playPauseButton.Text = "Play";
            playPauseButton.UseVisualStyleBackColor = true;
            playPauseButton.Click += playPauseButton_Click;
            // 
            // stationName
            // 
            stationName.AutoSize = true;
            stationName.Location = new Point(6, 7);
            stationName.Name = "stationName";
            stationName.Size = new Size(37, 15);
            stationName.TabIndex = 2;
            stationName.Text = "name";
            // 
            // pictureBoxFlag
            // 
            pictureBoxFlag.Location = new Point(82, 25);
            pictureBoxFlag.Name = "pictureBoxFlag";
            pictureBoxFlag.Size = new Size(40, 30);
            pictureBoxFlag.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBoxFlag.TabIndex = 3;
            pictureBoxFlag.TabStop = false;
            // 
            // labelFavorite
            // 
            labelFavorite.AutoSize = true;
            labelFavorite.BackColor = Color.Transparent;
            labelFavorite.Font = new Font("Segoe UI", 16F);
            labelFavorite.ForeColor = Color.IndianRed;
            labelFavorite.Location = new Point(87, 70);
            labelFavorite.Name = "labelFavorite";
            labelFavorite.Size = new Size(31, 60);
            labelFavorite.TabIndex = 4;
            labelFavorite.Text = "\r\n☆";
            labelFavorite.Click += labelFavorite_Click;
            // 
            // StationControl
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = SystemColors.ActiveBorder;
            Controls.Add(labelFavorite);
            Controls.Add(pictureBoxFlag);
            Controls.Add(stationName);
            Controls.Add(playPauseButton);
            Controls.Add(stationIcon);
            Name = "StationControl";
            Size = new Size(125, 133);
            Load += StationControl_Load;
            ((System.ComponentModel.ISupportInitialize)stationIcon).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBoxFlag).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private PictureBox stationIcon;
        private Button playPauseButton;
        private Label stationName;
        private PictureBox pictureBoxFlag;
        private Label labelFavorite;
    }
}
