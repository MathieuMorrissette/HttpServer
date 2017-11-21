using HttpServer.helpers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.managers
{
    public static class FriendManager
    {
        public static bool IsFriendWith(this User currentUser, User user)
        {
            IDbDataParameter paramCurrentUserId = WebSite.Database.CreateParameter("@CurrentUserId", currentUser.Id);
            IDbDataParameter paramUserId = WebSite.Database.CreateParameter("@UserId", user.Id);

            object result = WebSite.Database.ExecuteScalar("SELECT COUNT(*) FROM friends WHERE (FirstUserId=@CurrentUserId AND SecondUserId=@UserId) OR (SecondUserId=@CurrentUserId AND FirstUserId=@UserId)", paramCurrentUserId, paramUserId);

            int count = 0;

            int.TryParse(result.ToString(), out count);

            return (count > 0);
        }

        public static void RemoveFriend(User user, User friend)
        {
            if (user == null || friend == null)
            {
                return;
            }

            if (user.IsFriendWith(friend))
            {
                return;
            }

            IDbDataParameter paramUserId = WebSite.Database.CreateParameter("@UserId", user.Id);
            IDbDataParameter paramFriendId = WebSite.Database.CreateParameter("@FriendId", friend.Id);

            WebSite.Database.ExecuteNonQuery("DELETE FROM friends WHERE (FirstUserId=@UserId AND SecondUserId=@FriendId) OR (FirstUserId=@FriendId AND SecondUserId=@UserId)", paramUserId, paramFriendId);
        }

        public static void AddFriend(User user, User newFriend)
        {
            if (user == null || newFriend == null)
            {
                return;
            }

            if (user.IsFriendWith(newFriend))
            {
                return;
            }

            IDbDataParameter paramUserId = WebSite.Database.CreateParameter("@UserId", user.Id);
            IDbDataParameter paramFriendId = WebSite.Database.CreateParameter("@FriendId", newFriend.Id);

            WebSite.Database.ExecuteNonQuery("INSERT INTO friends (FirstUserId, SecondUserId) VALUES (@UserId, @FriendId)", paramUserId, paramFriendId);
        }
    }
}
