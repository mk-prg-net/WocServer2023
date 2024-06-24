using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.MemberAccessHlp
{
    partial class MemberAccessHlp
    {

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="strPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out string strPropVal, out IRet ret)
            => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out strPropVal, out ret);


        /// <summary>
        /// mko, 24.06.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="intPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out long intPropVal, out IRet ret)
            => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out intPropVal, out ret);

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="dblPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out double dblPropVal, out IRet ret)
            => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out dblPropVal, out ret);

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="intPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out bool boolPropVal, out IRet ret)
            => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out boolPropVal, out ret);

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="nidPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out INID nidPropVal, out IRet ret)
                => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out nidPropVal, out ret);


        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="verPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out IVer verPropVal, out IRet ret)
                => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out verPropVal, out ret);

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="listPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out IDTList listPropVal, out IRet ret)
                => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out listPropVal, out ret);

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="instancePropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out IInstance instancePropVal, out IRet ret)
                => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out instancePropVal, out ret);

        /// <summary>
        /// mko, 24.6.2024
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="propNameAsNid"></param>
        /// <param name="methodPropVal"></param>
        /// <param name="ret"></param>
        /// <returns></returns>
        public bool TryGetPropFrom(IMethod m, long propNameAsNid, out IMethod methodPropVal, out IRet ret)
                => TryGetPropFrom(m, () => m.Parameters, propNameAsNid, out methodPropVal, out ret);
    }
}
