using System;
using System.Collections.Generic;
using System.Text;

using MKPRG.Naming;

namespace TryOut.Models
{
    /// <summary>
    /// mko, 25.4.02023
    /// Implementierung eines Namenscontainers als POCO
    /// </summary>
    public class NamingContainerSimple
        :ILangCNT, ILangCN, ILangDE, ILangEN, ILangES, ILangPL
    {
        public string NIDstr { get; set; }

        public string CN { get; set; }

        public string DE { get; set; }

        public string EN { get; set; }

        public string ES { get; set; }

        public string PL { get; set; }

        public string CNT { get; set; }
    }
}
