using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.HTML
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
            tWithClass("p", cssClass);
            return this;
        }

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HTMLDocument p_id(string id)
        {
            tWithId("p", id);
            return this;
        }

        public HTMLDocument p_id_class(string id, string CssClass)
        {
            tWithIdAndClass("p", id, CssClass);
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

            tWithClass("div", CssClass);
            return this;
        }

        public HTMLDocument div_id(string id)
        {

            tWithId("div", id);
            return this;
        }

        public HTMLDocument div_id_class(string id, string CssClass)
        {

            tWithIdAndClass("div", id, CssClass);
            return this;
        }

        // Article

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        public HTMLDocument article
        {
            get
            {
                t("article");
                return this;
            }
        }

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        /// <param name="CssClass"></param>
        /// <returns></returns>
        public HTMLDocument article_class(string CssClass)
        {

            tWithClass("article", CssClass);
            return this;
        }

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HTMLDocument article_id(string id)
        {

            tWithId("article", id);
            return this;
        }

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CssClass"></param>
        /// <returns></returns>
        public HTMLDocument article_id_class(string id, string CssClass)
        {

            tWithIdAndClass("article", id, CssClass);
            return this;
        }

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        public HTMLDocument section
        {
            get
            {
                t("section");
                return this;
            }
        }

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        /// <param name="CssClass"></param>
        /// <returns></returns>
        public HTMLDocument section_class(string CssClass)
        {

            tWithClass("section", CssClass);
            return this;
        }

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public HTMLDocument section_id(string id)
        {

            tWithId("section", id);
            return this;
        }

        /// <summary>
        /// mko, 12.12.2022
        /// </summary>
        /// <param name="id"></param>
        /// <param name="CssClass"></param>
        /// <returns></returns>
        public HTMLDocument section_id_class(string id, string CssClass)
        {

            tWithIdAndClass("section", id, CssClass);
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

            tWithClass("span", CssClass);
            return this;
        }

        public HTMLDocument span_id(string id)
        {
            tWithId("span", id);
           return this;
        }

        public HTMLDocument span_id_class(string id, string CssClass)
        {
            tWithIdAndClass("span", id, CssClass);
            return this;
        }

    }
}
