using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.HTML
{
    /// <summary>
    /// mko, 23.11.2020
    /// Listenblöcke
    /// </summary>
    partial class HTMLDocument
    {

        // Ordered List

        public HTMLDocument ol
        {
            get
            {
                t("ol");
                return this;
            }
        }

        public HTMLDocument ol_class(string cssClass)
        {
            tags.Push("ol");
            bldDoc.Append($"<ol class='{cssClass}'");            
            return this;
        }

        public HTMLDocument ol_id(string id)
        {
            tags.Push("ol");
            bldDoc.Append($"<ol id='{id}'");
            t("ol");
            return this;
        }

        // Unordered List

        public HTMLDocument ul
        {
            get
            {
                t("ul");
                return this;
            }
        }

        public HTMLDocument ul_class(string cssClass)
        {
            tags.Push("ul");
            bldDoc.Append($"<ul class='{cssClass}'");            
            return this;
        }

        public HTMLDocument ul_id(string id)
        {
            tags.Push("ul");
            bldDoc.Append($"<ul id='{id}'");            
            return this;
        }

        // List Item

        public HTMLDocument li
        {
            get
            {
                t("li");
                return this;
            }
        }

        public HTMLDocument li_class(string cssClass)
        {
            tags.Push("li");
            bldDoc.Append($"<li class='{cssClass}'");            
            return this;
        }

        public HTMLDocument li_id(string id)
        {
            tags.Push("li");
            bldDoc.Append($"<li id='{id}'");            
            return this;
        }

        //Definition List

        public HTMLDocument dl
        {
            get
            {
                t("dl");
                return this;
            }
        }

        public HTMLDocument dl_class(string cssClass)
        {
            tags.Push("dl");
            bldDoc.Append($"<dl class='{cssClass}'");
            return this;
        }

        public HTMLDocument dl_id(string id)
        {
            tags.Push("dl");
            bldDoc.Append($"<dl id='{id}'");
            return this;
        }

        public HTMLDocument dt
        {
            get
            {
                t("dt");
                return this;
            }
        }

        public HTMLDocument dt_class(string cssClass)
        {
            tags.Push("dt");
            bldDoc.Append($"<dt class='{cssClass}'");
            return this;
        }

        public HTMLDocument dt_id(string id)
        {
            tags.Push("dt");
            bldDoc.Append($"<dt id='{id}'");
            return this;
        }

        public HTMLDocument dd
        {
            get
            {
                t("dd");
                return this;
            }
        }

        public HTMLDocument dd_class(string cssClass)
        {
            tags.Push("dd");
            bldDoc.Append($"<dd class='{cssClass}'");
            return this;
        }

        public HTMLDocument dd_id(string id)
        {
            tags.Push("dd");
            bldDoc.Append($"<dd id='{id}'");
            return this;
        }
    }
}
