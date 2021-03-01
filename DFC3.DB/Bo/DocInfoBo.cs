using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 15.6.2017
    /// DZA- DocInfo- Tabelle
    /// 
    /// mko, 18.6.2018
    /// Added UserState col.
    /// 
    /// mko, 8.8.2018
    /// Changed Type of UserState from int to DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates
    /// </summary>
    public class DocInfoBo
    {
        /// <summary>
        /// == ID
        /// </summary>
        public long DocId { get; set; }

        public DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates UserState { get; set; }

        public string InfoText { get; set; }

        public int NrLayers { get; set; }

    }
}
