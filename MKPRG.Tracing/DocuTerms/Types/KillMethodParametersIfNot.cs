using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// </summary>
    /// 
    public class KillMethodParameterIfNot
    : DocuEntity,
    IKillMethodPrarmeterIfNot
    {
        public KillMethodParameterIfNot(bool Condition, Func<IMethodParameter> createMethodParameter)
            : base(DocuEntityTypes.KillIfNot)
        {
            this.Condition = Condition;

            if (createMethodParameter != null)
                _createMethodParameter = createMethodParameter;
        }

        public static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Composer.Errors.KillIfNotParamIsNull.UID));

        Func<IMethodParameter> _createMethodParameter = new Func<IMethodParameter>(() => _defaultValue);

        public IMethodParameter MethodParameter => _createMethodParameter();

        public bool Condition { get; }

    }

}
