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
        public Sheet_Template()
        {
        }

        public string Render()
        {
            Header_Template headerTemplate = new Header_Template();
            Footer_Template footerTemplate = new Footer_Template();
            RightMenu_Template rightMenuTemplate = new RightMenu_Template();

            string content = File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/main_layout.html");

            content = content.Replace("__RIGHT_MENU__", rightMenuTemplate.Render());
            content = content.Replace("__HEADER__", headerTemplate.Render());
            content = content.Replace("__FOOTER__", footerTemplate.Render());


            content = content.Replace("__CONTENT__", File.ReadAllText(WebSite.WEBSITE_ROOT_PATH + "public/html/sheet.html"));

            return content;
        }
    }
}
