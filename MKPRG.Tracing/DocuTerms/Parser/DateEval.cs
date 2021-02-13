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
    /// <summary>
    /// mko, 26.3.2018
    /// 
    /// </summary>
    public class DateEval : EvalBase
    {
        public DateEval(DocuTerms.IComposer pnL)
        {
            this.pnL = pnL;
        }

        DocuTerms.IComposer pnL;
        /// <summary>
        /// mko, 15.6.2020
        /// Datumsstempel werden jetzt als nummerisches Triple aus (Jahr, Monat, Tag) erwartet.
        /// </summary>
        /// <param name="stack">day month year #d</param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok.IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Date.UID,
                    ANC.DocuTerms.Parser.Errors.Date_DateParticleExpected.UID,
                    ANC.TechTerms.Timeline.Year.UID));

            var yearTok = (IntToken)stack.Pop();

            tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok.IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Date.UID,
                    ANC.DocuTerms.Parser.Errors.Date_DateParticleExpected.UID,
                    ANC.TechTerms.Timeline.Month.UID));

            var monthTok = (IntToken)stack.Pop();

            tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok.IsInteger,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.Date.UID,
                    ANC.DocuTerms.Parser.Errors.Date_DateParticleExpected.UID,
                    ANC.TechTerms.Timeline.Day.UID));

            var dayTok = (IntToken)stack.Pop();

            stack.Push(pnL.date(yearTok.ValueAsInt, monthTok.ValueAsInt, dayTok.ValueAsInt));
        }
    }
}
