using HttpServer.helpers;
using HttpServer.websites.mathieu_morrissette.classes;
using HttpServer.websites.mathieu_morrissette.model;
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
        public static void DeletePost(Post post)
        {
            if (post == null)
            {
                return;
            }

            IDbDataParameter paramPostId = WebSite.Database.CreateParameter("@Id", post.Id);

            WebSite.Database.ExecuteNonQuery("DELETE FROM posts WHERE Id=@Id", paramPostId);

            return;
        }

        public static Post GetPost(int id)
        {
            if (id == -1)
            {
                return null;
            }

            IDbDataParameter parameter = WebSite.Database.CreateParameter("@ID", id);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT Id, UserId, Date, Data FROM posts WHERE Id=@ID", parameter);

            if (table == null)
            {
                return null;
            }

            if (table.Rows.Count < 1)
            {
                return null;
            }

            DataRow dataRow = table.Rows[0];

            return new Post((int)dataRow[Post.ID_FIELD], (int)dataRow[Post.USER_ID_FIELD], (DateTime)dataRow[Post.DATE_FIELD], (string)dataRow[Post.DATA_FIELD]);
        }

        public static void UpdatePost(Post post)
        {
            if (post == null)
            {
                return;
            }

            IDbDataParameter paramId = WebSite.Database.CreateParameter("@Id", post.Id);
            IDbDataParameter paramData = WebSite.Database.CreateParameter("@Data", post.Data);

            WebSite.Database.ExecuteNonQuery("UPDATE posts SET Data=@Data WHERE Id=@Id", paramId, paramData);

            return;
        }

        public static void CreatePost(User user, string data)
        {
            if (user == null)
            {
                return;
            }

            IDbDataParameter paramUserId = WebSite.Database.CreateParameter("@UserId", user.Id);
            IDbDataParameter paramData = WebSite.Database.CreateParameter("@Data", data);
            IDbDataParameter paramDate = WebSite.Database.CreateParameter("@Date", DateTime.Now);

            WebSite.Database.ExecuteNonQuery("INSERT INTO posts (UserId, Data, Date) VALUES (@UserId, @Data, @Date)", paramUserId, paramData, paramDate);

            return;
        }

        public static Post[] GetHomeFeedPosts(User user)
        {
            if (user == null)
            {
                return new Post[0];
            }

            IDbDataParameter parameter = WebSite.Database.CreateParameter("@UserId", user.Id);            

            DataTable table = WebSite.Database.ExecuteQuery(
                "" +
                "SELECT Id, UserId, Date, Data " +
                "FROM posts " +
                "WHERE UserId=@UserId OR " +
                "UserId IN (" +
                "               SELECT FirstUserId AS UserId " +
                "               FROM friends " +
                "               WHERE SecondUserId=@UserId " +
                "               UNION SELECT SecondUserId AS UserId " +
                "               FROM friends " +
                "               WHERE FirstUserId=@UserId" +
                "           ) " +
                "ORDER BY Date DESC", parameter);

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
                posts.Add(new Post((int)row[Post.ID_FIELD], (int)row[Post.USER_ID_FIELD], (DateTime)row[Post.DATE_FIELD], (string)row[Post.DATA_FIELD]));
            }

            return posts.ToArray();
        }

        public static Post[] GetUserPosts(User user)
        {
            if (user == null)
            {
                return new Post[0];
            }

            IDbDataParameter parameter = WebSite.Database.CreateParameter("@UserId", user.Id);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT Id, UserId, Date, Data FROM posts WHERE UserId=@UserId", parameter);

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
                posts.Add(new Post((int)row[Post.ID_FIELD], (int)row[Post.USER_ID_FIELD], (DateTime)row[Post.DATE_FIELD], (string)row[Post.DATA_FIELD]));
            }

            return posts.ToArray();
        }
    }
}
