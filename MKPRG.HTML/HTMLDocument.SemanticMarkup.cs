using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using xhtm = mkoIt.Xhtml;

namespace ATMO.mko.Logging.HTML
{
    partial class HTMLDocument
    {
        /// <summary>
        /// Einleitung fett gedruckt
        /// </summary>
        /// <returns></returns>
        public HTMLDocument b
        {
            get
            {
                t("b");                
                return this;
            }
        }

        /// <summary>
        /// Text fett setzen
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public string B(string txt)
        {
            return $"<b>{txt}</b>";
        }

        /// <summary>
        /// Einleitung kursiv drucken
        /// </summary>
        /// <returns></returns>
        public HTMLDocument i
        {
            get
            {
                t("i");
                return this;
            }
        }

        /// <summary>
        /// Text kursiv setzen
        /// </summary>
        /// <param name="txt"></param>
        /// <returns></returns>
        public string I(string txt)
        {
            return $"<i>{txt}</i>";
        }

        /// <summary>
        /// Unterstreichen
        /// </summary>
        /// <returns></returns>
        public HTMLDocument u
        {
            get
            {
                t("u");
                return this;
            }
        }

        public string U(string txt)
        {
            return $"<u>{txt}</u>";
        }

        /// <summary>
        /// Durchgestrichen, gelöscht
        /// </summary>
        /// <returns></returns>

        public HTMLDocument del
        {
            get
            {
                t("del");
                return this;
            }
        }


        public string Del(string txt)
        {
            return $"<del>{txt}</del>";
        }

        public HTMLDocument hr
        {
            get
            {
                bldDoc.Append(@"<hr>");
                return this;
            }
        }

        public string HR()
        {
            return $"<hr>";
        }



    }
}
