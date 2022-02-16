using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    public class MethodParametersToEmbedClass
        : DocuEntity,
        IMethodParametersToEmbed
    {

        public MethodParametersToEmbedClass(IEnumerable<IMethodParameter> ToEmbed)
            : base(DocuEntityTypes.ListToEmbed)
        {
            if(ToEmbed != null)
            {
                MethodParametersToEmbed = ToEmbed;
            }
        }

        public IEnumerable<IMethodParameter> MethodParametersToEmbed { get; } = new IMethodParameter[] { };
    }
}
