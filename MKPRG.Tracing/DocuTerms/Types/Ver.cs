using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 16.6.2020
    /// 
    /// mko, 9.8.2021
    /// </summary>
    public class Ver
        : DocuEntity,
        IVer
    {
        public Ver(IString verString)
            : base(DocuEntityTypes.Version)
        {
            if (verString != null)
                VersionString = verString.ValueAsString;
        }

        public Ver(string verString)
            : base(DocuEntityTypes.Version)
        {
            if (verString != null)
                VersionString = verString;
        }

        public string VersionString { get; } = "0.0.0";
    }
}
