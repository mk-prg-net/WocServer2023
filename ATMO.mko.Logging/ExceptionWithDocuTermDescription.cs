using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 26.6.2020
    /// </summary>
    public class ExceptionWithDocuTermDescription
        : Exception,
        IExceptionWithDocuTermDescription
    {
        public ExceptionWithDocuTermDescription(IDocuEntity messageAsDocuTerm)
        {
            this.MessageAsDocuTerm = messageAsDocuTerm;
        }

        public ExceptionWithDocuTermDescription(IDocuEntity messageAsDocuTerm, Exception innerException)
            : base("", innerException)
        {
            this.MessageAsDocuTerm = messageAsDocuTerm;
        }


        public IDocuEntity MessageAsDocuTerm { get; }
    }
}
