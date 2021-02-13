using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
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
    public class ListToEmbed 
        : IListToEmbed
    {

        public ListToEmbed(IEnumerable<IListMember> ToEmbed)
        {
            // mko, 4.12.2020
            // Behandlung von null- Wert
            Childs =  ToEmbed != null ? ToEmbed : new IListMember[] { };
        }

        public DocuEntityTypes EntityType => DocuEntityTypes.ListToEmbed;

        public IEnumerable<IListMember> ToEmbed => Childs.Select(r => (IListMember)r);

        public IEnumerable<IDocuEntity> Childs { get; }

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
