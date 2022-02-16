using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 8.6.2020
    /// 
    /// mko, 6.8.2021
    /// </summary>
    public class Boolean 
        : DocuEntity,
        IBoolean
        //IEventParameter
        //IReturnValue
    {       

        public Boolean(bool decision) 
            : base(DocuEntityTypes.Bool)
        {
            ValueAsBool = decision;
        }

        /// <summary>
        /// Der extakte boolsche Wert 
        /// </summary>
        public bool ValueAsBool { get; } = false;

    }
}
