using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 21.11.2021
    /// </summary>
    public class Instance
        : DocuEntity,
        IInstance
    {
        public Instance()
            : base(DocuEntityTypes.Instance)
        {
        }

        public Instance(IInstanceMember[] Members)
            : base(DocuEntityTypes.Instance)
        {
            if (Members != null)
            {
                var fullList = Members;

                // Auflösen der Einbettungen
                if (Members.Any(r => r is IInstanceMembersToEmbed))
                {
                    var newList = new List<IInstanceMember>(Members.Length + 10);
                    foreach (var member in Members)
                    {
                        if (member is IInstanceMembersToEmbed eList)
                        {
                            newList.AddRange(eList.InstanceMembersToEmbed);
                        }
                        else
                        {
                            newList.Add(member);
                        }
                    }

                    fullList = newList.ToArray();
                }

                // Auflösen der KillIfNot Terme
                if (fullList.Any(r => r is IKillInstanceMemberIfNot))
                {
                    InstanceMembers = fullList.Where(r => (r is IKillInstanceMemberIfNot k && k.Condition) || !(r is IKillInstanceMemberIfNot))
                                              .Select(r => r is IKillInstanceMemberIfNot k ? k.InstanceMember : r)
                                              .ToArray();
                }
                else
                {
                    InstanceMembers = fullList;
                }
            }

        }

        /// <summary>
        /// Instanz- Member. 
        /// 
        /// mko, 12.7.2021
        /// Default ist anstatt null die leere Liste!
        /// </summary>
        public IInstanceMember[] InstanceMembers { get; } = new IInstanceMember[] { };

        public static Instance NullValue = new InstanceWithNameAsNID(new NID(TT.Sets.NullValue.UID));

    }
}
