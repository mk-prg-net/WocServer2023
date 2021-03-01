using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;

namespace DFC3.DB.Tables.DZA
{
    /// <summary>
    /// mko, 20.6.2018
    /// Mapping of FolderXdoc DZA Table
    /// </summary>
    public class FolderXDocTab : Table
    {
        /// <summary>
        /// Singelton
        /// </summary>
        public static FolderXDocTab _
        {
            get
            {
                if (__ == null)
                {
                    __ = new FolderXDocTab();
                }
                return __;
            }
        }
        static FolderXDocTab __;


        public FolderXDocTab()
            : base("dza_admin.FOLDERXDOC")
        {
            FolderId = new ColName(TableName, "FLDID");
            XOrder = new ColName(TableName, "XORDER");
            DocId = new ColName(TableName, "DOCID");
        }

        public ColName FolderId { get; }
        public ColName XOrder { get; }
        public ColName DocId { get; }
    }
}
