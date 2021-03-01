using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.ComposerSubTrees;
using ColTool = DFC3.DB.Tools.TabColAccess;

using DfcTree = ATMO.DFC.Tree;

using ATMO.DFC.Material;
using TT = ATMO.DFC.Naming.TechTerms;
using TTD = ATMO.DFC.Naming.DocuTerms;

using PN = ATMO.mko.Logging.PNDocuTerms;

using static DFCSecurity.SitesExt;


namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 13.7.2018
    /// Scheme of Mara Table
    /// 
    /// mko, 1.10.2020
    /// Erweitert um die Spalten
    /// MAKTX, EVWP und DOKU
    /// </summary>
    public class Mara : Table
    {
        /// <summary>
        /// Singelton
        /// </summary>
        public static Mara _
        {
            get
            {
                if (__ == null)
                {
                    __ = new Mara();
                }
                return __;
            }
        }
        static Mara __;



        public Mara(string Alias = null) : base("dza_admin.BOSCH106MARA", Alias)
        {
            MatNr = new ColName(TableName, "MATNR");
            MAKTX = new ColName(TableName, "MAKTX");
            MKlasse = new ColName(TableName, "MKLASSE");
            MTArt = new ColName(TableName, "MTART");
            MSTAE = new ColName(TableName, "MSTAE");
            NodeType = new ColName(TableName, "NODETYPE");
            Verbaut = new ColName(TableName, "VERBAUT");
            StdBg = new ColName(TableName, "STDBG");
            ZAT = new ColName(TableName, "ZAT");
            ZeichungsNr = new ColName(TableName, "ZEINR");
            EVWP = new ColName(TableName, "ZZERSKZ");
            DOKU = new ColName(TableName, "ZZDOKU");
        }

        public ColName MatNr { get; }

        /// <summary>
        /// Materialkurztext
        /// </summary>
        public ColName MAKTX { get; }

        /// <summary>
        /// Materialnummer der Zeichnung, die diesem Teil zugeordnet ist.
        /// 1) Ist das Feld leer, dann steht ist die in MatNr geführte Materialnummer gleich der 
        ///    Marterialnummer der Zeichnung.
        /// 2) Ist das Feld nicht leer und inhaltlich gleich MatNr, dann wie 1)
        /// 3) Ist das Feld nicht leer und verschieden gegenüber dem Inhalt von MatNr, dann liegt eine
        ///    Verweiszeichnung vor.
        /// </summary>
        public ColName ZeichungsNr { get; }

        /// <summary>
        /// Neue Klassifikation ab 29.9.2020. Erweitert MKlasse um PROJEKT, FLEXCON, ME und EL
        /// </summary>
        public ColName NodeType { get; }

        /// <summary>
        /// Indicates if part is a Assy, Singlepart or Processmodule etc.
        /// </summary>
        public ColName MKlasse { get; }

        /// <summary>
        /// mko, 26.7.2019
        /// Ist ein frei beschreibbares Feld in SAP zur Materialklassifikation.
        /// Fologende Klassen sind bekannt (Stand 26.7.2019):
        /// ANFE = Anfertigung                  Aktiv
        /// ANGB = Angebot Aktiv
        /// DIEN = Dienstleitung Aktiv
        /// HALB = Halbzeug Aktiv
        /// KATA = Katalog Aktiv
        /// KAUF = Kaufteil / oder BG         Leiche
        /// NLAG = Weiß ich nicht
        /// UNBW = (unbewertet) Leiche
        /// VERP = Verpackung Aktiv
        /// WRKFL = Workflow Leiche
        /// </summary>
        public ColName MTArt { get; }

        /// <summary>
        /// Materialstatus Einkauf
        /// </summary>
        public ColName MSTAE { get; }

        /// <summary>
        /// Count of Bom Positions, where this part appears. Shows, how often it is used in projects.
        /// </summary>
        public ColName Verbaut { get; }

        /// <summary>
        /// Indicated if part is a Standardbaugruppe
        /// </summary>
        public ColName StdBg { get; }

        /// <summary>
        /// Defines per site if it is a Standardbaugruppe
        /// </summary>
        public ColName ZAT { get; }

        /// <summary>
        /// Initiale Ersatz- und Verschleissteilkennung
        /// </summary>
        public ColName EVWP { get; }

        /// <summary>
        /// Initiale Dokuhaken- Einstellung
        /// </summary>
        public ColName DOKU { get; }

        // Hilfsfunktionen zum Auslesen von Spalten

        /// <summary>
        /// mko, 1.10.2020
        /// Interpretieren des ZAT- Feldes als Liste von Standorten, in denen das Teil als Standardbaugruppe definiert ist.
        /// Wirft eine Exception, wenn ungültige Standorte definiert wurden
        /// </summary>
        /// <param name="MARA_ZAT"></param>
        /// <param name="pnL"></param>
        /// <returns></returns>
        public DFCSecurity.Site[] ParseZAT(string MARA_ZAT, IComposer pnL)
        {
            var ZAT = new List<DFCSecurity.Site>();

            if (!string.IsNullOrWhiteSpace(MARA_ZAT))
            {
                // Parse ZAT entries
                var ZatPerSite = MARA_ZAT.Split(new char[] { ';' }, StringSplitOptions.RemoveEmptyEntries);

                foreach (var entry in ZatPerSite)
                {
                    if (entry.Length == 4)
                    {
                        var siteName = entry.Substring(2, 2);

                        DFCSecurity.Site site;
                        if (!TryParse(siteName, out site))
                        {
                            TraceHlp.ThrowEx(pnL.m(TT.Parser.Parse.UID,
                                                pnL.p(TT.ATMO.SiteName.UID, siteName),
                                                pnL.ret(
                                                    pnL.eFails(
                                                        pnL.i(TTD.MetaData.Details.UID,
                                                            pnL.p(TT.ATMO.SiteName.UID, pnL.NID(TT.Validation.Errors.Unknown.UID)))))));
                        }

                        ZAT.Add(siteName.ParseOrDefault());
                    }
                }
            }

            return ZAT.ToArray();
        }

    }
}
