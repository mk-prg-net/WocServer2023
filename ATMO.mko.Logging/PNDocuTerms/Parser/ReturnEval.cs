using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.ComposerSubTrees;

using ANC = MKPRG.Naming;

namespace ATMO.mko.Logging.PNDocuTerms.Parser
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

            TraceHlp.ThrowArgExIfNot(stack.Peek() is IReturnValue, 
                pnL.ReturnDocuTermSyntaxError(
                    ANC.DocuTerms.Types.Return.UID, 
                    ANC.DocuTerms.Parser.Errors.Return_ReturnValueAsChildExpected.UID));

            var rVal = (IReturnValue)stack.Pop();
            stack.Push(pnL.ret(rVal));

        }

    }
}
