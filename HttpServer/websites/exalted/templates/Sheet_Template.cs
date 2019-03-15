using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.exalted.templates
{
    class Sheet_Template : ITemplate
    {
        private User user;
        
        public Sheet_Template(User user)
        {
            this.user = user;
        }

        public string Render()
        {
            Header_Template headerTemplate = new Header_Template();
            Footer_Template footerTemplate = new Footer_Template();

            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/sheet.html");

            return headerTemplate.Render() + content + footerTemplate.Render();
        }
    }
}
