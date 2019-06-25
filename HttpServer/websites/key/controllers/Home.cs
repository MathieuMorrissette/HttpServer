
using HttpServer.websites.key.templates;
using System.Net;

namespace HttpServer.websites.key.controllers
{
    class Home : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {

            Home_Template template = new Home_Template();
            context.Send(template.Render());

            return true;
        }
    }
}
