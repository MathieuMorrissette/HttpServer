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

        HttpListenerContext context;

        private static Dictionary<string, Func<IController>> routes = new Dictionary<string, Func<IController>>
        {
            {"home", () => new Home()},
            {"images", () => new Images()}
        };

        IController controller;

        public WebSite(HttpListenerContext context)
        {
            this.context = context;

            string[] parsedArgs = WebHelper.GetUrlArguments(this.context.Request.Url.Segments);

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
            this.context.Response.StatusCode = 404;
            this.context.Response.OutputStream.Close();
        }

        public void HandleRequest()
        {
            if (controller == null)
            {
                this.context.Response.OutputStream.Close();
                return;
            }

            controller.HandleRequest(0, this.context);

            this.context.Response.Close();
        }
    }
}
