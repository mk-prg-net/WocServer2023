using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.Arithmetik
{
    public class GenTriple123 : mko.RPN.EvalBase
    {
        IFunctionNames fn;

        public GenTriple123(IFunctionNames fn)
        {
            this.fn = fn;
        }

        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            stack.Push(new IntToken(3));
            stack.Push(new FunctionNameToken(fn.GenPair12));            
        }
    }
}
