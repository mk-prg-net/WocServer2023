using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// 
    /// mko, 9.8.2021
    /// </summary>
    public class Return
        : DocuEntity,
        IReturn
    {

        public Return()
            : base(DocuEntityTypes.ReturnValue)
        { }

        public Return(IReturnValue retVal)
            : base(DocuEntityTypes.ReturnValue)
        {
            if (retVal != null)
                ReturnValue = retVal;
        }


        protected static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Types.UndefinedReturnValue.UID));

        public IReturnValue ReturnValue { get; } = _defaultValue;

        public IReturnValue DocuTermDefaultValue => _defaultValue;

        public bool IsSetToDefaultValue => ReturnValue is IDocuEntityWithNameAsNid nid && nid.DocuTermNid.NamingId == TTD.Types.UndefinedReturnValue.UID;
    }
}
