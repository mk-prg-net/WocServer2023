using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using ATMO.mko.QueryBuilder;
using ColTool = DFC3.DB.Tools.TabColAccess;


namespace DFC3.DB.Queries.DZA
{
    public class XUsers : QueriesBase
    {
        public XUsers(Composer pnL)
            : base(pnL)
        { }

        /// <summary>
        /// mko, 21.9.2018
        /// Gets userdata for user with id == UserId
        /// </summary>
        /// <param name="UserID"></param>
        /// <returns></returns>
        public RCV3WithValue<RCV3, Result<Bo.XUser>> GetUser(long UserID)
        {
            var ret = RCV3WithValue<RCV3, Result<Bo.XUser>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());
            try
            {
                var sql1 = new SQL<Bo.XUser>();
                var tabXUser = new Tables.DZA.XUserTab();

                // 1) Get DZA- UserID
                var qUserId = SelectFrom(sql1)
                    .Where(sql1.StrEq(tabXUser.ID, sql1.Long(UserID)))
                    .done();

                ret = GetRecord(qUserId);
            }
            catch (Exception ex)
            {
                ret = RCV3WithValue<RCV3, Result<Bo.XUser>>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;

        }

        /// <summary>
        /// mko, 12.6.2018
        /// Gets for a given DFC- UserName the Entry in dza_admin.XUser Table
        /// 
        /// mko, 21.9.2018
        /// Isolated from XUserController
        /// </summary>
        /// <param name="UserName"></param>
        /// <returns></returns>
        public RCV3WithValue<RCV3, Result<Bo.XUser>> GetUser(string UserName)
        {
            UserName = UserName.ToLower();
            var ret = RCV3WithValue<RCV3, Result<Bo.XUser>>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());
            try
            {
                var sql1 = new SQL<Bo.XUser>();
                var tabXUser = new Tables.DZA.XUserTab();

                // 1) Get DZA- UserID
                var qUserId = SelectFrom(sql1)
                    .Where(sql1.StrEq(tabXUser.UserName, sql1.Txt(UserName)))
                    .done();

                ret = GetRecord(qUserId);

            }
            catch (Exception ex)
            {
                ret = RCV3WithValue<RCV3, Result<Bo.XUser>>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }

        WhereBuilder<Bo.XUser> SelectFrom(SQL<Bo.XUser> sql1)
        {
            var tabXUser = new Tables.DZA.XUserTab();
            return sql1.Select(
                        sql1.Map(tabXUser.ID, (bo, v) => bo.ID = ColTool.GetSave(v, -1L)),
                        sql1.Map(tabXUser.UserName, (bo, v) => bo.Name = ColTool.GetSave(v, "")),
                        sql1.Map(tabXUser.FirstName, (bo, v) => bo.FirstName = ColTool.GetSave(v, "")),
                        sql1.Map(tabXUser.LastName, (bo, v) => bo.LastName = ColTool.GetSave(v, "")),
                        sql1.Map(tabXUser.Title, (bo, v) => bo.Title = ColTool.GetSave(v, "")),
                        sql1.Map(tabXUser.Language, (bo, v) => bo.Language = ColTool.GetSave(v, 0))
                    )
                    .From(tabXUser);
        }
    }
}
