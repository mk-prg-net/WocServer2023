using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 10.3.2024
    /// </summary>
    public interface IRetBldFactory
    {
        /// <summary>
        /// Erzeugr eine Klassenfabrik für IRet Objekte, die den Zustand eines Funktionsaufrufes beschreiben.
        /// </summary>
        /// <param name="fcallParamDescriptors"></param>
        /// <returns></returns>
        IRetBld CreateRetBld(params DocuTerms.IMethodParameter[] fcallParamDescriptors);
    }
}
