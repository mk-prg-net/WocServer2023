using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MkPrgNet.Pattern.Automaton.Builder.Impl
{
    public class MooreTransistionFunctionBuilder<TStateEnum> : IMooreTransitionFunctionBuilder<TStateEnum>
        where TStateEnum : struct
    {
        Tuple<TStateEnum, TStateEnum, HashSet<TStateEnum>> stateDeco;
        TransistionFunctionBuilder<TStateEnum> tfb = new TransistionFunctionBuilder<TStateEnum>();


        public MooreTransistionFunctionBuilder(Tuple<TStateEnum, TStateEnum, HashSet<TStateEnum>> stateDeco)
        {
            this.stateDeco = stateDeco;
        }

        public IMooreOutputFunctionBuilder<TStateEnum> CreateOutputFunctionBuilder()
        {
            return new MooreOutputFunctionBuilder<TStateEnum>(stateDeco, tfb.Inputs, tfb.Transistions);
        }

        public void DefTransistionFor(IInput input, params TStateEnum[] subsequentStates)
        {
            tfb.DefTransistionFor(input, subsequentStates);
        }


        public void DefTransistionFor(TStateEnum currentState, IInput input, TStateEnum subsequentState)
        {
            tfb.DefTransistionFor(currentState, input, subsequentState);
        }
    }
}
