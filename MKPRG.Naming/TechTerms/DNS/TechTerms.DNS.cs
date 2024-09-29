using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.DNS
{
    /// <summary>
    /// DNS- Namensdienst
    /// </summary>
    public class DNS
        : NamingBase
    {
        public const long UID = 0xF57429AB;

        public DNS()
            : base(UID)
        {
        }

        public override string CNT => "dns";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "DNS";

        public override string ES => EN;
    }

    public class DNSServer
    : NamingBase
    {
        public const long UID = 0x686942CE;

        public DNSServer()
            : base(UID)
        {
        }

        public override string CNT => "dnsServer";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "DNS Server";

        public override string ES => EN;
    }

    public class DNSComputerName
        : NamingBase
    {
        public const long UID = 0xAEDAE20E;

        public DNSComputerName()
            : base(UID)
        {
        }

        public override string CNT => "dnsComputerName";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "DNS computer name";

        public override string ES => EN;
    }

    public class DNSDomainName
    : NamingBase
    {
        public const long UID = 0xD534B53A;

        public DNSDomainName()
            : base(UID)
        {
        }

        public override string CNT => "dnsDomainName";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "DNS Domain Name";

        public override string ES => EN;
    }


    public class DNSName
        : NamingBase
    {
        public const long UID = 0x281556;

        public DNSName()
            : base(UID)
        {
        }

        public override string CNT => "dnsName";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "DNS Name";

        public override string ES => EN;
    }

    public class FileShareURN
        : NamingBase
    {
        public const long UID = 0xDF2B9D1D;

        public FileShareURN()
            : base(UID)
        {
        }

        public override string CNT => "fileShareURN";

        public override string CN => EN;

        public override string DE => EN;

        public override string EN => "File share URN";

        public override string ES => EN;
    }



}
