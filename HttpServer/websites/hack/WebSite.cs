using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.hack
{
    public class WebSite : BaseWebsite
    {
        public WebSite(Client client, HttpListenerContext context) : base(client, context)
        {
        }

        public override void HandleRequest()
        {
            this.Context.Send("hack");
            this.Context.Response.OutputStream.Close();
        }
    }
}
