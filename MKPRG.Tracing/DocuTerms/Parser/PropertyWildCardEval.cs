using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using MKPRG.Tracing.DocuTerms;
using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;

using ANC = MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 15.6.2020
    /// List einen WildCard für Eigenschaftswerte ein und erzeugt einen WildCard- Knoten
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
                TraceHlp.ThrowArgExIfNot(stack.Peek() is IDocuEntity,
                    pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.WildCard.UID, ANC.DocuTerms.Parser.Errors.WildCard_ParameterMustBeAnComplexDocuTermAndNotASimpleValue.UID));

                var restriction = (IDocuEntity)stack.Pop();

                stack.Push(pnL._(restriction));
            }
            else
            {
                stack.Push(pnL._());
            }            
        }
    }
}
