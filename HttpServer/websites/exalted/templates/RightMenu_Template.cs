using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.templates
{
    class RightMenu_Template : ITemplate
    {        
        public RightMenu_Template()
        {
        }

        public string Render()
        {

            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/right_menu.html");

            return content;
        }
    }
}
