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
    /// mko, 7.3.2018
    /// 
    /// mko, 24.6.2020
    /// Streng typisiert reimplementiert.
    /// </summary>
    public class TextEval : EvalBase
    {
        public TextEval(IFunctionNames fn, IComposer pnL)
        {
            this.fn = fn;
            this.pnL= pnL;
        }

        IFunctionNames fn;
        IComposer pnL;
        int CountEvaluated = 0;        

        /// <summary>
        /// #li str1 str2 ... strN #txt
        /// str i are strings
        /// </summary>
        /// <param name="stack"></param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var bld = new StringBuilder();

            stack.ParseVariadicParameters(fn.ListEnd, (stackP, iParam) => {
                var tok = stack.Pop();

                TraceHlp.ThrowArgExIfNot(
                    tok is StringToken
                    || tok is BoolToken
                    || tok is IntToken
                    || tok is DoubleToken,
                    pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.Text.UID, ANC.DocuTerms.Parser.Errors.Txt_SimpleValueAsPartExpected.UID));

                CountEvaluated += tok.CountOfEvaluatedTokens;
                
                if (tok is BoolToken bTok)
                {
                    bld.Append($"{bTok.ValueAsBool} ");
                }
                else if (tok is IntToken iTok)
                {
                    bld.Append($"{iTok.ValueAsLong} ");
                }
                else if (tok is DoubleToken dTok)
                {
                    bld.Append($"{dTok.ValueAsDouble} ");
                }
                else if(tok is StringToken str)
                {
                    bld.Append($"{str.Value} ");
                }                
            });

            stack.Push(pnL.txt(bld.ToString().TrimEnd(' ')));
        }
    }
}
