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
    /// mko, 15.3.2021
    /// 
    /// mko, 9.8.2021
    /// 
    /// </summary>
    public class Method
        : DocuEntity,
        IMethod
    {
        public Method()
            : base(DocuEntityTypes.Method)
        {
        }

        public Method(IMethodParameter[] methodParams)
            : base(DocuEntityTypes.Method)
        {
            if (Parameters != null)
            {
                var fullList = methodParams;

                // Auflösen der Einbettungen
                if(methodParams.Any(r => r is IMethodParametersToEmbed))
                {
                    var newList = new List<IMethodParameter>(methodParams.Length + 10);
                    foreach(var param in methodParams)
                    {
                        if(param is IMethodParametersToEmbed eList)
                        {
                            newList.AddRange(eList.MethodParametersToEmbed);
                        }
                        else
                        {
                            newList.Add(param);
                        }
                    }

                    fullList = newList.ToArray();
                }

                if(fullList.Any(r => r is IKillMethodPrarmeterIfNot))
                {
                    Parameters = fullList.Where(r => (r is IKillMethodPrarmeterIfNot k && k.Condition) || !(r is IKillMethodPrarmeterIfNot))
                                         .Select(r => r is IKillMethodPrarmeterIfNot k ? k.MethodParameter : r)
                                         .ToArray();

                }
                else
                {
                    Parameters = fullList;
                }                
            }
        }

        public IMethodParameter[] Parameters { get; } = new IMethodParameter[] { };
    }
}
