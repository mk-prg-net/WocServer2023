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
    /// <summary>
    /// mko, 7.2.2018
    /// </summary>
    public class InstanceEval : EvalBase
    {        

        public InstanceEval(DocuEntities.IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            // mko, 26.6.2019
            // Bestimmung von Namen und Parameteliste separiert, um auch Instanzen mit leeren 
            // Parameterlisten zu ermöglichen

            var name = EvalHlp.EvaluateName(stack, pnL, ANC.DocuTerms.Types.Instance.UID);

            // gibt es eine Parameterliste ?
            if (stack.Any() && stack.Peek() is DTList memberList)
            {
                stack.Pop();
                // Prüfe, ob alle Listenelemente gültige Instance- Member sind
                TraceHlp.ThrowArgExIfNot(memberList.ListMembers.All(m => m is IInstanceMember),
                    pnL.ReturnDocuTermSyntaxErrorWithDetails(
                        ANC.DocuTerms.Types.Instance.UID,
                        ANC.DocuTerms.Parser.Errors.Instance_NotAllChildsAreInstanceMembers.UID,
                        name));

                // Instanz erzeugen und auf Stack legen
                if(name is NID nid)
                    stack.Push(pnL.i(nid.NamingId, memberList.ListMembers.Select(m => (IInstanceMember)m).ToArray()));
                else if(name is DocuEntities.String str)
                    stack.Push(pnL.i(str.Value, memberList.ListMembers.Select(m => (IInstanceMember)m).ToArray()));
            }
            else
            {
                // Für Instanzen mit leerer Memberliste 
                if (name is NID nid)
                    stack.Push(pnL.i(nid.NamingId));
                else if (name is DocuEntities.String str)
                    stack.Push(pnL.i(str.Value));
            }
        }
    }
}
