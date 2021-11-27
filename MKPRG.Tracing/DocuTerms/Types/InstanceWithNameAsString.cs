using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// </summary>
    public class InstanceWithNameAsString
        : Instance,
        IInstanceWithNameAsString
    {

        public InstanceWithNameAsString(string name)
        {
            if (name != null)
                DocuTermName = name;
        }

        public InstanceWithNameAsString(string name, IInstanceMember[] instanceMember)
            : base(instanceMember)
        {
            if (name != null)
                DocuTermName = name;
        }

        public string DocuTermName { get; } = String.NameIsNull.ValueAsString;
    }
}
