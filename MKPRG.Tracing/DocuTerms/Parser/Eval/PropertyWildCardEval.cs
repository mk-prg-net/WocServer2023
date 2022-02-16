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
    /// <summary>
    /// mko, 15.6.2020
    /// Liest einen WildCard für Eigenschaftswerte ein und erzeugt einen WildCard- Knoten
    /// 
    /// mko, 10.8.2021
    /// 
    /// </summary>
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
                TraceHlp.ThrowArgExIfNot(stack.Peek() is Parser.IDocuEntityToken,
                    pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.WildCard.UID, ANC.DocuTerms.Parser.Errors.WildCard_ParameterMustBeAnComplexDocuTermAndNotASimpleValue.UID));

                var restriction = (Parser.IDocuEntityToken)stack.Pop();

                var wcToken = new Parser.WildCardToken(restriction);
                stack.Push(wcToken);
            }
            else
            {
                var wcToken = new Parser.WildCardToken();
                stack.Push(wcToken);
            }            
        }
    }
}
