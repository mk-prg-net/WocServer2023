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
    /// <summary>
    /// mko, 26.3.2018
    /// </summary>
    public class TimeEval : EvalBase
    {
        public TimeEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;
        /// <summary>
        /// mko, 15.6.2020
        /// Zeitstempel werden jetzt als nummerisches Triple aus (Stunde, Minute, Sekunde) erwartet.
        /// </summary>
        /// <param name="stack">ss mm hh #t</param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {            
            TraceHlp.ThrowArgExIfNot(stack.Count > 0 && stack.Peek().IsInteger, 
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Time.UID, 
                    ANC.DocuTerms.Parser.Errors.Time_TimeParticleExpected.UID,
                    ANC.TechTerms.Timeline.Hour.UID));

            var hhTok = (IntToken)stack.Pop();
                        
            TraceHlp.ThrowArgExIfNot(stack.Count > 0 && stack.Peek().IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Time.UID,
                    ANC.DocuTerms.Parser.Errors.Time_TimeParticleExpected.UID,
                    ANC.TechTerms.Timeline.Minute.UID));

            var mmTok = (IntToken)stack.Pop();
                        
            TraceHlp.ThrowArgExIfNot(stack.Count > 0 && stack.Peek().IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Time.UID,
                    ANC.DocuTerms.Parser.Errors.Time_TimeParticleExpected.UID,
                    ANC.TechTerms.Timeline.Second.UID));

            var ssTok = (IntToken)stack.Pop();
            
            TraceHlp.ThrowArgExIfNot(stack.Count > 0 && stack.Peek().IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Time.UID,
                    ANC.DocuTerms.Parser.Errors.Time_TimeParticleExpected.UID,
                    ANC.TechTerms.Timeline.Millisecond.UID));

            var msecTok = (IntToken)stack.Pop();
            
            stack.Push(
                new TimeToken(hhTok, mmTok, ssTok, msecTok));
        }
    }
}
