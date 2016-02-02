using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Register : IController
    {
        public bool HandleRequest(int index, Client client)
        {
            if (client.Connected)
            {
                client.Redirect("home");
                return true;
            }

            if (client.HttpMethod == HttpMethod.GET)
            {
                this.ShowRegister(client);
            }
            else if (client.HttpMethod == HttpMethod.POST)
            {

            }

            return true;
        }

        private void ShowRegister(Client client)
        {
            if (client == null)
            {
                return;
            }

            client.Send(File.ReadAllText(Server.SERVER_ROOT_PATH + "register.html"));
        }
    }
}
