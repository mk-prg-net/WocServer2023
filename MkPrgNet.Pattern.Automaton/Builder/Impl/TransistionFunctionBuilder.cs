using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.Builder.Impl
{
    class TransistionFunctionBuilder<TStateEnum> : IStateTransitionsBuilder<TStateEnum>
        where TStateEnum : struct
    {
        TStateEnum[] states = (TStateEnum[])Enum.GetValues(typeof(TStateEnum));

        public TransistionFunctionBuilder()
        {
        }

        public HashSet<IInput> Inputs = new HashSet<IInput>();

        /// <summary>
        /// Abbildung des Zustandsautomatennetzes 
        /// </summary>        
        public Dictionary<int, TStateEnum> Transistions = new Dictionary<int, TStateEnum>();

        public void DefTransistionFor(IInput input, params TStateEnum[] subsequentStates)
        {
            Inputs.Add(input);

            for (int i = 0; i < states.Length; i++)
            {
                Transistions[Tuple.Create(input, states[i]).GetHashCode()] = subsequentStates[i];
            }
        }

        /// <summary>
        /// mko, 12.8.2019
        /// Ermöglicht, 
        /// </summary>
        /// <param name="currentState"></param>
        /// <param name="input"></param>
        /// <param name="subsequentState"></param>
        public void DefTransistionFor(TStateEnum currentState, IInput input, TStateEnum subsequentState)
        {
            Inputs.Add(input);

            Transistions[Tuple.Create(input, currentState).GetHashCode()] = subsequentState;
        }


    }
}
