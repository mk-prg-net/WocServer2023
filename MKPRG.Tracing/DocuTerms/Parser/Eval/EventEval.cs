using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using pnL = MKPRG.Tracing.DocuTerms.Composer;

using ANC = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 7.3.2018
    /// 
    /// mko, 23.7.2021
    /// 
    /// Alte Implementierung von Event- DocuTerms auf neue EventTokenWith... umgestellt.
    /// </summary>
    public class EventEval : EvalBase
    {
        /// <summary>
        /// mko, 9.8.2021        
        /// Wenn die Namen bekannte Event- Typen definieren, dann wird anstatt
        /// eines *TokenWithNameAsString* ein *TokenWithNameAsNID* erzeugt.                    
        /// Dieses Dictionary ordnet die bekannten Namen den NIDs zu.
        /// </summary>
        Dictionary<string, NIDToken> GetNidTokenForEventOfType = new Dictionary<string, NIDToken>
        {
            {RC.NC[TTD.Event.End.UID].CNT, new NIDToken(TTD.Event.End.UID)},
            {RC.NC[TTD.Event.Failed.UID].CNT, new NIDToken(TTD.Event.Failed.UID)},
            {RC.NC[TTD.Event.Fails.UID].CNT, new NIDToken(TTD.Event.Fails.UID)},
            {RC.NC[TTD.Event.Info.UID].CNT, new NIDToken(TTD.Event.Info.UID)},
            {RC.NC[TTD.Event.NotCompleted.UID].CNT, new NIDToken(TTD.Event.NotCompleted.UID)},
            {RC.NC[TTD.Event.Start.UID].CNT, new NIDToken(TTD.Event.Start.UID)},
            {RC.NC[TTD.Event.Succeeded.UID].CNT, new NIDToken(TTD.Event.Succeeded.UID)},
            {RC.NC[TTD.Event.Warn.UID].CNT, new NIDToken(TTD.Event.Warn.UID)}
        };

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

            if (stack.Any() && stack.Peek() is IEventParameterToken eParam)
            {
                // Es liegt zum Event ein EventParameter auf dem Stack.
                stack.Pop();

                if(Name is NIDToken nid)
                {
                    stack.Push(new EventTokenWithNameAsNid(nid, eParam));
                }
                else if(Name is StringToken str)
                {
                    // mko, 8.9.2021
                    // Wenn die Namen bekannte Event- Typen definieren, dann wird anstatt
                    // eines *TokenWithNameAsString* ein *TokenWithNameAsNID* erzeugt.                    

                    if (GetNidTokenForEventOfType.ContainsKey(str.ValueAsString))
                    {
                        var nidFromStr = GetNidTokenForEventOfType[str.ValueAsString];
                        stack.Push(new EventTokenWithNameAsNid(nidFromStr, eParam));
                    }
                    else
                    {
                        stack.Push(new EventTokenWithNameAsString(str, eParam));
                    }                    
                }
                else
                {
                    TraceHlp.ThrowArgEx(
                            pnL.ReturnDocuTermSyntaxError(
                                TTD.Types.Event.UID, 
                                TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));
                }
            }
            else
            {
                // Es gibt zum Event kein EventParameter: leere Events erzeugen.

                if (Name is NIDToken nid)
                {
                    stack.Push(new EventTokenWithNameAsNid(nid));
                }
                else if (Name is StringToken str)
                {
                    stack.Push(new EventTokenWithNameAsString(str));
                }
                else
                {
                    TraceHlp.ThrowArgEx(
                            pnL.ReturnDocuTermSyntaxError(
                                TTD.Types.Event.UID, 
                                TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));
                }
            }
        }
    }
}
