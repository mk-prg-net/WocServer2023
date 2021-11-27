using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// </summary>
    public class DocuEntity : IDocuEntity
    {
        internal DocuEntity(
            DocuEntityTypes docEntityType)
        {     
            EntityType = docEntityType;         
        }

        public DocuEntityTypes EntityType { get; }

        public override string ToString()
        {
            return RC.fmtPN.Print(this);            
        }
    }
}
