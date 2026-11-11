
using RadioBrowserLibrary;
using RadioPlaybackLabrary;
using RadioPlayBeckLabrary;
using RadioPlayBeckLabryry;
namespace WinFormsApp1_Radio
{
    public partial class Form1 : Form
    {
        private RadioBrowser radioBrowser;
        private IRadioPlayback radioPlayback;
        public Form1()
        {
            InitializeComponent();
            radioBrowser = new RadioBrowser();
            radioPlayback = PlaybackCenter.Instance.GetRadioPlayback();
            radioPlayback.SetVolumeLevel(0.2);
        }

        private async void buttonFavorite_Click(object sender, EventArgs e)
        {
            var favoriteIds = RadioStorageManager.Instance.GetFavorites;
            if (favoriteIds.Count == 0)
            {
                itemsPanel.Controls.Clear();
               
                return;
            }

            List<StationInfo> stations = await radioBrowser.GetStationsByUUIDs(favoriteIds);
            FillItemsPanel(stations);

        }

        private async void buttonHistory_Click(object sender, EventArgs e)
        {
            var historyIds = RadioStorageManager.Instance.GetLastStation();
            if (historyIds.Count == 0)
            {
                itemsPanel.Controls.Clear();
                
                return;
            }

            List<StationInfo> stations = await radioBrowser.GetStationsByUUIDs(historyIds);

            var orderedStations = historyIds
                .Select(id => stations.FirstOrDefault(s => s.Stationuuid == id))
                .Where(s => s != null)
                .ToList()!;

            FillItemsPanel(orderedStations);
        }

        

        private async void Form1_Load(object sender, EventArgs e)
        {
            await LoadData();
        }
        private async Task LoadData()
        {
            ServerInfo serverInfo = await radioBrowser.GetServerInfo();
            await radioBrowser.UpdateServerUrl();
            List<CountryInfo> countries = await radioBrowser.GetCountries();
            FillCountries(countries);
        }

        private void FillCountries(List<CountryInfo> countries)
        {
            foreach (var item in countries)
            {
                comboBox1.Items.Add(item);
            }
            try
            {
                int startIndex = countries.FindIndex(c => string.Equals(c.CountryCode, "ua", StringComparison.OrdinalIgnoreCase));
                if(startIndex>=0 && startIndex< comboBox1.Items.Count)
                {
                    comboBox1.SelectedIndex = startIndex;
                }
                else if (comboBox1.Items.Count>0)
                {
                    comboBox1.SelectedIndex = 0;
                }
           
            }
            catch
            {
                comboBox1.SelectedIndex = -1;
            }
            

        }


        private async void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedItem is CountryInfo countryInfo)
            {
                string countryCode = countryInfo.CountryCode.ToLower();
                await LoadStationsdata(countryCode);
            }
        }
        private async Task LoadStationsdata(string countryCode)
        {
            List<StationInfo> stations = await radioBrowser.GetStation(countryCode, 0, 20);
           
            FillItemsPanel(stations);
        }
       

        
        

        private void FillItemsPanel(List<StationInfo> stations)
        {
            List<StationInfo> stationInfos = new List<StationInfo>();
            stationInfos = stations.DistinctBy(c => new { c.StrimUrl, Name = Name.Trim().ToLower() }).ToList();
            itemsPanel.Controls.Clear();
            foreach (var item in stationInfos)
            {
                
                itemsPanel.Controls.Add(new StationControl(item));
            }
        }
        
            
        
    }
}
