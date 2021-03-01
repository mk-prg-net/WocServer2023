using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using ATMO.mko.QueryBuilder;
using DFC3.DB.Bo;

using DFCObjects.Common;
using Doc = DFCObjects.Common.Doc;

using ColTool = DFC3.DB.Tools.TabColAccess;

using DFC.UpDowngrades;


namespace DFC3.DB.Queries
{
    /// <summary>
    /// mko, 6.11.2018
    /// Operationen auf den DFC- Logs
    /// </summary>
    public class LoggingSQL : QueriesBase
    {
        public LoggingSQL(IComposer pnL) : base(pnL) { }

        public RCV3 LogIntoLog2(
            long logCounter,
            DateTime logDate,
            string computer,
            string user,
            string custId,
            string pgmVersion,
            EnumLogType logType, string msg)
        {
            var fullMsg = $"#LC {DateTime.Now.ToLongTimeString()}.{logCounter} {msg}";
            var limitedMsg = fullMsg.Length > 255 ? fullMsg.Substring(0, 255) : fullMsg;

            var sql = new SQL<int>();
            var tab = new Tables.DFCLog2();

            var cmd = sql.Insert(
                    tab,
                    sql.NewVal(tab.UserId, sql.Txt(user)),
                    sql.NewVal(tab.Computername, sql.Txt(computer)),
                    sql.NewVal(tab.PgmVersion, sql.Txt(pgmVersion)),
                    sql.NewVal(tab.CustId, sql.Txt(custId)),
                    sql.NewVal(tab.Transaction, sql.Txt($"Log.{logType}")),
                    sql.NewVal(tab.TransactionId, sql.Txt("00-00")),
                    sql.NewVal(tab.TransactionValue, sql.Txt(limitedMsg)),
                    sql.NewVal(tab.TimeClient, sql.Date(logDate)),
                    sql.NewVal(tab.TimeServer, sql.Date(DateTime.UtcNow))
                );

            return ExecuteDML(cmd);
        }

        /// <summary>
        /// mko, 7.1.2018
        /// Zur erfassung von Logmeldungen nach altem System (vor der DZA- Ablösung)
        /// </summary>
        /// <param name="logDateServer"></param>
        /// <param name="logDateClient"></param>
        /// <param name="computer"></param>
        /// <param name="user"></param>
        /// <param name="custId"></param>
        /// <param name="pgmVersion"></param>
        /// <param name="Transaction"></param>
        /// <param name="TransactionId"></param>
        /// <param name="msg"></param>
        /// <returns></returns>
        public RCV3 LogIntoLog2(
            DateTime logDateClient,
            string computer,
            string user,
            string custId,
            string pgmVersion,
            string Transaction,
            string TransactionId,
            string msg)
        {
            var fullMsg = msg;
            var limitedMsg = fullMsg.Length > 255 ? fullMsg.Substring(0, 255) : fullMsg;

            var sql = new SQL<int>();
            var tab = new Tables.DFCLog2();

            var cmd = sql.Insert(
                    tab,
                    sql.NewVal(tab.UserId, sql.Txt(user)),
                    sql.NewVal(tab.Computername, sql.Txt(computer)),
                    sql.NewVal(tab.PgmVersion, sql.Txt(pgmVersion)),
                    sql.NewVal(tab.CustId, sql.Txt(custId)),
                    sql.NewVal(tab.Transaction, sql.Txt(Transaction)),
                    sql.NewVal(tab.TransactionId, sql.Txt(TransactionId)),
                    sql.NewVal(tab.TransactionValue, sql.Txt(limitedMsg)),
                    sql.NewVal(tab.TimeClient, sql.Date(logDateClient)),
                    sql.NewVal(tab.TimeServer, sql.Date(logDateClient.ToUniversalTime()))
                );

            return ExecuteDML(cmd);
        }



