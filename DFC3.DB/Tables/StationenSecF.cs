using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 8.1.2021
    /// Abbildung der View, die am 8.1.2021 zusammen mit Jaochim erstellt wurde, um die 
    /// für die Sicherheitsmerkmale relevanten Daten der Stationen eines Projektsaus der Tab Projektliste2 zu filtern.
    /// Zu jedem Station werden die zugehörigen Sicherheitsmerkmale aufgelsitet.

    /// </summary>
    public class StationenSecF
        : Table
    {
        public StationenSecF(string Alias = null) : base("dza_admin.BOSCH106_Y_PROJECT_STATNR", Alias)
        {
            PSP = new ColName(TableName, "PSP");
            PrjNr = new ColName(TableName, "PJNR");
            StatNr = new ColName(TableName, "STATNR");
            MatNr = new ColName(TableName, "MATNR");
            ProjectDisabled = new ColName(TableName, "DISABLED");
            PA1 = new ColName(TableName, "PA1");
            PA2 = new ColName(TableName, "PA2");
            PA3 = new ColName(TableName, "PA3");
            PA4 = new ColName(TableName, "PA4");
            PA5 = new ColName(TableName, "PA5");
            PA6 = new ColName(TableName, "PA6");
            PA7 = new ColName(TableName, "PA7");
            PA8 = new ColName(TableName, "PA8");
            PA9 = new ColName(TableName, "PA9");
            CustAccess = new ColName(TableName, "CUSTACCESS");

        }

        public ColName PSP { get; }
        public ColName PrjNr { get; }
        public ColName StatNr { get; }
        public ColName MatNr { get; }

        /// <summary>
        /// Wenn not null, dann ist das Projekt für DFC deaktiviert worden, da in SAP die 
        /// 
        /// </summary>
        public ColName ProjectDisabled { get; }

        /// <summary>
        /// List of customergroups with permissions to this project
        /// Values are separeted by |
        /// </summary>
        public ColName CustAccess { get; }

        public ColName PA1 { get; }
        public ColName PA2 { get; }
        public ColName PA3 { get; }
        public ColName PA4 { get; }
        public ColName PA5 { get; }
        public ColName PA6 { get; }
        public ColName PA7 { get; }
        public ColName PA8 { get; }
        public ColName PA9 { get; }

        /// <summary>
        /// mko, 1.12.2020
        /// Ermittelt aus den Einträgen in der Spalte PA1..PA9 die Liste der 
        /// Freischaltungen.
        /// 
        /// mko, 8.1.2021
        /// Übertragen aus Projektliste2 in ProjektSecF
        /// </summary>
        /// <param name="PA1"></param>
        /// <param name="PA2"></param>
        /// <param name="PA3"></param>
        /// <param name="PA4"></param>
        /// <param name="PA5"></param>
        /// <param name="PA6"></param>
        /// <param name="PA7"></param>
        /// <param name="PA8"></param>
        /// <param name="PA9"></param>
        /// <returns></returns>
        public DFCSecurity.Site[] GetAccessAllowedInLocationsFrom(
            string PA1,
            string PA2,
            string PA3,
            string PA4,
            string PA5,
            string PA6,
            string PA7,
            string PA8,
            string PA9)
        {
            var list = new List<DFCSecurity.Site>();

            if (!string.IsNullOrWhiteSpace(PA1))
            {
                list.Add(DFCSecurity.Site.ATMO_1);
            }

            if (!string.IsNullOrWhiteSpace(PA2))
            {
                list.Add(DFCSecurity.Site.ATMO_2);
            }

            if (!string.IsNullOrWhiteSpace(PA3))
            {
                list.Add(DFCSecurity.Site.ATMO_3);
            }

            if (!string.IsNullOrWhiteSpace(PA4))
            {
                list.Add(DFCSecurity.Site.ATMO_4);
            }

            if (!string.IsNullOrWhiteSpace(PA5))
            {
                list.Add(DFCSecurity.Site.ATMO_5);
            }

            if (!string.IsNullOrWhiteSpace(PA6))
            {
                list.Add(DFCSecurity.Site.ATMO_6);
            }

            if (!string.IsNullOrWhiteSpace(PA7))
            {
                list.Add(DFCSecurity.Site.ATMO_7);
            }

            if (!string.IsNullOrWhiteSpace(PA8))
            {
                list.Add(DFCSecurity.Site.ATMO_8);
            }

            if (!string.IsNullOrWhiteSpace(PA9))
            {
                list.Add(DFCSecurity.Site.MH);
            }

            return list.ToArray();
        }

    }
}
