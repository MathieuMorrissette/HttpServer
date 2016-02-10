using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites
{
    public class BaseWebsite
    {
        public Client Client { get; private set; }

        public BaseWebsite(Client client)
        {
            this.Client = client;
        }

        public virtual void HandleRequest()
        {
        }

    }
}
