using System;
using System.Collections.Generic;
using System.IO;

namespace HttpServer
{
    public class Login : IController
    {
        public bool HandleRequest(Client client, params string[] args)
        {
            if (client.Connected)
            {
                client.Redirect("../home");
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
                        client.Redirect("../home");
                    }
                    else
                    {
                        this.ShowLogin(client, "Invalid username or password");
                    }
                }
                else
                {
                    this.ShowLogin(client, "Error");
                }
            }

            return true;
        }

        private void ShowLogin(Client client, string error = "")
        {
            if (client == null)
            {
                return;
            }

            string data = File.ReadAllText(Server.SERVER_ROOT_PATH + "login.html");

            string error_panel = string.Empty;
            if (error != string.Empty)
            {
                error_panel = File.ReadAllText(Server.SERVER_ROOT_PATH + "error_panel.html");
                error_panel = error_panel.Replace("__ErrorMessage__", error);
            }

            data = data.Replace("__Error__", error_panel);

            client.Send(data);
        }
    }
}
