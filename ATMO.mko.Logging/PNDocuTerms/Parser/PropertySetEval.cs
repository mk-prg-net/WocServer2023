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
    /// mko, 7.3.2018
    /// 
    /// mko, 25.6.2020
    /// Aktuell wird das Konzept eines Property- Setters als Dokuterm nicht weiterverfolgt. Dehalb 
    /// wird hier eine Property generiert!
    /// </summary>
    public class PropertySetEval : EvalBase
    {
        public PropertySetEval(IComposer pnL)
        {
            this.pnL = pnL;
        }

        IComposer pnL;

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {

            var Name = EvalHlp.EvaluateName(stack, pnL, ANC.DocuTerms.Types.PropertySet.UID);

            TraceHlp.ThrowArgExIfNot(stack.Peek() is IPropertyValue,
                pnL.ReturnDocuTermSyntaxErrorWithDetails(
                    ANC.DocuTerms.Types.PropertySet.UID,
                    ANC.DocuTerms.Parser.Errors.Property_ChildIsNotValidPropertyValue.UID,
                    Name));

            var eParam = (IPropertyValue)stack.Pop();
            // mko, 25.6.02020
            // Aktuell wird das Konzept eines Property- Setters als Dokuterm nicht weiterverfolgt. Dehalb 
            // wird hier eine Property generiert!
            stack.Push(Name is NID ? pnL.pSet(((NID)Name).NamingId, eParam) : pnL.pSet(((DocuEntities.String)Name).Value, eParam));
        }
    }
}
