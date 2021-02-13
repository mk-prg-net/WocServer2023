using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.HTML
{
    partial class HTMLDocument
    {
        public HTMLDocument table
        {
            get
            {
                t("table");
                return this;
            }
        }

        public HTMLDocument table_class(string cssClass)
        {
            tWithClass("taqble", cssClass);
            return this;
        }

        public HTMLDocument tr
        {
            get
            {
                t("tr");
                return this;
            }
        }


        public HTMLDocument tr_class(string cssClass)
        {
            tWithClass("tr", cssClass);
            return this;
        }

        public HTMLDocument th
        {
            get
            {
                t("th");
                return this;
            }
        }

        public HTMLDocument th_class(string cssClass)
        {
            tWithClass("th", cssClass);
            return this;
        }

        public HTMLDocument td
        {
            get
            {
                t("td");
                return this;
            }
        }

        public HTMLDocument td_class(string cssClass)
        {
            tWithClass("td", cssClass);
            return this;
        }

    }
}
