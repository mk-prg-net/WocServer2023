using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// Allgemeine Listen aus Eigenschaften, Instanzen und Sublisten
    /// 
    /// mko, 9.7.2021
    /// Listenelemente nicht mehr als DocuEntity.Childs, sondern streng typisiert als IListMember abgelegt
    /// 
    /// mko, 6.8.2021
    /// </summary>
    public class DTList
        : DocuEntity,
        IDTList
    {
        public DTList()
            : base(DocuEntityTypes.List) { }

        public DTList(params IListMember[] listMember)
            : base(DocuEntityTypes.List)
        {
            if(listMember != null)
            {
                var fullList = listMember;

                // Auflösen der Einbettungen
                if(listMember.Any(r => r is IListMembersToEmbed))
                {
                    var newList = new List<IListMember>(listMember.Length + 10);
                    foreach(var member in listMember)
                    {
                        if(member is IListMembersToEmbed eList)
                        {
                            newList.AddRange(eList.ListMembersToEmbed);
                        }
                        else
                        {
                            newList.Add(member);
                        }
                    }
                    fullList = newList.ToArray();
                }

                // Auflösen der KillIfNot- Terme
                if (fullList.Any(r => r is IKillListElementIfNot))
                {
                    ListMembers = fullList.Where(r => (r is IKillListElementIfNot k && k.Condition) || !(r is IKillListElementIfNot))
                                            .Select(r => r is IKillListElementIfNot k ? k.ListMember : r)                                            
                                            .ToArray();
                }
                else
                {
                    ListMembers = fullList;
                }
            }

        }
        
        public IListMember[] ListMembers { get; } = new IListMember[] { };
    }
}
