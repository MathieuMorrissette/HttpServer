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
                client.Redirect("../home");
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
                            client.Redirect("../home");
                            return true;
                        }
                    }
                    else
                    {
                        this.ShowRegister(client, new Error("Couldn't create the user!"));
                        return false;
                    }
                }

                this.ShowRegister(client, new Error("Something wrong happened there!"));
            }

            return true;
        }

        private void ShowRegister(Client client, Error error = null)
        {
            if (client == null)
            {
                return;
            }

            Register_Template registerTemplate = new Register_Template();
            registerTemplate.Errors = new Error[] { error };
            client.Send(registerTemplate.Render());
        }
    }
}
