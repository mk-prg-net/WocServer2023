using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;


namespace DFCSecurity
{
    /// <summary>
    /// mko, 21.2.2020
    /// Ezeugen von Access- Controllern
    /// </summary>
    public interface IAccessControllerBuilder20_01
    {
        /// <summary>
        /// Erzeugt für einen User einen Access- Controller. Mittels dieses Controllers kann ermittelt werden,
        /// ob der Zugriff auf eine DFC- Ressource erlaubt ist oder nicht.
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pnL"></param>
        /// <returns></returns>
        Task<RCV3sV<IAccessController>> CreateDfcAccessControllerFor(IUserV19_10 user, Composer pnL);        
    }
}
