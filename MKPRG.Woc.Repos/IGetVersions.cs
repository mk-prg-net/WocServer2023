using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing;

namespace MKPRG.Woc.Repos
{
    public interface IGetVersions
    {
        /// <summary>
        /// mko, 26.3.2021
        /// Zu jedem Woc in einer Liste von Wocs wird bestimmt, ob im Repo eine neuere Version 
        /// existiert. Zurückgegeben wird eine Liste mit allen gefundenen neueren Versionen.
        /// </summary>
        /// <param name="CurrentAvailableWocs">Liste der aktuell verfügbaren Wocs</param>
        /// <returns>Liste von Wocs aus dem Repo, deren Id einem in CurrentAvailableWocs entsprechen, jedoch eine höhere Version haben.</returns>
        //Task<RC<IWocVersion[]>> GetNewerVersionsOf(IEnumerable<IWocVersion> CurrentAvailableWocs);
    }
}
