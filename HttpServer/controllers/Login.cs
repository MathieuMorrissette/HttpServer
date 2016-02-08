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
                        this.ShowLogin(client, new Error("Invalid username or password"));
                    }
                }
                else
                {
                    this.ShowLogin(client, new Error("Error"));
                }
            }

            return true;
        }

        private void ShowLogin(Client client, Error error = null)
        {
            if (client == null)
            {
                return;
            }

            Login_Template login_template = new Login_Template();
            login_template.Errors = new Error[] { error };
            client.Send(login_template.Render());
        }
    }
}
