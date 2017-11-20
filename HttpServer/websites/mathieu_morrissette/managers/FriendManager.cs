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

            object result = WebSite.Database.ExecuteScalar("SELECT COUNT(*) FROM friends WHERE UserId=@CurrentUserId AND FriendId=@UserId", paramCurrentUserId, paramUserId);

            int count = 0;

            int.TryParse(result.ToString(), out count);

            return (count > 0);
        }
    }
}
