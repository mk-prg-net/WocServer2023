using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;

using ANC = MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    public static class EvalHlp
    {
        /// <summary>
        /// mko, 10.6.2020
        /// Neuimplementiert unter Berücksichtigung von Namen als sprachneutrale NID's
        /// 
        /// mko, 23.7.2021
        /// Rückgabetyp von `IPropertyValue` auf `IPrpertyValueToken` geändert.
        /// </summary>
        /// <param name="stack"></param>
        /// <param name="errorMsg"></param>
        /// <returns></returns>
        public static IPropertyValueToken EvaluateName(Stack<IToken> stack, IComposer pnL, long NID_DocuTermType)
        {

            IPropertyValueToken Name = null;

            var token = stack.Peek();

            TraceHlp.ThrowArgExIfNot(token is NIDToken || global::mko.RPN.StringToken.Test(token), 
                pnL.ReturnDocuTermSyntaxError(NID_DocuTermType, ANC.DocuTerms.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));

            if (token is NIDToken nid)
            {
                // Name liegt als sprachneutrale NID vor- vom Stapel nehmen
                stack.Pop();
                Name = nid;
            }
            else
            {
                // Name wurde als String definiert- vom Stapel nehmen
                stack.Pop();
                Name = new StringToken(token.Value);
            }

            return Name;
        }
    }
}
