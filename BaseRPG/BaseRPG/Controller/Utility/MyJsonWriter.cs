using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseRPG.Controller.Utility
{
    public static class MyJsonWriter
    {
        public static void SaveAsDictionary<KEY, VALUE>(string fileName, Dictionary<KEY,VALUE> dict)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string json = JsonConvert.SerializeObject(dict);
            File.WriteAllText(Path.Combine(projectPath,fileName), json);
        }
        public static void SaveAsList<T>(string fileName,List<T> list)
        {
            string projectPath = AppDomain.CurrentDomain.BaseDirectory;
            string json = JsonConvert.SerializeObject(list);
            string path = Path.Combine(projectPath, fileName);
            File.WriteAllText(path, json);
        }
    }
}
