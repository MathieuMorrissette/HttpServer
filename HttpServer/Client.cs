// TODO : Separate client from the first website.

using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace HttpServer
{
    public class Client
    {
        public Client(Guid ID)
        {
            this.ID = ID;
            this.Dictionary = new Dictionary<string, object>();
            this.DateCreated = DateTime.Now;
        }

        public Guid ID { get; private set; }
        public HttpListenerContext Context { get; set; }
        public Dictionary<string, object> Dictionary { get; private set; }
        public DateTime DateCreated { get; private set; }

        public HttpMethod HttpMethod
        {
            get
            {
                if (this.Context.Request.HttpMethod == "POST")
                    return HttpMethod.POST;

                if (this.Context.Request.HttpMethod == "GET")
                    return HttpMethod.GET;

                return HttpMethod.NONE;
            }
        }

        public Dictionary<string, string> GetData()
        {
            StreamReader streamReader = new StreamReader(this.Context.Request.InputStream);
            string data = streamReader.ReadToEnd();

            Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

            string[] KeyValues = data.Split('&');

            foreach (string keyValue in KeyValues)
            {
                string[] arrayKeyValue = keyValue.Split('=');
                dataDictionary.Add(arrayKeyValue[0], arrayKeyValue[1]);
            }

            return dataDictionary;
        }

        public void Send(string data)
        {
            StreamWriter streamwriter = new StreamWriter(this.Context.Response.OutputStream);
            streamwriter.Write(data);
            streamwriter.Flush();
        }

        // This should not be in this class. Some website can use other things to verify if the user is connected.
        public bool Connected
        {
            get
            {
                return this.Dictionary.ContainsKey("UID");
            }
        }

        public void Redirect(string url)
        {
            this.Context.Response.Redirect(url);
        }
    }
}
