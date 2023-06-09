﻿//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: IMooreAutomatonBuilder.cs
//  Aufgabe/Fkt...: Definiert für jede Eingabe alle Folgezustände.
//                  Anschließend kann die Ausgabefunktion für jeden 
//                  Zustand definiert werden    
//                  Die Zustände sind als Enum zu definieren.
//
//
//
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

namespace MkPrgNet.Pattern.Automaton
{
    /// <summary>
    /// mko,
    /// 
    /// Phase 1 of automaton- definition.
    /// Defines a set of functions to specify States with special meaning, 
    /// and to get interface for Phase 2 of automaton- definition.
    /// 
    /// mko, 12.8.2019
    /// <typeparam name="TState">Enum, that defines all states of automaton</typeparam>
    public interface IMooreAutomatonBuilder<TStateEnum> : IStateDecoratorBuilder<TStateEnum>
        where TStateEnum : struct
    {

        /// <summary>
        /// Creates a Builder, that defines for every state a output function.
        /// </summary>
        /// <returns></returns>
        IMooreTransitionFunctionBuilder<TStateEnum> CreateTransistionFunctionBuilder();

    }
}
