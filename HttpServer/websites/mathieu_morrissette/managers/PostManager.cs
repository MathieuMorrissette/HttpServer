using HttpServer.helpers;
using HttpServer.websites.mathieu_morrissette.classes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.managers
{
    public static class PostManager
    {
        public static Post[] GetUserPosts(User user)
        {
            if (user == null)
            {
                return new Post[0];
            }

            IDbDataParameter parameter = WebSite.Database.CreateParameter("@UserId", user.Id);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT Id, Date, Data FROM posts WHERE UserId=@UserId", parameter);

            if (table == null)
            {
                return new Post[0];
            }

            if (table.Rows.Count < 1)
            {
                return new Post[0];
            }

            List<Post> posts = new List<Post>();

            foreach (DataRow row in table.Rows)
            {
                posts.Add(new Post((int)row[Post.ID_FIELD], user.Id, (DateTime)row[Post.DATE_FIELD], (string)row[Post.DATA_FIELD]));
            }

            return posts.ToArray();
        }
    }
}
