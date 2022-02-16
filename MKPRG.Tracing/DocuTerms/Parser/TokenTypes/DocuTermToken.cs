using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

namespace MKPRG.Tracing.DocuTerms.Parser
{

    /// <summary>
    /// mko, 22.7.2021
    /// 
    /// mko, 29.9.2021
    /// Anstatt `IDocuEntity` und `IToken` jetzt verpflichtet `IDocuEntityToken`
    /// zu implementieren.
    /// </summary>
    public abstract class DocuTermToken 
        : IDocuEntityToken
    {
        public DocuTermToken(
            DocuEntityTypes docEntityType)
        {
            EntityType = docEntityType;
        }

        public DocuEntityTypes EntityType { get; }

        public bool IsFunctionName => true;

        public bool IsInteger => false;

        public bool IsBoolean => false;

        public bool IsNummeric => false;

        public virtual string Value => EntityType.ToString();

        public abstract int CountOfEvaluatedTokens { get; }

        public IToken Copy()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            return RCV3.fmtPN.Print(this);
        }

    }
}
