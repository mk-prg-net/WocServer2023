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
        /// mko, 26.3.2021
        /// Erstellt die erste Woc- Version
        /// </summary>
        /// <param name="newContent"></param>
        /// <returns></returns>
        Task<RC<T>> CreateFirstVersionOfWoc(C newContent);

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
        Task<RC<(T newVersion, T oldVersionContent)>> CreateNewVersionOfWoc(T oldVersion, C newContent);
    }
}
