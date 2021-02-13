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
            var tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok.IsInteger, 
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Time.UID, 
                    ANC.DocuTerms.Parser.Errors.Time_TimeParticleExpected.UID,
                    ANC.TechTerms.Timeline.Hour.UID));

            var hhTok = (IntToken)stack.Pop();

            tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok.IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Time.UID,
                    ANC.DocuTerms.Parser.Errors.Time_TimeParticleExpected.UID,
                    ANC.TechTerms.Timeline.Minute.UID));

            var mmTok = (IntToken)stack.Pop();

            tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok.IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Time.UID,
                    ANC.DocuTerms.Parser.Errors.Time_TimeParticleExpected.UID,
                    ANC.TechTerms.Timeline.Second.UID));

            var ssTok = (IntToken)stack.Pop();

            tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok.IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Time.UID,
                    ANC.DocuTerms.Parser.Errors.Time_TimeParticleExpected.UID,
                    ANC.TechTerms.Timeline.Millisecond.UID));

            var msecTok = (IntToken)stack.Pop();

            stack.Push(
                pnL.time(hhTok.ValueAsInt, mmTok.ValueAsInt, ssTok.ValueAsInt, msecTok.ValueAsInt));
        }
    }
}
