using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Text.RegularExpressions;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using ATMO.mko.QueryBuilder;

using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.DocuEntityHlp;

using ColTool = DFC3.DB.Tools.TabColAccess;

using ANC = ATMO.DFC.Naming;

namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 7.12.2018
    /// 
    /// mko, 19.12.2019
    /// Abfragen für Installationsnummer von DFC Installer integriert
    /// </summary>
    public class MasterSQL 
        : QueriesBase

    {
        public MasterSQL(IComposer pnL)
            : base(pnL)
        {
            tab = new Tables.Master();
            qResFactory = new ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory(pnL);
        }

        Tables.Master tab;

        ATMO.mko.QueryBuilder.Results.PlxQueryResultDescriptionFactory qResFactory;

        /// <summary>
        /// mko, 19.12.2017
        /// 
        /// mko, 7.12.2018
        /// aus DFCObjects.DB.DBMethods hierher kopiert und angepasst.
        /// </summary>
        /// <param name="verStr"></param>
        /// <param name="defaultVersion"></param>
        /// <returns>
        ///     ret.MessageEntity:
        /// 
        ///         - Version erfolgreich geparst
        ///         #i FinStateDescr #m Create #ret #e succeeded
        /// 
        ///         - Versionsnummer ist leer
        ///         #i FinStateDescr #m IsNotNullOrWhitespace #ret #e fails    
        ///         
        ///         - Versionsnummer ist nicht nach folgendem Schema aufgebaut: \d+\.\d+\.\d+
        ///         #i FinStateDescr #m MatchVersionNumberPattern #ret #e fails
        ///         
        ///         - Hautpversion ist keine ganze Zahl
        ///         #i FinStateDescr #m ParseMainVersion #ret #e fails
        ///         
        ///         - Unterversion ist keine ganze Zahl
        ///         #i FinStateDescr #m ParseSubVersion #ret #e fails
        ///
        ///         - Build ist keine ganze Zahl
        ///         #i FinStateDescr #m ParseBulidVersion #ret #e fails
        /// 
        /// </returns>
        private RCV3sV<DFC.UpDowngrades.VersionDescriptor> CreateVersionDescriptor(string verStr, DFC.UpDowngrades.IVersionDescriptor defaultVersion = null)
        {
            var rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: pnL.ReturnNotCompleted("CreateVersionDescriptor"));
            
            if (string.IsNullOrWhiteSpace(verStr))
            {
                rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                    value: null,
                    //ErrorDescription: pnL.i(Composer.TechTerms.FinStateDescr, pnL.m("IsNotNullOrWhitespace", pnL.ret(pnL.eFails()))));
                    ErrorDescription: pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(ANC.DocuTerms.MetaData.Arg.UID, "verStr")));
            }
            else
            {
                var mtchs = Regex.Matches(verStr, @"\d+\.\d+\.\d+");

                if (mtchs.Count != 1)
                {
                    rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                        value: null,
                        ErrorDescription:
                        //pnL.i(Composer.TechTerms.FinStateDescr, pnL.m("MatchVersionNumberPattern", pnL.p("verStr", pnL.txt(verStr)), pnL.ret(pnL.eFails(pnL.txt("verStr don't matches a valid version number"))))));
                        pnL.ReturnValidatePreconditionFailed(
                            pnL.m(ANC.TechTerms.PatternMatching.mTestIfMatch.UID,
                                pnL.p(ANC.DocuTerms.MetaData.Arg.UID, "verStr"),
                                pnL.p(ANC.TechTerms.PatternMatching.pRegularExpression.UID, @"\d+\.\d+\.\d+"))));
                }
                else
                {
                    verStr = mtchs[0].Value;
                    var verParts = verStr.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                    if (int.TryParse(verParts[0], out var MainVersion))
                    {
                        if (int.TryParse(verParts[1], out var SubVersion))
                        {
                            if (int.TryParse(verParts[2], out var Build))
                            {
                                var vd = new DFC.UpDowngrades.VersionDescriptor()
                                {
                                    MainVersion = MainVersion,
                                    SubVersion = SubVersion,
                                    Build = Build
                                };

                                vd.IsBeta = !defaultVersion?.Equ(vd) ?? false;

                                rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(
                                    value: vd,
                                    //Message: pnL.i(Composer.TechTerms.FinStateDescr, pnL.m("Create", pnL.ret(pnL.eSucceeded()))));
                                    Message: pnL.ReturnAfterSuccess("Create", pnL.p("verStr", verStr)));
                            }
                            else
                            {
                                rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                                    value: null,
                                    //ErrorDescription: pnL.i(Composer.TechTerms.FinStateDescr, pnL.m("ParseBuild", pnL.p("verStr", pnL.txt(verStr)), pnL.ret(pnL.eFails()))));
                                    ErrorDescription: pnL.ReturnAfterFailure("TryParseBuild", pnL.p("verStr", verStr)));
                            }
                        }
                        else
                        {
                            rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                                value: null,
                                ErrorDescription: pnL.ReturnAfterFailure("TryParseSubVersion", pnL.p("verStr", verStr)));
                                //pnL.i(Composer.TechTerms.FinStateDescr, pnL.m("ParseSubVersion", pnL.p("verStr", pnL.txt(verStr)), pnL.ret(pnL.eFails()))));
                        }
                    }
                    else
                    {
                        rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                            value: null,
                            ErrorDescription: pnL.ReturnAfterFailure("ParseMainVersion", pnL.p("verStr", verStr)));
                            //pnL.i(Composer.TechTerms.FinStateDescr, pnL.m("ParseMainVersion", pnL.p("verStr", pnL.txt(verStr)), pnL.ret(pnL.eFails()))));
                    }
                }
            }

            return rc;
        }



        /// <summary>
        /// mko, 7.12.2018
        /// Ermittelt die aktuell definierte Standardversion von DFC2
        /// </summary>
        /// <returns></returns>
        public RCV3sV<DFC.UpDowngrades.VersionDescriptor> GetDfcDefaultVersion()
        {
            try
            {

                var sql = new SQL<DFC.UpDowngrades.VersionDescriptor>();

                var q = sql.Select(
                        sql.Map(tab.Wert, (bo, v) =>
                        {
                            var vStr = ColTool.GetSave(v, "");

                            var rc = CreateVersionDescriptor(vStr);
                            if (rc.Succeeded)
                            {
                                bo.MainVersion = rc.Value.MainVersion;
                                bo.SubVersion = rc.Value.SubVersion;
                                bo.Build = rc.Value.Build;
                            }
                            else
                            {
                                throw new InvalidCastException(rc.Message);
                            }
                        }
                    ))
                    .From(tab)
                    .Where(sql.StrEq(tab.Operand, sql.Txt("DFC2_VersionDefault_UpDown")))
                    .done();

                var getRec = GetRecord(q);

                if (!getRec.Succeeded)
                {
                    return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getRec.ToPlx()));
                }
                else if (getRec.Succeeded && getRec.Value.IsEmpty)
                {
                    return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(value: getRec.Value.Entity, Message: plxResFactory.CreateQueryResultOk(1, pnL.EncapsulateAsPropertyValue(getRec.ToPlx())));
                }
            }
            catch (Exception ex)
            {
                return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }
        }

        /// <summary>
        /// mko, 12.1.2021
        /// Bestimmt die aktuell gültige Version für den TEF- Client
        /// </summary>
        /// <returns></returns>
        public RCV3sV<DFC.UpDowngrades.VersionDescriptor> GetDfcTefDefaultVersion()
        {
            try
            {

                var sql = new SQL<DFC.UpDowngrades.VersionDescriptor>();

                var q = sql.Select(
                        sql.Map(tab.Wert, (bo, v) =>
                        {
                            var vStr = ColTool.GetSave(v, "");

                            var rc = CreateVersionDescriptor(vStr);
                            if (rc.Succeeded)
                            {
                                bo.MainVersion = rc.Value.MainVersion;
                                bo.SubVersion = rc.Value.SubVersion;
                                bo.Build = rc.Value.Build;
                            }
                            else
                            {
                                throw new InvalidCastException(rc.Message);
                            }
                        }
                    ))
                    .From(tab)
                    .Where(sql.StrEq(tab.Operand, sql.Txt("DFC2_TEF_VersionsAllowed")))
                    .done();

                var getRec = GetRecord(q);

                if (!getRec.Succeeded)
                {
                    return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getRec.ToPlx()));
                }
                else if (getRec.Succeeded && getRec.Value.IsEmpty)
                {
                    return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(value: getRec.Value.Entity, Message: plxResFactory.CreateQueryResultOk(1, pnL.EncapsulateAsPropertyValue(getRec.ToPlx())));
                }
            }
            catch (Exception ex)
            {
                return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }
        }



        /// <summary>
        /// mko, 7.12.2018
        /// Liefert true, wenn die Version auf die schwarze Liste gesetzt wurde.
        /// </summary>
        /// <param name="version"></param>
        /// <returns></returns>
        public RCV3sV<bool> IsBlackListedVersion(DFC.UpDowngrades.VersionDescriptor version)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// mko, 7.12.2018
        /// Liefert die für den Benutzer aktuell gültige DFC- Version. prüft, ob diese Version auf die schwarze Liste gesetzt wurde.
        /// Wenn ja, dann wird dies durch eine Fehlermeldung dokumentiert.
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RCV3sV<DFC.UpDowngrades.VersionDescriptor> GetSupportedDFCVersionFor(string UserId)
        {
            var ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: pnL.i(Composer.TechTerms.FinStateDescr, pnL.eNotCompleted()));

            try
            {
                // 1. Standardversion bestimmen
                var getDefaultVersion = GetDfcDefaultVersion();

                if (!getDefaultVersion.Succeeded)
                {
                    ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                        value: null, 
                        ErrorDescription: qResFactory.CreateQueryExecutionFailed(pnL.m("GetDefaultVersion", pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getDefaultVersion.ToPlx()))))));
                }
                else
                {
                    // 2. Zuerst annehmen, User ist ein Kunde (schwächere Rechte). Wenn ja, für diesen die Version bestimmen

                    var sql = new SQL<Bo.Customer>();
                    var tabCust = new Tables.UserCustTab();
                    var getFixed = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());
                    var getCurrentUsed = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

                    var q = sql.Select(
                            sql.Map(tabCust.DFC2FixAssignedVersion, (bo, v) =>
                            {
                                var verStr = ColTool.GetSave(v, "");
                                getFixed = CreateVersionDescriptor(verStr, getDefaultVersion.Value);
                            }),
                            sql.Map(tabCust.DFC2CurrentUsedVersion, (bo, v) =>
                            {
                                var verStr = ColTool.GetSave(v, "");
                                getCurrentUsed = CreateVersionDescriptor(verStr, getDefaultVersion.Value);
                            })
                        )
                        .From(tabCust)
                        .Where(sql.StrEq(tabCust.UserID, sql.Txt(UserId)))
                        .done();

                    var getCustomer = GetRecord(q);

                    if (!getCustomer.Succeeded)
                    {
                        // Die Ausführung der Abfrage ist gescheitert
                        ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: qResFactory.CreateQueryExecutionFailed(getCustomer.ToPlx()));
                    }
                    else if (!getCustomer.Value.IsEmpty)
                    {
                        // Ein Kunde liegt vor

                        // prüfen, ob dem Kunden eine Version fest zugewiesen wurde
                        if (getFixed.Succeeded)
                        {
                            // Ja, dem Kunden wurde eine Version fest zugewiesen
                            ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(value: getFixed.Value, Message: qResFactory.CreateQueryResultOk(1));
                        }
                        else
                        {
                            var deco = new DocuEntityLinqDeco(getFixed.MessageEntity);
                            deco = deco.IsInstance(Composer.TechTerms.FinStateDescr) ? deco : new DocuEntityLinqDeco(deco.FindNamedEntity(DocuEntityTypes.Instance, Composer.TechTerms.FinStateDescr));

                            if (deco.Methods.Any(m => m.IsNamed("IsNotNullOrWhitespace"))
                                && deco.Methods.First(m => m.IsNamed("IsNotNullOrWhitespace")).Returns.First().Events.Any(e => e.IsNamed(Composer.TechTerms.eFails)))
                            {
                                // Das Feld für die fest zugewiesene Versionsnummer ist leer. -> Keine Version wurde dem Kunden fest zugewiesen
                                // Die Standardversion ist gültig
                                ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(value: getDefaultVersion.Value, Message: qResFactory.CreateQueryResultOk(1));
                            }
                            else
                            {
                                // Beim Parsen der fest zugewiesenen Versionsnummer trat ein Fehler auf
                                ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                                    value: null, 
                                    ErrorDescription: qResFactory.CreateQueryExecutionFailed(pnL.m("GetfixedVersion", pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getFixed.ToPlx()))))));
                            }
                        }
                    }
                    else
                    {
                        // 3. Dann annehmen, User ist ein Mitarbeiter. Version bestimmen
                        var sql2 = new SQL<DFCObjects.Common.Impl.UserPersonalDataV18_10>();
                        var tabUser2 = new Tables.User2Tab();
                        getFixed = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());
                        getCurrentUsed = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: pnL.eNotCompleted());

                        var q2 = sql.Select(
                                sql.Map(tabUser2.DFC2FixAssignedVersion, (bo, v) =>
                                {
                                    var verStr = ColTool.GetSave(v, "");
                                    getFixed = CreateVersionDescriptor(verStr, getDefaultVersion.Value);
                                }),
                                sql.Map(tabUser2.DFC2CurrentUsedVersion, (bo, v) =>
                                {
                                    var verStr = ColTool.GetSave(v, "");
                                    getCurrentUsed = CreateVersionDescriptor(verStr, getDefaultVersion.Value);
                                })
                            )
                            .From(tabUser2)
                            .Where(sql.StrEq(tabUser2.UserID, sql.Txt(UserId)))
                            .done();

                        var getUser = GetRecord(q2);

                        if (!getUser.Succeeded)
                        {
                            // Die Ausführung der Abfrage ist gescheitert
                            ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: qResFactory.CreateQueryExecutionFailed(getUser.ToPlx()));
                        }
                        else if (!getUser.Value.IsEmpty)
                        {
                            // Ein Mitarbeiter liegt vor

                            // prüfen, ob dem Mitarbeiter eine Version fest zugewiesen wurde
                            if (getFixed.Succeeded)
                            {
                                // Ja, dem Mitarbiter wurde eine Version fest zugewiesen
                                ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(value: getFixed.Value, Message: qResFactory.CreateQueryResultOk(1));
                            }
                            else
                            {
                                var deco = new DocuEntityLinqDeco(getFixed.MessageEntity);
                                deco = deco.IsInstance(Composer.TechTerms.FinStateDescr) ? deco : new DocuEntityLinqDeco(deco.FindNamedEntity(DocuEntityTypes.Instance, Composer.TechTerms.FinStateDescr));

                                if (deco.Methods.Any(m => m.IsNamed("IsNotNullOrWhitespace"))
                                && deco.Methods.First(m => m.IsNamed("IsNotNullOrWhitespace")).Returns.First().Events.Any(e => e.IsNamed(Composer.TechTerms.eFails)))
                                {
                                    // Das Feld für die fest zugewiesene Versionsnummer ist leer. -> Keine Version wurde dem Mitarbeiter fest zugewiesen
                                    // Die Standardversion ist gültig
                                    ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(value: getDefaultVersion.Value, Message: qResFactory.CreateQueryResultOk(1));
                                }
                                else
                                {
                                    // Beim Parsen der fest zugewiesenen Versionsnummer trat ein Fehler auf
                                    ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                                        value: null, 
                                        ErrorDescription: qResFactory.CreateQueryExecutionFailed(pnL.m("GetfixedVersion", pnL.ret(pnL.eFails(pnL.EncapsulateAsEventParameter(getFixed.ToPlx()))))));
                                }
                            }
                        }
                        else
                        {
                            // 4. Weder Kunde noch Mitarbeiter: Fehlermeldung zurückgeben

                            ret = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                                value: null, 
                                ErrorDescription: qResFactory.CreateQueryExecutionFailed(
                                                    pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(UserId)));
                        }
                    }

                    // 5. Prüfen, ob Version in BlackList gelistet ist. Wenn ja, dann Fehlermeldung zurückgeben.

                }
            }
            catch (Exception ex)
            {
                return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
        }

        /// <summary>
        /// mko, 23.10.2019
        /// Liest aus der DFC- Mastertabelle den aktuellen Systemzustand aus
        /// </summary>
        /// <returns></returns>
        public RCV3sV<Bo.DFCSystemStatusBo> GetDFCSystemStatus()
        {
            try
            {
                var sql = new SQL<Bo.DFCSystemStatusBo>();

                var q = sql.Select(
                        sql.Map(tab.Wert, (bo, v) =>
                        {
                            // Statuswert parsen

                            var txt = ColTool.GetSave(v, "ok");

                            try
                            {
                                bo.Status = (SystemStatus)Enum.Parse(typeof(SystemStatus), txt, true);
                            }
                            catch (Exception)
                            {
                                // Wenn das Statusfeld nicht gelesen werden kann, dann einen den 
                                // Systemstatus auf Fehler setzen
                                bo.Status = SystemStatus.errors;
                            }
                        }),
                        sql.Map(tab.Description, (bo, v) =>
                        {
                            // Beschreibung des aktuellen Systemzustandes einlesen

                            bo.DetailsString = ColTool.GetSave(v, "");
                            bo.AreDetailsInPNFormat = false;

                            // versuchen, die Details als Strukturierten Dokuterm zu parsen
                            var getPN = ATMO.mko.Logging.PNDocuTerms.Parser.Parser.Parse20_06(bo.DetailsString, ATMO.mko.Logging.PNDocuTerms.Fn._, pnL, false);
                            if (getPN.Succeeded)
                            {
                                bo.AreDetailsInPNFormat = true;
                                bo.DetailsPN = getPN.Value;
                            }
                        })
                    )
                    .From(tab)
                    .Where(sql.StrEq(tab.Operand, sql.Txt("DFC_SYSTEMSTATUS")))
                    .done();

                var getRec = GetRecord(q);

                if (!getRec.Succeeded)
                {
                    return RCV3sV<Bo.DFCSystemStatusBo>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getRec.ToPlx()));
                }
                else if (getRec.Succeeded && getRec.Value.IsEmpty)
                {
                    return RCV3sV<Bo.DFCSystemStatusBo>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty());
                }
                else
                {
                    return RCV3sV<Bo.DFCSystemStatusBo>.Ok(value: getRec.Value.Entity, Message: plxResFactory.CreateQueryResultOk(1, pnL.EncapsulateAsPropertyValue(getRec.ToPlx())));
                }
            }
            catch (Exception ex)
            {
                return RCV3sV<Bo.DFCSystemStatusBo>.Failed(value: null, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }

        }

        const string VersionFieldForDfcInstaller = "DFC2_UpDowngradeTool_Version";

        /// <summary>
        /// mko, 19.12.2019
        /// 
        /// Aus DBMethods portiert.
        /// Abfrage der aktuell freigeschalteten DFC.Updowngrades.cmd Installation.
        /// </summary>
        /// <returns></returns>
        public RCV3sV<DFC.UpDowngrades.VersionDescriptor> GetDfcInstallerVersionSupported()
        {
            var rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, pnL.eNotCompleted());

            try
            {
                // first: get the default version
                // no user/version mapping exists: default version is the supported version for user

                var sql = new SQL<Bo.StringObj>();

                var q = sql.Select(
                        sql.Map(tab.Wert, (bo, v) =>
                        {
                            bo.Value = ColTool.GetSave(v, "0.0.0");
                        })
                    )
                    .From(tab)
                    .Where(
                        sql.StrEq(tab.Operand, sql.Txt(VersionFieldForDfcInstaller))
                    )
                    .done();

                var getRec = GetRecord(q);

                if (!getRec.Succeeded)
                {
                    return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryExecutionFailed(getRec.ToPlx()));
                }
                else if (getRec.Value.IsEmpty)
                {
                    return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: plxResFactory.CreateQueryResultEmpty(pnL.EncapsulateAsPropertyValue(getRec.ToPlx())));
                }
                else
                {
                    var getVer = CreateVersionDescriptor(getRec.Value.Entity.Value, pnL);

                    if (getVer.Succeeded)
                    {
                        return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(getVer.Value);
                    }
                    else
                    {
                        return RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, ErrorDescription: getVer.ToPlx());
                    }                    
                }
            }
            catch (Exception ex)
            {
                rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex), value: null);
            }

            return rc;
        }

        /// <summary>
        /// mko, 19.12.2017
        /// </summary>
        /// <param name="UserName"></param>
        /// <param name="rc"></param>
        /// <param name="verStr"></param>
        /// <returns></returns>
        private static RCV3sV<DFC.UpDowngrades.VersionDescriptor> CreateVersionDescriptor(string verStr, IComposer pnL, DFC.UpDowngrades.IVersionDescriptor mandantoryVersion = null)
        {
            var mtchs = Regex.Matches(verStr, @"\d+\.\d+\.\d+");

            RCV3sV<DFC.UpDowngrades.VersionDescriptor> rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(value: null, pnL.eNotCompleted());

            if (mtchs.Count == 1)
            {
                verStr = mtchs[0].Value;
                var verParts = verStr.Split(new char[] { '.' }, StringSplitOptions.RemoveEmptyEntries);
                if (int.TryParse(verParts[0], out var MainVersion))
                {
                    if (int.TryParse(verParts[1], out var SubVersion))
                    {
                        if (int.TryParse(verParts[2], out var Build))
                        {
                            var vd = new DFC.UpDowngrades.VersionDescriptor()
                            {
                                MainVersion = MainVersion,
                                SubVersion = SubVersion,
                                Build = Build
                            };

                            vd.IsBeta = !mandantoryVersion?.Equ(vd) ?? false;

                            rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Ok(value: vd);
                        } else
                        {
                            rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                                value: null,
                                ErrorDescription: pnL.ReturnAfterFailureWithDetails(
                                                    "ParseVersionString",                                                    
                                                    pnL.i(ANC.DocuTerms.StateDescription.WhatsUp.UID, 
                                                        pnL.p(ANC.TechTerms.Parser.SyntaxError.UID, "Build is not a Number")), 
                                                    pnL.p("VersionString", verStr)));                                
                        }
                    }
                    else
                    {
                        rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                            value: null, 
                            ErrorDescription: pnL.ReturnAfterFailureWithDetails(
                                                    "ParseVersionString",
                                                    pnL.i(ANC.DocuTerms.StateDescription.WhatsUp.UID,
                                                        pnL.p(ANC.TechTerms.Parser.SyntaxError.UID, "Subversion is not a Number")),
                                                    pnL.p("VersionString", verStr)));
                        //pnL.m("ParseVersionString", pnL.p("VersionString", verStr), pnL.ret(pnL.eFails("Subversion is not a Number"))));
                    }
                }
                else
                {
                    rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                        value: null, 
                        ErrorDescription: pnL.ReturnAfterFailureWithDetails(
                                                    "ParseVersionString",
                                                    pnL.i(ANC.DocuTerms.StateDescription.WhatsUp.UID,
                                                        pnL.p(ANC.TechTerms.Parser.SyntaxError.UID, "Mainversion is not a Number")),
                                                    pnL.p("VersionString", verStr)));
                    //pnL.m("ParseVersionString", pnL.p("VersionString", verStr), pnL.ret(pnL.eFails("Mainversion is not a Number"))));
                }
            }
            else
            {
                rc = RCV3sV<DFC.UpDowngrades.VersionDescriptor>.Failed(
                    value: null, 
                    ErrorDescription: pnL.ReturnAfterFailureWithDetails(
                                                    "ParseVersionString",
                                                    pnL.i(ANC.DocuTerms.StateDescription.WhatsUp.UID,
                                                        pnL.p(ANC.TechTerms.Parser.SyntaxError.UID, "Versionstring is invalid")),
                                                    pnL.p("VersionString", verStr)));
                //pnL.m("ParseVersionString", pnL.p("VersionString", verStr), pnL.ret(pnL.eFails("Versionstring is invalid"))));
            }

            return rc;
        }


    }
}
