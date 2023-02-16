using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SPADemo.Tools
{
    /// <summary>
    /// mko, 17.2.2023
    /// </summary>
    public class UrlTools
    {
        /// <summary>
        /// Rekturns the origin of an Uri
        /// </summary>
        /// <param name="reqUri"></param>
        /// <returns></returns>
        public string ParseOrigin(Uri reqUri)
        {
            var origin = $"{reqUri.Scheme}://{reqUri.Authority}";

            return origin;
        }
    }
}