using HttpServer.websites.mathieu_morrissette.managers;
using HttpServer.websites.mathieu_morrissette.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.responses
{
    public class FriendRequestResponse
    {
        public int id { get; set; }

        public string username { get; set; }

        public string requestedUsername { get; set; }

        public string message { get; set; }

        public static FriendRequestResponse FromFriendRequest(FriendRequest friendRequest)
        {
            FriendRequestResponse response = new FriendRequestResponse();

            response.id = friendRequest.Id;

            User requestingUser = UserManager.GetUser(friendRequest.UserId);

            response.username = requestingUser.Username;

            User requestedUser = UserManager.GetUser(friendRequest.RequestedUserId);

            response.requestedUsername = requestedUser.Username;

            response.message = friendRequest.Message;

            return response;
        }
    }
}
