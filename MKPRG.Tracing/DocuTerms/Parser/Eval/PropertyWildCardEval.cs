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
    public class PropertyWildCardEval
    : EvalBase
    {
        public PropertyWildCardEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack">#*</param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            if (stack.Any())
            {
                TraceHlp.ThrowArgExIfNot(stack.Peek() is IDocuEntityToken,
                    pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.WildCard.UID, ANC.DocuTerms.Parser.Errors.WildCard_ParameterMustBeAnComplexDocuTermAndNotASimpleValue.UID));

                var restriction = (IDocuEntityToken)stack.Pop();

                var wcToken = new WildCardToken(restriction);
                stack.Push(wcToken);
            }
            else
            {
                var wcToken = new WildCardToken();
                stack.Push(wcToken);
            }
        }
    }

}
