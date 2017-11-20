using HttpServer.websites.mathieu_morrissette.api.helpers;
using HttpServer.websites.mathieu_morrissette.managers;
using HttpServer.websites.mathieu_morrissette.model;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.handlers
{
    public class FriendRequestsHandler : IController
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
               FriendRequest[] requests = FriendRequestsManager.GetUserFriendRequests(user);

                string output = JsonConvert.SerializeObject(requests.ToResponse());

                context.Send(output);
            }

            if (context.Request.HttpMethod == "POST")
            {
                string data = string.Empty;

                using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    data = reader.ReadToEnd();
                }

                Dictionary<string, string> formData = WebHelper.ParsePostData(data);

                if (formData.ContainsKey("username") && !string.IsNullOrEmpty(formData["username"]))
                {
                   User requestedUser = UserManager.GetUser(formData["username"]);

                    if (requestedUser != null)
                    {
                        string message = null;

                        if (formData.ContainsKey("message"))
                        {
                            message = formData["message"];
                        }

                        FriendRequestsManager.CreateFriendRequests(user, requestedUser, message);
                    }
                }

                context.Redirect(context.Request.UrlReferrer.AbsoluteUri);
            }

            return true;
        }
    }
}
