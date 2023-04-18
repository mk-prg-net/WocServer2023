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
        public override string CN => "工作内容";
        public override string DE => "Auftrag";
        public override string EN => "Job";
        public override string ES => "Job";

        public override string Glyph => Glyphs.Runtime.Job2;
    }

    /// <summary>
    /// mko, 19.5.2021
    /// </summary>
    public class JobId
        : NamingBase
    {

        public const long UID = 0xA9AE48B0;

        public JobId()
            : base(UID)
        {
        }

        public override string CNT => "jobId";
        public override string CN => "工作编号";
        public override string DE => "Job Nr.";
        public override string EN => "Job Id";
        public override string ES => "Job Id";

        public override string Glyph => Glyphs.Authentication.ID;

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
        public override string CN => "工作监控";
        public override string DE => "Auftragsüberwachung";
        public override string EN => "Job Monitoring";
        public override string ES => "Seguimiento del trabajo";

        public override string Glyph => Glyphs.Runtime.Monitoring;
    }

}
