using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.SiegelOrSowilo
{
    /// <summary>
    /// mko, 9.3.2024
    /// Eine Datenverarbeitungsstufe kann zwei Ausgänge haben: 
    /// - ᛋ Siegel: Standard- oder Normalfall
    /// - ᛊ Sowilo: Ausnahme- oder Fehlerfall
    /// </summary>
    public interface ISiegelOrSowilo<TResult>
    {
        void S(Action<TResult, DocuTerms.IMethod> nextStage);
        void W(Action<TResult, DocuTerms.IMethod> nextStage);
    }


    // F(p1, ..., pn).(...)(...)
    public interface INextStage<TResult>
    {
        Task NextStage(Action<TResult, DocuTerms.IMethod> nextStage);
    }
}
