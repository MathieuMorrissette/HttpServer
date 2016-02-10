using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer.websites.mathieu_morrissette.controllers
{
    class API : IController
    {
        public bool HandleRequest(Client client, params string[] args)
        {
            return true;
        }
    }
}
