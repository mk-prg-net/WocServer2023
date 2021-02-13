using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class Method
        : DocuEntity,
        IMethod
    {
        public Method(IFormater fmt, NID nid, IDTList MethodParamsEncapsulatedInList)
            : base(fmt, DocuEntityTypes.Method, nid, MethodParamsEncapsulatedInList)
        {
        }

        public Method(IFormater fmt, NID nid)
            : base(fmt, DocuEntityTypes.Method, nid, new DTList(fmt))
        {
        }

        public Method(IFormater fmt, String name, IDTList MethodParamsEncapsulatedInList)
            : base(fmt, DocuEntityTypes.Method, name, MethodParamsEncapsulatedInList)
        {
        }

        public Method(IFormater fmt, String name)
            : base(fmt, DocuEntityTypes.Method, name, new DTList(fmt))
        {
        }

        public IMethodParameter[] Parameters => ((IDTList)Childs.Skip(1).First()).ListMembers.Select(m => (IMethodParameter)m).ToArray();
    }
}
