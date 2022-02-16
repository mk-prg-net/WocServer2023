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
    /// mko, 18.6.2020
    /// 
    /// mko, 12.7.2021
    /// Strenger typisiert- Keine Eigenschaften mit allgemeinen Typ IDocuEntity mehr enthalten.
    /// 
    /// mko, 9.8.2021
    /// Alle Member der mko.IToken- Schnittstelle entfernt
    /// </summary>
    public class KillMethodParameterIfNot
        : DocuEntity,
        IKillMethodPrarmeterIfNot
    {
        public KillMethodParameterIfNot(bool Condition, Func<IMethodParameter> createMethodParameter)
            : base(DocuEntityTypes.KillIfNot)
        {
            this.Condition = Condition;

            if(createMethodParameter != null)
                _createMethodParameter = createMethodParameter;
        }

        public static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Composer.Errors.KillIfNotParamIsNull.UID));

        Func<IMethodParameter> _createMethodParameter = new Func<IMethodParameter>(() => _defaultValue);

        public IMethodParameter MethodParameter => _createMethodParameter();

        public bool Condition { get; }        

    }
}
