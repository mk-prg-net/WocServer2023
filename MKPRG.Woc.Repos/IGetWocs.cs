using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing;

namespace MKPRG.Woc.Repos
{
    /// <summary>
    /// mko, 26.3.2021
    /// Grundlegende Zugriffsfunktionen für Wocs
    /// </summary>
    public interface IGetWocs
    {
        /// <summary>
        /// Liefert zu einer Liste von WocId's die zugehörigen Wocs
        /// </summary>
        /// <param name="WocIds"></param>
        /// <returns></returns>
        Task<RC<IWoc[]>> GetWocs(IEnumerable<long> WocIds);


        /// <summary>
        /// mko, 26.3.2021
        /// Liefert alle WocIds + Versionsnummer von Wocs, die bezüglich eines Typs von Woc- Referenzen ein bestimmtes Woc 
        /// referenzieren.
        /// Es werden keine vollständigen Wocs geliefert, da die zurückgelieferten Mengen sehr groß werden können.
        /// Die Vollständigen Wocs können dann mit GetWoc etc. geladen werden
        /// </summary>
        /// <param name="RefType">Art der Woc- Referenz</param>
        /// <param name="ReferencingWocId">Bezüglich des RefTypes referenziertes Woc</param>
        /// <returns></returns>
        //Task<RC<IWocVersion[]>> GetWocsReferencing(long RefType, long ReferencingWocId);
    }
}
