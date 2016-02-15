﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HttpServer
{
    interface IController
    {
        bool HandleRequest(Client client, HttpListenerContext context, params string[] args);
    }
}
