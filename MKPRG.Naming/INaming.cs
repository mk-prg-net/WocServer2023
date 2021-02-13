using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{

    /// <summary>
    /// Auflistung der unterstützen Sprachen in den hier definierten Sprachtabellen
    /// </summary>
    public enum Language
    {
        /// <summary>
        /// mko, 10.6.2020
        /// Anstatt bezeichner in einer bestimmten Sprache werden die Naming- ID's ausgegeben
        /// </summary>
        NID,

        /// <summary>
        /// culture- neutral
        /// Entspricht z.B. bei Dokuterms den ursprünglichen Bezeichnungen für 
        /// Ereigenisse wie fails, succeeded etc. Namen sollte stets aus einem Wort
        /// bestehen, wenn zusammengesetzt, dann in camelBack- Notation
        /// </summary>
        CNT,
        
        /// <summary>
        /// deutsch 
        /// </summary>
        DE,

        
        /// <summary>
        /// englisch
        /// </summary>
        EN, 
        
        /// <summary>
        /// spanisch
        /// </summary>
        ES,
        
        /// <summary>
        /// chinesisch
        /// </summary>
        CN
    }

    /// <summary>
    /// mko, 18.2.2020
    /// Liefert die Bennenung einer Entität in mehreren Sprachen.
    /// Zudem erhält die Entität einen technisch eindeutigen Namen in Form einer GUID.
    /// 
    /// mko, 26.1.2021
    /// Erweitert um Eigenschaft Glyph. Diese stellt einen Unicode für ein Ideogramm/Glyphen dar,
    /// der für den Namen steht. Z. B. &#x2139; für das Informationssymbol
    /// </summary>
    public interface INaming
    {
        /// <summary>
        /// Sprachneutraler und technisch eindeutiger Namen in Form eines 64bit Hashwertes
        /// (global unique)
        /// </summary>
        long ID { get; }

        /// <summary>
        /// Sprachneutraler Namen als Text zur Verwendung in DocTerms
        /// </summary>
        string IDAsName { get; }

        /// <summary>
        /// Holt den Namen in der gewünschten Sprache
        /// </summary>
        /// <param name="lng"></param>
        /// <returns></returns>
        string NameIn(Language lng);

        /// <summary>
        /// Bennenung in einer cultur- neutralen Form.
        /// Entspricht z.B. bei Dokuterms den ursprünglichen Bezeichnungen für 
        /// Ereigenisse wie fails, succeeded etc. Namen sollte stets aus einem Wort
        /// bestehen, wenn zusammengesetzt, dann in camelBack- Notation.
        /// </summary>
        string CNT { get; }

        /// <summary>
        /// Bennenung in deutsch
        /// </summary>
        string DE { get; }

        /// <summary>
        /// Bennenung in englisch
        /// </summary>
        string EN { get; }

        /// <summary>
        /// Benennung in spanisch
        /// </summary>
        string ES { get; }

        /// <summary>
        ///  Benennung in chinesich
        /// </summary>
        string CN { get; }

        /// <summary>
        /// Stellt einen Unicode für ein Ideogramm/Glyphen dar,
        /// der für den Namen steht. Z. B. &#x2139; für das Informationssymbol.
        /// </summary>
        string Glyph { get; }
    }
}
