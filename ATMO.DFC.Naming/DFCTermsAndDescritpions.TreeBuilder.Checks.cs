using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFCTerms.DFCTree.Build
{
    /// <summary>
    /// mko, 11.2.2020
    /// Meldungen während des Prüfprozesses
    /// </summary>
    public static partial class Checks
    {

        /// <summary>
        /// Bennenung der SAP- Konsitenzprüfung von Beugruppen beim erzeugen des DFC- Trees.
        /// Die Baugruppe kann in ihrer Struktur ungültig sein wg. Synchronisationsproblemen mit
        /// SAP ProjectBuilder oder ZSTLM
        /// </summary>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static string SAP_consistency_check_Assy(Languages lng, string MatNo)
        {
            switch (lng)
            {
                case Languages.DE:
                    return $"SAP- Konsistenz der Baugruppe {MatNo} prüfen ";
                case Languages.EN:
                    return $"Check SAP consistency of the assembly {MatNo}";
                default:
                    return $"Check SAP consistency of the assembly {MatNo}";

            }
        }
    }
}
