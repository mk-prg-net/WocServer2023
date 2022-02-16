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
    /// mko, 23.7.2021
    /// 
    /// mko, 9.8.2021
    /// Name gegen Nullwerte gehärtet.
    /// </summary>
    public class PropertyTokenWithNameAsString
        : PropertyToken,
        IPropertyWithNameAsString
    {
        public string DocuTermName => DocuTermNameAsToken.ValueAsString;
        public StringToken DocuTermNameAsToken { get; } = StringToken.NameIsNull;

        public PropertyTokenWithNameAsString(StringToken Name, IPropertyValueToken propValue)
            : base(propValue)
        {
            if(Name != null)
                DocuTermNameAsToken = Name;
        }

        public override int CountOfEvaluatedTokens => base.CountOfEvaluatedTokens + DocuTermNameAsToken.CountOfEvaluatedTokens;

    }
}
