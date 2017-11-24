using HttpServer.websites.mathieu_morrissette.api.helpers;
using HttpServer.websites.mathieu_morrissette.classes;
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
using System.Web;

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

            if (context.Request.HttpMethod == "GET")
            {
                if (args.Length == 0)
                {
                    Post[] posts = PostManager.GetHomeFeedPosts(user);

                    string output = JsonConvert.SerializeObject(posts.ToResponse());

                    context.Send(output);

                    return true;
                }

                int postId = 0;

                if (int.TryParse(args[0], out postId))
                {
                    Post post = PostManager.GetPost(postId);

                    if (post == null)
                    {
                        return true;
                    }

                    if (args.Length == 1)
                    {
                        context.Send(JsonConvert.SerializeObject(new Post[] { post }.ToResponse()));
                    }
                    else
                    {
                        switch (args[1])
                        {
                            case "delete":
                                if (post.UserId == user.Id)
                                {
                                    PostManager.DeletePost(post);
                                }
                                break;
                        }

                        context.Redirect(context.Request.UrlReferrer.AbsoluteUri);
                    }
                }
            }

            if (context.Request.HttpMethod == "POST")
            {
                string data = string.Empty;

                using (StreamReader reader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding))
                {
                    data = reader.ReadToEnd();
                }

                Dictionary<string, string> formData = WebHelper.ParsePostData(data);

                if (formData.ContainsKey("data") && !string.IsNullOrEmpty(formData["data"]))
                {
                    PostManager.CreatePost(user, formData["data"]);
                }                

                // context.Response.StatusCode = 204; // prevent page refresh by returning no content code
                context.Redirect(context.Request.UrlReferrer.AbsoluteUri);
            }

            return true;
        }
    }
}
