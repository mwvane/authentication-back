using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;

namespace authentication_back
{
    public static  class StorageHelper
    {
        public static string defaultPath = Directory.GetCurrentDirectory() + "\\users.json";
        public static void Save(List<User> users)
        {
            var jsonFormmated = JsonConvert.SerializeObject(users);
            Console.WriteLine(jsonFormmated);
            File.WriteAllText(defaultPath, jsonFormmated);

        }
        public static List<User> GetUsers()
        {
            List<User> users = new List<User>();
            if (File.Exists(defaultPath))
            {
                string justText = File.ReadAllText(defaultPath);
                List<User> u = JsonConvert.DeserializeObject<List<User>>(justText);
                Console.WriteLine(u);
                return u;
            }
            return null;
        }
    }
}
