using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 13.7.2018
    /// Entity of MaraPj
    /// </summary>
    public class MaraPjBo
    {
        public string PjNrStatNrMatNr { get; set; }
        public long PjNr { get; set; }
        public long StatNr { get; set; }
        public string MatNr { get; set; }
        public string StlStatus { get; set; }

        /// <summary>
        /// Ersatzteil/Verschleisteil- Kennung
        /// </summary>
        public string EVW { get; set; }

        /// <summary>
        /// Doku- Haken
        /// </summary>
        public string Doku { get; set; }

        public DateTime Lup { get; set; }

        /// <summary>
        /// mko, 13.12.2018
        /// Die StlNr ist vom Typ long und nicht string
        /// </summary>
        //public string StlNr { get; set; }
        public long StlNr { get; set; }

        /// <summary>
        /// Materialnummer der Baugruppe innerhalb eines Projektes, dessen Merkmale hier bewertet werden
        /// </summary>
        public string MatNoOfAssyWithCharacteristicValues { get; set; }

        /// <summary>
        /// Positionsnummer einer Baugruppe, deren Merkmale hier bewertet werden
        /// </summary>
        public int BomPosOfAssyWithCharacteristicValues { get; set; }

        /// <summary>
        /// Bewertete Merkmale der Baugruppe, notiert als Pipe- separierte Liste aus Attribut- Wertepaaren
        /// </summary>
        public string CharacteristicValues { get; set; }

    }
}
