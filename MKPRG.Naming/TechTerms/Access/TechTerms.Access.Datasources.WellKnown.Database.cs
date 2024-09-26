using System;
using System.Collections.Generic;
using System.Text;


namespace MKPRG.Naming.TechTerms.Access.Datasources.WellKnown.Database
{
    /// <summary>
    /// mko, 26.5.2024
    /// </summary>
    public class Database
        : NamingBase
    {
        public const long UID = 0x3D2F067CDC2166A0L;

        public Database()
            : base(UID)
        {
        }

        public override string CNT => "database";
        public override string CN => EN;
        public override string DE => "Datenbank";
        public override string EN => "Database";
        public override string ES => EN;

        public override string Glyph => Glyphs.Sets.Table;
    }

    public class DatabaseQuery
    : NamingBase
    {
        public const long UID = 0x1A116D4B2BE852AEL;

        public DatabaseQuery()
            : base(UID)
        {
        }

        public override string CNT => "databaseQuery";
        public override string CN => EN;
        public override string DE => "Datenbank Abfrage";
        public override string EN => "Database Query";
        public override string ES => EN;

        public override string Glyph => Glyphs.Math.Sets.FilterOp;
    }

    public class DatabaseQueryFailed
        : NamingBase
    {
        public const long UID = 0x460E6A533FFA9E4DL;

        public DatabaseQueryFailed()
            : base(UID)
        {
        }

        public override string CNT => "databaseQueryFailed";
        public override string CN => EN;
        public override string DE => "Datenbank Abfrage ist gescheitert";
        public override string EN => "Database Query failed";
        public override string ES => EN;

        public override string Glyph => Glyphs.Validation.Invalid;
    }


}
