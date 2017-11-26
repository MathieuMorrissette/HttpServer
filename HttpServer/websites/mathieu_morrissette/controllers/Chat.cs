using HttpServer.websites.mathieu_morrissette.templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.controllers
{
    public class Chat : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            Chat_Template template = new Chat_Template();
            context.Send(template.Render());
            return true;
        }
    }
}
