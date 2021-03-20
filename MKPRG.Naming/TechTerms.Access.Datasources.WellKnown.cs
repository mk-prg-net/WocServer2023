using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Access.Datasources.WellKnown
{
    /// <summary>
    /// mko, 22.6.2020
    /// </summary>
    public class WindowsRegistry
     : NamingBase
    {

        public const long UID = 0x9596600;

        public WindowsRegistry()
            : base(UID)
        {
        }


        public override string CNT => "winRegistry";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Windows Registry";
        public override string ES => EN;
    }

    public class ActiveDirectory
        : NamingBase
    {
        public const long UID = 0x2172B3C3;

        public ActiveDirectory()
            : base(UID)
        {
        }

        public override string CNT => "ad";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "Active Directory";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 3.12.2020
    /// </summary>
    public class DataBaseTable
        : NamingBase
    {
        public const long UID = 0xE82778C3;

        public DataBaseTable()
            : base(UID)
        {
        }

        public override string CNT => "dataBaseTable";
        public override string CN => EN;
        public override string DE => "Datenbank Tabelle";
        public override string EN => "Database Table";
        public override string ES => "Tabla de la base de datos";
    }


}
