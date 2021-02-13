//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 18.12.2015
//
//  Projekt.......: mko.Woc.DocStore
//  Name..........: IUpload
//  Aufgabe/Fkt...: Schnittstelle eines DocStores, über welche neue Dokumente auf den
//                  DocStore hochgeladen werden können.
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
    public interface IUpload
    {
        /// <summary>
        /// Hochladen eines neuen Dokumentes auf den Woc- Server
        /// </summary>
        /// <param name="doc">Datenstrom mit den Daten des Dokumentes</param>
        /// <param name="tags">beschreibenden Tags des Dokumentinhaltes - können Bestandteile des Uris sein, wenn nicht zu lang</param>
        /// <returns></returns>
        Task<Uri> Upload(System.IO.Stream doc, params string[] tags);

    }
}
