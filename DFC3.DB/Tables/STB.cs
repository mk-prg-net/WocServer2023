using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging;
using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables
{
    /// <summary>
    /// mko, 2.5.2019
    /// DFC- Sprachtabelle
    /// </summary>
    public class STB : Table
    {
        public ColName ID { get; }
        public ColName BCODE { get; }
        public ColName DE { get; }
        public ColName EN { get; }
        public ColName FR { get; }
        public ColName IT { get; }
        public ColName PT { get; }
        public ColName ES { get; }
        public ColName CN { get; }
        public ColName TR { get; }

        public ColName Created { get; }
        public ColName Creator { get; }
        public ColName LastUpdate { get; }
        public ColName UpdatedBy { get; }

        public ColName SearchField { get; }
        public ColName SearchFiled2 { get; }

        public ColName SGSCategory { get; }
        public ColName SGSDeliverytime { get; }
        public ColName SGSImpactproduction { get; }

        public ColName SAPClass { get; }
        public ColName MCAD { get; }
        public ColName OPCON { get; }
        public ColName ME { get; }
        public ColName N11Proposal { get; }

        public ColName DE_NN11 { get; }
        public ColName EN_NN11 { get; }
        public ColName FR_NN11 { get; }
        public ColName IT_NN11 { get; }
        public ColName PT_NN11 { get; }
        public ColName ES_NN11 { get; }
        public ColName CN_NN11 { get; }
        public ColName TR_NN11 { get; }

        public ColName TR_DATE { get; }


        public STB(string Alias = null) : base("dza_admin.BOSCH106STB", Alias)
        {
            ID = new ColName(TableName, "ID");
            BCODE = new ColName(TableName, "BCODE");
            DE = new ColName(TableName, "MAKTX_DE");
            EN = new ColName(TableName, "MAKTX_EN");
            FR = new ColName(TableName, "MAKTX_FR");
            IT = new ColName(TableName, "MAKTX_IT");
            PT = new ColName(TableName, "MAKTX_PT");
            ES = new ColName(TableName, "MAKTX_ES");
            CN = new ColName(TableName, "MAKTX_CN");
            TR = new ColName(TableName, "MAKTX_TR");

            Created = new ColName(TableName, "ERSDA");
            Creator = new ColName(TableName, "ERNAM");
            LastUpdate = new ColName(TableName, "LAEDA");
            UpdatedBy = new ColName(TableName, "AENAM");

            SearchField = new ColName(TableName, "SEARCHFIELD");
            SearchFiled2 = new ColName(TableName, "SEARCHFIELD2");

            SGSCategory = new ColName(TableName, "SGS_CATEGORY");
            SGSDeliverytime = new ColName(TableName, "SGS_DELIVERYTIME");
            SGSImpactproduction = new ColName(TableName, "SGS_IMPACTPRODUCTION");

            SAPClass = new ColName(TableName, "SAP_CLASS");
            MCAD = new ColName(TableName, "MCAD");
            OPCON = new ColName(TableName, "OPCON");
            ME = new ColName(TableName, "ME");
            N11Proposal = new ColName(TableName, "N11ANTRAG");

            DE_NN11 = new ColName(TableName, "MAKTX_DE_NN11");
            EN_NN11 = new ColName(TableName, "MAKTX_EN_NN11");
            FR_NN11 = new ColName(TableName, "MAKTX_FR_NN11");
            IT_NN11 = new ColName(TableName, "MAKTX_IT_NN11");
            PT_NN11 = new ColName(TableName, "MAKTX_PT_NN11");
            ES_NN11 = new ColName(TableName, "MAKTX_ES_NN11");
            CN_NN11 = new ColName(TableName, "MAKTX_CN_NN11");
            TR_NN11 = new ColName(TableName, "MAKTX_TR_NN11");

            TR_DATE = new ColName(TableName, "MAKTX_TR_DATE");

        }




}
}
