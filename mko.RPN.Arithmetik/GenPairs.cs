using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.Arithmetik
{
    public class GenPair12 : mko.RPN.EvalBase
    {
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            stack.Push(new IntToken(2));
            stack.Push(new IntToken(1));
        }
    }
}
