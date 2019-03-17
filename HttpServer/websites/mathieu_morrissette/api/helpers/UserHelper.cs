using HttpServer.websites.mathieu_morrissette.api.responses;
using HttpServer.websites.mathieu_morrissette.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.helpers
{
    public static class UserHelper
    {
        public static UserResponse[] ToResponse(this User[] users)
        {
            List<UserResponse> responses = new List<UserResponse>();

            foreach (User user in users)
            {
                responses.Add(UserResponse.FromUser(user));
            }

            return responses.ToArray();
        }
    }
}
