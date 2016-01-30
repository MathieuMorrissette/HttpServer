using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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
