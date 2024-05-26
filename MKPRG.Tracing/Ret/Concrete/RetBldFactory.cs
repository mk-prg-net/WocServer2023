using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 10.3.2024
    /// 
    /// mko, 26.5.2024
    /// </summary>
    public class RetBldFactory
        : IRetBldFactory
    {
        public RetBldFactory(DocuTerms.IComposer pnL) 
        { 
            this.pnL = pnL;
        }

        DocuTerms.IComposer pnL;

        public IRetBld CreateRetBld(params DocuTerms.IMethodParameter[] fcallParamDescriptors)
        {
            var stackFrame = new System.Diagnostics.StackTrace().GetFrame(1);        
            var mth = stackFrame.GetMethod();
            var methodName = mth.Name;
            var cls = mth.ReflectedType.Module.Name;
            var netAssemblyName = mth.ReflectedType.Assembly.GetName().Name;

            return new RetBld(pnL,netAssemblyName, cls, methodName, fcallParamDescriptors);
        }
    }
}
