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
            TraceHlp.ThrowArgExIfNot(StringToken.Test(tok),
                pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.Version.UID, ANC.DocuTerms.Parser.Errors.Version_VersionNoAsStringExpected.UID));

            var strTok = (StringToken)stack.Pop();

            stack.Push(pnL.ver(strTok.Value));
        }
    }
}
