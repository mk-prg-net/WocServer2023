//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 8.8.2017
//
//  Projekt.......: MkPrgNet.Pattern.Automaton
//  Name..........: InputBase.cs
//  Aufgabe/Fkt...: Basisimplementierung der IInuput- Schnitsttelle
//                  
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
    /// Basic implementation of IInput
    /// </summary>

    //public class InputBase
    //{
    //    public InputBase(int prio)
    //    {
    //        Priority = prio;
    //    }

    //    public bool On
    //    {
    //        get
    //        {
    //            return _on;
    //        }
    //    }
    //    protected bool _on = false;

    //    public int Priority
    //    {
    //        get;
    //    }

    //    public void Reset()
    //    {
    //        _on = false;
    //    }

    //}


    /// <summary>
    /// mko, 12.9.2019
    /// Basic implementation of IInput, Multithreadfeste Implementierung.
    /// </summary>
    public class InputBase : IInput
    {
        public InputBase(int Priority = 0)
        {
            this.Priority = Priority;
        }

        public int Priority { get; }

        /// <summary>
        /// Threadsafe Implementierung von On. Wir benötigt im asynchronen Upload.
        /// </summary>
        public virtual bool On
        {
            get
            {
                // Atomarer Zugriff auf _on
                return System.Threading.Interlocked.CompareExchange(ref _on, 1, 1) == 1;
            }
            set
            {
                // Atomarer Zugriff auf _on
                if (value) System.Threading.Interlocked.CompareExchange(ref _on, 1, 0);
                else System.Threading.Interlocked.CompareExchange(ref _on, 0, 1);
            }
        }

        int _on;

        public void Reset()
        {
            On = false;
        }
    }

}
