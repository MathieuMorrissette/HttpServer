using HttpServer.websites.mathieu_morrissette.api.helpers;
using HttpServer.websites.mathieu_morrissette.managers;
using HttpServer.websites.mathieu_morrissette.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.controllers
{
    public class Profile : IController
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

            context.Send("      your password hash is :" + user.Hash);
            context.Send( "           your username is :" + user.Username);
            context.Send("            your id is :" + user.Id);
            context.Send(JsonConvert.SerializeObject(PostManager.GetUserPosts(user).ToResponse()));

            return true;

        }
    }
}
