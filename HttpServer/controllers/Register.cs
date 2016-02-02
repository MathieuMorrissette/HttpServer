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
                Dictionary<string, string> requestData = client.GetData();

                if (requestData.ContainsKey("username") && requestData.ContainsKey("password"))
                {
                    string username = requestData["username"];
                    string password = PasswordManager.Hash(requestData["password"], 10000);

                    User user = new User(username, password);

                    if (UserManager.CreateUser(user))
                    {
                        client.Dictionary.Add("User", user);
                        client.Redirect("home");
                    }
                    else
                    {
                        this.ShowRegister(client);
                        client.Send("Something wrong happened there!");
                    }
                }
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
