using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    public class STPOView602 : Table
    {
        // varchar(10)
        public ColName BGMatNr { get; }

        // int
        public ColName PosNr { get; }

        // int
        public ColName Menge { get; }

        // varchar(10)
        public ColName MatNr { get; }

        // Datum letzter Stücklistenaktualisierung
        public ColName Lup { get; }

        /// <summary>
        /// char(1)
        /// Steht für Spalte ZZERSKZ: Initialwert für EVW- Kennung bei der Materialnalage
        /// </summary>
        public ColName EVWInitialwertBeiAnlage {get;}
                
        /// <summary>
        /// int
        /// ZZBCODE Bennenungscode Materialstamm. Unter diesem Code ist die Bennennung in verscheidenen
        /// Sprachen abgelegt.
        /// Nicht immer vorhanden. Tabelle BCode, enthält die Übersetzung in verschiedene Sprachen(6) 
        /// </summary>
        public ColName MatSprachCodeBenennung { get; }

        /// <summary>
        /// char(1)
        /// ZZDoku enthält den Intialwert des Dokuhakens bei Anlage der Materials durch die MAT
        /// </summary>
        public ColName DokuHakenInitialwertBeiAnlage { get; }

        // char(4) Materialart
        public ColName MatArt { get; }

        // char(12) Materialklasse
        public ColName MatKlasse { get; }

        // char(1), Ist Standardbaugruppe
        public ColName StdBg { get; }

        // char(2), Materialeinkaufsstatus ATMO- weit
        public ColName MSTAE { get; }

        // char(12), Materialnummer der Zeichnung
        public ColName ZeichnungsNummer { get; }

        /// <summary>
        /// Neue Klassifikation ab 29.9.2020. Erweitert MKlasse um PROJEKT, FLEXCON, ME und EL
        /// </summary>
        public ColName NodeType { get; }

        /// <summary>
        /// mko, 1.12.2020
        /// Zentrale Implementierung der Berechnung der Materialklasse aus den Felder MKLASSE und NODETYPE 
        /// der BOSCH106STPOVIEW602
        /// </summary>
        /// <param name="NodeType"></param>
        /// <param name="MKlasse"></param>
        /// <param name="pnL"></param>
        /// <returns></returns>
        public RCV3sV<ATMO.DFC.Material.MatClass> MatClassFromNodeTypeAndMKlasseField(
            string NodeType, 
            string MKlasse, ATMO.mko.Logging.PNDocuTerms.DocuEntities.IComposer pnL)
        {
            var txt = string.IsNullOrWhiteSpace(NodeType) ? MKlasse : NodeType;
            return converter.ToMatClass(txt, pnL);
        }

        /// <summary>
        /// mko, 14.12.2020
        /// Liefert einen Prüfterm für die Where- Clausel, um gegen eine Materialklasse zu prüfen.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sql"></param>
        /// <param name="mClass"></param>
        /// <returns></returns>
        public IColXpr MatClassEq<T>(SQL<T> sql, ATMO.DFC.Material.MatClass mClass)
        {
            var mClassStr = converter.ToMatClassString(mClass);
            //return sql.Or(
            //            sql.And(sql.IsNotNullNorEmpty(NodeType.FQN), sql.Eq(NodeType.FQN, mClassStr)),
            //            sql.And(sql.IsNotNullNorEmpty(MatKlasse.FQN), sql.Eq(MatKlasse.FQN, mClassStr)));

            return sql.Or(sql.Eq(NodeType.FQN, mClassStr), sql.Eq(MatKlasse.FQN, mClassStr));


        }


        /// <summary>
        /// Bei der Implementierung von Abrfragen sollte immer eine instanz von STPOView602 erzeugt und eingesetzt werden.
        /// Dieser Converter ist dann dieser Instanz zugeordnet. So wird sichergestellt, dass nur  ein Thread zu einem
        /// Zeitpunkt auf die Instanz zugreift.
        /// </summary>
        ATMO.DFC.Material.StringToMatClassConverter converter = new ATMO.DFC.Material.StringToMatClassConverter();

        // varchar(40), Kurzbeschreibung des Materials. In Sprache des letzten Bearbeiters.
        public ColName MaterialKurzText { get; }   

        public STPOView602(string Alias = null) : base("dza_admin.BOSCH106STPOVIEW602", Alias)
        {

            BGMatNr = new ColName(TableName, "BGMATNR");
            PosNr = new ColName(TableName, "POSNR");
            Menge = new ColName(TableName, "MENGE");
            MatNr = new ColName(TableName, "MATNR");
            EVWInitialwertBeiAnlage = new ColName(TableName, "ZZERSKZ");
            DokuHakenInitialwertBeiAnlage = new ColName(TableName, "ZZDOKU");            
            Lup = new ColName(TableName, "LUP");            
            MatArt = new ColName(TableName, "MTART");
            MatKlasse = new ColName(TableName, "MKLASSE");
            StdBg = new ColName(TableName, "STDBG");
            MSTAE = new ColName(TableName, "MSTAE");
            ZeichnungsNummer = new ColName(TableName, "ZEINR");
            MatSprachCodeBenennung = new ColName(TableName, "ZZBCODE");
            MaterialKurzText = new ColName(TableName, "MAKTX");
            NodeType = new ColName(TableName, "NODETYPE");
        }
    }
}
