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

        private const string COOKIE_SESSION_ID = "SESSID";
        private const int SESSION_EXPIRE_TIME = 168; // Hours
        public const bool DEBUG = true;

        // Don't forget to delete them after a while.
        private static List<Client> Clients = new List<Client>();

        public Server()
        {
            this.httpListener = new HttpListener();

            this.httpListener.Prefixes.Add("http://localhost:8080/");
            this.httpListener.Prefixes.Add("http://127.0.0.1:8080/");
            this.httpListener.Prefixes.Add("http://192.168.1.120:8080/");
        }

        public void Start()
        {
            this.stop = false;
            this.httpListener.Start();

            while (!stop)
            {
                HttpListenerContext httpListenerContext = this.httpListener.GetContext();
                this.CheckExpiredClient();
                Task.Run(() =>
                {
                    Client client = this.GetClient(httpListenerContext);
                    var siteWeb = new WebSite(client);
                    siteWeb.HandleRequest();
                });
            }
        }

        private void CheckExpiredClient()
        {
            Stack<Client> clientToRemove = new Stack<Client>();

            for (int i = 0; i < Server.Clients.Count; i++)
            {
                Client client = Server.Clients[i];
                if (client.DateCreated.AddHours(Server.SESSION_EXPIRE_TIME) < DateTime.Now)
                {
                    clientToRemove.Push(client);
                }
            }

            while (clientToRemove.Count > 0)
            {
                Server.Clients.Remove(clientToRemove.Pop());
            }
        }

        private Client GetClient(HttpListenerContext httpListenerContext)
        {
            CookieCollection cookies = httpListenerContext.Request.Cookies;
            Client client = null;

            // Try to find a session with the cookie provided
            for (int i = 0; i < cookies.Count; i++)
            {
                Cookie cookie = cookies[i];

                if (cookie.Name == COOKIE_SESSION_ID)
                {
                    Guid cookieSessionId = Guid.Empty;
                    if (Guid.TryParse(cookie.Value, out cookieSessionId))
                    {
                        for (int j = 0; j < Server.Clients.Count; j++)
                        {
                            Client serverClients = Server.Clients[j];
                            if (serverClients.ID == cookieSessionId)
                            {
                                client = serverClients;
                                break;
                            }
                        }
                    }

                    break;
                }
            }

            //If no client found, create one.
            if (client == null)
            {
                client = new Client(Guid.NewGuid());
                Server.Clients.Add(client);
                client.Dictionary.Add("index", Server.Clients.IndexOf(client));
                httpListenerContext.Response.SetCookie(new Cookie(COOKIE_SESSION_ID, client.ID.ToString()) { Expires = DateTime.Now.AddMinutes(60) });
            }

            client.Context = httpListenerContext;
            return client;
        }

        public void Stop()
        {
            this.httpListener.Stop();
            this.stop = true;
        }       
    }
}
