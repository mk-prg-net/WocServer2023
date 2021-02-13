using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class KillInstanceMemberIfNot
        : IKillInstanceMemberIfNot        
    {
        public KillInstanceMemberIfNot(bool Condition, Func<IInstanceMember> createInstanceMembers)
        {
            this.Condition = Condition;
            _createInstanceMembers = createInstanceMembers;
        }

        Func<IInstanceMember> _createInstanceMembers;

        public IInstanceMember InstanceMembers => _createInstanceMembers();

        public bool Condition { get; }

        public IListMember DocuEntity => _createInstanceMembers();

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
