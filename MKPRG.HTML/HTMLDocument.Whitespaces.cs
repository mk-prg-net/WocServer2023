using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.HTML
{
    /// <summary>
    /// Leerraumzeichen
    /// </summary>
    partial class HTMLDocument
    {
        /// <summary>
        /// Geschützes Leerraumzeichen (An diesem Zeichen findet kein Zeilenumbruch statt https://de.wikipedia.org/wiki/Gesch%C3%BCtztes_Leerzeichen)
        /// </summary>
        public HTMLDocument nbsp
        {
            get
            {
                bldDoc.Append("&nbsp;");
                return this;
            }
        }

        public string Nbsp() => "&nbsp";

        /// <summary>
        /// Geschützes, schmales Leerraumzeichen
        /// </summary>
        public HTMLDocument nnbsp
        {
            get
            {
                bldDoc.Append("&#x202F;");
                return this;
            }
        }

        public string NNbsp() => "&x202F";



    }
}
