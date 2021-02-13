using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Access.Datasources.WellKnown.ATMO.DFC
{
    /// <summary>
    /// mko, 2.7.2020
    /// Datenbank des Grunddatenmoduls
    /// </summary>
    public class GDMDatabase
     : NamingBase
    {

        public const long UID = 0x5AC4FE13;

        public GDMDatabase()
            : base(UID)
        {
        }

        public override string CNT => "gdmDB";
        public override string CN => EN;
        public override string DE => "GDM Datenbank";
        public override string EN => "GDM Database";
        public override string ES => EN;
    }

    /// <summary>
    /// DFC Dateiablage für Dokumente
    /// </summary>
    public class Filesstore
        : NamingBase
    {

        public const long UID = 0x76129FFA;

        public Filesstore()
            : base(UID)
        {
        }

        public override string CNT => "filestore";
        public override string CN => EN;
        public override string DE => "Dateiablage";
        public override string EN => "Filestore";
        public override string ES => EN;
    }

    /// <summary>
    /// Arbeitsverzeichnis des DFC- Clients
    /// </summary>
    public class WorkDir
        : NamingBase
    {

        public const long UID = 0xA91792F8;

        public WorkDir()
            : base(UID)
        {
        }

        public override string CNT => "workDir";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Workdir";
        public override string ES => EN;
    }

    /// <summary>
    /// Path- Tabelle der GDM Datenbank
    /// </summary>
    public class PathTab
        : NamingBase
    {

        public const long UID = 0xDDE5C7DF;

        public PathTab()
            : base(UID)
        {
        }

        public override string CNT => "PathTab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 2.7.2020
    /// Mara- Tabelle der GDM Datenbank
    /// </summary>
    public class MaraTab
        : NamingBase
    {

        public const long UID = 0xF6B68398;

        public MaraTab()
            : base(UID)
        {
        }

        public override string CNT => "maraTab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 2.7.2020
    /// Mara- Tabelle der GDM Datenbank
    /// </summary>
    public class Mara2Tab
        : NamingBase
    {

        public const long UID = 0xFC6F4AAC;

        public Mara2Tab()
            : base(UID)
        {
        }

        public override string CNT => "mara2Tab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }




    /// <summary>
    /// mko, 2.7.2020
    /// DokuMat- Tabelle der GDM Datenbank
    /// </summary>
    public class DokuMatTab
        : NamingBase
    {

        public const long UID = 0xC1BB025C;

        public DokuMatTab()
            : base(UID)
        {
        }

        public override string CNT => "dokuMatTab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// <summary>
    /// Users2- Tabelle der GDM Datenbank
    /// </summary>
    public class Users2Tab
        : NamingBase
    {

        public const long UID = 0x2A0B2AA;

        public Users2Tab()
            : base(UID)
        {
        }

        public override string CNT => "users2Tab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// UsersCust- Tabelle der GDM Datenbank
    /// </summary>
    public class UsersCustTab
        : NamingBase
    {

        public const long UID = 0x2F2CA093;

        public UsersCustTab()
            : base(UID)
        {
        }

        public override string CNT => "UsersCustTab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// UsersCust- Tabelle der GDM Datenbank
    /// </summary>
    public class DFCMasterTab
        : NamingBase
    {

        public const long UID = 0xAA6A3C71;

        public DFCMasterTab()
            : base(UID)
        {
        }

        public override string CNT => "DFCMasterTab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    public class UserXRolesTab
        : NamingBase
    {

        public const long UID = 0x67C97683;

        public UserXRolesTab()
            : base(UID)
        {
        }

        public override string CNT => "Bosch106UserXRolesTab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    public class StpoTab
    : NamingBase
    {

        public const long UID = 0xFA826089;

        public StpoTab()
            : base(UID)
        {
        }

        public override string CNT => "stpoTab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    public class Projektliste2Tab
        : NamingBase
    {

        public const long UID = 0xFF30443D;

        public Projektliste2Tab()
            : base(UID)
        {
        }

        public override string CNT => "projektliste2Tab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }




}
