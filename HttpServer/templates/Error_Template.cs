using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public class Error_Template : ITemplate
    {
        Error error;

        public Error_Template(Error error)
        {
            this.error = error;
        }

        public string Render()
        {
            if (error == null)
            {
                return string.Empty;
            }

            string data = File.ReadAllText(Server.SERVER_ROOT_PATH + "html/error_alert.html");
            data = data.Replace("__ErrorMessage__", error.Message);
            return data;
        }
    }
}
