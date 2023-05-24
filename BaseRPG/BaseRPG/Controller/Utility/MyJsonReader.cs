using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Utility
{
    public static class MyJsonReader
    {
        public static Dictionary<KEY,VALUE> AsDictionary<KEY, VALUE>(string fileName) {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string json = File.ReadAllText(Path.Combine(projectPath,fileName));
            return JsonConvert.DeserializeObject<Dictionary<KEY, VALUE>>(json);
        }
        public static List<T> AsList<T>(string fileName)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string json = File.ReadAllText(Path.Combine(projectPath, fileName));
            return JsonConvert.DeserializeObject<List<T>>(json);
        }
    }
}
