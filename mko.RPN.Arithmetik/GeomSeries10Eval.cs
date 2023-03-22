using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN.Arithmetik
{
    public class GeomSeries10Eval: mko.RPN.EvalBase
    {
        public override void ReadParametersAndEvaluate(Stack<IToken> stack)
        {
            var k = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };

            var ser = k.Select(r => k.Take(r).Sum(rr => r)).ToArray();            

            foreach(var el in ser)
            {
                stack.Push(new IntToken(el));
            }
        }
    }
}
