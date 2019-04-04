using HttpServer.classes;
using HttpServer.managers;
using HttpServer.websites;
using HttpServer.websites.mathieu_morrissette;
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

            ConfigurationManager.LoadConfiguration();

            foreach (string url in ConfigurationManager.ServerConfig.listen)
            {
                this.httpListener.Prefixes.Add(url);
            }
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
                    try
                    {
                        // Hide server header hack
                        httpListenerContext.Response.Headers.Add("Server", "\r\n\r\n");


                        Client client = this.GetClient(httpListenerContext);

                        string hostName = httpListenerContext.Request.Url.Host;

                        bool handled = false;

                        foreach (Route route in ConfigurationManager.ServerConfig.routes)
                        {
                            if (route.value.Contains(hostName))
                            {
                                Type type = Type.GetType(route.type);

                                BaseWebsite website = (BaseWebsite)Activator.CreateInstance(type, client, httpListenerContext);
                                website.HandleRequest();
                                handled = true;
                                break;
                            }
                        }

                        if (!handled)
                        {
                            httpListenerContext.Send("error");
                        }

                        httpListenerContext.Response.AddHeader("Server", "troll");

                        httpListenerContext.Response.Close();
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message + " - " + ex.StackTrace);
                    }
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

                Cookie cook = new Cookie(COOKIE_SESSION_ID, client.ID.ToString()) { Expires = DateTime.Now.AddHours(Server.SESSION_EXPIRE_TIME), HttpOnly = true };

                httpListenerContext.Response.SetCookie(cook);
            }

            return client;
        }

        public void Stop()
        {
            this.httpListener.Stop();
            this.stop = true;
        }       
    }
}
