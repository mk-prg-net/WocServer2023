using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class Return
        : DocuEntity,
        IReturn
    {
        //public Return(IFormater fmt, IDTList retValsEncapsulatedInList)
        //    : base(fmt, DocuEntityTypes.ReturnValue, retValsEncapsulatedInList)
        //{ }

        public Return(IFormater fmt, IReturnValue retVal)
            : base(fmt, DocuEntityTypes.ReturnValue, retVal)
        { }


        public IReturnValue ReturnValue => (IReturnValue)Childs.FirstOrDefault();
    }
}
