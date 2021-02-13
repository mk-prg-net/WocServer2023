using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Runtime.Jobs
{
    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Job
    : NamingBase
    {

        public const long UID = 0x26EBF14E;

        public Job()
            : base(UID)
        {
        }

        public override string CNT => "job";
        public override string CN => EN;
        public override string DE => "Auftrag";
        public override string EN => "Job";
        public override string ES => "Job";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class JobMonitoring
        : NamingBase
    {

        public const long UID = 0x158EFAD1;

        public JobMonitoring()
            : base(UID)
        {
        }

        public override string CNT => "jobMonitoring";
        public override string CN => EN;
        public override string DE => "Auftragsüberwachung";
        public override string EN => "Job";
        public override string ES => "Seguimiento del trabajo";
    }
}
