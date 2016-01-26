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
        const string DEFAULT_ROUTE = "home";

        Client client;

        private static Dictionary<string, Func<IController>> routes = new Dictionary<string, Func<IController>>
        {
            { "home", () => new Home()},
            { "images", () => new FileProvider()},
            { "javascript", () => new FileProvider()},
            { "css", () => new FileProvider()},
            { "font", () => new FileProvider()}
        };

        IController controller;

        public WebSite(Client client)
        {
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
