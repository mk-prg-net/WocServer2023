using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 18.6.2018
    /// Metadata of DFC document protocoll
    /// </summary>
    public class LogDFC : Table
    {
        /// <summary>
        /// Singelton
        /// </summary>
        public static LogDFC _
        {
            get
            {
                if (__ == null)
                {
                    __ = new LogDFC();
                }
                return __;
            }
        }
        static LogDFC __;



        public LogDFC(string Alias = null) : base("dza_admin.BOSCH106LOG_DFC", Alias)
        {
            Id = new ColName(TableName, "ID");

            // Client logging time
            TimeClient = new ColName(TableName, "TIME_CLIENT");

            // Server logging time (GMT)
            TimeGMT = new ColName(TableName, "TIME_GMT");

            // Name of client computer
            ComputerName = new ColName(TableName, "COMPUTERNAME");

            // When the Client is started, a new "client session" is always defined. 
            // A unique ID is generated for each client session.
            // This so called Session_id assigns the corresponding client session to each log entry. 
            SessionId = new ColName(TableName, "SESSION_ID");

            // Userid of User currently using DFC client
            PgmUserId = new ColName(TableName, "PGM_USRID");

            // Name of Client (i.e. DFC2 Client)
            PgmName = new ColName(TableName, "PGM_NAME");

            // Client Version number 
            PgmVersion = new ColName(TableName, "PGM_VERSION");

            // Within a session the log count defines a order of log messages
            LogCount = new ColName(TableName, "LOG_COUNT");

            // Classifies log messages into error-, status- and info- messages
            LogType = new ColName(TableName, "LOG_TYPE");

            // Defines syntactic scheme for log messages (i.e. plain text, json or pn)
            LogFormat = new ColName(TableName, "LOG_FORMAT");


            LogAction = new ColName(TableName, "LOG_ACTION");

            // Name of triggering component. A coponent is a class or program module. 
            // Triggering means, that the logged function call 
            ComponentTriggering = new ColName(TableName, "COMPONENT_T");

            // Name of documented component
            ComponentDocumented = new ColName(TableName, "COMPONENT_D");

            Msg1 = new ColName(TableName, "MSG1");
            Msg2 = new ColName(TableName, "MSG2");
            Msg3 = new ColName(TableName, "MSG3");
            Msg4 = new ColName(TableName, "MSG4");

            PrjNo = new ColName(TableName, "PJNR");
            StationNo = new ColName(TableName, "STATNR");
            MatNo = new ColName(TableName, "MATNR");
            UserId = new ColName(TableName, "USRID");
            Infos = new ColName(TableName, "INFOS");
            DocId = new ColName(TableName, "DOCID");
        }

        public ColName Id { get; }
        public ColName TimeClient { get; }
        public ColName TimeGMT { get; }
        public ColName ComputerName { get; }
        public ColName SessionId { get; }
        public ColName PgmUserId { get; }
        public ColName PgmName { get; }
        public ColName PgmVersion { get; }
        public ColName LogCount { get; }
        public ColName LogType { get; }
        public ColName LogFormat { get; }
        public ColName LogAction { get;}
        public ColName ComponentTriggering { get; }
        public ColName ComponentDocumented { get; }
        public ColName Msg1 { get; }
        public ColName Msg2 { get; }
        public ColName Msg3 { get; }
        public ColName Msg4 { get; }
        public ColName PrjNo { get; }
        public ColName StationNo { get; }
        public ColName MatNo { get; }
        public ColName UserId { get; }
        public ColName Infos { get; }
        public ColName DocId { get; }

    }
}
