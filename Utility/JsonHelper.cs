using Newtonsoft.Json;

namespace Utility;

public static class JsonHelper
{
    public static T Deserialize<T>(string jsonData) => JsonConvert.DeserializeObject<T>(jsonData);
}
