
using System.Text.Json;

namespace Client.Lib.Utilities
{
    public static class SerializationsUtil
    {
        public static string? SerializeObj<T>(T modelObject) => JsonSerializer.Serialize(modelObject);
        public static T? DeserializeObj<T>(string jsonString) => JsonSerializer.Deserialize<T>(jsonString);
        public static IList<T>? DeserializeList<T>(string jsonString) => JsonSerializer.Deserialize<IList<T>>(jsonString);
    }
}
