using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    public abstract class PluralForm
        : NamingBase,
        IPluralForm
    {
        internal PluralForm(long nid)
            : base(nid)
        { }

        public virtual long PluralFormOfNameInSingluarNID => -1;
    }
}
