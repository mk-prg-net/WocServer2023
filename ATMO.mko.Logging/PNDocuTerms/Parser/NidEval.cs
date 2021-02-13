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
    /// mko, 8.6.2020
    /// Evaluiert eine Naming- Konstante auf dem Stack
    /// </summary>
    public class NidEval : EvalBase
    {
        public NidEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="stack">Main.Sub.Build #ver</param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok is IntToken,  
                pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.NID.UID, ANC.DocuTerms.Parser.Errors.NID_IntTokenExpected.UID));

            var IntTok = (IntToken)stack.Pop();

            stack.Push(pnL.NID(IntTok.ValueAsLong));
        }
    }
}
