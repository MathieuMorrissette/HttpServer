using HttpServer.websites.mathieu_morrissette.api.responses;
using HttpServer.websites.mathieu_morrissette.managers;
using HttpServer.websites.mathieu_morrissette.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.handlers
{
    public class UsersHandler : IController
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

            if (args.Length > 0)
            {
                switch(args[0])
                {
                    case "me":
                        context.Send(JsonConvert.SerializeObject(UserResponse.FromUser(user)));
                        break;

                }
            }

            return true;
        }
    }
}
