using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 2.5.2019
    /// 
    /// </summary>
    public class DocuMatBo
    {
        public string ID { get; set; }
        public string MatNo { get; set; }
        public string DocMatNo { get; set; }
        public string ManualLanguageISO { get; set; }
        public DateTime LastUpdate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime Created { get; set; }
        public string Creator { get; set; }
    }
}
