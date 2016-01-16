using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace HttpServer
{
    public class FileProvider : IController
    {
        public bool HandleRequest(int index, HttpListenerContext context)
        {
            string path = Path.Combine(context.Request.Url.Segments.SubArray(1));

            if (File.Exists(path))
            {
                context.Response.ContentType = MimeMapping.GetMimeMapping(path);

                byte[] buffer = File.ReadAllBytes(path);
                context.Response.OutputStream.Write(buffer, 0, buffer.Length);
                context.Response.OutputStream.Flush();

                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
