using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 17.1.2018
    /// Struktur Datensatz der Projektliste
    /// </summary>
    public class Projektliste2Bo
    {        
        public int ProjectNo { get; set; }
        public string PjArt { get; set; }
        public string PjFolder { get; set; }        
        public short StationNo { get; set; }
        public string MatNr { get; set; }
        public string Bennennung { get; set; }
        public string CustAccess { get; set; }
        public string Projekt { get; set; }        
        public string Verantw { get; set; }
        public string Sysstatus { get; set; }
        public string Anwstatus { get; set; }
        public long VE { get; set; }
        public DateTime VE_Datum { get; set; }
        public long UM { get; set; }
        public long PHEK { get; set; }
        public long THEK { get; set; }
        public long Obligo { get; set; }
        public long WIP { get; set; }
        public DateTime Lup_1 { get; set; }
        public DateTime Lup_2 { get; set; }
        public byte Stufe { get; set; }
        public string Basisprojekt { get; set; }
        public DateTime ErsDat { get; set; }
        public string Disable { get; set; }
        public string DisableComment { get; set; }
        public DateTime BaseLineDate { get; set; }
        public string BaseLineUsr { get; set; }
        public string BaseLineComment { get; set; }
        public string Praefix { get; set; }
        public string PA1 { get; set; }
        public string PA2 { get; set; }
        public string PA3 { get; set; }
        public string PA4 { get; set; }
        public string PA5 { get; set; }
        public string PA6 { get; set; }
        public string PA7 { get; set; }
        public string PA8 { get; set; }
        public string PA9 { get; set; }
        public string PMH { get; set; }
        public string Owner { get; set; }
        public string IS_PM { get; set; }
        public string IS_VAB { get; set; }
        public string IS_VSM { get; set; }
        public string IS_VDP { get; set; }
        public string IS_VMK { get; set; }
        public string IS_Projektleiter { get; set; }
        public string P_BESCHAFF { get; set; }

        /// <summary>
        /// mko, 2.8.2019
        /// Dieses Feld befindet sich in der STKO- Tabelle. Da der Zugriff auf die Projektliste 
        /// in der Regel mit einem Join auf die STKO erfolgt, ist das Fald hier aufgenommen.
        /// </summary>
        public DateTime LUP { get; set; }
    }
}
