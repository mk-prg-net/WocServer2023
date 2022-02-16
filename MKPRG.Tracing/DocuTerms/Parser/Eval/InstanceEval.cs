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
    /// mko, 7.2.2018
    /// </summary>
    public class InstanceEval : EvalBase
    {        

        public InstanceEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            // mko, 26.6.2019
            // Bestimmung von Namen und Parameteliste separiert, um auch Instanzen mit leeren 
            // Parameterlisten zu ermöglichen

            var name = EvalHlp.EvaluateName(stack, pnL, TTD.Types.Instance.UID);

            // gibt es eine Parameterliste ?
            if (stack.Any() && stack.Peek() is DTListToken memberList)
            {
                stack.Pop();
                // Prüfe, ob alle Listenelemente gültige Instance- Member sind
                TraceHlp.ThrowArgExIfNot(memberList.ListMembers.All(m => m is IInstanceMemberToken),
                    pnL.ReturnDocuTermSyntaxErrorWithDetails(
                        TTD.Types.Instance.UID,
                        TTD.Parser.Errors.Instance_NotAllChildsAreInstanceMembers.UID,
                        name));

                // Instanz erzeugen und auf Stack legen
                if(name is NIDToken nid)
                    stack.Push(new InstanceTokenWithNameAsNid(nid, memberList.ListMembers.Cast<IInstanceMemberToken>().ToArray()));
                else if(name is StringToken str)
                    stack.Push(new InstanceTokenWithNameAsString(str, memberList.ListMembers.Cast<IInstanceMemberToken>().ToArray()));
                else
                    TraceHlp.ThrowArgEx(
                            pnL.ReturnDocuTermSyntaxError(
                                TTD.Types.Instance.UID,
                                TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));
            }
            else
            {
                // Für Instanzen mit leerer Memberliste 
                if (name is NIDToken nid)
                    stack.Push(new InstanceTokenWithNameAsNid(nid));
                else if (name is StringToken str)
                    stack.Push(new InstanceTokenWithNameAsString(str));
                else
                    TraceHlp.ThrowArgEx(
                        pnL.ReturnDocuTermSyntaxError(
                            TTD.Types.Instance.UID,
                            TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));

            }
        }
    }
}
