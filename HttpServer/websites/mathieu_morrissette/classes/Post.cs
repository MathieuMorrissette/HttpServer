using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.classes
{
    public class Post
    {
        public const string ID_FIELD = "Id";
        public const string USER_ID_FIELD = "UserId";
        public const string DATE_FIELD = "Date";
        public const string DATA_FIELD = "Data";

        public Post(int id, int userId, DateTime date, string data)
        {
            this.Id = id;
            this.UserId = userId;
            this.Date = date;
            this.Data = data;
        }

        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime Date { get; set; }

        public string Data { get; set; }
    }
}
