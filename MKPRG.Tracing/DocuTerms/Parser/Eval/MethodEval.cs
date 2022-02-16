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
    public class MethodEval : EvalBase
    {
        public MethodEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;

        /// <summary>
        /// mko
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            // mko, 21.12.2018
            // Bestimmung von Namen und Parameteliste separiert, um auch Methoden mit leeren 
            // Parameterlisten zu ermöglichen

            var name = EvalHlp.EvaluateName(stack, pnL, TTD.Types.Method.UID);

            // gibt es eine Parameterliste ?
            if (stack.Any() && stack.Peek() is DTListToken memberList)
            {
                stack.Pop();
                // Prüfe, ob alle Listenelemente Methodenparameter sind
                TraceHlp.ThrowArgExIfNot(memberList.ListMembers.All(m => m is IMethodParameter),
                    pnL.ReturnDocuTermSyntaxErrorWithDetails(TTD.Types.Method.UID,
                     TTD.Parser.Errors.Method_NotAllChildsAreMethodMembers.UID,
                     name));

                // Methode erzeugen und auf Stack legen
                if (name is NIDToken nid)
                    stack.Push(new MethodTokenWithNameAsNid(nid, memberList.ListMembers.Cast<IMethodParameterToken>().ToArray()));
                else if (name is StringToken str)
                    stack.Push(new MethodTokenWithNameAsString(str, memberList.ListMembers.Cast<IMethodParameterToken>().ToArray()));
                else
                    TraceHlp.ThrowArgEx(
                            pnL.ReturnDocuTermSyntaxError(
                                TTD.Types.Method.UID,
                                TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));
            }
            else
            {
                // Für Methoden mit leerer Parameterliste 
                if (name is NIDToken nid)
                    stack.Push(new MethodTokenWithNameAsNid(nid));
                else if (name is StringToken str)
                    stack.Push(new MethodTokenWithNameAsString(str));
                else
                    TraceHlp.ThrowArgEx(
                            pnL.ReturnDocuTermSyntaxError(
                                TTD.Types.Method.UID,
                                TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));
            }
        }
    }
}
