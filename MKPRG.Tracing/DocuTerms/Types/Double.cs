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
    public class Double
      : DocuEntity,
        IDouble
    {
        public Double(double val)
            : base(DocuEntityTypes.Float)
        {
            ValueAsDouble = val;
        }

        public double ValueAsDouble { get; } = 0.0;
    }
}
