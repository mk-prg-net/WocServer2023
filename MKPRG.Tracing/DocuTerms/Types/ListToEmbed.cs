using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.2.2019
    /// List mit Docu-Termen, die zB. als zusätzliche Parameter in einer Methode oder Member einer instanz einzubetten sind.
    /// 
    /// mko, 16.6.2020
    /// Markiert für ausschließlichen Einsatz in Listen, Instanzmember- Listen und Methodenparameter- Listen
    /// 
    /// mko, 4.12.2020
    /// Behandlung von null- Werten im Fall, ToEmbed ist null
    /// </summary>    
    public class ListMembersToEmbedClass 
        : DocuEntity,
        IListMembersToEmbed
    {

        public ListMembersToEmbedClass(IEnumerable<IListMember> ToEmbed)
            : base(DocuEntityTypes.KillIfNot)
    {
        // mko, 4.12.2020
        // Behandlung von null- Wert
        if (ToEmbed != null)
            ListMembersToEmbed = ToEmbed;
    }

    public IEnumerable<IListMember> ListMembersToEmbed { get; } = new IListMember[] { };
}

}
