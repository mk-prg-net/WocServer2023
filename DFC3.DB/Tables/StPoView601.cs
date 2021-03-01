using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    public class StPoView601 : Table
    {
        // varchar(10)
        public ColName BGMatNr { get; }

        // int
        public ColName PosNr { get; }

        // int
        public ColName Menge { get; }

        // varchar(10)
        public ColName MatNr { get; }

        // char(1) x
        public ColName EVW { get; }

        // char(1), Dokuhaken  x
        public ColName Doku { get; }

        // char(1), Beschaffungshaken x
        public ColName Beschaff { get; }

        // Datum letzter Stücklistenaktualisierung
        public ColName Lup { get; }
        
        public ColName LupTrans { get; }

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

        // varchar(40), Kurzbeschreibung des Materials. In Sprache des letzten Bearbeiters.
        public ColName MaterialKurzText { get; }

        //  ZZBCODE Bennenungscode Materialstamm. Nicht immer vorhanden Tabelle BCode, enthält die Übersetzung in verschiedene Sprachen (6)

        // ZZERSKZ initialwert aus Materialnalage für EVW

        // ZZDoku initialwert

        public StPoView601(string Alias = null) : base("dza_admin.BOSCH106STPOVIEW601", Alias)
        {

            BGMatNr = new ColName(TableName, "BGMATNR");
            PosNr = new ColName(TableName, "POSNR");
            Menge = new ColName(TableName, "MENGE");
            MatNr = new ColName(TableName, "MATNR");
            EVW = new ColName(TableName, "EVW");
            Doku = new ColName(TableName, "DOKU");
            Beschaff = new ColName(TableName, "BESCHAFF");
            Lup = new ColName(TableName, "LUP");
            LupTrans = new ColName(TableName, "LUPTRANS");
            MatArt = new ColName(TableName, "MTART");
            MatKlasse = new ColName(TableName, "MKLASSE");
            StdBg = new ColName(TableName, "STDBG");
            MSTAE = new ColName(TableName, "MSTAE");
            ZeichnungsNummer = new ColName(TableName, "ZEINR");
            MaterialKurzText = new ColName(TableName, "MAKTX");
        }
    }
}
