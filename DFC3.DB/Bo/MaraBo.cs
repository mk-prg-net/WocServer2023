using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 13.7.2018
    /// Entity of Mara- Table
    /// </summary>
    public class MaraBo
    {
        /// <summary>
        /// Materialnummer.
        /// Die Mara- Tabelle stellt für jede  Materialnummer einen Datensatz bereit.
        /// </summary>
        public string MatNr { get; set; }

        /// <summary>
        /// Materialnummer der Zeichnung, die diesem Teil zugeordnet ist.
        /// 1) Ist das Feld leer, dann steht ist die in MatNr geführte Materialnummer gleich der 
        ///    Marterialnummer der Zeichnung.
        /// 2) Ist das Feld nicht leer und inhaltlich gleich MatNr, dann wie 1)
        /// 3) Ist das Feld nicht leer und verschieden gegenüber dem Inhalt von MatNr, dann liegt eine
        ///    Verweiszeichnung vor.
        /// </summary>
        public string ZeichungsNr { get; set; }

        /// <summary>
        /// Neue Klassifikation ab 29.9.2020. Erweitert MKlasse um PROJEKT, FLEXCON, ME und EL
        /// </summary>
        public string NodeType { get; set; }

        /// <summary>
        /// Indicates if part is a Station, Singlepart etc.
        /// </summary>
        public string MKlasse { get; set; }


        /// <summary>
        /// mko, 26.7.2019
        /// Ist ein frei beschreibbares Feld in SAP zur Materialklassifikation.
        /// Fologende Klassen sind bekannt (Stand 26.7.2019):
        /// ANFE = Anfertigung                  Aktiv
        /// ANGB = Angebot Aktiv
        /// DIEN = Dienstleitung Aktiv
        /// HALB = Halbzeug Aktiv
        /// KATA = Katalog Aktiv
        /// KAUF = Kaufteil / oder BG         Leiche
        /// NLAG = Weiß ich nicht
        /// UNBW = (unbewertet) Leiche
        /// VERP = Verpackung Aktiv
        /// WRKFL = Workflow Leiche
        /// </summary>
        public string MTArt { get; set; }

        /// <summary>
        /// mko, 27.7.2019
        /// MEINS = "Material Einheit für Stückzahl"
        /// BG = Baugruppe (Achtung: Konstrukteur kann nachträglich Baugruppen in Einzelteile umdeklarieren. Wenn
        ///                          aber eine Beschaffung lief, kann MEINS nicht mehr angepasst werden- so verbleibt
        ///                          dann MEINS auf BG, obwohl ST jetzt korrekt wäre.
        ///                          -: BG nur in letzter Instanz heranziehen bei der Materialnummernklassifizierung
        /// ST = Stück
        /// </summary>
        public string MEINS { get; set; }

        /// <summary>
        ///  Materialstatus Einkauf
        /// </summary>
        public string MSTAE { get; set; }

        /// <summary>
        /// Count of Bom Positions, where this part appears. Shows, how often it is used in projects.
        /// </summary>
        public int Verbaut { get; set; }

        public string StandardBaugruppe { get; set; }

        /// <summary>
        /// Defines per site if it is a Standardbaugruppe
        /// </summary>
        public string ZAT { get; set; }



    }
}
