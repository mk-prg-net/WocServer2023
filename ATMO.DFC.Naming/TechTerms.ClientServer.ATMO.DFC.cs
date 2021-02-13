using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ClientServer.ATMO.DFC
{
    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class DfcWebService
     : NamingBase
    {

        public const long UID = 0xF0A91623;

        public DfcWebService()
            : base(UID)
        {
        }


        public override string CNT => "dfcWebService";
        public override string CN => CNT;
        public override string DE => "DFC Web- Dienste";
        public override string EN => "DFC web services";
        public override string ES => "Servicios Web DFC";
    }
}
