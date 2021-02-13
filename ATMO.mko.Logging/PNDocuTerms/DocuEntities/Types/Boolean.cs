using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 8.6.2020
    /// </summary>
    public class Boolean 
        : DocuEntity,
        IPropertyValue
        //IEventParameter
        //IReturnValue
    {       

        public Boolean(bool val, IFormater fmt) 
            : base(fmt, DocuEntityTypes.Bool)
        {
            ValueAsBool = val;
        }

        public override int CountOfEvaluatedTokens => 1;

        /// <summary>
        /// Der extakte boolsche Wert 
        /// </summary>
        public bool ValueAsBool { get; }        

    }
}