        /// <summary>
        /// mko, 15.11.2018
        /// Aufzeichnen von Logmeldungen in der Bosch106Log_FS
        /// </summary>
        /// <param name="SessionId"></param>
        /// <param name="logCounter"></param>
        /// <param name="logType"></param>
        /// <param name="clientName"></param>
        /// <param name="clientVersion"></param>
        /// <param name="componentTriggering"></param>
        /// <param name="componentDocumented"></param>
        /// <param name="plxMsg"></param>
        /// <returns></returns>
        public RCV3 LogIntoLogFS(
                long SessionId,
                long logCounter,
                EnumLogTypeDFC logType,
                string ComputerName,
                string clientName,
                string clientVersion,
                string clientUserId,
                DateTime clientTime,
                string componentTriggering,
                string componentDocumented,
                IDocuEntity plxMsg)
        {
            var fmt = new PNFormater(ATMO.mko.Logging.PNDocuTerms.Fn._, RCV3.NC);
            var msg1 = fmt.Print(plxMsg);

            // Aufteilen der Logmeldung auf bis zu vier msg- Felder a 255 Zeichen
            string msg2 = "", msg3 = "", msg4 = "";
            if (msg1.Length > 255)
            {
                msg2 = msg1.Substring(255);
                msg1 = msg1.Substring(0, 255);

                if (msg2.Length > 255)
                {
                    msg3 = msg2.Substring(255);
                    msg2 = msg2.Substring(0, 255);

                    if (msg3.Length > 255)
                    {
                        msg4 = msg3.Substring(255);
                        msg3 = msg3.Substring(0, 255);

                        if (msg4.Length > 255)
                        {
                            msg4 = msg4.Substring(0, 255);
                        }
                    }
                }
            }

            var sql = new SQL<int>();
            var tab = new Tables.LogDFC_FS();

            Random rnd = new Random((int)DateTime.Now.Ticks);

            var b8 = new byte[8];
            rnd.NextBytes(b8);

            // mko, 15.11.2018
            // Long- Guid erzeugen als Hilfsschlüssel
            //long id = (b8[0] 
            //    | ((long)b8[1] << 8) 
            //    | ((long)b8[2] << 16) 
            //    | ((long)b8[3] << 24) 
            //    | ((long)b8[4] << 32) 
            //    | ((long)b8[5] << 40)
            //    | ((long)b8[6] << 48) 
            //    | ((long)b8[7] << 56)) & 0xFFFFFFFFL;


            var cmd = sql.Insert(
                    tab,
                    //sql.NewVal(tab.Id, sql.Long(id)),
                    sql.NewVal(tab.TimeClient, sql.Date(clientTime)),
                    //sql.NewVal(tab.TimeGMT, sql.Date(DateTime.UtcNow)),
                    // mko, 07.1.2019
                    sql.NewVal(tab.TimeGMT, sql.Date(clientTime.ToUniversalTime())),
                    sql.NewVal(tab.UserId, sql.Txt(clientUserId)),
                    sql.NewVal(tab.ComputerName, sql.Txt(ComputerName)),
                    sql.NewVal(tab.PgmName, sql.Txt(clientName)),
                    sql.NewVal(tab.PgmVersion, sql.Txt(clientVersion)),
                    sql.NewVal(tab.PgmUserId, sql.Txt(clientUserId)),
                    sql.NewVal(tab.ComponentTriggering, sql.Txt(componentTriggering)),
                    sql.NewVal(tab.ComponentDocumented, sql.Txt(componentDocumented)),
                    sql.NewVal(tab.SessionId, sql.Long(SessionId)),
                    sql.NewVal(tab.LogCount, sql.Long(logCounter)),

                    // siehe Tabelle Bosch106Descr_Logformat, pn=5
                    sql.NewVal(tab.LogFormat, sql.Int(5)),
                    sql.NewVal(tab.LogType, sql.Int((int)logType)),

                    // siehe Tabelle Bosch106Descr_LogAction, 0=Auf Magnetplatte archiviert
                    sql.NewVal(tab.LogAction, sql.Int(0)),

                    sql.NewVal(tab.Msg1, sql.Txt(msg1)),
                    sql.NewVal(tab.Msg2, sql.Txt(msg2)),
                    sql.NewVal(tab.Msg3, sql.Txt(msg3)),
                    sql.NewVal(tab.Msg4, sql.Txt(msg4))
                    );

            return ExecuteDML(cmd);
        }

