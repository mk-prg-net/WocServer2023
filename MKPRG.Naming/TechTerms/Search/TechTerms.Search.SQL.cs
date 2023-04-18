using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Search.SQL
{
    /// <summary>
    /// mko, 8.1.2021
    /// </summary>
    public class SqlQuery
    : NamingBase
    {

        public const long UID = 0x3444444;

        public SqlQuery()
            : base(UID)
        {
        }

        public override string CNT => "sql";

        public override string CN => "SQL查询";

        public override string DE => "SQL Abfrage";

        public override string EN => "SQL Query";

        public override string ES => "Consulta SQL";
    }

}
