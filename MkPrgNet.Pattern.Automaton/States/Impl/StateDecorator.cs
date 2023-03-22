using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.States.Impl
{
    /// <summary>
    /// mko, 10.9.2019
    /// Implementierung von IsFinal und IsStart getauscht, da vorher vertauscht.
    /// </summary>
    /// <typeparam name="TStateEnum"></typeparam>
    public class StateDecorator<TStateEnum> : IStateDecorator<TStateEnum>
        where TStateEnum : struct
    {

        internal StateDecorator(Tuple<TStateEnum, TStateEnum, HashSet<TStateEnum>> stateDeco)
        {
            this.stateDeco = stateDeco;
        }

        Tuple<TStateEnum, TStateEnum, HashSet<TStateEnum>> stateDeco;

        public bool IsFinal(TStateEnum state)
        {
            return stateDeco.Item3.Contains(state);            
        }

        public bool IsStart(TStateEnum state)
        {
            return stateDeco.Item1.Equals(state);
        }

        public bool IsDefault(TStateEnum state)
        {
            return stateDeco.Item2.Equals(state);
        }
    }
}
