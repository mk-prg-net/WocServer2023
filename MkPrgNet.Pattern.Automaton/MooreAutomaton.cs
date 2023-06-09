﻿//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: MoreAutomaton.cs
//  Aufgabe/Fkt...: Implementierung eines Automaten nach Moore
//                  
//                            +-----------------+
//                  Input1 ---| Automat         |
//                            |-------->\       |
//                            |      +-> STF----|----+
//                            |      |          |    |
//                  Input2 ---|  CurrentState <-|----+
//                            |    |            |     
//                            |    |            |
//                  Input n---|    +---> Output-|--------> Ausgabe
//                            +-----------------+
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Version.......: 1.1
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 
//  Änderungen....: 
//
//</unit_history>
//</unit_header>        


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MkPrgNet.Pattern.Automaton.States;

namespace MkPrgNet.Pattern.Automaton
{
    public class MooreAutomaton<TStateEnum> : IAutomaton<TStateEnum>
        where TStateEnum : struct
    {
        Tuple<TStateEnum, TStateEnum, HashSet<TStateEnum>> stateDeco;
        HashSet<IInput> _Inputs;
        Dictionary<int, TStateEnum> Transistions;
        Dictionary<TStateEnum, IOutput<TStateEnum>> outputs = new Dictionary<TStateEnum, IOutput<TStateEnum>>();


        internal MooreAutomaton(Tuple<TStateEnum, TStateEnum, HashSet<TStateEnum>> stateDeco,
                                HashSet<IInput> Inputs,
                                Dictionary<int, TStateEnum> Transistions,
                                Dictionary<TStateEnum, IOutput<TStateEnum>> outputs)
        {
            this.stateDeco = stateDeco;
            StateProperties = new States.Impl.StateDecorator<TStateEnum>(stateDeco);
            _Inputs = Inputs;
            this.Transistions = Transistions;
            this.outputs = outputs;
        }

        public TStateEnum CurrentState
        {
            get
            {
                return _current;
            }
        }
        TStateEnum _current;

        public IEnumerable<IInput> Inputs
        {
            get
            {
                return _Inputs;
            }
        }

        public IStateDecorator<TStateEnum> StateProperties
        {
            get;
        }

        public IEnumerable<TStateEnum> PossibleSubsequentStatesOf(TStateEnum State)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<TStateEnum> PossibleSubsequentStatesOfCurrent()
        {
            throw new NotImplementedException();
        }

        public void Start()
        {
            _current = stateDeco.Item1;
        }

        public void Transistion()
        {
            if (_Inputs.Any(i => i.On))
            {
                // aktiven Input mit der höchsten Priorität bestimmen
                var input = _Inputs.OrderByDescending(i => i.Priority).First(i => i.On);

                var prevState = _current;

                // mko, 12.8.2019
                // Falls ein Zustandsübergang nicht definiert ist, wird in den Default- Zustand 
                // (in der Regel ein Fehlerzustand) gewechselt
                var key = Tuple.Create(input, _current).GetHashCode();
                if (Transistions.ContainsKey(key))
                {
                    _current = Transistions[key];
                }
                else
                {
                    _current = stateDeco.Item2;
                }

                // mko, 20.3.2019
                // Vorher wurden die inputs nach Aufruf der outputs- Funktion 
                // resetted. Dadurch wurden neue Eingaben in den outputs neutralisiert.

                input.Reset();

                // Die mit dem Zustand verbundene Ausgabe durchführen
                outputs[_current].Write(input, prevState);

            }
        }
    }
}
