using HttpServer.websites.mathieu_morrissette.classes;
using HttpServer.websites.mathieu_morrissette.managers;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.handlers
{
    public class PostsHandler : IController
    {
        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            if (!UserManager.Connected(client))
            {
                context.Send("not connected");
                return true;
            }

            User user = UserManager.GetUser(UserManager.GetUserID(client));

            if (user == null)
            {
                context.Send("user not found");
                return true;
            }

            Post[] posts = PostManager.GetUserPosts(user);

            string output = JsonConvert.SerializeObject(posts);

            context.Send(output);

            return true;
        }
    }
}
