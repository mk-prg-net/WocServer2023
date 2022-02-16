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

            var strToks = new List<StringToken>();

            stack.ParseVariadicParameters(fn.ListEnd, (stackP, iParam) => {
                var tok = stack.Pop();

                // mko, 8.9.2021
                // Akzeptiert jetzt auch DocuEntites.StringToken. Diese werden z.B. 
                // beim Auflösen von NID's erstellt
                TraceHlp.ThrowArgExIfNot(
                    tok is global::mko.RPN.StringToken  
                    || tok is StringToken
                    || tok is BoolToken
                    || tok is IntToken
                    || tok is global::mko.RPN.DoubleToken,
                    pnL.ReturnDocuTermSyntaxError(ANC.DocuTerms.Types.Text.UID, ANC.DocuTerms.Parser.Errors.Txt_SimpleValueAsPartExpected.UID));

                CountEvaluated += tok.CountOfEvaluatedTokens;
                
                if (tok is BoolToken bTok)
                {
                    //bld.Append($"{bTok.ValueAsBool} ");
                    strToks.Add(new StringToken($"{bTok.ValueAsBool}"));
                }
                else if (tok is IntToken iTok)
                {
                    //bld.Append($"{iTok.ValueAsLong} ");
                    strToks.Add(new StringToken($"{iTok.ValueAsLong}"));
                }
                else if (tok is global::mko.RPN.DoubleToken dTok)
                {
                    //bld.Append($"{dTok.ValueAsDouble} ");
                    strToks.Add(new StringToken($"{dTok.ValueAsDouble}"));
                }
                else if (tok is StringToken str)
                {
                    //bld.Append($"{str.Value} ");
                    strToks.Add(str);
                }
                else if (tok is global::mko.RPN.StringToken mkostr)
                {
                    //bld.Append($"{str.Value} ");
                    strToks.Add(new StringToken(mkostr.Value));
                }                
            });

            stack.Push(new TxtToken(strToks.ToArray()));
        }
    }
}
