using HttpServer.websites.exalted.managers;
using HttpServer.websites.exalted.model;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.api.handlers
{
    public class CharacterHandler : IController
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

            if (context.Request.HttpMethod == "POST")
            {
                if (args.Length == 0)
                {
                    return true;
                }

                if (args[0] == "create")
                {
                    string body = new StreamReader(context.Request.InputStream).ReadToEnd();

                    JObject json = JObject.Parse(body);

                    CharacterManager.CreateCharacter(user, json["charactername"].ToString(), string.Empty);
                }


                if (args.Length == 2)
                {
                    int characterId = Convert.ToInt32(args[0]);

                    Character character = CharacterManager.GetCharacter(characterId);

                    if (character == null)
                    {
                        return true;
                    }

                    if (character.UserId != user.Id)
                    {
                        return true;
                    }

                    if (args[1] == "delete")
                    {
                        CharacterManager.DeleteCharacter(characterId);
                    }
                }
            }



            return true;
        }
    }
}
