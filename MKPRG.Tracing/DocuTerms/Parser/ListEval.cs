using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using MKPRG.Tracing.DocuTerms;
using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;

using ANC = MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms.Parser.Parser
{
    public class ListEval : EvalBase
    {
        public ListEval(IFn fn, IComposer pnL)
        {
            this.fn = fn;
            this.pnL = pnL;
        }

        IFn fn;
        IComposer pnL;
        int CountEvaluated = 0;

        List<IListMember> parts = new List<IListMember>();

        /// <summary>
        /// #li P1 P2 ... PN #pl
        /// Pi are DocuEnties
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            parts.Clear();
            stack.ParseVariadicParameters(fn.ListEnd, (stackP, iParam) => {

                var tok = stack.Pop();

                //TraceHlp.ThrowArgExIfNot(tok.IsFunctionName, $"{tok.ToString()} is not a parameter");
                TraceHlp.ThrowArgExIfNot(tok is IListMember, 
                    pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.List.UID, ANC.DocuTerms.Parser.Errors.List_NotAllChildsAreListMembers.UID));

                var dokE = (IListMember)tok;
                CountEvaluated += tok.CountOfEvaluatedTokens;

                parts.Add(dokE);
            });

            stack.Push(pnL.List(parts.ToArray()));
        }
    }
}
