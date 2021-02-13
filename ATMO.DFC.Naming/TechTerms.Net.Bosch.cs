using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Net.Bosch
{

    /// <summary>
    /// 13.7.2020
    /// </summary>
    public class ConnectionTypeBCN
    : NamingBase
    {
        public const long UID = 0x631886F;

        public ConnectionTypeBCN()
            : base(UID)
        {
        }

        public override string CNT => "bcnConnection";
        public override string CN => EN;
        public override string DE => "BCN Verbindung";
        public override string EN => "BCN connection";
        public override string ES => "Conexión BCN";
    }


    public class BCN
        : NamingBase
    {
        public const long UID = 0xE5ECD6DE;

        public BCN()
            : base(UID)
        {
        }

        public override string CNT => "bcn";
        public override string CN => EN;
        public override string DE => "Bosch Netzwerk BCN";
        public override string EN => "Bosch Network BCN";
        public override string ES => EN;
    }

    public class APAC
        : NamingBase
    {
        public const long UID = 0x3FAAB39;

        public APAC()
            : base(UID)
        {
        }

        public override string CNT => "bcnApac";
        public override string CN => EN;
        public override string DE => "Bosch Netzwerkregion Asia-Pacific (APAC)";
        public override string EN => "Bosch Network region Asia-Pacific (APAC)";
        public override string ES => EN;
    }

    public class EMEA
    : NamingBase
    {
        public const long UID = 0x150DE648;

        public EMEA()
            : base(UID)
        {
        }

        public override string CNT => "bcnEmea";
        public override string CN => EN;
        public override string DE => "Bosch Netzwerkregion Europa-Mexiko-USA (EMEA)";
        public override string EN => "Bosch Network region Europe-Mexico-USA (EMEA)";
        public override string ES => EN;
    }



}
