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
    /// </summary>
    public class KillInstanceMemberIfNot
        : DocuEntity,
        IKillInstanceMemberIfNot
    {
        public KillInstanceMemberIfNot(bool Condition, Func<IInstanceMember> createInstanceMembers)
            : base(DocuEntityTypes.KillIfNot)
        {
            this.Condition = Condition;

            if (createInstanceMembers != null)
                _createInstanceMembers = createInstanceMembers;
        }

        public static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Composer.Errors.KillIfNotParamIsNull.UID));

        Func<IInstanceMember> _createInstanceMembers = new Func<IInstanceMember>(() => _defaultValue);

        public IInstanceMember InstanceMember => _createInstanceMembers();

        public bool Condition { get; }

    }
}
