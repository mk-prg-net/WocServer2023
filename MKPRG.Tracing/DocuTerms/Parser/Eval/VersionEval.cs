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
    /// mko, 7.2.2018
    /// </summary>
    public class VersionEval : EvalBase
    {
        public VersionEval(IComposer pnL)
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
            TraceHlp.ThrowArgExIfNot(global::mko.RPN.StringToken.Test(tok),
                pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.Version.UID, ANC.DocuTerms.Parser.Errors.Version_VersionNoAsStringExpected.UID));

            var strTok = (global::mko.RPN.StringToken)stack.Pop();
            var version = new VerToken(new StringToken(strTok.Value));
            stack.Push(version);
        }
    }
}
