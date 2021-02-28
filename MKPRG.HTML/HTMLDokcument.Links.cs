using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.HTML
{
    /// <summary>
    /// HTML- Links und Anker
    /// </summary>
    partial class HTMLDocument
    {

        public HTMLDocument a(string href)
        {
            tags.Push("a");
            bldDoc.Append($"<a href='{href}'>");
            return this;
        }

        public string A(string href, string descr)
        {
            return $"<a href='{href}'>{descr}</a>";
        }
    }
}
