using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.Tracing.DocuTerms
{

    /// <summary>
    /// mko, 9.7.2021
    /// Strenger Typisierte Form einer Memberliste für Instanzen. Zuvor war die 
    /// Basiklasse DTList im Einsatz, welche die Gefahr barg, das nicht Instanzmember
    /// wie IReturn einer Instanz als Member untergejubelt werden.
    /// </summary>
    //public class InstanceMemberList
    //    : IInstanceMemberList
    //{
    //    IComposer pnL;
    //    public InstanceMemberList(IComposer pnL, IFormater fmt)            
    //    {
    //        this.pnL = pnL;
    //        InstanceMembers = new IInstanceMember[] { };

    //    }

    //    public InstanceMemberList(IComposer pnL, IFormater fmt, IInstanceMember[] im)            
    //    {
    //        this.pnL = pnL;
    //        if (im == null)
    //            InstanceMembers = new IInstanceMember[] { };
    //        else
    //            InstanceMembers = pnL.Kill im;
    //    }

    //    // KillIfNot fehlt!
    //    public IInstanceMember[] InstanceMembers { get; }
    //        //=> Childs?.Any() ?? false
    //        //    ? Childs.Select(c => c is IInstanceMember lm 
    //        //                            ? lm 
    //        //                            : pnL.p(TTD.Composer.Errors.ComposerError.UID, 
    //        //                                    pnL.EncapsulateAsPropertyValue(c))).ToArray()
    //        //    : new IInstanceMember[] { };

    //}
}
