using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpServer.websites.mathieu_morrissette.controllers
{
    public class FileProvider : IController
    {
        public bool HandleRequest(Client client, params string[] args)
        {
            string path = Path.Combine(client.Context.Request.Url.Segments);
            path = WebSite.WEBSITE_ROOT_PATH + path;
            if (File.Exists(path))
            {
                client.Context.Response.ContentType = MimeMapping.GetMimeMapping(path);

                byte[] buffer = File.ReadAllBytes(path);
                client.Context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                client.Context.Response.OutputStream.Flush();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
