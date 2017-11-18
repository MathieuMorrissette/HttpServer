using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.templates
{
    class Home_Template : ITemplate
    {
        private User user;

        public Home_Template(User user)
        {
            this.user = user;
        }

        public string Render()
        {
            Header_Template headerTemplate = new Header_Template();
            Footer_Template footerTemplate = new Footer_Template();

            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/home.html");
            content = content.Replace("__Username__", this.user.Username);
            content = content.Replace("__ID__", this.user.Id.ToString());
            content = content.Replace("__PasswordHash__", this.user.Hash);

            return headerTemplate.Render() + content + footerTemplate.Render();
        }
    }
}
