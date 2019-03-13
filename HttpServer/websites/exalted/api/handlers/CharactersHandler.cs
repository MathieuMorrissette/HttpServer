using HttpServer.websites.exalted.managers;
using HttpServer.websites.exalted.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.api.handlers
{
    public class CharactersHandler : IController
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

            if (context.Request.HttpMethod == "GET")
            {
                if (args.Length == 0)
                {
                    Character[] characters = CharacterManager.GetCharacters(user);

                    string output = JsonConvert.SerializeObject(CharacterHelper.ToResponse(characters));

                    context.Send(output);

                    return true;
                }

                int userId = 0;

                if (int.TryParse(args[0], out userId))
                {
                  /*  User friend = UserManager.GetUser(userId);

                    if (friend == null)
                    {
                        return true;
                    }

                    if (args.Length == 1)
                    {
                        context.Send(JsonConvert.SerializeObject(new User[] { friend }.ToResponse()));
                    }
                    else
                    {
                        switch (args[1])
                        {
                            case "remove":
                                FriendManager.RemoveFriend(user, friend);
                                break;
                        }

                        context.Redirect(context.Request.UrlReferrer.AbsoluteUri);
                    }*/
                }
            }

            return true;
        }
    }
}
