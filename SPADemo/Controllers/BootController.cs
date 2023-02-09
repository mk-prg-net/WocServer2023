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
    /// Ist der Einstiegspunkt in die Anwendung
    /// </summary>
    public class BootController : ApiController
    {

        public class ReturnStartPage
            : IHttpActionResult
        {

            string PathStartPageFile;
            HttpRequestMessage reqMsg;

            public ReturnStartPage(string pathToStartPageFile, HttpRequestMessage reqMsg)
            {
                PathStartPageFile = pathToStartPageFile;
                this.reqMsg = reqMsg;
            }


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
            var root = System.Web.Hosting.HostingEnvironment.MapPath("\\Content\\HTML\\index.html");
            var ret = new ReturnStartPage($"{root}", Request);
            return ret;
        }

    }
}
