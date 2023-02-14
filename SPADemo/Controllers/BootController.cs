using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

using System.IO;


namespace SPADemo.Controllers
{
    /// <summary>
    /// mko, 9.2.2023
    /// Ist der Einstiegspunkt in die Anwendung
    /// </summary>
    public class BootController : ApiController
    {
        /// <summary>
        /// 
        /// </summary>
        public class CreateStartPageJob
            : IHttpActionResult
        {

            string PathStartPageFile;
            HttpRequestMessage reqMsg;

            public CreateStartPageJob(string pathToStartPageFile, HttpRequestMessage reqMsg)
            {
                PathStartPageFile = pathToStartPageFile;
                this.reqMsg = reqMsg;
            }

            /// <summary>
            /// Hier wird der Job ausgeführt
            /// </summary>
            /// <param name="cancellationToken"></param>
            /// <returns></returns>
            public async Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                HttpResponseMessage response;

                if (!File.Exists(PathStartPageFile))
                {
                    response = reqMsg.CreateErrorResponse(HttpStatusCode.NotFound, $"Startpage File not Found: {PathStartPageFile}");
                }
                else
                {
                    try
                    {
                        var reader = new StreamReader(PathStartPageFile);

                        var htmlFile = await reader.ReadToEndAsync();
                        var html = new Models.HtmlContainer() { Html = htmlFile };

                        response = reqMsg.CreateResponse(HttpStatusCode.OK, html, "text/html");
                        
                    }catch(Exception ex)
                    {
                        response = reqMsg.CreateErrorResponse(HttpStatusCode.InternalServerError, ex);
                    }
                }

                return response;
            }
        }

        /// <summary>
        /// Typen von Response- Nachrichten
        /// https://www.c-sharpcorner.com/article/types-of-web-api-action-results/
        /// </summary>
        /// <returns></returns>
        [Route("start")]
        public IHttpActionResult Get()
        {
            // Die Get- Methode instanziert einen Job, der an die WebApi Pipeline delegiert wird.
            var root = System.Web.Hosting.HostingEnvironment.MapPath("\\Content\\HTML\\index.html");
            var ret = new CreateStartPageJob($"{root}", Request);
            return ret;
        }

    }
}
