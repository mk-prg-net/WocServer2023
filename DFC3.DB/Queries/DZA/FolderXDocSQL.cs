using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using ATMO.mko.QueryBuilder;
using DFC3.DB.Bo;
using DFC3.DB.Tables.DZA;

namespace DFC3.DB.Queries.DZA
{
    public class FolderXDocSQL : QueriesBase
    {

        public FolderXDocSQL(Composer pnL)
            : base(pnL) { }

        public RCV3WithValue<RCV3, Bo.FolderXDocInfo> GetNewestFolderXDocInfo(long docId, bool checkFamily = false)
        {
            RCV3WithValue<RCV3, Bo.FolderXDocInfo> ret = RCV3WithValue<RCV3, Bo.FolderXDocInfo>.Failed(null);
            try
            {
                var sql = new SQL<Bo.FolderXDocInfo>();

                var diTab = new DocInfoTab();
                var fxTab = new FolderXDocTab();

                var selFrom = sql.Select(
                        sql.Map(fxTab.FolderId, (bo, v) => bo.FolderId = (long)v),
                        sql.Map(fxTab.DocId, (bo, v) => bo.DocId = (long)v),
                        sql.Map(fxTab.XOrder, (bo, v) => bo.XOrder = (long)v),
                        sql.Map(diTab.ID, (bo, v) => bo.DocInfo.DocId = (long)v),
                        sql.Map(diTab.UserState, (bo, v) => bo.DocInfo.UserState = (DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates)(int)v),
                        sql.Map(diTab.NrLayers, (bo, v) => bo.DocInfo.NrLayers = (int)v),
                        sql.Map(diTab.InfoText, (bo, v) => bo.DocInfo.InfoText = (string)v)
                    )
                    .From(fxTab, diTab);

                QueryBuilderResult<Bo.FolderXDocInfo> q = null;
                if (checkFamily)
                {
                    q = selFrom.Where(sql.And(
                                            sql.NotEq(diTab.Family, sql.Txt("202")),
                                            sql.NotEq(diTab.Family, sql.Txt("2")),
                                            sql.Eq(fxTab.FolderId, diTab.ID),
                                            sql.Eq(fxTab.DocId, sql.Long(docId))))
                               .ByDescending(fxTab.XOrder)
                               .done();
                }
                else
                {
                    q = selFrom.Where(sql.And(
                                            sql.Eq(fxTab.FolderId, diTab.ID), 
                                            sql.Eq(fxTab.DocId, sql.Long(docId))))
                               .ByDescending(fxTab.XOrder)
                               .done();
                }


                var retQ = GetRecord(q);

                if (retQ.Succeeded && !retQ.Value.IsEmpty)
                {
                    ret = RCV3WithValue<RCV3, Bo.FolderXDocInfo>.Ok(value: retQ.Value.Entity);
                }
                else if (retQ.Value.IsEmpty)
                {
                    ret = RCV3WithValue<RCV3, Bo.FolderXDocInfo>.Failed(value: null, ErrorDescription: pnL.i("GetRecord", pnL.eFails("empty")));
                }
                else
                {
                    ret = RCV3WithValue<RCV3, Bo.FolderXDocInfo>.Failed(value: null, ErrorDescription: pnL.i("GetRecord", pnL.eFails()), inner: retQ);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3WithValue<RCV3, FolderXDocInfo>.Failed(value: null, ex: ex);
            }

            return ret;
        }

    }
}
