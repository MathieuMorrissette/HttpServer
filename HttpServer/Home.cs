using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Home : IController
    {
        public bool HandleRequest(int index, HttpListenerContext context)
        {
            context.Response.ContentType = null;
            StreamWriter sw = new StreamWriter(context.Response.OutputStream);
            sw.Write(File.ReadAllText("index.html"));
            sw.Flush();

            return true;
        }
    }
}
