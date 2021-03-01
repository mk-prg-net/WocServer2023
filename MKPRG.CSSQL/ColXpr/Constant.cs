using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;

namespace MKPRG.CSSQL
{
    /// <summary>
    /// mko, Januar 2018
    /// </summary>
    public class Constant : NaLisp.Core.NaLispTerminal, IColXpr
    {
        public Constant(string val)
        {
            Value = $"'{val}'";
        }

        public Constant(int val)
        {
            Value = $"{val}";
        }

        public Constant(long val)
        {
            Value = $"{val}";
        }

        public Constant(double val)
        {
            Value = $"{val}";
        }

        /// <summary>
        /// Oracle has non boolean datatype. Instead use Number(0) for false and Number(1) for true. 
        /// https://stackoverflow.com/questions/3726758/is-there-a-boolean-type-in-oracle-databases
        /// </summary>
        /// <param name="val"></param>
        public Constant(bool val)
        {            
            Value = val ? "1" : "0";
        }

        /// <summary>
        /// mko, 19.6.2018
        /// Creates an NaLisp constant as DateTime
        /// </summary>
        /// <param name="dat"></param>
        public Constant(DateTime date)
        {
            Value = $"TO_DATE('{date.ToString("MM/dd/yyyy HH:mm:ss")}','MM/DD/YY HH24:MI:SS')";
        }

        public string Value { get; }

        public override INaLisp Clone(bool deep = true)
        {
            return new Constant(Value);
        }

        public override INaLisp Eval(NaLispStack StackInstance, bool DebugOn)
        {
            return NaLisp.Factories.Txt._.Create(Value);
        }

        public override Inspector.ProtocolEntry Validate(NaLispStack Stack)
        {
            return new Inspector.ProtocolEntry(this, true, true, typeof(NaLisp.Data.IConstValue<string>));
        }
    }
}
