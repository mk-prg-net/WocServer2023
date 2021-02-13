using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.HTML
{
    /// <summary>
    /// mko, 11.11.2020
    /// </summary>
    partial class HTMLDocument
    {
        // Absätze

        public HTMLDocument p
        {
            get
            {
                t("p");
                return this;
            }
        }

        public HTMLDocument p_class(string cssClass)
        {
            tags.Push("p");
            bldDoc.Append($"<p class='{cssClass}'>");
            return this;
        }

        // Div- Blöcke
        public HTMLDocument div
        {
            get
            {
                t("div");
                return this;
            }
        }

        public HTMLDocument div_class(string CssClass)
        {
            tags.Push("div");
            bldDoc.Append($"<div class='{CssClass}'>");
            return this;
        }

        public HTMLDocument div_id(string id)
        {

            tags.Push("div");
            bldDoc.Append($"<div id='{id}'>");
            return this;
        }

        // Text- Abschnitte
        public HTMLDocument span
        {
            get
            {
                t("span");
                return this;
            }
        }

        public HTMLDocument span_class(string CssClass)
        {
            tags.Push("span");
            bldDoc.Append($"<span class='{CssClass}'>");
            return this;
        }

        public HTMLDocument span_id(string id)
        {
            tags.Push("span");
            bldDoc.Append($"<span id='{id}'>");
            return this;
        }
    }
}
