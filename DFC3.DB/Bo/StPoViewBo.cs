using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    public class StPoViewBo
    {
        public string BGMatNr { get; set; }

        // int
        public int PosNr { get; set; }

        // int
        public int Menge { get; set; }

        // varchar(10)
        public string MatNr { get; set; }

        // char(1)
        public bool IstEVW { get; set; }

        // char(1), Dokuhaken
        public bool Dokuhaken { get; set; }

        // char(1), Beschaffungshaken
        public bool Beschaffungshaken { get; set; }
        public DateTime Lup { get; set; }        

        // char(4) Materialart
        public string MatArt { get; set; }

        // char(12) Materialklasse
        public ATMO.DFC.Material.MatClass MatKlasse { get; set; }

        // char(1), Ist Standardbaugruppe
        public bool StdBg { get; set; }

        // char(2), Materialeinkaufsstatus
        public ATMO.DFC.Material.MSTAE MSTAE { get; set; }

        // char(12), Materialnummer der Zeichnung
        public string ZeichnungsNummer { get; set; }

        // int 
        public int MatSprachCodeBenennung { get; set; }

        // varchar(40), Kurzbeschreibung des Materials
        public string MaterialKurzText { get; set; }

        public ATMO.DFC.Material.MatClass NodeType { get; set; }

    }
}
