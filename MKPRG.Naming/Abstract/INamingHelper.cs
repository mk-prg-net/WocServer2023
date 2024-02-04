using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 4.2.2024
    /// </summary>
    public interface INamingHelper
    {
        /// <summary>
        /// Liefert zur NID den Text in der voreingestellten Sprache zurück
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        string _(long NID);

        /// <summary>
        /// liefert zur NID den Text in der gewünschten Sprache zurück
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        string _(long NID, Language lng);

        /// <summary>
        /// liefert zur NID den assoziierten Glyphen, oder ein Leerzeichen zurück, 
        /// falls kein Glyph definiert wurde.
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        string glyph(long NID);

        /// <summary>
        /// liefert den Glyph als HTML Entität zurück.
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        string htmlGlyph(long NID);
    }
}
