using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;

namespace DojoTypeDescriptor.Controllers.Api
{
    [RoutePrefix("api/dojopage")]
    public class ApiDojoPageController : ApiController
    {
        private static Dictionary<string, string> cache = new Dictionary<string, string>();


        [Route("")]
        public async Task<String> Get(string url)
        {
            if (!cache.ContainsKey(url))
            {
                
                String path = Path.GetTempPath() + "DojoCache\\";
                Directory.CreateDirectory(path);
                path += url.Replace("/", "_").Replace(".", "_");
                Stream stream;
                bool fileExists = File.Exists(path);
                if (fileExists)
                {
                    stream = File.OpenRead(path);
                }
                else
                {
                    var request = (HttpWebRequest)WebRequest.Create("http://dojotoolkit.org/api/" + url);
                    request.Headers.Add("X-Requested-With", "XMLHttpRequest");
                    var response = await request.GetResponseAsync();

                    stream = response.GetResponseStream();
                }

                var sw = new StreamReader(stream);
                cache[url] = await sw.ReadToEndAsync();

                if (!fileExists) {
                    File.WriteAllText(path, cache[url]);
                }
                
            }

            return cache[url];
        }

    }
}