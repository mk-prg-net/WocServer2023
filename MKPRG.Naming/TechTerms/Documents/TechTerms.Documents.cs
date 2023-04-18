using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 27.1.2021
/// Dokumentstrukturen
/// </summary>
namespace MKPRG.Naming.TechTerms.Documents
{
    /// <summary>
    /// Zeile in einem Dokument
    /// </summary>
    public class Line
        : NamingBase
    {

        public const long UID = 0x72733496;

        public Line()
            : base(UID)
        {
        }

        public override string CNT => "line";
        public override string CN => "线路";
        public override string DE => "Zeile";
        public override string EN => "Line";
        public override string ES => "Línea";
    }

    /// <summary>
    /// Spalte in einem Dokument
    /// </summary>
    public class Column
        : NamingBase
    {

        public const long UID = 0x89F9518C;

        public Column()
            : base(UID)
        {
        }

        public override string CNT => "column";
        public override string CN => "专栏";
        public override string DE => "Spalte";
        public override string EN => "Column";
        public override string ES => "columna";
    }





}
