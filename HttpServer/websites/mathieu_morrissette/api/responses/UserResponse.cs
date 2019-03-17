using HttpServer.websites.mathieu_morrissette.model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.api.responses
{
    public class UserResponse
    {
        public int id { get; set; }

        public string username { get; set; }

        public static UserResponse FromUser(User user)
        {
            UserResponse response = new UserResponse();

            response.id = user.Id;
            response.username = user.Username;

            return response;
        }
    }
}
