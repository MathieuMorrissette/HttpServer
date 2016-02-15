// TODO : Separate client from the first website.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

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
        public Dictionary<string, object> Dictionary { get; private set; }
        public DateTime DateCreated { get; private set; }

        // This should not be in this class. Some website can use other things to verify if the user is connected.
        public bool Connected
        {
            get
            {
                return this.Dictionary.ContainsKey("UID");
            }
        }
    }
}
