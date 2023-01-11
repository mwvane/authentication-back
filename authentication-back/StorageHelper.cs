using System.Xml.Linq;
using System.IO;
using Newtonsoft.Json;

namespace authentication_back
{
    public static class StorageHelper
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
            if (File.Exists(defaultPath))
            {
                string justText = File.ReadAllText(defaultPath);
                List<User> user = JsonConvert.DeserializeObject<List<User>>(justText);
                Console.WriteLine(user);
                return user;
            }
            return null;
        }
    }
}
