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
    public class ReturnEval : EvalBase
    {

        public ReturnEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;

        /// <summary>
        /// Reads [value name #p] from stack and evaluates
        /// name is a string
        /// value can be a basic type like string, bool, num or as DocuEntiy
        /// </summary>
        /// <param name="stack">value name #p</param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {

            TraceHlp.ThrowArgExIfNot(stack.Peek() is IReturnValueToken, 
                pnL.ReturnDocuTermSyntaxError(
                    ANC.DocuTerms.Types.Return.UID, 
                    ANC.DocuTerms.Parser.Errors.Return_ReturnValueAsChildExpected.UID));

            var rVal = (IReturnValueToken)stack.Pop();
            stack.Push(new ReturnToken(rVal));

        }

    }
}
