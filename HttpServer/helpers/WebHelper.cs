using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    public static class WebHelper
    {
        public static string[] GetUrlArguments(string[] arguments)
        {
            return arguments.Select(arg => arg.Replace("/", "")).Where(arg => !string.IsNullOrEmpty(arg)).ToArray();
        }

    }
}
