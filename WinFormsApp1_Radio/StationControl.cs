using RadioBrowserLibrary;
using RadioPlaybackLabrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Text;
using System.Windows.Forms;

namespace WinFormsApp1_Radio
{
    public partial class StationControl : UserControl
    {
        private StationInfo Station;
        private bool isFavorite = false;
        bool isPlaying = false;
        List<string> stationsId = new();
        public StationControl(StationInfo station)
        {
            InitializeComponent();
            this.Station = station;

            PlaybackCenter.Instance.NotifyPlaybackStateChanged += PlaybackCenter_NotifyPlaybackStateChanged;
        }

        private void PlaybackCenter_NotifyPlaybackStateChanged(PlayState state, string playbackUrl)
        {
            UpdateState(state, playbackUrl);
        }
        private void UpdateState(PlayState state, string playbackUrl)
        {
            if (state == PlayState.Play)
            {
                if (string.Equals(Station.StrimUrl, playbackUrl))
                {
                    playPauseButton.Text = "Pause";
                    stationName.ForeColor = Color.Green;
                    isPlaying = true;

                }
            }
            else
            {
                playPauseButton.Text = "Play";
                stationName.ForeColor = Color.Black;
                isPlaying = false;
            }
        }

        private void StationControl_Load(object sender, EventArgs e)
        {
            Init();
        }
        private async void Init()
        {

            if (Station != null)
            {
                Image image = Image.FromFile("Image/music_14800979.png");
                stationIcon.Image = image;
                stationIcon.SizeMode = PictureBoxSizeMode.Zoom;
                stationName.Text = Station.Name;
                isFavorite = RadioStorageManager.Instance.IsFavorite(Station.Stationuuid);
                UpdateFavoriteUI();
                string countrCode = Station.Countrycode.Trim().ToLower();
                string flagUrl = $"https://flagcdn.com/40x30/{countrCode}.png";
                if (!string.IsNullOrEmpty(Station.Favicon))
                {
                    try
                    {
                        using (HttpClient client = new HttpClient())
                        {

                            client.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0");
                            byte[] imageBytes = await client.GetByteArrayAsync(Station.Favicon);
                            using (MemoryStream ms = new MemoryStream(imageBytes))
                            {
                                stationIcon.Image = Image.FromStream(ms);
                            }
                        }
                    }
                    catch
                    {
                        stationIcon.Image = image;
                    }
                }
                try
                {
                    if ( !string.IsNullOrEmpty(flagUrl))
                    {
                        
                        pictureBoxFlag.LoadAsync(flagUrl);
                    }
                    

                }
                catch
                {
                    
                }
            }
        }
        

        private async void playPauseButton_Click(object sender, EventArgs e)
        {
            PlaybackCenter.Instance.GetRadioPlayback().Play(Station.StrimUrl);
            if(!string.IsNullOrEmpty(Station.Stationuuid))
            {
                await RadioStorageManager.Instance.AddHistoryAsync(Station.Stationuuid);
            }
        }
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            e.Graphics.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;
            int BorderRadius = 30;
            Rectangle rect = new Rectangle(0, 0, this.Width - 1, this.Height - 1);

            using (GraphicsPath path = GetRoundedPath(rect, BorderRadius))
            {
                this.Region = new Region(path);
                e.Graphics.DrawPath(new Pen(Color.FromArgb(90, Color.White)), path);
                e.Graphics.FillPath(new SolidBrush(Color.FromArgb(90, Color.White)), path);
            }


        }
        private System.Drawing.Drawing2D.GraphicsPath GetRoundedPath(Rectangle rect, float radius)
        {
            System.Drawing.Drawing2D.GraphicsPath path = new System.Drawing.Drawing2D.GraphicsPath();
            float m = 0.5F;
            path.StartFigure();
            path.AddArc(rect.X + m, rect.Y + m, radius, radius, 180, 90);
            path.AddArc(rect.Width - radius - m, rect.Y + m, radius, radius, 270, 90);
            path.AddArc(rect.Width - radius - m, rect.Height - radius - m, radius, radius, 0, 90);
            path.AddArc(rect.X + m, rect.Height - radius - m, radius, radius, 90, 90);
            path.CloseAllFigures();
            return path;
        }
        private void UpdateFavoriteUI()
        {
            if (isFavorite)
            {
                labelFavorite.Text = "⭐";
                labelFavorite.ForeColor = Color.Gold;

            }
            else
            {
                labelFavorite.Text = "☆";
                labelFavorite.ForeColor = Color.Gray;
            }

        }

        private async void labelFavorite_Click(object sender, EventArgs e)
        {
            isFavorite = !isFavorite;
            UpdateFavoriteUI();
            if (Station != null && !string.IsNullOrEmpty(Station.Stationuuid))
            {
                if(isFavorite)
                {
                    await RadioStorageManager.Instance.AddFavoriteAsync(Station.Stationuuid);
                }
                else
                {
                    await RadioStorageManager.Instance.RemoveFavoriteAsync(Station.Stationuuid);
                }
            }

        }
    }
}
