using HttpServer.websites.mathieu_morrissette.managers;
using HttpServer.websites.mathieu_morrissette.model;
using HttpServer.websites.mathieu_morrissette.templates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.controllers
{
    public class Friends : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            if (!UserManager.Connected(client))
            {
                context.Redirect("../login");
                return true;
            }

            User user = UserManager.GetUser(UserManager.GetUserID(client));

            if (user == null)
            {
                context.Redirect("../login");
                return true;
            }

            Friends_Template friends_template = new Friends_Template(user);
            context.Send(friends_template.Render());

            return true;
        }
    }
}
