using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

using ATMO.mko.QueryBuilder;

using ColTool = DFC3.DB.Tools.TabColAccess;

namespace DFC3.DB.Queries
{

    /// <summary>
    /// mko, 19.11.2018
    /// </summary>
    public class CustGroupsQueries : QueriesBase
    {
        ResultSet<Bo.CustomerGroup> custGroupsRecords;

        ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory plxResultDescrFactory;


        // mko, 19.11.2019
        // erstellt
        // 
        // mko, 25.6.2019
        // CustGroupId wird jetzt stets auf lower case umgestellt
        public CustGroupsQueries(IComposer pnL)
            : base(pnL)
        {

            plxResultDescrFactory = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);

            var tab = new Tables.CustGroupTab();
            var sql = new SQL<Bo.CustomerGroup>();
            var q = sql.Select(
                    sql.Map(tab.ID, (bo, v) => bo.ID = ColTool.GetSave(v, -1L)),
                    sql.Map(tab.CustGroupId, (bo, v) => bo.CustGroupId = ColTool.GetSave(v, "").ToLower()),
                    sql.Map(tab.CustGroupDescription, (bo, v) => bo.CustGroupDescription = ColTool.GetSave(v, "")),
                    sql.Map(tab.CustGroupAdmins, (bo, v) => bo.custGroupAdmins = ColTool.GetSave(v, ""))                    
                )
                .AllSortedFrom(new Tables.CustGroupTab())
                .By(tab.CustGroupId)
                .done();

            var getCustGroups = GetRecords(q);

            TraceHlp.ThrowArgExIfNot(getCustGroups.Succeeded, getCustGroups.ToPlx());
            TraceHlp.ThrowArgExIf(getCustGroups.Value.IsEmpty, new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL).CreateQueryResultEmpty());

            custGroupsRecords = getCustGroups.Value;
        }


        /// <summary>
        /// mko, 20.11.2018
        /// prüft, ob die Kundengruppe existiert.
        /// </summary>
        /// <param name="custGroupId"></param>
        /// <returns></returns>
        public bool Exists(string custGroupId)
        {
            return custGroupsRecords.Entities.Any(r => r.CustGroupId == custGroupId);
        }


        /// <summary>
        /// mko, 19.11.2018
        /// Liefert Beschreibung einer Kundengruppe
        /// </summary>
        /// <param name="custGroupId"></param>
        /// <returns></returns>
        public RCV3sV<string> GetDescription(string custGroupId)
        {
            RCV3sV<string> ret = RCV3sV<string>.Failed(value: "", ErrorDescription: plxResultDescrFactory.CreateQueryResultEmpty());

            if (custGroupsRecords.Entities.Any(r => r.CustGroupId == custGroupId))
            {
                var descr = custGroupsRecords.Entities.First(r => r.CustGroupId == custGroupId).CustGroupDescription;
                ret = RCV3sV<string>.Ok(value: descr, Message: plxResultDescrFactory.CreateQueryResultOk(1));
            }

            return ret;

        }

        /// <summary>
        /// mko, 19.11.2018
        /// </summary>
        /// <param name="custGroupId"></param>
        /// <returns></returns>
        public string GetDescriptionIntern(string custGroupId)
        {
            var descr = custGroupsRecords.Entities.FirstOrDefault(r => r.CustGroupId == custGroupId)?.CustGroupDescription;
            return string.IsNullOrWhiteSpace(descr) ? "" : descr;
        }


        /// <summary>
        /// mko, 19.11.2018
        /// Liefert alle Kundengruppen (sortiert)
        /// </summary>
        /// <returns></returns>
        public RCV3sV<IEnumerable<DFCObjects.Common.CustomerGroup>> GetAll()
        {
            var ret = RCV3sV<IEnumerable<DFCObjects.Common.CustomerGroup>>.Failed(value: null, ErrorDescription: plxResultDescrFactory.CreateQueryResultEmpty());

            if (custGroupsRecords.Entities.Any())
            {                
                ret = RCV3sV<IEnumerable<DFCObjects.Common.CustomerGroup>>
                        .Ok(value: custGroupsRecords.Entities.Select(r => new DFCObjects.Common.CustomerGroup() { Key = r.CustGroupId, Description = r.CustGroupDescription }), Message: plxResultDescrFactory.CreateQueryResultOk(custGroupsRecords.Entities.Count()));
            }

            return ret;
        }

    }
}
