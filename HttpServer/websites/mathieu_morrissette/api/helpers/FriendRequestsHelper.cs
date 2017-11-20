using HttpServer.websites.mathieu_morrissette.api.responses;
using HttpServer.websites.mathieu_morrissette.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.helpers
{
    public static class FriendRequestsHelper
    {
        public static FriendRequestResponse[] ToResponse(this FriendRequest[] friendRequests)
        {
            List<FriendRequestResponse> responses = new List<FriendRequestResponse>();

            foreach (FriendRequest friendRequest in friendRequests)
            {
                responses.Add(FriendRequestResponse.FromFriendRequest(friendRequest));
            }

            return responses.ToArray();
        }
    }
}
