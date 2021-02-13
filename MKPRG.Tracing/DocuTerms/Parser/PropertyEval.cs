using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using MKPRG.Tracing.DocuTerms;
using ANC = MKPRG.Naming;



namespace MKPRG.Tracing.DocuTerms.Parser.Parser
{
    /// <summary>
    /// mko, 7.3.2018
    /// </summary>
    public class PropertyEval : EvalBase
    {

        public PropertyEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;

        /// <summary>
        /// Reads [value name #p] from stack and evaluates
        /// name is a string
        /// value can be a basic type like string, bool, num or as DocuEntiy
        /// </summary>
        /// <param name="stack">value name #p</param>
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var Name = EvalHlp.EvaluateName(stack, pnL, ANC.DocuTerms.Types.Event.UID);

            // Behandeln der typisierten Tokens von Elementarwerten- umwandeln in DocuTerms

            var tok = stack.Peek();

            if (tok is BoolToken bTok)
            {
                stack.Pop();
                stack.Push(pnL.boolean(bTok.ValueAsBool));
            }
            else if (tok is IntToken iTok)
            {
                stack.Pop();
                stack.Push(pnL.integer(iTok.ValueAsLong));
            }
            else if (tok is DoubleToken dTok)
            {
                stack.Pop();
                stack.Push(pnL.dbl(dTok.ValueAsDouble));
            }
            else if (tok is StringToken strTok)
            {
                stack.Pop();
                stack.Push(pnL.str(strTok.Value));
            }

            TraceHlp.ThrowArgExIfNot(stack.Peek() is IPropertyValue,
                    pnL.ReturnDocuTermSyntaxErrorWithDetails(
                        ANC.DocuTerms.Types.Property.UID,
                        ANC.DocuTerms.Parser.Errors.Property_ChildIsNotValidPropertyValue.UID,
                        Name));

            var pVal = (IPropertyValue)stack.Pop();
            stack.Push(Name is NID ? pnL.p((NID)Name, pVal) : pnL.p((DocuTerms.String)Name, pVal));

        }
    }
}
