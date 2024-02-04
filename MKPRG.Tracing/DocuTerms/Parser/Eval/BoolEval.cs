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
    /// mko, 25.4.2022
    /// 
    /// Liest ein Bool- Token ein
    /// </summary>
    public class BoolEval
        : EvalBase
    {
        IComposer pnL;
        ANC.INamingHelper NH;


        public BoolEval(IComposer pnL, ANC.INamingHelper NH)
        {
            this.pnL = pnL;
            this.NH = NH;
        }

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var tok = stack.Peek();
            TraceHlp.ThrowArgExIfNot(tok is mko.RPN.BoolToken || tok is mko.RPN.StringToken || tok is StringToken,
                pnL.ReturnDocuTermSyntaxError(TTD.Boolean.Boolean.UID, TTD.Parser.Errors.BooleanExpected.UID));

            if (stack.Peek() is mko.RPN.BoolToken mkoBool)
            {
                stack.Pop();                
                stack.Push(new BooleanToken(mkoBool.ValueAsBool));                
            }
            else if (stack.Peek() is mko.RPN.StringToken mkoStr)
            {
                stack.Pop();

                if (mkoStr.Value == NH._(TTD.Boolean.True.UID, ANC.Language.CNT) || mkoStr.Value == NH.glyph(TTD.Boolean.True.UID))
                {
                    stack.Push(new BooleanToken(true));
                }
                else if (mkoStr.Value == NH._(TTD.Boolean.False.UID, ANC.Language.CNT) || mkoStr.Value == NH.glyph(TTD.Boolean.False.UID))
                {
                    stack.Push(new BooleanToken(false));
                }
                else
                {
                    TraceHlp.ThrowArgEx(pnL.ReturnDocuTermSyntaxError(TTD.Boolean.Boolean.UID, TTD.Parser.Errors.BooleanExpected.UID));
                }
            }
            else if (stack.Peek() is StringToken Str)
            {
                stack.Pop();

                if (Str.Value == NH._(TTD.Boolean.True.UID, ANC.Language.CNT) || Str.Value == NH.glyph(TTD.Boolean.True.UID))
                {
                    stack.Push(new BooleanToken(true));
                }
                else if (Str.Value == NH._(TTD.Boolean.False.UID, ANC.Language.CNT) || Str.Value == NH.glyph(TTD.Boolean.False.UID))
                {
                    stack.Push(new BooleanToken(false));
                }
                else
                {
                    TraceHlp.ThrowArgEx(pnL.ReturnDocuTermSyntaxError(TTD.Boolean.Boolean.UID, TTD.Parser.Errors.BooleanExpected.UID));
                }
            }
        }
    }
}
