using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using DFC.UpDowngrades;
using ATMO.mko.QueryBuilder;
using ATMO.mko.QueryBuilder.Results;
using ColTool = DFC3.DB.Tools.TabColAccess;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

using DFCObjects.Common.Impl;


namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 14.9.2018
    /// Bundels queries for common user managment tasks
    /// </summary>
    public class UserMgmt : QueriesBase
    {       

        public UserMgmt(IComposer pnL)
            : base(pnL)
        {   
        }

        public RCV3sV<Result<UserPersonalData>> GetUser(string UserId)
        {
            try
            {
                var sql = new SQL<DFCObjects.Common.Impl.UserPersonalData>();
                var tab = new Tables.User2Tab();

                var query = sql.Select(
                                sql.Map(tab.UserID, (bo, v) => bo.Username = ColTool.GetSave(v, "")),
                                sql.Map(tab.FirstName, (bo, v) => bo.FirstName = ColTool.GetSave(v, "")),
                                sql.Map(tab.LastName, (bo, v) => bo.LastName = ColTool.GetSave(v, "")),
                                sql.Map(tab.EMail, (bo, v) => bo.EMail = ColTool.GetSave(v, "")),
                                sql.Map(tab.Phone, (bo, v) => bo.Phone = ColTool.GetSave(v, "")),
                                sql.Map(tab.Department, (bo, v) => bo.Department = ColTool.GetSave(v, "")),
                                sql.Map(tab.DFC2CurrentUsedVersion, (bo, v) =>
                                {
                                    if (!string.IsNullOrWhiteSpace(ColTool.GetSave(v, "")))
                                    {
                                        var ver = (string)v;
                                        bo.DFC2CurrentUsedVersion = new VersionDescriptor(ver, false);
                                    }
                                    else
                                    {
                                        bo.DFC2CurrentUsedVersion = new VersionDescriptor();
                                    }
                                }),
                                sql.Map(tab.DFC2FixAssignedVersion, (bo, v) =>
                                {
                                    if (!string.IsNullOrWhiteSpace(ColTool.GetSave(v, "")))
                                    {
                                        var ver = (string)v;
                                        bo.DFC2FixAssignedVersion = new VersionDescriptor(ver, false);
                                    }
                                    else
                                    {
                                        bo.DFC2FixAssignedVersion = new VersionDescriptor();
                                    }
                                }),
                                sql.Map(tab.LastLogin, (bo, v) => bo.LastLogin = ColTool.GetSave(v, DateTime.MinValue)),
                                sql.Map(tab.CostCenter, (bo, v) => bo.CostCenter = ColTool.GetSave(v, ""))
                            ).From(tab)
                            .Where(sql.Eq(tab.UserID, sql.Txt(UserId.ToUpper())))
                            .done();

                var ret = GetRecord(query);

                if (ret.Succeeded)
                {
                    return RCV3sV<Result<DFCObjects.Common.Impl.UserPersonalData>>.Ok(ret.Value);
                }
                else
                {
                    return RCV3sV<Result<DFCObjects.Common.Impl.UserPersonalData>>.Failed(value: null, ErrorDescription: ret.MessageEntity);
                }
            }
            catch (Exception ex)
            {
                return RCV3sV<Result<DFCObjects.Common.Impl.UserPersonalData>>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }
        }


        /// <summary>
        /// mko, 25.10.2018
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RCV3sV<CustomerPersonalData > GetCustomer(string UserId)
        {
            var ret = RCV3sV<CustomerPersonalData>.Failed(value: null, ErrorDescription: pnL.eInfo("Nothing done"));

            try
            {
                var sql = new SQL<Bo.Customer>();
                var tab = new Tables.UserCustTab();

                // Define query and mappings from relational -> oo model
                var query = sql.Select(
                        sql.Map(tab.CustomerGroup, (bo, v) => bo.CustGroupId = ColTool.GetSave(v, "")),
                        sql.Map(tab.UserID, (bo, v) => bo.Username = ColTool.GetSave(v, "")),
                        sql.Map(tab.FirstName, (bo, v) => bo.FirstName = ColTool.GetSave(v, "")),
                        sql.Map(tab.LastName, (bo, v) => bo.LastName = ColTool.GetSave(v, "")),
                        sql.Map(tab.EMail, (bo, v) => bo.EMail = ColTool.GetSave(v, "")),
                        sql.Map(tab.Phone, (bo, v) => bo.Phone = ColTool.GetSave(v, "")),
                        sql.Map(tab.Department, (bo, v) => bo.Department = ColTool.GetSave(v, "")),
                        sql.Map(tab.CostCenter, (bo, v) => bo.CostCenter = ColTool.GetSave(v, "")),
                        sql.Map(tab.DFC2CurrentUsedVersion, (bo, v) =>
                        {
                            if (!string.IsNullOrWhiteSpace(ColTool.GetSave(v, "")))
                            {
                                var ver = (string)v;
                                bo.DFC2CurrentUsedVersion = new VersionDescriptor(ver, false);
                            }
                            else
                            {
                                bo.DFC2CurrentUsedVersion = new VersionDescriptor();
                            }
                        }),
                        sql.Map(tab.DFC2FixAssignedVersion, (bo, v) =>
                        {
                            if (!string.IsNullOrWhiteSpace(ColTool.GetSave(v, "")))
                            {
                                var ver = (string)v;
                                bo.DFC2FixAssignedVersion = new VersionDescriptor(ver, false);
                            }
                            else
                            {
                                bo.DFC2FixAssignedVersion = new VersionDescriptor();
                            }
                        }),
                        sql.Map(tab.LastLogin, (bo, v) => bo.LastLogin = ColTool.GetSave(v, DateTime.MinValue)),
                        sql.Map(tab.RequestCount, (bo, v) => bo.RequestCount = ColTool.GetSave(v, 0))                         

                    ).From(tab)
                    .Where(sql.Eq(tab.UserID, sql.Txt(UserId)))
                    .done();

                var getCust = GetRecords(query);

                if (!getCust.Succeeded)
                {
                    ret = RCV3sV<CustomerPersonalData>.Failed(value: null, plxResFactory.CreateQueryExecutionFailed(getCust.ToPlx()));
                }
                else if (getCust.Value.IsEmpty)
                {
                    ret = RCV3sV<CustomerPersonalData>.Failed(value: null, plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    // Make necessary post processing on results (because DB not in Normalform...)
                    // If more than one record is found for a customer, then the customer is assigned to multiple customer groups
                    // All customer groups ar collectes in a List inside resulting object
                    var cust = new CustomerPersonalData();

                    var first = getCust.Value.Entities.First();

                    cust.CostCenter = first.CostCenter;
                    cust.Department = first.Department;
                    cust.DFC2FixAssignedVersion = new VersionDescriptor() { Build = first.DFC2FixAssignedVersion.Build, IsBeta = first.DFC2FixAssignedVersion.IsBeta, MainVersion = first.DFC2FixAssignedVersion.MainVersion, SubVersion = first.DFC2FixAssignedVersion.SubVersion };
                    cust.EMail = first.EMail;
                    cust.FirstName = first.FirstName;
                    cust.LastLogin = first.LastLogin;
                    cust.LastName = first.LastName;
                    cust.Phone = first.Phone;
                    cust.RequestCount = first.RequestCount;
                    cust.Username = first.Username;
                    cust.CustomerGroups = getCust.Value.Entities.Select(r => new DFCObjects.Common.CustomerGroup(r.CustGroupId, "")).ToArray();

                    //ret = ATMOLog.RCV2<DFCObjects.Common.Impl.CustomerPersonalData>.Ok(cust);
                    ret = RCV3sV<CustomerPersonalData>.Ok(cust, plxResFactory.CreateQueryResultOk(1, pnL.EncapsulateAsPropertyValue(getCust.ToPlx())), getCust.User, getCust.InnerRC_T);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<CustomerPersonalData>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }

        /// <summary>
        /// mko, 14.9.2018
        /// Loads all DFC- Roles from DB thats are assigned to an ATMO employee.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RCV3sV<ResultSet<Bo.LongObj>> GetUserRoles(string UserId)
        {
            RCV3sV<ResultSet<Bo.LongObj>> ret = null;

            try
            {
                var sql = new SQL<Bo.LongObj>();
                var tab = new Tables.User2XRoleTab();

                var query = sql.Select(
                                sql.Map(tab.RoleID, (bo, v) =>
                                bo.Value = ColTool.GetSave(v, 0L)))
                                .From(tab)
                                .Where(sql.Eq(tab.UserID, sql.Txt(UserId.ToUpper())))
                                .done();

                var getRoles = GetRecords(query);

                if (getRoles.Succeeded)
                {
                    ret = RCV3sV<ResultSet<Bo.LongObj>>.Ok(value: getRoles.Value);
                }
                else
                {
                    ret = RCV3sV<ResultSet<Bo.LongObj>>.Failed(value: null, ErrorDescription: getRoles.MessageEntity);
                }

            }
            catch (Exception ex)
            {
                ret = RCV3sV<ResultSet<Bo.LongObj>>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }
    }
}
