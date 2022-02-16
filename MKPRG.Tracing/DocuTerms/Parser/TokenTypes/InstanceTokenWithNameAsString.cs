using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;


namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 23.7.2021
    /// 
    /// mko, 9.8.2021
    /// Name gegen Nullwerte gehärtet
    /// </summary>
    public class InstanceTokenWithNameAsString
        : InstanceToken,
        IInstanceWithNameAsString
    {

        public string DocuTermName => DocuTermNameAsToken.ValueAsString;
        public StringToken DocuTermNameAsToken { get; } = StringToken.NameIsNull;

        public InstanceTokenWithNameAsString(StringToken Name)
        {
            if(Name != null)
                DocuTermNameAsToken = Name;
        }

        public InstanceTokenWithNameAsString(StringToken Name, IInstanceMemberToken[] ListEncapsulatedMembers)
            : base(ListEncapsulatedMembers)
        {
            if(Name != null)
                DocuTermNameAsToken = Name;
        }

        public override int CountOfEvaluatedTokens => base.CountOfEvaluatedTokens + DocuTermNameAsToken.CountOfEvaluatedTokens;
        
    }
}
