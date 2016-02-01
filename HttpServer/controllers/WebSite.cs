using HttpServer.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class WebSite
    {
        const string DEFAULT_ROUTE = "login";
        const string CONNECTION_STRING = @"Server=127.0.0.1;Database=test;Uid=root;Pwd=root;";

        Client client;

        public Database Database { get; private set; }

        private static Dictionary<string, Func<IController>> routes = new Dictionary<string, Func<IController>>
        {
            { "login", () => new Login()},
            { "home", () =>  new Home()},
            { "images", () => new FileProvider()},
            { "javascript", () => new FileProvider()},
            { "css", () => new FileProvider()},
            { "font", () => new FileProvider()}
        };

        IController controller;

        public WebSite(Client client)
        {
            this.Database = new Database(CONNECTION_STRING);
            this.Database.ExecuteNonQuery("INSERT INTO users (Username, Password) VALUES ('bob', 'ross')");

            this.client = client;

            string[] parsedArgs = WebHelper.GetUrlArguments(this.client.Context.Request.Url.Segments);

            string controller = string.Empty;

            if (parsedArgs.Length == 0)
            {
                controller = DEFAULT_ROUTE;
            }
            else if (parsedArgs[0] == string.Empty)
            {
                controller = DEFAULT_ROUTE;
            }
            else
            {
                controller = parsedArgs[0];
            }

            if (routes.ContainsKey(controller))
            {
                this.controller = routes[controller]();
            }
            else
            {
                Error();
                return;
            }
        }

        private void Error()
        {
            this.client.Context.Response.StatusCode = 404;
            this.client.Context.Response.OutputStream.Close();
        }

        public void HandleRequest()
        {
            if (controller == null)
            {
                this.client.Context.Response.OutputStream.Close();
                return;
            }

            controller.HandleRequest(0, this.client);

            this.client.Context.Response.Close();
        }
    }
}
