using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.templates
{
    public class Friends_Template : ITemplate
    {
        private User user;

        public Friends_Template(User user)
        {
            this.user = user;
        }

        public string Render()
        {
            Header_Template headerTemplate = new Header_Template();
            Footer_Template footerTemplate = new Footer_Template();

            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/friends.html");
            content = content.Replace("__Username__", this.user.Username);
            return headerTemplate.Render() + content + footerTemplate.Render();
        }
    }
}
