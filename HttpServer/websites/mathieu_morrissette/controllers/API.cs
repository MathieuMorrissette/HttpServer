using HttpServer.websites.mathieu_morrissette.api.handlers;
using HttpServer.websites.mathieu_morrissette.managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.controllers
{
    class API : IController
    {
        public Dictionary<string, Func<IController>> handlersDictionary = new Dictionary<string, Func<IController>>
        {
            { "posts", () => new PostsHandler() },
            { "users", () => new UsersHandler() },
            { "friend-requests", () => new FriendRequestsHandler() },
            { "friends", () => new FriendsHandler() }
        };

        public bool HandleRequest(Client client, HttpListenerContext context, params string[] args)
        {
            if (!UserManager.Connected(client))
            {
                context.Redirect("../login");
                return true;
            }

            if (args.Length == 0)
            {
                context.Send("pleb");
                return true;
            }

            Func<IController> handler = handlersDictionary[args[0]];

            if (handler != null)
            {
                IController controller = handler();

                string[] arguments = args.Skip(1).ToArray();

                controller.HandleRequest(client, context, arguments);
            }

            return true;
        }
    }
}
