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
    /// mko, 8.6.2020
    /// Evaluiert eine Naming- Konstante auf dem Stack
    /// 
    /// mko, 20.9.2021
    /// Behandlung der als NID kodierten boolschen Werte True und False.
    /// Jetzt werden dieses erkannt, und statt der NID's werden Booleans auf dem 
    /// Stack abgelegt.
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
                pnL.ReturnDocuTermSyntaxError(TTD.Types.NID.UID, TTD.Parser.Errors.NID_IntTokenExpected.UID));

            var IntTok = (IntToken)stack.Pop();

            if(IntTok.ValueAsLong == TTD.Boolean.False.UID)
            {
                stack.Push(new BooleanToken(false));
            }
            else if (IntTok.ValueAsLong == TTD.Boolean.True.UID)
            {
                stack.Push(new BooleanToken(true));
            }
            else
            {
                stack.Push(new NIDToken(IntTok.ValueAsLong));
            }            
        }
    }
}
