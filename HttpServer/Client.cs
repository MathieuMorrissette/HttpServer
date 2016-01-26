using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Client
    {
        public Client(Guid ID)
        {
            this.ID = ID;
            this.Dictionary = new Dictionary<string, object>();
            this.DateCreated = DateTime.Now;
        }

        public Guid ID { get; private set; }
        public HttpListenerContext Context { get; set; }
        public Dictionary<string, object> Dictionary { get; private set; }
        public DateTime DateCreated { get; private set; }
    }
}
