

using HttpServer.websites.key.controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;

namespace HttpServer.websites.key
{
    public class WebSite : BaseWebsite
    {
        const string DEFAULT_ROUTE = "home";
        public const string WEBSITE_ROOT_PATH = "../../websites/key/";



        private string connectionString;

        private static Dictionary<string, Func<IController>> routes = new Dictionary<string, Func<IController>>
        {
            { "home", () =>  new Home()}
        };

        IController controller;

        private void LoadConfiguration()
        {

        }

        public WebSite(Client client, HttpListenerContext context) : base(client, context)
        {
            this.LoadConfiguration();


            string[] parsedArgs = WebHelper.GetUrlArguments(this.Context.Request.Url.Segments);

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
            this.Context.Response.StatusCode = 404;
            this.Context.Response.OutputStream.Close();
        }

        public override void HandleRequest()
        {
            if (controller == null)
            {
                this.Context.Response.OutputStream.Close();
                return;
            }

            string[] arguments = WebHelper.GetUrlArguments(this.Context.Request.Url.Segments);

            // Remove the first arguments since it is the controller name.
            arguments = arguments.Skip(1).ToArray();

            controller.HandleRequest(this.Client, this.Context, arguments);
        }
    }
}
