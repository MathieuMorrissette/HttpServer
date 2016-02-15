using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.controllers
{
    class Home : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            if (!client.Connected)
            {
                context.Redirect("../login");
                return true;
            }

            context.Send(File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "html/home.html"));

            return true;
        }
    }
}
