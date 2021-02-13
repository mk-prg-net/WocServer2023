using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 12.10.2020
/// Besondere Zeitpunkte im Lebenszyklus eines Objketes
/// </summary>
namespace MKPRG.Naming.TechTerms.Lifecycle.Timeline
{
    /// <summary>
    /// mko, 12.10.2020
    /// Zeitpunkt der Erstellung
    /// </summary>
    public class Created
        : NamingBase
    {

        public const long UID = 0x40F4940A;

        public Created()
            : base(UID)
        {
        }

        public override string CNT => "created";
        public override string CN => EN;
        public override string DE => "Zeitpunkt der Erstellung";
        public override string EN => "created";
        public override string ES => "creado";
    }

    /// <summary>
    /// mko, 12.10.2020
    /// Zeitpunkt der Löschnung
    /// </summary>
    public class Deleted
        : NamingBase
    {

        public const long UID = 0xE07412DE;

        public Deleted()
            : base(UID)
        {
        }

        public override string CNT => "deleted";
        public override string CN => EN;
        public override string DE => "Zeitpunkt der Löschung";
        public override string EN => "deleted";
        public override string ES => "eliminado";
    }



}
