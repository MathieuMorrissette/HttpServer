using HttpServer.websites.mathieu_morrissette.api.helpers;
using HttpServer.websites.mathieu_morrissette.managers;
using HttpServer.websites.mathieu_morrissette.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.responses
{
    public class PostResponse
    {
        public int id { get; set; }

        public string data { get; set; }

        public string username { get; set; }

        public string date { get; set; }

        public static PostResponse FromPost(Post post)
        {
            PostResponse response = new PostResponse();

            response.id = post.Id;
            response.data = CensorshipHelper.Censor(post.Data);
            response.date = post.Date.ToString();
            response.username = UserManager.GetUser(post.UserId).Username;

            return response;
        }
    }
}