        /// <summary>
        /// mko, 4.12.2018
        /// Zeichnet die aktuelle Clientversion einwandfrei auf.
        /// Es ist zwischen Kunden und Mitarbeitern zu unterscheiden. Deshalb wird mittels Abfragen bestimmt,
        /// zu welcher Gruppe die UserId gehört. Kann sie keiner Gruppe zugeordnet werden, dann wird ein 
        /// Fehler zurückgegeben.
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="ClientVersion"></param>
        /// <returns></returns>
        public RCV3 UpdateDFCCurrentUsedClientVersionInUser2(string userId, string ClientVersion, long currentSessionId)
        {
            var userMgmt = new UserMgmtV18_10(pnL);

            var isRas = userId.Contains("\\");

            // prüfen, ob ein Kunde vorliegt
            var getCust = userMgmt.GetCustomer(userId, isRas);
            var qAnalyt = new ATMO.mko.QueryBuilder.Results.PlxQueryResultAnalyzer(pnL, getCust.MessageEntity);
            if (getCust.Succeeded && !qAnalyt.EmptyResultset)
            {
                var sql = new SQL<int>();
                var tab = new Tables.UserCustTab();
                var cmd = sql.Update(
                                    tab,
                                    sql.Set(tab.DFC2CurrentUsedVersion, sql.Txt(ClientVersion)),
                                    sql.Set(tab.LastLogin, sql.Date(DateTime.Now.ToUniversalTime())),
                                    sql.Set(tab.LastSessionId, sql.Txt(currentSessionId.ToString()))
                                )
                                .Where(sql.Eq(tab.UserID, sql.Txt(getCust.Value.Username)))
                                .done();

                return ExecuteDML(cmd);

            }
            else
            {
                var getCoWorker = userMgmt.GetUser(userId, isRas);
                var qAnalyt2 = new ATMO.mko.QueryBuilder.Results.PlxQueryResultAnalyzer(pnL, getCoWorker.MessageEntity);
                if (getCoWorker.Succeeded && !qAnalyt2.EmptyResultset)
                {

                    var sql = new SQL<int>();
                    var tab = new Tables.User2Tab();
                    var cmd = sql.Update(
                                        tab,
                                        sql.Set(tab.DFC2CurrentUsedVersion, sql.Txt(ClientVersion)),
                                        sql.Set(tab.LastLogin, sql.Date(DateTime.Now.ToUniversalTime())),
                                        sql.Set(tab.LastSessionId, sql.Long(currentSessionId))
                                    )
                                    .Where(sql.Eq(tab.UserID, sql.Txt(getCoWorker.Value.Username)))
                                    .done();

                    return ExecuteDML(cmd);
                }
                else
                {                    
                    return RCV3.Failed(pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(userId));
                }
            }
        }


