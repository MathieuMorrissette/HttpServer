using System;
using System.Collections.Generic;
using System.IO;

namespace HttpServer
{
    public class Login : IController
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
                this.ShowLogin(client);
            }
            else if(client.HttpMethod == HttpMethod.POST)
            {
                Dictionary<string, string> requestData = client.GetData();

                if (requestData.ContainsKey("username") && requestData.ContainsKey("password"))
                {
                    string username = requestData["username"];
                    string password = requestData["password"];

                    if (UserManager.Login(username, password, client))
                    {
                        client.Redirect("home");
                    }
                    else
                    {
                        this.ShowLogin(client);
                        client.Send("Invalid username or password");
                    }
                }
                else
                {
                    this.ShowLogin(client);
                    client.Send("Error");
                }
            }

            return true;
        }

        private void ShowLogin(Client client)
        {
            if (client == null)
            {
                return;
            }

            client.Send(File.ReadAllText(Server.SERVER_ROOT_PATH + "login.html"));
        }
    }
}
