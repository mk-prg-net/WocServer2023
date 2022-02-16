using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 27.7.2021
    /// 
    /// mko, 6.8.2021
    /// Null- Wert resitent gemacht
    /// </summary>
    public class ReturnToken
        : DocuTermToken,
        IReturn,
        IMethodParameterToken,
        IListMemberToken
    {

        public ReturnToken()
            : base(DocuEntityTypes.ReturnValue)
        { }

        public ReturnToken(IReturnValueToken ret)
            : base(DocuEntityTypes.ReturnValue)
        {
            if (ret != null)
                ReturnValueToken = ret;
        }

        public IReturnValue ReturnValue => ReturnValueToken;

        public IReturnValueToken ReturnValueToken { get; } = _defaultValue;

        public override int CountOfEvaluatedTokens => ReturnValueToken.CountOfEvaluatedTokens + 1;

        public IReturnValue DocuTermDefaultValue => _defaultValue;

        public bool IsSetToDefaultValue => ReturnValue is IDocuEntityWithNameAsNid dt && dt.DocuTermNid.NamingId == _defaultValue.DocuTermNid.NamingId ;

        protected static InstanceTokenWithNameAsNid _defaultValue = new InstanceTokenWithNameAsNid(new NIDToken(TTD.Types.UndefinedReturnValue.UID));
    }
}
