//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 15.12.2015
//
//  Projekt.......: WocStore
//  Name..........: IWocDocMap.cs
//  Aufgabe/Fkt...: Bildet WocID's auf Dokumente ab
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

namespace mko.Woc.DocStore
{
    /// <summary>
    /// Bildet WocID's auf Dokumente ab.
    /// </summary>
    public interface IWocDocMap<TWocID>
    {
        /// <summary>
        /// Liefert den Uri auf ein Dokument im Kontext des aktuellen WocStores, für welches die WocID steht.
        /// </summary>
        /// <param name="WocID"></param>
        /// <returns></returns>
        Uri GetUri(TWocID WocID);

        /// <summary>
        /// Liefert zu einem Uri im Kontext des aktuellen Wocstores die WocID.
        /// </summary>
        /// <param name="uri"></param>
        /// <returns></returns>
        TWocID GetWocID(Uri uri);


        /// <summary>
        /// Die Abbildung einer Woc- ID auf eine Resource definieren
        /// </summary>
        /// <param name="wocID"></param>
        /// <param name="uri"></param>
        void DefMapping(TWocID wocID, Uri uri);

        /// <summary>
        /// Die Abbildung einer WocID auf eine Resource entfernen
        /// </summary>
        /// <param name="wocID"></param>
        void RemoveMapping(TWocID wocID);

    }
}
