using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.key.templates
{
    class Home_Template : ITemplate
    {
        public Home_Template()
        {
        }

        public string Render()
        {

            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/sender.html");

            return content;
        }
    }
}
