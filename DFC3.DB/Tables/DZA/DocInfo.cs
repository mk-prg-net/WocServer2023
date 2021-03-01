using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables.DZA
{
    /// <summary>
    /// mko, 18.6.2018
    /// Meta data of dza_admin.DocInfo.
    /// In future this table becomes obsolete because DFC is dcoupled from DZA.
    /// All informations in this table are redundant in dza_admin.BOSCH106PATH etc.    /// 
    /// </summary>
    public class DocInfoTab : Table
    {

        /// <summary>
        /// Singelton
        /// </summary>
        public static DocInfoTab _
        {
            get
            {
                if (__ == null)
                {
                    __ = new DocInfoTab();
                }
                return __;
            }
        }
        static DocInfoTab __;


        public DocInfoTab()
            : base("dza_admin.DocInfo")
        {
            ID = new ColName(TableName, "ID");
            Family = new ColName(TableName, "FAMILY");
            UserState = new ColName(TableName, "USERSTATE");
            NrLayers = new ColName(TableName, "NRLAYERS");
            InfoText = new ColName(TableName, "INFOTEXT");
        }

        public ColName ID { get; }
        public ColName Family { get; }
        public ColName UserState { get; }
        public ColName InfoText { get; }
        public ColName NrLayers { get; }
    }
}
