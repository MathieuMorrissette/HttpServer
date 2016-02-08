using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Header_Template : ITemplate
    {
        public string Render()
        {
            string data = File.ReadAllText(Server.SERVER_ROOT_PATH + "html/header.html");
            return data;
        }
    }
}
