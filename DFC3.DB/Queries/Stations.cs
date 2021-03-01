using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;

using ATMO.mko.QueryBuilder;


using ColTool = DFC3.DB.Tools.TabColAccess;
using ATMO.DFC.Tree;

using static DFCSecurity.SitesExt;

using static DZAUtilities_Dictionaries.GlobalDictionaries;


namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 8.11.2019
    /// Liefert alle Stationen zu einem Projekt.
    /// </summary>
    public class Stations : QueriesBase
    {

        public Stations(IComposer pnL) : base(pnL) { }

        public RCV3sV<StationCore> GetStation(string MatNo)
        {

            var tabSTPO = new Tables.STPO();
            var tabSTKO = new Tables.STKO();
            var sql = new SQL<StationCore>();

            throw new NotImplementedException();            

        }

    }
}
