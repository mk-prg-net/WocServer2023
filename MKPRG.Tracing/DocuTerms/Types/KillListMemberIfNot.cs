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
    /// mko, 12.7.2021
    /// 
    /// mko, 9.8.2021
    /// Alle Member der mko.Itoken- Schnittstelle entfernt
    /// </summary>
    public class KillListMemberIfNot
        : DocuEntity,
        IKillListElementIfNot
    {
        public KillListMemberIfNot(bool Condition, Func<IListMember> createListMember)
            : base(DocuEntityTypes.KillIfNot)
        {
            this.Condition = Condition;

            if(createListMember != null)
                _createListMember = createListMember;
        }

        public static InstanceWithNameAsNID _defaultValue = new InstanceWithNameAsNID(new NID(TTD.Composer.Errors.KillIfNotParamIsNull.UID));

        Func<IListMember> _createListMember = new Func<IListMember>(() => _defaultValue);

        public IListMember ListMember => _createListMember();

        public bool Condition { get; }

    }
}
