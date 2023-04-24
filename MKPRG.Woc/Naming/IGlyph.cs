using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 24.4.2023
    /// </summary>
    public interface IGlyph
    {
        /// <summary>
        /// Stellt einen Unicode für ein Ideogramm/Glyphen dar,
        /// der für den Namen steht. Z. B. &#x2139; für das Informationssymbol.
        /// </summary>
        string Glyph { get; }

    }
}
