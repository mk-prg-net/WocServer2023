using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Net.RAS
{
    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class RAS
        : NamingBase
    {
        public const long UID = 0x823409ED;

        public RAS()
            : base(UID)
        {
        }

        public override string CNT => "ras";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "RAS";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class RASUserId
        : NamingBase
    {
        public const long UID = 0x140145D7;

        public RASUserId()
            : base(UID)
        {
        }

        public override string CNT => "rasUserId";
        public override string CN => EN;
        public override string DE => "RAS Benutzerkennung";
        public override string EN => "RAS User ID";
        public override string ES => EN;
    }

    /// <summary>
    /// 13.7.2020
    /// </summary>
    public class ConnectionTypeRASConnection
    : NamingBase
    {
        public const long UID = 0x72876F18;

        public  ConnectionTypeRASConnection()
            : base(UID)
        {
        }

        public override string CNT => "rasConnection";
        public override string CN => EN;
        public override string DE => "RAS Verbindung";
        public override string EN => "RAS connection";
        public override string ES => "Conexión RAS";
    }
}
