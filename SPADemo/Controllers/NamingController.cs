using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;

namespace SPADemo.Controllers
{
    public class NamingController : ApiController
    {
        /// <summary>
        /// Liefert zu einer NId einen Naming- Container aus.
        /// </summary>
        public class DeliverNamingContainerJob
            : IHttpActionResult
        {
            long NID;
            HttpRequestMessage reqMsg;            

            public DeliverNamingContainerJob(long NID, HttpRequestMessage requestMessage) 
            {
                this.NID = NID;
                this.reqMsg = requestMessage;
            }

            public Task<HttpResponseMessage> ExecuteAsync(CancellationToken cancellationToken)
            {
                MKPRG.Naming.INaming nc = new MKPRG.Naming.DocuTerms.Types.UndefinedDocuTerm();
                HttpResponseMessage rsp = null;

                try
                {
                    System.Web.HttpContext.Current.Application.Lock();
                    var ncdict= (System.Collections.Concurrent.ConcurrentDictionary<long, MKPRG.Naming.INaming>)System.Web.HttpContext.Current.Application[WebApiApplication.ncDictKey];

                    nc = ncdict[NID];
                    rsp = reqMsg.CreateResponse(HttpStatusCode.OK, nc, "json/nc");
                } catch(Exception ex)
                {
                    rsp = reqMsg.CreateResponse(HttpStatusCode.InternalServerError, ex);
                }
                finally
                {
                    System.Web.HttpContext.Current.Application.UnLock();
                }

                return Task.FromResult(rsp);
            }
        }

        [HttpGet]
        [Route("ncs/nc/{NID}")]
        public IHttpActionResult GetNamingContainer(long NID)
        {
            var job = new DeliverNamingContainerJob(NID, Request);

            return job;
        }

    }
}
