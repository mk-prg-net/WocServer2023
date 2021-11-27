using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// Mit INoSubTrees markiert
    /// </summary>
    public interface IVer
        : IInstanceMember,
        IMethodParameter,
        IListMember,
        IPropertyValue,
        IReturnValue,

        INoSubTrees
    {
        string VersionString { get; }
    }
}
