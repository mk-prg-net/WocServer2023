using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 8.6.2020
    /// 
    /// </summary>
    public class Double
      : DocuEntity,
        IPropertyValue
        //IEventParameter,
        //IReturnValue
    {
        public Double(double val, IFormater fmt)
            : base(fmt, DocuEntityTypes.Float)
        {
            Value = val;
        }

        public override int CountOfEvaluatedTokens => 1;

        /// <summary>
        /// Der extakte boolsche Wert 
        /// </summary>
        public double Value { get; }
    }

}
