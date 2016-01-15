using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Server
    {
        private HttpListener httpListener;
        private bool stop = false;

        public Server()
        {
            this.httpListener = new HttpListener();

            this.httpListener.Prefixes.Add("http://localhost:8080/");
            this.httpListener.Prefixes.Add("http://127.0.0.1:8080/");
        }

        public void Start()
        {
            this.stop = false;
            this.httpListener.Start();

            while (!stop)
            {
                HttpListenerContext httpListenerContext = this.httpListener.GetContext();

                Task.Run(() =>
                {
                    var siteWeb = new WebSite(httpListenerContext);
                    siteWeb.HandleRequest();
                });
            }
        }

        public void Stop()
        {
            this.httpListener.Stop();
            this.stop = true;
        }       
    }
}
