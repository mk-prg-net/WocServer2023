using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing.DocuTerms.MemberAccessHlp
{
    /// <summary>
    /// mko, 13.6.2024
    /// </summary>
    public partial class MemberAccessHlp
    {
        IComposer pnL;
        IRetBldFactory retBldFactory;

        public MemberAccessHlp(IComposer pnL, IRetBldFactory retBldFactory)
        {
            this.pnL = pnL;
            this.retBldFactory = retBldFactory;
        }
    }
}
