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
    public class MethodEval : EvalBase
    {
        public MethodEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;

        List<IMethodParameter> parts = new List<IMethodParameter>();

        /// <summary>
        /// mko
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            // mko, 21.12.2018
            // Bestimmung von Namen und Parameteliste separiert, um auch Methoden mit leeren 
            // Parameterlisten zu ermöglichen

            var name = EvalHlp.EvaluateName(stack, pnL, ANC.DocuTerms.Types.Method.UID);

            // gibt es eine Parameterliste ?
            if (stack.Any() && stack.Peek() is DTList memberList)
            {
                stack.Pop();
                // Prüfe, ob alle Listenelemente Methodenparameter sind
                TraceHlp.ThrowArgExIfNot(memberList.ListMembers.All(m => m is IMethodParameter),
                    pnL.ReturnDocuTermSyntaxErrorWithDetails(ANC.DocuTerms.Types.Method.UID,
                     ANC.DocuTerms.Parser.Errors.Method_NotAllChildsAreMethodMembers.UID,
                     name));

                // Methode erzeugen und auf Stack legen
                if (name is NID nid)
                    stack.Push(pnL.m(nid.NamingId, memberList.ListMembers.Select(m => (IMethodParameter)m).ToArray()));
                else if (name is DocuEntities.String str)
                    stack.Push(pnL.m(str.Value, memberList.ListMembers.Select(m => (IMethodParameter)m).ToArray()));

            }
            else
            {
                // Für Methoden mit leerer Parameterliste 
                if (name is NID nid)
                    stack.Push(pnL.m(nid.NamingId));
                else if (name is DocuEntities.String str)
                    stack.Push(pnL.m(str.Value));
            }
        }
    }
}
