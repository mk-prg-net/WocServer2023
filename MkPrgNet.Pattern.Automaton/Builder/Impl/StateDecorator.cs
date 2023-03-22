using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.Builder.Impl
{
    public class StateDecoratorBuilder<TStateEnum> : IStateDecoratorBuilder<TStateEnum>
        where TStateEnum : struct
    {
        HashSet<TStateEnum> finals = new HashSet<TStateEnum>();
        TStateEnum start;
        TStateEnum defaultState;

        public StateDecoratorBuilder()
        {
        }

        public void DefineStateAsFinal(TStateEnum state)
        {
            finals.Add(state);
        }

        public void DefineStateAsStart(TStateEnum state)
        {
            start = state;
        }

        public void DefineStateAsDefault(TStateEnum state)
        {
            defaultState = state;
        }

        public Tuple<TStateEnum, TStateEnum, HashSet<TStateEnum>> Definitions
        {
            get
            {
                return Tuple.Create(start, defaultState, finals);
            }
        }

    }
}
