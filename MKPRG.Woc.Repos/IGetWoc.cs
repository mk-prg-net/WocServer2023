using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing;

namespace MKPRG.Woc.Repos
{
    /// <summary>
    /// mko, 2.3.2021
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IGetWoc<T>
        where T : IWoc
    {
        /// <summary>
        /// 2.3.2021
        /// Lädt ein als Woc definiertes Objekt vom Typ T aus dem Repository
        /// </summary>
        /// <param name="WocId"></param>
        /// <returns></returns>
        Task<RC<T>> GetWoc(long WocId);

    }
}
