using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using DFC.UpDowngrades;
using ATMO.mko.QueryBuilder;

using ColTool = DFC3.DB.Tools.TabColAccess;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;

using DFCObjects.Common.Impl;







namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko 26.10.2018
    /// 
    /// mko, 10.1.2018
    /// Erweitert um Zugriff auf Mengen von Benutzern, die Rollen zugeordnet sind. 
    /// Wird benötigt, um z.B. Listen in EDC/SFC- Dialogen aufzubauen.
    /// </summary>
    public class UserMgmtV18_10 : QueriesBase
    {
        public UserMgmtV18_10(IComposer pnL)
            : base(pnL)
        {
        }

        CMap<DFCObjects.Common.Impl.UserPersonalDataV18_10>[] User2Mapping(SQL<UserPersonalDataV18_10> sql, Tables.User2Tab tab)
        {
            return new CMap<DFCObjects.Common.Impl.UserPersonalDataV18_10>[]
            {
                sql.Map(tab.UserID.FQN, (bo, v) => bo.Username = ColTool.GetSave(v, "")),
                sql.Map(tab.FirstName.FQN, (bo, v) => bo.FirstName = ColTool.GetSave(v, "")),
                sql.Map(tab.LastName.FQN, (bo, v) => bo.LastName = ColTool.GetSave(v, "")),
                sql.Map(tab.EMail.FQN, (bo, v) => bo.EMail = ColTool.GetSave(v, "")),
                sql.Map(tab.Phone.FQN, (bo, v) => bo.Phone = ColTool.GetSave(v, "")),
                sql.Map(tab.Department.FQN, (bo, v) => bo.Department = ColTool.GetSave(v, "")),
                sql.Map(tab.DFC2CurrentUsedVersion.FQN, (bo, v) =>
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
                sql.Map(tab.DFC2FixAssignedVersion.FQN, (bo, v) =>
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
                sql.Map(tab.LastLogin.FQN, (bo, v) => bo.LastLogin = ColTool.GetSave(v, System.DateTime.MinValue)),
                sql.Map(tab.CostCenter.FQN, (bo, v) => bo.CostCenter = ColTool.GetSave(v, "")),
                sql.Map(tab.RASUserId.FQN, (bo, v) => bo.RASUserId = ColTool.GetSave(v, "")),

                // mko, 16.11.2018
                sql.Map(tab.Disabled.FQN, (bo, v) => bo.Disabled = !string.IsNullOrWhiteSpace(ColTool.GetSave(v, "")))

            };
        }

        /// <summary>
        /// mko, 26.10.2018
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="IsRasId">f true, usrerId is a RAS- ID</param>
        /// <returns></returns>
        public RCV3sV<UserPersonalDataV18_10> GetUser(string UserId, bool IsRasId = false)
        {
            try
            {
                var sql = new SQL<UserPersonalDataV18_10>();
                var tab = new Tables.User2Tab();

                var query = sql.Select(
                                User2Mapping(sql, tab)
                            ).From(tab)
                            .Where(
                                sql.And(
                                    (IsRasId ? sql.StrEq(tab.RASUserId, sql.Txt(UserId)) : sql.StrEq(tab.UserID, sql.Txt(UserId))),
                                    // mko, 27.11.2018
                                    // Nur nicht- deaktivierte Benutzer laden
                                    sql.IsNull(tab.Disabled)))
                            .done();

                var ret = GetRecord(query);

                if (!ret.Succeeded)
                {
                    return RCV3sV<UserPersonalDataV18_10>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
                }
                else if (ret.Succeeded && ret.Value.IsEmpty)
                {
                    return RCV3sV<UserPersonalDataV18_10>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    return RCV3sV<UserPersonalDataV18_10>.Ok(value: ret.Value.Entity, Message: plxResFactory.CreateQueryResultOk(1, pnL.EncapsulateAsPropertyValue(ret.ToPlx())));
                }
            }
            catch (Exception ex)
            {
                return RCV3sV<UserPersonalDataV18_10>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }
        }


        /// <summary>
        /// mko, 25.10.2018
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RCV3sV<CustomerPersonalDataV18_10> GetCustomer(string UserId, bool IsRasId = false)
        {
            var ret = RCV3sV<CustomerPersonalDataV18_10>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

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
                        sql.Map(tab.LastLogin, (bo, v) => bo.LastLogin = ColTool.GetSave(v, System.DateTime.MinValue)),
                        sql.Map(tab.RequestCount, (bo, v) => bo.RequestCount = ColTool.GetSave(v, 0)),
                        sql.Map(tab.RASUserId, (bo, v) => bo.RASUserId = ColTool.GetSave(v, ""))

                    ).From(tab)
                    .Where(IsRasId ? sql.StrEq(tab.RASUserId, sql.Txt(UserId)) : sql.StrEq(tab.UserID, sql.Txt(UserId)))
                    .done();

                var getCust = GetRecords(query);

                if (!getCust.Succeeded)
                {
                    ret = RCV3sV<CustomerPersonalDataV18_10>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getCust.ToPlx()));
                }
                else if (getCust.Value.IsEmpty)
                {
                    ret = RCV3sV<CustomerPersonalDataV18_10>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    // Make necessary post processing on results (because DB not in Normalform...)
                    // If more than one record is found for a customer, then the customer is assigned to multiple customer groups
                    // All customer groups ar collectes in a List inside resulting object
                    var cust = new CustomerPersonalDataV18_10();

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

                    var custGrpQ = new Queries.CustGroupsQueries(pnL);

                    cust.CustomerGroups = getCust.Value.Entities
                                                .Select(r => new DFCObjects.Common.CustomerGroup(r.CustGroupId,
                                                                                                 custGrpQ.GetDescriptionIntern(r.CustGroupId))).ToArray();

                    //ret = ATMOLog.RCV2<DFCObjects.Common.Impl.CustomerPersonalData>.Ok(cust);
                    ret = RCV3sV<CustomerPersonalDataV18_10>.Ok(cust, plxResFactory.CreateQueryResultOk(1, pnL.EncapsulateAsPropertyValue(getCust.ToPlx())), getCust.User, getCust.InnerRC_T);
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<CustomerPersonalDataV18_10>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }


        /// <summary>
        /// mko, 14.9.2018
        /// Loads all DFC- Roles from DB thats are assigned to an ATMO employee.
        /// 
        /// mko, 26.10.2018
        /// Return Type replaced bey RCV3[long]
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<long>> GetRolesOfUser(string UserId)
        {
            RCV3sV<IEnumerable<long>> ret = null;

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

                if (!getRoles.Succeeded)
                {
                    ret = RCV3sV<IEnumerable<long>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getRoles.ToPlx()));
                }
                else if (getRoles.Succeeded && getRoles.Value.IsEmpty)
                {
                    // mko, 3.12.2018
                    // Wenn keine Rollen vergeben wurden, dann nicht mehr Fehler melden, sonden qualifizierte Rückmeldung
                    //ret = RCV3sV<IEnumerable<long>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
                    ret = RCV3sV<IEnumerable<long>>.Ok(value: null, Message: plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    ret = RCV3sV<IEnumerable<long>>.Ok(
                        value: getRoles.Value.Entities.Select(r => r.Value), 
                        Message: plxResFactory.CreateQueryResultOk(getRoles.Value.Entities.Count(), pnL.EncapsulateAsPropertyValue(getRoles.ToPlx())));
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<IEnumerable<long>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }

            return ret;
        }


        /// <summary>
        /// mko, 10.1.2018
        /// Baut rekursiv einen Filterterm für einen Set von Rollen auf.
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="tabRoles"></param>
        /// <param name="roles"></param>
        /// <returns></returns>
        IColXpr OrNextRole(SQL<UserPersonalDataV18_10> sql, Tables.User2XRoleTab tabRoles, IEnumerable<DFCSecurity.V1.PredefinedRolesV02.RoleNo> roles)
        {
            return roles.Any() ? sql.Or(
                                    sql.Eq(tabRoles.RoleID.FQN, sql.Long((long)roles.First())),
                                    OrNextRole(sql, tabRoles, roles.Skip(1))) : (IColXpr)sql.Nop();
        }

        /// <summary>
        /// mko, 10.1.2018
        /// Liefert alle User, die in gegebenen Rollen definiert sind.
        /// </summary>
        /// <param name="roleSet"></param>
        /// <returns></returns>
        public RCV3sV<IEnumerable<DFCObjects.Common.Impl.UserPersonalDataV18_10>> GetUsersInRoles(IEnumerable<DFCSecurity.V1.PredefinedRolesV02.RoleNo> roleSet)
        {
            try
            {
                var sql = new SQL<UserPersonalDataV18_10>();
                var tabUsr2 = new Tables.User2Tab();
                var tabRoles = new Tables.User2XRoleTab();

                var query = sql.Select(
                                User2Mapping(sql, tabUsr2)
                            ).From(tabRoles, tabUsr2)
                            .Where(
                                sql.And(
                                        sql.Eq(tabRoles.UserID.FQN, tabUsr2.UserID.FQN),
                                        OrNextRole(sql, tabRoles, roleSet)
                                    ))
                            .done();

                var ret = GetRecords(query);

                if (!ret.Succeeded)
                {
                    return RCV3sV<IEnumerable<UserPersonalDataV18_10>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(ret.ToPlx()));
                }
                else if (ret.Succeeded && ret.Value.IsEmpty)
                {
                    return RCV3sV<IEnumerable<UserPersonalDataV18_10>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    return RCV3sV<IEnumerable<UserPersonalDataV18_10>>.Ok(value: ret.Value.Entities, Message: plxResFactory.CreateQueryResultOk(1, pnL.EncapsulateAsPropertyValue(ret.ToPlx())));
                }
            }
            catch (Exception ex)
            {
                return RCV3sV<IEnumerable<UserPersonalDataV18_10>>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(TraceHlp.FlattenExceptionMessagesPN(ex)));
            }
        }

        /// <summary>
        /// mko, 2.8.2019
        /// Nach einer Konvention werden Personennamen bei Bosch in folgender 
        /// Form abgelegt:
        /// {PARTIKEL_NAME}+ VORNAME [(ABTEILUNGSBEZEICHNUNG)]
        /// 
        /// mko, 6.8.2019
        /// Unter einem Name+Vorname kann es mehrere Personen geben. In diesem Fall wird wie folgt aufgelöst:
        /// a) Wenn eine Abteilungsbezeichnung existiert und der Abteilungsbezeichnung ist eine
        ///    Person mit Name+Vorname zugeordnet-> Treffer
        /// b) Sonst: Alle Personen mit Name+vorname werden zurückgeliefert
        /// 
        /// </summary>
        /// <param name="boschFirstLastnameStr"></param>
        /// <returns></returns>
        public RCV3sV<DFCObjects.Common.MemberOfDepartment[]> ParseFromBoschFirstLastnameString(string boschFirstLastnameStr, bool IsCustomer = false)
        {
            var ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Failed(null, pnL.ReturnNotCompleted("ParseFromBoschFirstLastnameString", pnL.p("BoschFirstLastName", boschFirstLastnameStr)));

            try
            {
                boschFirstLastnameStr = boschFirstLastnameStr.Trim();

                if (string.IsNullOrEmpty(boschFirstLastnameStr))
                {
                    ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Failed(null, pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(TechTerms.MetaData.Arg, "BoschFirstLastName")));
                }
                else
                {
                    var NameParts = boschFirstLastnameStr.Split(new char[] { ' ', '\t', '\n' }, StringSplitOptions.RemoveEmptyEntries);

                    // Es müssen mindestens Name und Vorname definiert sein
                    if (NameParts.Length < 2)
                    {
                        ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Failed(null,
                            pnL.ReturnValidatePreconditionFailed(
                                pnL.m("CheckIfBoschFirstLastNameConsistsAtMinimumOfFistAndLastname",
                                    pnL.p("BoschFirstLastName", boschFirstLastnameStr))));
                    }
                    else
                    {
                        var posFirstName = NameParts.Length - 1;

                        // Methode für den Zugriff auf die Department- Id, falls vorhanden.
                        Func<string> GetDeptIdFromNameParts = () => NameParts.Last().Substring(1, NameParts.Last().Length - 2).Trim().ToUpper();

                        var hasDepartmentId = System.Text.RegularExpressions.Regex.IsMatch(NameParts.Last(), @"^\(.*\)$");
                        if (hasDepartmentId)
                        {
                            posFirstName -= 1;
                        }

                        var FirstName = NameParts[posFirstName];
                        var LastName = string.Join(" ", NameParts.Take(posFirstName)).ToLower();


                        if (IsCustomer)
                        {
                            // Nachschalgen in der Kundentabelle

                            var sql = new SQL<CustomerPersonalDataV18_10>();
                            var tab = new Tables.UserCustTab();


                            var q = sql.Select(
                                    sql.Map(tab.UserID, (bo, v) => bo.Username = ColTool.GetSave(v, "")),
                                    sql.Map(tab.Department, (bo, v) => bo.Department = ColTool.GetSave(v, "").Trim().ToUpper())
                                )
                                .From(tab)
                                .Where(sql.And(
                                        sql.StrEq(tab.FirstName, sql.Txt(FirstName)),
                                        sql.StrEq(tab.LastName, sql.Txt(LastName))
                                    ))
                                .done();

                            var getUserId = GetRecords(q);

                            if (!getUserId.Succeeded)
                            {
                                ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Failed(null, plxResFactory.CreateQueryExecutionFailed(getUserId.ToPlx()));
                            }
                            else if (getUserId.Value.IsEmpty)
                            {
                                ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Failed(null, plxResFactory.CreateQueryResultEmpty(pnL.i(TechTerms.Search.iFilter, pnL.p("FirstName", FirstName), pnL.p("LastName", LastName))));
                            }
                            else
                            {

                                var result = getUserId.Value.Entities;

                                // Kann über die Abteilung eingeschränkt werden ?
                                if (getUserId.Value.Entities.Count() > 1 && hasDepartmentId)
                                {
                                    // Abteilungsbezeichnung von runden Klammern befreien
                                    var DeptId = GetDeptIdFromNameParts();

                                    if (getUserId.Value.Entities.Any(r => r.Department == DeptId))
                                    {
                                        result = result.Where(r => r.Department == DeptId);
                                    }
                                }

                                ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Ok(
                                        result.Select(r =>
                                                new DFCObjects.Common.MemberOfDepartment()
                                                {
                                                    // Achtung: Bo, geladen aus der DB hat für folgende Eigenschften keine Einträge
                                                    //          -> die geparsten Werte werden genommen                                                    
                                                    FirstName = FirstName,
                                                    LastName = LastName,

                                                    Username = r.Username,
                                                    Department = r.Department
                                                })
                                                .ToArray());
                            }
                        }
                        else
                        {
                            // Nachschlagen nach dem User in der User_Cust- Tabelle 

                            var sql = new SQL<UserPersonalDataV18_10>();
                            var tab = new Tables.User2Tab();


                            var q = sql.Select(
                                    sql.Map(tab.UserID, (bo, v) => bo.Username = ColTool.GetSave(v, "")),
                                    sql.Map(tab.Department, (bo, v) => bo.Department = ColTool.GetSave(v, "").Trim().ToUpper()),
                                    sql.Map(tab.EMail, (bo, v) => bo.EMail = ColTool.GetSave(v, "").Trim().ToLower())
                                )
                                .From(tab)
                                .Where(sql.And(
                                        sql.StrEq(tab.FirstName, sql.Txt(FirstName)),
                                        sql.StrEq(tab.LastName, sql.Txt(LastName))
                                    ))
                                .done();

                            var getUserId = GetRecords(q);

                            if (!getUserId.Succeeded)
                            {
                                ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Failed(null, plxResFactory.CreateQueryExecutionFailed(getUserId.ToPlx()));
                            }
                            else if (getUserId.Value.IsEmpty)
                            {
                                ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Failed(null, plxResFactory.CreateQueryResultEmpty(pnL.i(TechTerms.Search.iFilter, pnL.p("FirstName", FirstName), pnL.p("LastName", LastName))));
                            }
                            else
                            {
                                var result = getUserId.Value.Entities;

                                // Kann über die Abteilung eingeschränkt werden ?
                                if (getUserId.Value.Entities.Count() > 1 && hasDepartmentId)
                                {
                                    // Abteilungsbezeichnung von runden Klammern befreien
                                    var DeptId = GetDeptIdFromNameParts();

                                    if (getUserId.Value.Entities.Any(r => r.Department == DeptId))
                                    {
                                        result = result.Where(r => r.Department == DeptId);
                                    }
                                }

                                ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Ok(
                                            result.Select(r =>
                                            new DFCObjects.Common.MemberOfDepartment()
                                            {
                                                // Achtung: Bo, geladen aus der DB hat für folgende Eigenschften keine Einträge
                                                //          -> die geparsten Werte werden genommen                                                    
                                                FirstName = FirstName,
                                                LastName = LastName,

                                                Username = r.Username,
                                                Department = r.Department,
                                                EMail = r.EMail
                                            })
                                            .ToArray());
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<DFCObjects.Common.MemberOfDepartment[]>.Failed(null, TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }



    }
}
