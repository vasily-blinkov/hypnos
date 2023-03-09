using System.IO;
using Jil;

namespace Hypnos.Desktop.Utils
{
    public static class JsonUtility
    {
        public static string ToJsonString<T>(this T data)
        {
            using (var output = new StringWriter())
            {
                JSON.Serialize<T>(data, output);
                return output.ToString();
            }
        }
    }
}
