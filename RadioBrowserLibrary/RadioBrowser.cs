using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using Utility;

namespace RadioBrowserLibrary;
//order:

//1) check if server is OK

//de1.api.radio-browser.info/json/stats

//2) get list of possible mirross

//de1.api.radio-browser.info/json/servers

//3) get list of countries:

//de1.api.radio-browser.info/json/countries

//4) get list of the stations by country code(iso)

//de1.api.radio-browser.info/json/stations/search? countrycode = de

//5) get top 10 popular stations:

//de1.api.radio-browser.info/json/stations/topvote? offset = 0 & limit = 10

public static class RadioUrl

{

    public const string ServerInfoUrl = "http://all.api.radio-browser.info";
    public const string Format ="/json";
    public const string StatsCatalog ="/stats";
    public const string ServersCatalog ="/servers";
    public const string CountriesCatalog ="/countries";


}

public class RadioBrowser
{
    private static readonly string[] blockedCountries = new string[] { "IR", "BY", "RU" };
    private static readonly string[] blockedStations = new string[] { "5295cf5a-a921-4556-9187-873b651a2306" };
    private static readonly string[] blockedStationNames = new string[] { "ulsamad" };
    private HttpClient httpClient=new HttpClient();
    public string ServerUrl { get;private set; }=string.Empty;

    public RadioBrowser()
    {
        httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/237.84.2.178 Safari/537.36");
    }

    public async Task<bool> IsServerRunning()
    {
        if (await GetServerInfo() is ServerInfo serverInfo)
        {
            return serverInfo.IsRunning();
        }
        return false;
    }

    public async Task<ServerInfo> GetServerInfo()
    {
        ServerInfo serverInfo = null;
        try
        {
            //string url = RadioUrl.ServerInfoUrl+"/"+ RadioUrl.Format+"/"+RadioUrl.StatsCatalog;
            //string url =Path.Combine(RadioUrl.ServerInfoUrl,RadioUrl.Format,RadioUrl.StatsCatalog);
            string url =string.Concat(RadioUrl.ServerInfoUrl,RadioUrl.Format,RadioUrl.StatsCatalog);
            string data = await httpClient.GetStringAsync(url);
            serverInfo=JsonHelper.Deserialize<ServerInfo>(data);
        }
        catch (HttpRequestException re)
        {
            Debug.WriteLine("HttpRequestException: " + re.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception: " + ex.Message);
        }
        return serverInfo;
    }
    public async Task UpdateServerUrl()
    {
        try
        {
            string url = string.Concat(RadioUrl.ServerInfoUrl, RadioUrl.Format, RadioUrl.ServersCatalog);
            string data = await httpClient.GetStringAsync(url);
            List<MirrorInfo> mirrors = Utility.JsonHelper.Deserialize<List<MirrorInfo>>(data);
            // !!!
            // IEqualityComparer (not only dy name but also by ip address type
            // mirrors.Distinct()
            // OR
            // use Regex c# to detect real IPv4

            ServerUrl = mirrors.Any() ? string.Concat("https://", mirrors[0].Name) : string.Empty;
        }
        catch (HttpRequestException re)
        {
            Debug.WriteLine("HttpRequestException: " + re.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception: " + ex.Message);
        }
    }
    public async Task<List<CountryInfo>> GetCountries()
    {
        List<CountryInfo> countries = new List<CountryInfo>();
        try
        {
            string url = string.Concat(ServerUrl, RadioUrl.Format, RadioUrl.CountriesCatalog);
            string data = await httpClient.GetStringAsync(url);
            countries = Utility.JsonHelper.Deserialize<List<CountryInfo>>(data);  
            countries = FilterBlockedCountries(countries);
        }
        catch (HttpRequestException re)
        {
            Debug.WriteLine("HttpRequestException: " + re.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception: " + ex.Message);
        }
        return countries;
    }
    private List<CountryInfo> FilterBlockedCountries(List<CountryInfo> countries)
    {
        //return countries.Where(c => !blockedCountries.Contains(c.CountryCode)).ToList();
        return countries.Where(c =>! blockedCountries.Any(bc=>string.Equals(c.CountryCode,bc,StringComparison.OrdinalIgnoreCase))).ToList();
    }
    public async Task <List<StationInfo> > GetStation (string countryCode,int offset=0,int limit=10)
    {
        List<StationInfo> stations = new ();
        try
        {
            string urlBase = string.Concat(ServerUrl, RadioUrl.Format, "/stations/search?");
            string url = urlBase + $"countrycode={countryCode.ToLower()}&offset={offset}&limit={limit}";

            string data = await httpClient.GetStringAsync(url);
            stations = Utility.JsonHelper.Deserialize<List<StationInfo>>(data);
            stations = FilterBlockedStationsByName(stations);
            stations=stations.DistinctBy(c => c.Name).ToList();
        }
        catch (HttpRequestException re)
        {
            Debug.WriteLine("HttpRequestException: " + re.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception: " + ex.Message);
        }
        return stations;
    }
    private List<StationInfo> FilterBlockedStationsById(List<StationInfo> stations)
    {
        return stations.Where(s => !blockedStations.Any(bs => string.Equals(s.Stationuuid, bs, StringComparison.OrdinalIgnoreCase))).ToList();
    }

    private List<StationInfo> FilterBlockedStationsByName(List<StationInfo> stations)
    {
        return stations.Where(s => !blockedStationNames.Any(bs => !string.IsNullOrEmpty(s.Name) && s.Name.Contains(bs))).ToList();
    }


    
    public async Task<List<StationInfo>> GetStationsByUUIDs(List<string> uuids)
    {
        List<StationInfo> stations = new();
        if (uuids == null || uuids.Count == 0) return stations;

        try
        {
            // API Radio-browser приймає шлях /json/stations/byuuid?uuids=id1,id2,id3
            string uuidsJoined = string.Join(",", uuids);
            string url = string.Concat(ServerUrl, RadioUrl.Format, "/stations/byuuid?uuids=", uuidsJoined);

            string data = await httpClient.GetStringAsync(url);
            stations = Utility.JsonHelper.Deserialize<List<StationInfo>>(data);

            // Застосовуємо фільтри блокування
            stations = FilterBlockedStationsById(stations);
            stations = FilterBlockedStationsByName(stations);
        }
        catch (HttpRequestException re)
        {
            Debug.WriteLine("HttpRequestException: " + re.Message);
        }
        catch (Exception ex)
        {
            Debug.WriteLine("Exception: " + ex.Message);
        }
        return stations;
    }

}
