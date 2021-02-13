using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ActiveDirectory
{

    public class Forest
        : NamingBase
    {

        public const long UID = 0x2D7A12AA;

        public Forest()
            : base(UID)
        {
        }

        public override string CNT => "adForest";
        public override string CN => CNT;
        public override string DE => "Active Directory Gesamtstruktur (Forest)";
        public override string EN => "Active Directory Forest";
        public override string ES => EN;
    }

    public class Domain
        : NamingBase
    {

        public const long UID = 0x4A149816;

        public Domain()
            : base(UID)
        {
        }


        public override string CNT => "domain";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    public class SubDomain
        : NamingBase
    {

        public const long UID = 0x6FA5952E;

        public SubDomain()
            : base(UID)
        {
        }

        public override string CNT => "adSubDomain";
        public override string CN => CNT;
        public override string DE => "Active Directory Sub- Domäne";
        public override string EN => "Active Directory sub domain";
        public override string ES => EN;
    }

    public class OU
        : NamingBase
    {

        public const long UID = 0x8FE4C658;

        public OU()
            : base(UID)
        {
        }

        public override string CNT => "adOU";
        public override string CN => CNT;
        public override string DE => "Active Directory Organisationseinheit OU";
        public override string EN => "Active Directory organization unit OU";
        public override string ES => "Unidad de organización del Directorio Activo OU";
    }

    /// <summary>
    /// mko, 8.7.2020
    /// AD- Verzeichniseintrag für Benutzerkonto
    /// </summary>
    public class UserAccount
        : NamingBase
    {

        public const long UID = 0xF3A0402;

        public UserAccount()
            : base(UID)
        {
        }

        public override string CNT => "adUserAccount";
        public override string CN => CNT;
        public override string DE => "Active Directory Benutzerkonto";
        public override string EN => "Active Directory User account";
        public override string ES => "Cuenta de usuario de Active Directory";
    }

    public class UserNameFQN
    : NamingBase
    {

        public const long UID = 0xE0FD761E;

        public UserNameFQN()
            : base(UID)
        {
        }

        public override string CNT => "userNameFQN";
        public override string CN => CNT;
        public override string DE => "Vollqualfizierter Benutzername";
        public override string EN => "full qualified user name";
        public override string ES => EN;
    }


    public class ComputerAccount
    : NamingBase
    {

        public const long UID = 0x446A8C21;

        public ComputerAccount()
            : base(UID)
        {
        }

        public override string CNT => "adComputerAccount";
        public override string CN => CNT;
        public override string DE => "Active Directory Computer- Konto";
        public override string EN => "Active Directory computer account";
        public override string ES => "Cuenta de computadora del Directorio Activo";
    }

    public class ComputerName
    : NamingBase
    {

        public const long UID = 0x49C613A3;

        public ComputerName()
            : base(UID)
        {
        }

        public override string CNT => "computerName";
        public override string CN => CNT;
        public override string DE => "Computer Name";
        public override string EN => "Computer name";
        public override string ES => EN;
    }

    public class Group
        : NamingBase
    {

        public const long UID = 0xACB4FEFC;

        public Group()
            : base(UID)
        {
        }

        public override string CNT => "adGroup";
        public override string CN => CNT;
        public override string DE => "Active Directory Gruppe";
        public override string EN => "Active Directory Group";
        public override string ES => EN;
    }

    public class UniversalGroup
    : NamingBase
    {

        public const long UID = 0x428BCE18;

        public UniversalGroup()
            : base(UID)
        {
        }

        public override string CNT => "adUniversalGroup";
        public override string CN => CNT;
        public override string DE => "Active Directory universelle Gruppe";
        public override string EN => "Active Directory universal Group";
        public override string ES => EN;
    }

    public class GlobalGroup
    : NamingBase
    {

        public const long UID = 0xF9975940;

        public GlobalGroup()
            : base(UID)
        {
        }

        public override string CNT => "adGlobalGroup";
        public override string CN => CNT;
        public override string DE => "Active Directory globale Gruppe";
        public override string EN => "Active Directory global Group";
        public override string ES => EN;
    }

    public class Policy
    : NamingBase
    {

        public const long UID = 0xABBF7841;

        public Policy()
            : base(UID)
        {
        }

        public override string CNT => "adPolicy";
        public override string CN => CNT;
        public override string DE => "Active Directory Gruppenrichtlinie";
        public override string EN => "Active Directory policy";
        public override string ES => EN;
    }


    public class GC
        : NamingBase
    {

        public const long UID = 0x3EFCB426;

        public GC()
            : base(UID)
        {
        }


        public override string CNT => "gc";
        public override string CN => CNT;
        public override string DE => "Active Directory globaler Katalog";
        public override string EN => "Active Directory global Catalog";
        public override string ES => EN;
    }


    public class ADController
    : NamingBase
    {

        public const long UID = 0xF24AEBDC;

        public ADController()
            : base(UID)
        {
        }

        public override string CNT => "adController";
        public override string CN => CNT;
        public override string DE => "AD Diensthost";
        public override string EN => "AD Controller";
        public override string ES => CNT;
    }

}
