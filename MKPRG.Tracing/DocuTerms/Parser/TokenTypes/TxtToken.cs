using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 11.8.2021
    /// 
    /// mko, 28.9.2021
    /// Behandlung des Falles `params String[] words == null` implementiert!

    /// </summary>
    public class TxtToken
        : DocuTermToken,
        ITxt,
        IPropertyValueToken
    {
        public TxtToken(params StringToken[] words)
            : base(DocuEntityTypes.Text)
        {
            if (words != null)
            {
                WordTokens = words;
            }
        }

        public IString[] Words => WordTokens;

        public StringToken[] WordTokens { get; } = new StringToken[] { };

        public override int CountOfEvaluatedTokens => WordTokens.Sum(r => r.CountOfEvaluatedTokens) + 1;
    }
}
