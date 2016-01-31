using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    class Home : IController
    {
        public bool HandleRequest(int index, Client client)
        {
            if (!client.Connected)
            {
                client.Redirect("login");
                return true;
            }

            client.Send(File.ReadAllText(Server.SERVER_ROOT_PATH + "home.html"));

            return true;
        }
    }
}
