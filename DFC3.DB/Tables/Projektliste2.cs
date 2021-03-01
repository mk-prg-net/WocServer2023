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
    /// Metadata of dza_admin.BOSCH106Projektliste2
    /// </summary>
    public class Projektliste2 : Table
    {
        /// <summary>
        /// Singelton
        /// </summary>
        public static Projektliste2 _
        {
            get
            {
                if (__ == null)
                {
                    __ = new Projektliste2();
                }
                return __;
            }
        }
        static Projektliste2 __;


        public Projektliste2(string Alias = null) : base("dza_admin.BOSCH106PROJEKTLISTE2", Alias)
        {
            PSP = new ColName(TableName, "PSP");
            PrjNr = new ColName(TableName, "PJNR");
            PjArt = new ColName(TableName, "PJART");
            StatNr = new ColName(TableName, "STATNR");
            MatNr = new ColName(TableName, "MATNR");
            PrjFolder = new ColName(TableName, "PJFOLDER");
            Projekt = new ColName(TableName, "PROJEKT");
            Bennenung = new ColName(TableName, "BENENNUNG");
            CO_GB = new ColName(TableName, "CO_GB");
            CO_GFE = new ColName(TableName, "CO_GFE");
            Verantw = new ColName(TableName, "VERANTW");
            Sysstatus = new ColName(TableName, "SYSSTATUS");
            Anwstatus = new ColName(TableName, "ANWSTATUS");
            VE = new ColName(TableName, "VE");
            VE_Datum = new ColName(TableName, "VE_DATUM");
            UM = new ColName(TableName, "UM");
            PHEK = new ColName(TableName, "PHEK");
            THEK = new ColName(TableName, "THEK");
            Obligo = new ColName(TableName, "OBLIGO");
            WIP = new ColName(TableName, "WIP");
            Lup_1 = new ColName(TableName, "LUP_1");
            Lup_2 = new ColName(TableName, "LUP_2");
            Stufe = new ColName(TableName, "STUFE");
            Basisprojekt = new ColName(TableName, "BASISPROJEKT");
            ErsDat = new ColName(TableName, "ERSDAT");
            Disable = new ColName(TableName, "DISABLE");
            DisableComment = new ColName(TableName, "DISABLECOMMENT");
            BaseLineDate = new ColName(TableName, "BASELINEDATE");
            BaseLineUsr = new ColName(TableName, "BASELINEUSR");
            BaseLineComment = new ColName(TableName, "BASELINECOMMENT");
            Praefix = new ColName(TableName, "PRAEFIX");
            PA1 = new ColName(TableName, "PA1");
            PA2 = new ColName(TableName, "PA2");
            PA3 = new ColName(TableName, "PA3");
            PA4 = new ColName(TableName, "PA4");
            PA5 = new ColName(TableName, "PA5");
            PA6 = new ColName(TableName, "PA6");
            PA7 = new ColName(TableName, "PA7");
            PA8 = new ColName(TableName, "PA8");
            PA9 = new ColName(TableName, "PA9");
            PMH = new ColName(TableName, "PMH");
            Owner = new ColName(TableName, "OWNER");
            IS_PM = new ColName(TableName, "IS_PM");
            IS_VSM = new ColName(TableName, "IS_VSM");
            IS_VDP = new ColName(TableName, "IS_VDP");
            IS_VAB = new ColName(TableName, "IS_VAB");
            IS_VMK = new ColName(TableName, "IS_VMK");
            CustAccess = new ColName(TableName, "CUSTACCESS");
            P_BESCHAFF = new ColName(TableName, "P_BESCHAFF");
            IS_Projektleiter = new ColName(TableName, "IS_PROJEKTLEITER");

        }

        public ColName PSP { get; }
        public ColName PrjNr { get; }
        public ColName StatNr { get; }
        public ColName MatNr { get; }
        public ColName PrjFolder { get; }        
        public ColName Projekt { get; }
        public ColName Bennenung { get; }

        /// <summary>
        /// List of customergroups with permissions to this project
        /// Values are separeted by |
        /// </summary>
        public ColName CustAccess { get; }

        public ColName CO_GB { get; }
        public ColName CO_GFE { get; }
        public ColName Verantw { get; }
        public ColName Sysstatus { get; }
        public ColName Anwstatus { get; }
        public ColName VE { get; }
        public ColName VE_Datum { get; }
        public ColName UM { get; }
        public ColName UM_Datum { get; }
        public ColName PHEK { get; }
        public ColName THEK { get; }
        public ColName Obligo { get; }
        public ColName WIP { get; }
        public ColName Lup_1 { get; }
        public ColName Lup_2 { get; }
        public ColName Stufe { get; }
        public ColName PjArt { get; }
        public ColName Basisprojekt { get; }
        public ColName ErsDat { get; }
        public ColName Disable { get; }
        public ColName DisableComment { get; }
        public ColName BaseLineDate { get; }
        public ColName BaseLineUsr { get; }
        public ColName BaseLineComment { get; }
        public ColName Praefix { get; }        
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


        public ColName PMH { get; }
        public ColName Owner { get; }
        public ColName IS_PM { get; }
        public ColName IS_VSM { get; }
        public ColName IS_VAB { get; }
        public ColName IS_VDP { get; }
        public ColName IS_VMK { get; }
        public ColName IS_Projektleiter { get; }
        public ColName P_BESCHAFF { get; }


        
    }
}
