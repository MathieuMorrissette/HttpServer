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

        public Client Client { get; private set; }

        public static DatabaseManager Database { get; private set; }

        private static Dictionary<string, Func<IController>> routes = new Dictionary<string, Func<IController>>
        {
            { "login", () => new Login()},
            { "register", () => new Register()},
            { "home", () =>  new Home()},
            { "images", () => new FileProvider()},
            { "javascript", () => new FileProvider()},
            { "css", () => new FileProvider()},
            { "font", () => new FileProvider()},
            { "user", () => new UserController()}
        };

        IController controller;

        public WebSite(Client client)
        {
            WebSite.Database = new DatabaseManager(CONNECTION_STRING, new DatabaseMySql());

            //Create tables if they don't exists
            WebSite.Database.CreateDatabase();

            this.Client = client;

            string[] parsedArgs = WebHelper.GetUrlArguments(this.Client.Context.Request.Url.Segments);

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
            this.Client.Context.Response.StatusCode = 404;
            this.Client.Context.Response.OutputStream.Close();
        }

        public void HandleRequest()
        {
            if (controller == null)
            {
                this.Client.Context.Response.OutputStream.Close();
                return;
            }

            string[] arguments = WebHelper.GetUrlArguments(this.Client.Context.Request.Url.Segments);

            // Remove the first arguments since it is the controller name.
            arguments = arguments.Skip(1).ToArray();

            controller.HandleRequest(this.Client, arguments);

            this.Client.Context.Response.Close();
        }
    }
}
