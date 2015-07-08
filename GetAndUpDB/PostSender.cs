using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GetAndUpDB
{
    /**
        数据发送类
        工具类

        */
    class PostSender
    {
        public string server;
        public PostSender(string _server)
        {
            server = _server;
        }
        public void sendPost(Dictionary<string,string> data)
        {
            NameValueCollection nc = new NameValueCollection();
            foreach(var par in data)
            {
                nc[par.Key] = par.Value;
            }
            post(server, nc);
        }
        static string post(string URL, NameValueCollection values)
        {
            Console.WriteLine("[{0}]send post to({1}):{2}items", DateTime.Now.ToString(),URL,values.Count);
            using (var client = new WebClient())
            {
                var response = client.UploadValues(URL, values);
                var responseString = Encoding.Default.GetString(response);
                return responseString;
            }

        }
    }
}
