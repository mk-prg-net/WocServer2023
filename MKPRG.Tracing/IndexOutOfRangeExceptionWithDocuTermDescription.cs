using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MKPRG.Tracing.DocuTerms;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 26.6.2020
    /// Direkt von der IndexOutOfRangeException kann nicht abgeleitet werden, da diese versiegelt ist.
    /// </summary>
    public class IndexOutOfRangeExceptionWithDocuTermDescription
        : Exception,
        IExceptionWithDocuTermDescription
    {
        public IndexOutOfRangeExceptionWithDocuTermDescription(IDocuEntity messageAsDocuTerm)
        {
            this.MessageAsDocuTerm = messageAsDocuTerm;
        }

        public IndexOutOfRangeExceptionWithDocuTermDescription(IDocuEntity messageAsDocuTerm, Exception innerException)
            :base("", innerException)
        {
            this.MessageAsDocuTerm = messageAsDocuTerm;            
        }

        public override string Message => RC.fmtPN.Print(MessageAsDocuTerm);

        public IDocuEntity MessageAsDocuTerm { get; }
    }
}
