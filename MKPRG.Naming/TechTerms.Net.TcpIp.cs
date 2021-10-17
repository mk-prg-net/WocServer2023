using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Net.TcpIp
{
    public class IP4Address
        : NamingBase
    {
        public const long UID = 0xD65EC3BC;

        public IP4Address()
            : base(UID)
        {
        }

        public override string CNT => "ip4Address";
        public override string CN => "IP4地址";
        public override string DE => "IP 4 Adresse";
        public override string EN => "IP 4 Address";
        public override string ES => EN;
    }

    public class IP4SubnetMask
        : NamingBase
    {
        public const long UID = 0xA26602F2;

        public IP4SubnetMask()
            : base(UID)
        {
        }

        public override string CNT => "ip4SubnetMask";
        public override string CN => "IP4子网掩码";
        public override string DE => "IP 4 Subnetzmaske";
        public override string EN => "IP 4 Subnet mask";
        public override string ES => EN;
    }


    public class IP6Address
        : NamingBase
    {
        public const long UID = 0x43B12E69;

        public IP6Address()
            : base(UID)
        {
        }

        public override string CNT => "ip6Address";
        public override string CN => "IP6地址";
        public override string DE => "IP 6 Adresse";
        public override string EN => "IP 6 Address";
        public override string ES => EN;
    }



    public class TcpPort
        : NamingBase
    {
        public const long UID = 0xA153E6C6;

        public TcpPort()
            : base(UID)
        {
        }

        public override string CNT => "tcpPort";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "TCP Port";
        public override string ES => EN;
    }

    public class Router
    : NamingBase
    {
        public const long UID = 0xDF5DA01;

        public Router()
            : base(UID)
        {
        }

        public override string CNT => "router";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Router";
        public override string ES => EN;
    }


}
