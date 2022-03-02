using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 9.7.2021
    /// 
    /// Bittere Erkenntnis:
    /// Aktuell werden die Memberlisten von Methoden, Instanzen und allgemeine Listen von 
    /// von IDTList- Objekten gebildet. Damit können Listen, die z.B. reine Methoden- Member
    /// wie IReturn enthalten, Instanzen "untergejubelt" werden. Dies führt dann zu Laufzeitfehler
    /// beim Abruf wie **InvalidCastException**.
    /// </summary>
    public interface IInstanceMemberList
    {
        IInstanceMember[] InstanceMembers { get; }
    }
}
