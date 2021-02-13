using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using pnL = MKPRG.Tracing.DocuTerms.Parser.Composer;
using MKPRG.Tracing.DocuTerms;

using ANC = MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms.Parser.Parser
{
    /// <summary>
    /// mko, 7.3.2018
    /// </summary>
    public class EventEval : EvalBase
    {
        public EventEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;
        /// <summary>
        /// #li p1 p2 ... pN #pl name #e
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var Name = EvalHlp.EvaluateName(stack, pnL, ANC.DocuTerms.Types.Event.UID);

            if (stack.Any() && stack.Peek() is IEventParameter eParam)
            {

                //TraceHlp.ThrowArgExIfNot(stack.Peek() is IEventParameter,
                //    pnL.ReturnDocuTermSyntaxErrorWithDetails(
                //            ANC.DocuTerms.Types.Event.UID,
                //            ANC.DocuTerms.Parser.Errors.Event_EventParameterAsChildExpected.UID,
                //            Name));

                //var eParam = (IEventParameter)stack.Pop();
                stack.Pop();
                stack.Push(Name is NID ? pnL.e((NID)Name, eParam) : pnL.e((DocuTerms.String)Name, eParam));
            }
            else
            {
                stack.Push(Name is NID ? pnL.e((NID)Name) : pnL.e((DocuTerms.String)Name));
            }

        }
    }
}
