using HttpServer.helpers;
using HttpServer.websites.mathieu_morrissette.model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.managers
{
    public static class FriendRequestsManager
    {
        public static bool CreateFriendRequests(User requestingUser, User requestedUser, string message)
        {
            if (requestingUser == null || requestedUser == null)
            {
                return false;
            }

            if (requestingUser.HasAlreadyRequested(requestedUser) || requestingUser.IsFriendWith(requestedUser))
            {
                return false;
            }

            IDbDataParameter paramUserId = WebSite.Database.CreateParameter("@UserId", requestingUser.Id);
            IDbDataParameter paramRequestedUserId = WebSite.Database.CreateParameter("@RequestedUserId", requestedUser.Id);
            IDbDataParameter paramMessage = WebSite.Database.CreateParameter("@Message", message);

            WebSite.Database.ExecuteNonQuery("INSERT INTO friend_requests (UserId, RequestedUserId, Message) VALUES (@UserId, @RequestedUserId, @Message)", paramUserId, paramRequestedUserId, paramMessage);

            return true;
        }

        public static FriendRequest[] GetUserFriendRequests(User user)
        {
            if (user == null)
            {
                return new FriendRequest[0];
            }

            IDbDataParameter parameter = WebSite.Database.CreateParameter("@UserId", user.Id);
            DataTable table = WebSite.Database.ExecuteQuery("SELECT Id, UserId, RequestedUserId, Message FROM friend_requests WHERE UserId=@UserId OR RequestedUserId=@UserId", parameter);

            if (table == null)
            {
                return new FriendRequest[0];
            }

            if (table.Rows.Count < 1)
            {
                return new FriendRequest[0];
            }

            List<FriendRequest> friendRequests = new List<FriendRequest>();

            foreach (DataRow row in table.Rows)
            {
                friendRequests.Add(new FriendRequest((int)row[FriendRequest.ID_FIELD], (int)row[FriendRequest.USER_ID_FIELD], (int)row[FriendRequest.REQUESTED_USER_ID_FIELD], (string)row[FriendRequest.MESSAGE_FIELD]));
            }

            return friendRequests.ToArray();
        }

        public static bool HasAlreadyRequested(this User requestingUser, User requestedUser)
        {
            IDbDataParameter paramCurrentUserId = WebSite.Database.CreateParameter("@CurrentUserId", requestingUser.Id);
            IDbDataParameter paramRequestedUserId = WebSite.Database.CreateParameter("@RequestedUserId", requestedUser.Id);

            object result = WebSite.Database.ExecuteScalar("SELECT COUNT(*) FROM friend_requests WHERE UserId=@CurrentUserId AND RequestedUserId=@RequestedUserId", paramCurrentUserId, paramRequestedUserId);

            int count = 0;

            int.TryParse(result.ToString(), out count);

            return (count > 0);
        }

    }
}
