using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MKPRG.Tracing
{
    public class RetBldFactory
        : IRetBldFactory
    {
        public RetBldFactory(DocuTerms.IComposer pnL) 
        { 
            this.pnL = pnL;
        }

        DocuTerms.IComposer pnL;

        public IRetBld CreateRetBld(params string[] ParameterValues)
        {
            var stackFrame = new System.Diagnostics.StackTrace().GetFrame(1);        
            var mth = stackFrame.GetMethod();

            var methodName = mth.Name;
            var prameters = mth.GetParameters().Select(p => pnL.p(p.Name, p.)
        }
    }
}
