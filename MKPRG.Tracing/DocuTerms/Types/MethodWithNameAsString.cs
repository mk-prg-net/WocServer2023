using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{

    /// <summary>
    /// mko, 9.8.2021
    /// </summary>
    public class MethodWithNameAsString
        : Method,
        IMethodWithNameAsString
    {

        public MethodWithNameAsString(string name)
        {
            if(name != null)
                DocuTermName = name;
        }

        public MethodWithNameAsString(string name, IMethodParameter[] parameters)
            : base(parameters)
        {
            if(name != null)
                DocuTermName = name;
        }


        public string DocuTermName { get; } = String.NameIsNull.ValueAsString;
    }
}
