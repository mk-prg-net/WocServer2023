using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing;

namespace MKPRG.Woc.Repos
{
    public interface INewVersion<T, C>
        where T: IWoc
        where C: new()
    {
        /// <summary>
        /// 26.3.2021
        /// Erzeugt für ein Woc mit einem Veralteten Inhalt ein neues Woc mit dem neuen Inhalt.
        /// Der veraltete Inhalt wird in ein Woc mit einer neuen WocId  und der Version des Wocs mit dem
        /// veralteten Inhalt gekapselt. Auf dieses neue Woc mit dem veralteten Inhalt verweist dann das
        /// neue Woc mit dem neuen Inhalt.
        /// </summary>
        /// <param name="oldVersion"></param>
        /// <param name="newContent"></param>
        /// <returns></returns>
        Task<RC<(T newVersion, T oldVersionContent)>> CreateNewWocVersionOf(T oldVersion, C newContent);
    }
}
