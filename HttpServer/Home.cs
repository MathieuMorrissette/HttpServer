using System;
using System.Collections.Generic;
using System.IO;

namespace HttpServer
{
    public class Home : IController
    {
        public bool HandleRequest(int index, Client client)
        {
            if (client.HttpMethod == HttpMethod.GET)
            {
                StreamWriter sw = new StreamWriter(client.Context.Response.OutputStream);
                client.Send(File.ReadAllText("login.html"));
            }
            else if(client.HttpMethod == HttpMethod.POST)
            {
                Dictionary<string, string> requestData = client.GetData();
            }

            return true;
        }
    }
}
