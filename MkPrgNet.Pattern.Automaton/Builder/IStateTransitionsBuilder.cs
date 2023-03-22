//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 7.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: IStateTransistionBuilder.cs
//  Aufgabe/Fkt...: Builder, mit dem die Zustandsüberführungsfunktionen
//                  eines entlichen Automaten definiert werden
//
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
    /// Builder, that defines for every Input Type all subsequent States
    /// input x state -> state
    /// </summary>
    /// <typeparam name="TStateEnum">enum with all States</typeparam>
    public interface IStateTransitionsBuilder<TStateEnum>
        where TStateEnum: struct
    {
        /// <summary>
        /// Eine Eingabe wird mit jedem Zustand aus der Liste der Zustände kombiniert.
        /// Die Reihenfolge in der Liste der Zustände (z1, z2, ..., zn) indiziert
        /// dabei eine Reihenfolge der Liste von Kombinationen aus Eingabe x und Zustand zi:
        /// (z1/x, z2/x, ..., zn/x). Jeder dieser Kombinationen wird ein Folgezustand 
        /// zugeordnet aus der Liste der subsequentStates- Parameter. Sei si ein 
        /// Subsequent State aus der Liste subsequentStates = (s1, s2, ..., sn), 
        /// dann erzeugt diese Funktion folgende Zustandsüberführungsfunktion:
        /// z1/x -> s1
        /// z2/x -> s2
        /// ...
        /// zn/x -> sn
        /// </summary>
        /// <param name="input"></param>
        /// <param name="subsequentStates">Liste der Folgezustände si zu jeder Kombination zi/x </param>
        void DefTransistionFor(IInput input, params TStateEnum[] subsequentStates);



        /// <summary>
        /// mko, 12.8.2019
        /// Definiert einen gegebenen Zustand zi und einer Eingabe x den Folgezustand six
        /// </summary>
        /// <param name="currentState">Aktueller Zustand, in dem die Eingabe erfolgt</param>
        /// <param name="input">Eingabe</param>
        /// <param name="subsequentState">Folgezustand</param>
        void DefTransistionFor(TStateEnum currentState, IInput input, TStateEnum subsequentState);
    }
}
