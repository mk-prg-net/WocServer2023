using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;

using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 10.8.2021
    /// Modifiziert: Liste `parts` von einer Instanzvariable zu einer lokalen Variable gemacht,
    /// damit zukünfitig paralleisierungen möglich sind.
    /// Instanzvariable `CountEvaluated` gelöscht (war privat uns sonst nirgens im Einsatz)
    /// </summary>
    public class ListEval : EvalBase
    {
        public ListEval(IFn fn, IComposer pnL)
        {
            this.fn = fn;
            this.pnL = pnL;
        }

        IFn fn;
        IComposer pnL;
        //int CountEvaluated = 0;


        /// <summary>
        /// #li P1 P2 ... PN #pl
        /// Pi are DocuEnties
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var parts = new List<IListMemberToken>();
            parts.Clear();
            stack.ParseVariadicParameters(fn.ListEnd, (stackP, iParam) => {

                var tok = stack.Pop();

                //TraceHlp.ThrowArgExIfNot(tok.IsFunctionName, $"{tok.ToString()} is not a parameter");
                TraceHlp.ThrowArgExIfNot(tok is IListMemberToken, 
                    pnL.ReturnDocuTermSyntaxError(
                        TTD.Types.List.UID, 
                        TTD.Parser.Errors.List_NotAllChildsAreListMembers.UID));

                var dokE = (IListMemberToken)tok;
                //CountEvaluated += tok.CountOfEvaluatedTokens;

                parts.Add(dokE);
            });

            stack.Push(new DTListToken(parts.ToArray()));
        }
    }
}
