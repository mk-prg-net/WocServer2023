using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFCTerms
{
    /// <summary>
    /// mko, 17.2.2020
    /// </summary>
    public partial class SAP
    {

        /// <summary>
        /// Bezeichnung der SAP- Nummer
        /// </summary>
        /// <param name="lng"></param>
        /// <returns></returns>
        public string MatNo(Languages lng)
        {
            switch (lng)
            {
                case Languages.DE:
                    return "MatNr.";
                case Languages.EN:
                    return "MatNo.";
                default:
                    return "MatNo.";
            }
        }


        public string ProjectNo(Languages lng)
        {
            switch (lng)
            {
                case Languages.DE:
                    return "Projekt";
                case Languages.EN:
                    return "Project";
                default:
                    return "Project";
            }
        }

        public string StationNo(Languages lng)
        {
            switch (lng)
            {
                case Languages.EN:
                case Languages.DE:
                    return "Station";
                default:
                    return "Station";
            }
        }



    }
}
