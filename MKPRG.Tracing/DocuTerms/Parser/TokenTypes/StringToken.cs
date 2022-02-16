using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 23.7.2021
    /// 
    /// mko, 30.7.2021
    /// Mit INoSubTrees markiert
    /// </summary>
    public class StringToken
        : DocuTermToken,
        INoSubTrees,
        IString,
        IPropertyValueToken
    {

        public StringToken(string str)
            : base(DocuEntityTypes.String)
        {
            if(str != null)
                Value = str;
        }

        public IDocuEntity DocuTermDefaultValue => new StringToken("");

        public string ValueAsString => Value;

        public override string Value { get; } = "";

        public override int CountOfEvaluatedTokens => 1;

        public static StringToken NameIsNull = new StringToken(RCV3.NC[TTD.Composer.Errors.NameIsNull.UID].CNT);
    }
}
