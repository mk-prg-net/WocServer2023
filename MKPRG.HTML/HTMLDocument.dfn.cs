using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.HTML
{
    partial class HTMLDocument
    {
        /// <summary>
        /// mko, 4.1.2021
        /// Definition
        /// </summary>
        public HTMLDocument dfn
        {
            get
            {
                t("dfn");
                return this;
            }
        }

        /// <summary>
        /// mko, 4.1.2021
        /// </summary>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public HTMLDocument dfn_class(string cssClass)
        {
            tWithClass("dfn", cssClass);
            return this;
        }

        /// <summary>
        /// mko, 4.1.2021
        /// Definition
        /// </summary>
        /// <param name="content"></param>
        /// <returns></returns>
        public string Dfn(string content)
        {            
            return $"<dfn> {content} </dfn>";
        }
    }
}
