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
    /// mko, 31.1.2020
    /// Adaption der Konstantendeklarationen für den MS- Sql Server
    /// </summary>
    public class ConstantMSSql : NaLisp.Core.NaLispTerminal, IColXpr
    {
        public ConstantMSSql(string val)
        {
            Value = $"'{val}'";
        }

        public ConstantMSSql(int val)
        {
            Value = $"{val}";
        }

        public ConstantMSSql(long val)
        {
            Value = $"{val}";
        }

        public ConstantMSSql(double val)
        {
            Value = $"{val}";
        }

        /// <summary>
        /// Oracle has non boolean datatype. Instead use Number(0) for false and Number(1) for true. 
        /// https://stackoverflow.com/questions/3726758/is-there-a-boolean-type-in-oracle-databases
        /// </summary>
        /// <param name="val"></param>
        public ConstantMSSql(bool val)
        {
            Value = val ? "1" : "0";
        }

        /// <summary>
        /// mko, 19.6.2018
        /// Creates an NaLisp ConstantMSSql as DateTime
        /// </summary>
        /// <param name="dat"></param>
        public ConstantMSSql(DateTime date)
        {
            Value = $"'{date.ToString("MM/dd/yyyy HH:mm:ss")}'";
        }

        public string Value { get; }

        public override INaLisp Clone(bool deep = true)
        {
            return new ConstantMSSql(Value);
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
