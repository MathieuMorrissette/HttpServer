using HttpServer.classes;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.managers
{
    public static class ConfigurationManager
    {
        public static ServerConfig ServerConfig { get; private set; }

        public static void LoadConfiguration()
        {
            ServerConfig = JsonConvert.DeserializeObject<ServerConfig>(File.ReadAllText("configs/server.cfg"));
        }
    }
}
