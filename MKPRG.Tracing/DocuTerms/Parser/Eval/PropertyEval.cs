using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Parser
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

            // Behandeln der typisierten mko.RPN- Tokens von Elementarwerten- umwandeln in DocuTerms

            var tok = stack.Peek();

            if (tok is BoolToken bTok)
            {
                stack.Pop();
                stack.Push(new BooleanToken(bTok.ValueAsBool));
            }
            else if (tok is IntToken iTok)
            {
                stack.Pop();
                stack.Push(new IntegerToken(iTok.ValueAsLong));
            }
            else if (tok is global::mko.RPN.DoubleToken dTok)
            {
                stack.Pop();
                stack.Push(new DoubleToken(dTok.ValueAsDouble));
            }
            else if (tok is global::mko.RPN.StringToken strTok)
            {
                stack.Pop();
                stack.Push(new StringToken(strTok.Value));
            }

            TraceHlp.ThrowArgExIfNot(stack.Peek() is IPropertyValue,
                    pnL.ReturnDocuTermSyntaxErrorWithDetails(
                        TTD.Types.Property.UID,
                        TTD.Parser.Errors.Property_ChildIsNotValidPropertyValue.UID,
                        Name));

            var pVal = (IPropertyValueToken)stack.Pop();            

            if (Name is NIDToken nid)
                stack.Push(new PropertyTokenWithNameAsNid(nid, pVal));
            else if(Name is StringToken str)
                stack.Push(new PropertyTokenWithNameAsString(str, pVal));
            else
                TraceHlp.ThrowArgEx(
                    pnL.ReturnDocuTermSyntaxError(
                        TTD.Types.Event.UID,
                        TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID));


        }
    }
}
