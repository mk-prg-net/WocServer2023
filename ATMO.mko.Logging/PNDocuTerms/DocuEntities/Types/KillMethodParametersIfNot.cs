using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class KillMethodParameterIfNot
        : IKillMethodPrarmeterIfNot
    {
        public KillMethodParameterIfNot(bool Condition, Func<IMethodParameter> createMethodParameter)
        {
            this.Condition = Condition;
            _createMethodParameter = createMethodParameter;
        }

        Func<IMethodParameter> _createMethodParameter;

        public IMethodParameter MethodParameters => _createMethodParameter();

        public bool Condition { get; }

        public IListMember DocuEntity => _createMethodParameter();

        public DocuEntityTypes EntityType => DocuEntityTypes.KillIfNot;

        public IEnumerable<IDocuEntity> Childs => throw new NotImplementedException();

        public bool IsFunctionName => throw new NotImplementedException();

        public bool IsInteger => throw new NotImplementedException();

        public bool IsBoolean => throw new NotImplementedException();

        public bool IsNummeric => throw new NotImplementedException();

        public string Value => throw new NotImplementedException();

        public int CountOfEvaluatedTokens => throw new NotImplementedException();

        public IToken Copy()
        {
            throw new NotImplementedException();
        }
    }
}
