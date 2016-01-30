using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web;

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

        /*public Dictionary<string, string> RequestData
        {
            get
            {
                NameValueCollection collection = HttpUtility.ParseQueryString(data);

                Dictionary<string, string> dictData = new Dictionary<string, string>(collection.Count);
                foreach (string key in collection.AllKeys)
                {
                    dictData.Add(key, collection.Get(key));
                }

                return dictData;
            }
        }*/

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
    }
}
