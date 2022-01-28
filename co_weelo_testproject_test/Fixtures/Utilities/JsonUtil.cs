using System.IO;
using System.Text.Json;

namespace co_weelo_testproject_test.Fixtures.Utilities
{
    public class JsonUtil
    {
        public static T GetJsonObject<T>(string jsonFilePath)
        {
            T jsonObject = JsonSerializer.Deserialize<T>(LoadFile(jsonFilePath));
            return jsonObject;
        }

        public static string LoadFile(string filePath)
        {
            using (StreamReader r = new StreamReader(filePath))
            {
                var data= r.ReadToEnd();
                return data;
            }
        }
    }
}