        /// <summary>
        /// mko, 10.1.2019
        /// Diese Abfrage ist für den DFCinstaller gedacht. Immer, wenn er erfolgreich eine neue DFC- Version für den
        /// Client installiert, dann wird mit dieser Abfrage der Zeitpunkt der Installation und die installierte Version
        /// aufgezeichnet.
        /// </summary>
        /// <param name="UserId"></param>
        /// <param name="ver"></param>
        /// <returns></returns>
        public RCV3 LogInstalledVersionOfDFCClient(string UserId, IVersionDescriptor ver)
        {
            var ret = RCV3.Failed(ErrorDescription: pnL.eNotCompleted());

            // Bestimmen, ob user ein Kunde oder Mitarbeiter ist

            var sql = new SQL<Bo.StringObj>();
            var tabCust = new Tables.UserCustTab();

            var qCust = sql.Select(
                    sql.Map(tabCust.UserID, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                )
                .From(tabCust)
                .Where(sql.StrEq(tabCust.UserID, sql.Txt(UserId)))
                .done();

            var getCust = GetRecord(qCust);

            if (!getCust.Succeeded)
            {
                ret = RCV3.Failed(ErrorDescription: getCust.ToPlx());
            }
            else if (!getCust.Value.IsEmpty)
            {
                // Ein Kunde liegt vor.

                var upd = sql.Update(
                                    tabCust,
                                    sql.Set(tabCust.DFC2CurrentUsedVersion, sql.Txt(ver.ToString())),
                                    sql.Set(tabCust.LastDFCClientUpdate, sql.Date(DateTime.Now.ToUniversalTime())))
                                .Where(sql.StrEq(tabCust.UserID, sql.Txt(UserId)))
                                .done();

                ret = ExecuteDML(upd);
            }
            else
            {
                // Prüfen, ob ein ATMO- Mitarbeiter vorliegt
                var tabCoW = new Tables.User2Tab();

                var qCoW = sql.Select(
                        sql.Map(tabCoW.UserID, (bo, v) => bo.Value = ColTool.GetSave(v, ""))
                    )
                    .From(tabCoW)
                    .Where(sql.StrEq(tabCoW.UserID, sql.Txt(UserId)))
                    .done();

                var getCoW = GetRecord(qCoW);

                if (!getCoW.Succeeded)
                {
                    ret = RCV3.Failed(ErrorDescription: getCoW.ToPlx());
                }
                else if (!getCoW.Value.IsEmpty)
                {
                    // Ein Mitarbeiter liegt vor
                    var lastClientLup = getCoW.Value.Entity.Value;

                    // Aktualisieren des Lup- Feldes für die Client Aktualisierung
                    var upd = sql.Update(
                                            tabCoW,
                                            sql.Set(tabCoW.DFC2CurrentUsedVersion, sql.Txt(ver.ToString())),
                                            sql.Set(tabCoW.LastDFCClientUpdate, sql.Date(DateTime.Now.ToUniversalTime()))
                                        )
                                    .Where(sql.StrEq(tabCoW.UserID, sql.Txt(UserId)))
                                    .done();

                    ret = ExecuteDML(upd);
                }
                else
                {
                    // Die UserId ist weder einem Mitarbeiter noch einem Kunden zugeordnet-> 
                    // Fehlermeldung unbekannter Benutzer

                    ret = RCV3.Failed(
                            pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(UserId)
                        );
                }
            }

            return ret;
        }

        /// <summary>
        /// mko, 19.12.2019
        /// </summary>
        /// <param name="UserId"></param>
        /// <returns></returns>
        public RCV3 LogCurrentUsedVersionOfDFCInstaller(string UserId, IVersionDescriptor ver)
        {
            var ret = RCV3.Failed(ErrorDescription: pnL.eNotCompleted());

            // Bestimmen, ob user ein Kunde oder Mitarbeiter ist

            var sql = new SQL<Bo.StringObj>();
            var tabCust = new Tables.UserCustTab();

            var qCust = sql.Select(
                    sql.Map(tabCust.DFCInstallerCurrentUsedVersion, (bo, v) => bo.Value = ColTool.GetSave(v, "0.0.0"))
                )
                .From(tabCust)
                .Where(sql.StrEq(tabCust.UserID, sql.Txt(UserId)))
                .done();

            var getCust = GetRecord(qCust);

            if (!getCust.Succeeded)
            {
                ret = RCV3.Failed(ErrorDescription: getCust.ToPlx());
            }
            else if (!getCust.Value.IsEmpty)
            {
                // Ein Kunde liegt vor.

                var currentUsedDfcInstallerVersion = new VersionDescriptor("0.0.0");

                // Da in der Versionsspalte bis zur DFC- Version 19.12.11 Session- Id's gespeichert wurden anstatt Versionsnummern
                // des Installers, muss hier eine Fehlerbehandlung erfolgen.
                try
                {
                    currentUsedDfcInstallerVersion = new VersionDescriptor(getCust.Value.Entity.Value);
                }
                catch { }

                if (!currentUsedDfcInstallerVersion.Equ(ver))
                {
                    // Der letzt Protokolleintrag zur verwendeten DFC2- Installerversion ist veraltet und muss aktualisiert werden
                    var upd = sql.Update(
                                        tabCust,
                                        sql.Set(tabCust.DFCInstallerCurrentUsedVersion, sql.Txt(ver.ToString())),
                                        sql.Set(tabCust.LastDFCInstallerUpdate, sql.Date(DateTime.Now.ToUniversalTime())))
                                    .Where(sql.StrEq(tabCust.UserID, sql.Txt(UserId)))
                                    .done();

                    ret = ExecuteDML(upd);
                }
                else
                {
                    ret = RCV3.Ok();
                }
            }
            else
            {
                // Prüfen, ob ein ATMO- Mitarbeiter vorliegt
                var tabCoW = new Tables.User2Tab();

                var qCoW = sql.Select(
                        sql.Map(tabCoW.DFCInstallerCurrentUsedVersion, (bo, v) => bo.Value = ColTool.GetSave(v, "0.0.0"))
                    )
                    .From(tabCoW)
                    .Where(sql.StrEq(tabCoW.UserID, sql.Txt(UserId)))
                    .done();

                var getCoW = GetRecord(qCoW);

                if (!getCoW.Succeeded)
                {
                    ret = RCV3.Failed(ErrorDescription: getCoW.ToPlx());
                }
                else if (!getCoW.Value.IsEmpty)
                {
                    // Ein Mitarbeiter liegt vor

                    var currentUsedDfcInstallerVersion = new VersionDescriptor("0.0.0");

                    try
                    {
                        currentUsedDfcInstallerVersion = new VersionDescriptor(getCoW.Value.Entity.Value);
                    }
                    catch { }

                    if (!currentUsedDfcInstallerVersion.Equ(ver))
                    {
                        // Der letzt Protokolleintrag zur verwendeten DFC2- Installerversion ist veraltet und muss aktualisiert werden
                        var upd = sql.Update(
                                                tabCoW,
                                                sql.Set(tabCoW.DFCInstallerCurrentUsedVersion, sql.Txt(ver.ToString())),
                                                sql.Set(tabCoW.LastDFCInstallerUpdate, sql.Date(DateTime.Now.ToUniversalTime()))
                                            )
                                        .Where(sql.StrEq(tabCoW.UserID, sql.Txt(UserId)))
                                        .done();

                        ret = ExecuteDML(upd);
                    }
                    else
                    {
                        ret = RCV3.Ok();
                    }
                }
                else
                {
                    // Die UserId ist weder einem Mitarbeiter noch einem Kunden zugeordnet-> 
                    // Fehlermeldung unbekannter Benutzer

                    ret = RCV3.Failed(
                            pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(UserId)
                        );
                }
            }

            return ret;
        }
    }
}
