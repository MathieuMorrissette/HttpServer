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
        public bool HandleRequest(Client client, params string[] args)
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
                Dictionary<string, string> requestData = client.GetData();

                if (requestData.ContainsKey("username") && requestData.ContainsKey("password"))
                {
                    string username = requestData["username"];
                    string password = PasswordManager.Hash(requestData["password"]);

                    User user = UserManager.CreateUser(username, password);
                    if (user != null)
                    {
                        if (UserManager.Login(username, requestData["password"], client))
                        {
                            client.Redirect("home");
                            return true;
                        }
                    }
                }

                this.ShowRegister(client);
                client.Send("Something wrong happened there!");
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
