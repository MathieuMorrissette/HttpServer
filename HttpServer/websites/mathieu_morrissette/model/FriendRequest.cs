using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.model
{
    public class FriendRequest
    {
        public const string ID_FIELD = "Id";
        public const string USER_ID_FIELD = "UserId";
        public const string REQUESTED_USER_ID_FIELD = "RequestedUserId";
        public const string MESSAGE_FIELD = "Message";

        public FriendRequest(int id, int userId, int requestedUserId, string message)
        {
            this.Id = id;
            this.UserId = userId;
            this.RequestedUserId = requestedUserId;
            this.Message = message;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public int RequestedUserId { get; set; }

        public string Message { get; set; }
    }
}
