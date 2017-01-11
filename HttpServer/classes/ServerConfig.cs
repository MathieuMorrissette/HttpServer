using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.classes
{
    public class ServerConfig
    {
        public List<string> listen { get; set; }
        public List<Route> routes { get; set; }
    }
}
