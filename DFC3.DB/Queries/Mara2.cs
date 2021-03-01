using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.ComposerSubTrees;
using ATMO.mko.QueryBuilder;
using ColTool = DFC3.DB.Tools.TabColAccess;

using DfcTree = ATMO.DFC.Tree;

using ATMO.DFC.Material;
using TT = ATMO.DFC.Naming.TechTerms;
using TTD = ATMO.DFC.Naming.DocuTerms;

using PN = ATMO.mko.Logging.PNDocuTerms;

using static DFCSecurity.SitesExt;


namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 2.10.2020
    /// Abfragen auf der Mara2- Tabelle (Standortfreischaltungen für Material etc.)
    /// </summary>
    public class Mara2
        : QueriesBaseAsync
    {

        public Mara2(IComposer pnL)
            : base(pnL) { }

        /// <summary>
        /// mko, 6.4.2020
        /// Bestimmt alle Standortfreischaltungen zu einer Materialnummer
        /// 
        /// mko, 2.10.2020
        /// Ausgelagert aus ATMODocSQL in die Klasse Mara2 und in eine asynchrone Methode verwandelt.
        /// </summary>
        /// <param name="MatNo"></param>
        /// <returns></returns>
        public async Task<RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>> GetSiteActivationsFor(string MatNo)
        {

            var ret = RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>.Failed(value: (false, null), ErrorDescription: pnL.eNotCompleted());

            // lookup in Mara2 for site activations

            var sqlMara2 = new SQL<Bo.Mara2Bo>();

            var qMara2 = sqlMara2.Select(
                    sqlMara2.Map(Tables.Mara2._.PA1, (bo, v) => bo.PA1 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA2, (bo, v) => bo.PA2 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA3, (bo, v) => bo.PA3 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA4, (bo, v) => bo.PA4 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA5, (bo, v) => bo.PA5 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA6, (bo, v) => bo.PA6 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA7, (bo, v) => bo.PA7 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA8, (bo, v) => bo.PA8 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PA9, (bo, v) => bo.PA9 = ColTool.GetSave(v, "")),
                    sqlMara2.Map(Tables.Mara2._.PMH, (bo, v) => bo.PMH = ColTool.GetSave(v, ""))
                )
                .From(Tables.Mara2._)
                .Where(sqlMara2.Eq(Tables.Mara2._.MatNr, sqlMara2.Txt(MatNo)))
                .done();

            var getMara2 = await GetRecordAsync(qMara2);

            if (!getMara2.Succeeded)
            {
                ret = RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>.Failed(value: (false, null), getMara2.ToPlx());
            }
            else
            {
                // Parse site activations
                var sitesAllowed = new List<DFCSecurity.Site>();
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA1)) sitesAllowed.Add(DFCSecurity.Site.ATMO_1);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA2)) sitesAllowed.Add(DFCSecurity.Site.ATMO_2);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA3)) sitesAllowed.Add(DFCSecurity.Site.ATMO_3);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA4)) sitesAllowed.Add(DFCSecurity.Site.ATMO_4);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA5)) sitesAllowed.Add(DFCSecurity.Site.ATMO_5);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA6)) sitesAllowed.Add(DFCSecurity.Site.ATMO_6);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA7)) sitesAllowed.Add(DFCSecurity.Site.ATMO_7);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PA8)) sitesAllowed.Add(DFCSecurity.Site.ATMO_8);
                //if (!string.IsNullOrWhiteSpace(retMara2.Value.PA9)) sitesAllowed.Add(DFCSecurity.Site.ATMO_9);
                if (!string.IsNullOrWhiteSpace(getMara2.Value.Entity.PMH)) sitesAllowed.Add(DFCSecurity.Site.MH);

                if (!sitesAllowed.Any())
                {
                    ret = RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>.Ok(value: (true, GetAllAtmoSites()));
                }
                else
                {
                    ret = RCV3sV<(bool publicForAll, DFCSecurity.Site[] siteAccess)>.Ok(value: (false, sitesAllowed.ToArray()));
                }
            }

            return ret;
        }

    }
}
