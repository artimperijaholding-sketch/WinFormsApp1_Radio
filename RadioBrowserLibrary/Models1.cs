using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace RadioBrowserLibrary;
// https://de1.api.radio-browser.info/json/stats

//{
//  "status": "OK",
//  "stations": 55590,
//  "countries": 238
//}

public class ServerInfo
{
    [JsonProperty("status")]
    public string Status { get; set; } = string.Empty;

    [JsonProperty("stations")]
    public int Stations { get; set; }

    [JsonProperty("countries")]
    public int Countries { get; set; }
    public bool IsRunning() => string.Equals(Status, "OK", StringComparison.OrdinalIgnoreCase);

}
public class MirrorInfo
{
    [JsonProperty("ip")]
    public string Ip { get; set; } = string.Empty;

    [JsonProperty("name")]
    public string Name { get; set; } = string.Empty;
}


// https://de1.api.radio-browser.info/json/servers

//[
//  {
//    "ip": "91.98.4.78",
//    "name": "de1.api.radio-browser.info"
//  },
//  {
//    "ip": "2a01:4f8:1c1d:699::1",
//    "name": "de1.api.radio-browser.info"
//  }
//]

// https://de1.api.radio-browser.info/json/countries

//[

//  {

//    "name": "Andorra",

//    "iso_3166_1": "AD",

//    "stationcount": 8

//  },

//  {

//    "name": "The United Arab Emirates",

//    "iso_3166_1": "AE",

//    "stationcount": 771

//  },

//  ...

//]

public class CountryInfo

{

    [JsonProperty("name")]

    public string Name { get; set; } = string.Empty;

    [JsonProperty("iso_3166_1")]

    public string CountryCode { get; set; } = string.Empty;

    [JsonProperty("stationcount")]

    public int StationCount { get; set; }

    public override string ToString() => $"{Name} ({CountryCode.ToUpper()})";

}

// https://de1.api.radio-browser.info/json/stations/search?countrycode=de

//[

//    {

//        "stationuuid": "ffc0d701-f43b-4232-8be6-f7ff72add8ef",

//        "name": "\tBlasmusikradio mit Bernd",

//        "url_resolved": "https://stream.laut.fm/blasmusikradio_mit_bernd",

//        "homepage": "https://laut.fm/blasmusikradio_mit_bernd",

//        "favicon": "https://assets.laut.fm/b22749b7fdb382b8912269e3d8054380?t=_120x120",

//        "tags": "blasmusik,folk,marsch,polka",

//        "country": "Germany",

//        "countrycode": "DE",

//        "language": "german",

//        "codec": "MP3",

//        "bitrate": 128,

//      },

//      ...

//]

public class StationInfo

{

    [JsonProperty("stationuuid")]

    public string Stationuuid { get; set; } = string.Empty;

    [JsonProperty("name")]

    public string Name { get; set; } = string.Empty;

    [JsonProperty("url_resolved")]

    public string StrimUrl { get; set; } = string.Empty;

    [JsonProperty("homepage")]

    public string Homepage { get; set; } = string.Empty;

    [JsonProperty("favicon")]

    public string Favicon { get; set; } = string.Empty;

    [JsonProperty("tags")]

    public string Tags { get; set; } = string.Empty;

    [JsonProperty("country")]

    public string Country { get; set; } = string.Empty;

    [JsonProperty("countrycode")]

    public string Countrycode { get; set; } = string.Empty;

    [JsonProperty("language")]

    public string Language { get; set; } = string.Empty;

    [JsonProperty("codec")]

    public string Codec { get; set; } = string.Empty;

    [JsonProperty("bitrate")]

    public int Bitrate { get; set; }

    public override string ToString() => $"{Name} ({Tags})";

}



