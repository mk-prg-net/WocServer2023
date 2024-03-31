using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing.DocuTerms.ImmutableOps
{
    /// <summary>
    /// mko, 31.3.2024
    /// Operations on DocuTerms 
    /// </summary>
    public class ImmutableOps
    {
        IComposer pnL;

        public ImmutableOps(IComposer pnL) { 
            this.pnL = pnL; 
        }

        public (IRet ret, IInstance extendedInstance) CopyAndAdd(IInstanceWithNameAsNid inst, params IProperty[] propertiesToAdd) 
        {
            // ToDo: Check, if properties not already exists inside instance


            return pnL.i(inst.DocuTermNid.NamingId, pnL.EmbedInstanceMembers(inst.InstanceMembers), pnL.EmbedInstanceMembers(propertiesToAdd));
        }

        public (IRet ret, IInstance extendedInstance) CopyAndAdd(IInstanceWithNameAsString inst, params IProperty[] propertiesToAdd)
        {

            return pnL.i(inst.DocuTermName, pnL.EmbedInstanceMembers(inst.InstanceMembers), pnL.EmbedInstanceMembers(propertiesToAdd));
        }

    }
}
