using HttpServer.websites.mathieu_morrissette.api.responses;
using HttpServer.websites.mathieu_morrissette.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.helpers
{
    public static class PostsHelper
    {
        public static PostResponse[] ToResponse(this Post[] posts)
        {
            List<PostResponse> responses = new List<PostResponse>();

            foreach (Post post in posts)
            {
                responses.Add(PostResponse.FromPost(post));
            }

            return responses.ToArray();
        }
    }
}
