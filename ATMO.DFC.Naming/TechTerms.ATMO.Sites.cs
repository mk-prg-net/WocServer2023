using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DFCSecurity;

/// <summary>
/// mko, 6.8.2020
/// ATMO- Standorte
/// </summary>
namespace MKPRG.Naming.TechTerms.ATMO.Sites
{
    /// <summary>
    /// mko, 7.8.2020
    /// Zusätzliche Identifikationsmerkmale für ATMO- Sites
    /// </summary>
    public interface IATMOSite
    {
        /// <summary>
        /// Aufzählung der Standorte zwecks Berechnung von DFC- Zugriffsberechtigungen.
        /// </summary>
        DFCSecurity.Site DFCSecuritySiteEnum { get; }

        /// <summary>
        /// ATMO SAP- Werksnummer
        /// </summary>
        string[] FactoryIds { get; }
    }


    public class ATMO_1DE
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0x95566108;

        public ATMO_1DE()
            : base(UID)
        {
        }

        public override string CNT => "atmo-1de";
        public override string CN => EN;
        public override string DE => "ATMO-1DE (Deutschland/Stuttgart/Feuerbach)";
        public override string EN => "ATMO-1DE (Germany/Stuttgart/Feuerbach)";
        public override string ES => "ATMO-1DE (Alemania/Stuttgart/Feuerbach)";

        public string[] FactoryIds => new string[] { "1060" };

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.ATMO_1;
    }

    public class ATMO_2ES
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0xFB116849;

        public ATMO_2ES()
            : base(UID)
        {
        }

        public override string CNT => "atmo-2es";
        public override string CN => EN;
        public override string DE => "ATMO-2ES (Spanien/Mardrid)";
        public override string EN => "ATMO-2ES (Spain/Mardrid)";
        public override string ES => "ATMO-2ES (España/Mardrid)";

        public string[] FactoryIds => new string[] { "9651" };

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.ATMO_2;
    }

    public class ATMO_3CN
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0x38DAD4F1;

        public ATMO_3CN()
            : base(UID)
        {
        }

        public override string CNT => "atmo-3cn";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "ATMO-3CN (China/Suzhou)";
        public override string ES => EN;

        public string[] FactoryIds => new string[] { "2576" };

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.ATMO_3;
    }

    public class ATMO_4NA
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0x43DF5CA2;

        public ATMO_4NA()
            : base(UID)
        {
        }

        public override string CNT => "atmo-4na";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "ATMO-4NA (USA/Charleston)";
        public override string ES => DE;

        public string[] FactoryIds => new string[] { "7910" };

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.ATMO_4;
    }

    public class ATMO_5CN
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0xE793095B;

        public ATMO_5CN()
            : base(UID)
        {
        }

        public override string CNT => "atmo-5cn";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "ATMO-5CN (China/Changsha)";
        public override string ES => DE;

        public string[] FactoryIds => new string[] { "6755" };

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.ATMO_5;
    }

    public class ATMO_6IN
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0x35A15129;

        public ATMO_6IN()
            : base(UID)
        {
        }

        public override string CNT => "atmo-6in";
        public override string CN => EN;
        public override string DE => "ATMO-6IN (Indien/Bangalore)";
        public override string EN => "ATMO-6IN (India/Bengaluru)";
        public override string ES => EN;

        public string[] FactoryIds => new string[] { "603B", "903B" };

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.ATMO_6;
    }

    public class ATMO_7MX
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0xD5663D45;

        public ATMO_7MX()
            : base(UID)
        {
        }

        public override string CNT => "atmo-7mx";
        public override string CN => EN;
        public override string DE => "ATMO-7MX (Mexico/Toluca)";
        public override string EN => "ATMO-7MX (Mexico/Toluca)";
        public override string ES => "ATMO-7MX (México/Toluca)";

        public string[] FactoryIds => new string[] {"9046"};

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.ATMO_7;
    }

    public class ATMO_8TR
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0x6F8E1899;

        public ATMO_8TR()
            : base(UID)
        {
        }

        public override string CNT => "atmo-8tr";
        public override string CN => EN;
        public override string DE => "ATMO-8TR (Türkei/Bursa)";
        public override string EN => "ATMO-8TR (Turkey/Bursa)";
        public override string ES => "ATMO-8TR (Turquía/Bursa)";

        public string[] FactoryIds => new string[] { "9395" };

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.ATMO_8;
    }

    public class ATMO_9DE
        : NamingBase,
        IATMOSite
    {

        public const long UID = 0x7B55341;

        public ATMO_9DE()
            : base(UID)
        {
        }

        public override string CNT => "atmo-9de";
        public override string CN => EN;
        public override string DE => "ATMO-9DE (Deutschland/Moehwald)";
        public override string EN => "ATMO-9DE (Germany/Moehwald)";
        public override string ES => "ATMO-9DE (Alemania/Moehwald)";

        public string[] FactoryIds => new string[] {"6740"};

        public DFCSecurity.Site DFCSecuritySiteEnum => DFCSecurity.Site.MH;
    }
}
