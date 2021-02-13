using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    public class KillEventParamIfNot
        : IKillEventParamIfNot
    {
        public KillEventParamIfNot(bool Condition, Func<IEventParameter> createEventParameter)
        {
            this.Condition = Condition;
            _createEventParameter = createEventParameter;
        }

        Func<IEventParameter> _createEventParameter { get; }

        public IEventParameter EventParameter => _createEventParameter();

        public bool Condition { get; }

        public IListMember DocuEntity => _createEventParameter();

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
