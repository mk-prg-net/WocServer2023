using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{

    /// <summary>
    /// mko, 7.2.2018
    /// 
    /// mko, 29.6.2021
    /// Behandlung von childs == null in Konstrutor 1 verbessert
    /// </summary>
    public class DocuEntity : IDocuEntity
    {
        /// <summary>
        /// mko, 8.6.2020
        /// Zur Implementierung der Literale Boolean etc.
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="docEntityType"></param>
        /// <param name="childs"></param>
        internal DocuEntity(
            //IFormater fmt,
            DocuEntityTypes docEntityType)
        {
            //this.fmt = fmt;
            EntityType = docEntityType;
            //Childs = new IDocuEntity[] { };
        }

        public DocuEntityTypes EntityType { get; }

        public override string ToString()
        {
            return RC.fmtPN.Print(this);
            //return "";
        }

    }
}
