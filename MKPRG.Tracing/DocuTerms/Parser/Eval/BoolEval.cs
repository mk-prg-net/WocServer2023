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


        public BoolEval(IComposer pnL)
        {
            this.pnL = pnL;
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

                if (mkoStr.Value == RC.NC[TTD.Boolean.True.UID].CNT || mkoStr.Value == RC.NC[TTD.Boolean.True.UID].Glyph)
                {
                    stack.Push(new BooleanToken(true));
                }
                else if (mkoStr.Value == RC.NC[TTD.Boolean.False.UID].CNT || mkoStr.Value == RC.NC[TTD.Boolean.False.UID].Glyph)
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

                if (Str.Value == RC.NC[TTD.Boolean.True.UID].CNT || Str.Value == RC.NC[TTD.Boolean.True.UID].Glyph)
                {
                    stack.Push(new BooleanToken(true));
                }
                else if (Str.Value == RC.NC[TTD.Boolean.False.UID].CNT || Str.Value == RC.NC[TTD.Boolean.False.UID].Glyph)
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
