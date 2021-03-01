using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 20.6.2018
    /// </summary>
    public class FolderXDocInfo
    {
        public FolderXDocInfo()
        {
            DocInfo = new DocInfoBo();
        }

        public long FolderId { get; set; }
        public long DocId { get; set; }
        public long XOrder { get; set; }
        public DocInfoBo DocInfo { get; }
    }
}
