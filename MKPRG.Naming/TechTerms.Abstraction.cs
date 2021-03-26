using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Abstraction
{

    /// <summary>
    /// mko, 26.3.2021
    /// </summary>
    public abstract class _AbstractionBase
    : NamingBase
    {
        public _AbstractionBase(long UID, int WocVersion)
            : base(UID, WocVersion, Wocs.TechTerms._TypeTechTerms.UID, Wocs.Authors.KorneffelMartin.UID, Wocs.Nodes.DLL.MkprgNamingDll.UID,
                   new (long WocType, long Ref)[]
                    {
                        (Wocs._WocTypeNamespace.UID, Abstraction.UID)
                    })
        { }
    }

    /// <summary>
    /// mko, 3.8.2020
    /// 
    /// mko, 26.3.2026
    /// Zur Woc- Namespacedefinition erweitert.
    /// </summary>
    public class Abstraction
        : NamingBase
    {

        public const long UID = 0x95FD5F45;

        public Abstraction()
            : base(UID, 1, Wocs.TechTerms._TypeTechTerms.UID, Wocs.Authors.KorneffelMartin.UID, Wocs.Nodes.DLL.MkprgNamingDll.UID,
                    new (long RefTypeId, long WocId)[]
                    {
                        (Wocs._WocTypeNamespace.UID, Wocs.TechTerms._TypeTechTerms.UID)
                    }
                  )
        {
        }

        public override string CNT => "abstraction";
        public override string CN => "抽象";
        public override string DE => "Abstraktion";
        public override string EN => "Abstraction";
        public override string ES => "Abstracción";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Summary
        : _AbstractionBase
    {

        public const long UID = 0x25338192;

        public Summary()
            : base(UID, 1)
        {
        }

        public override string CNT => "summary";
        public override string CN => "总结";
        public override string DE => "Zusammenfassung";
        public override string EN => "Summary";
        public override string ES => "Resumen";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Breakdown
        : _AbstractionBase
    {

        public const long UID = 0xE1B819C5;

        public Breakdown()
            : base(UID, 1)
        {
        }

        public override string CNT => "breakdown";
        public override string CN => "崩溃";
        public override string DE => "Auffschlüsselung";
        public override string EN => "breakdown";
        public override string ES => "Descomposición";
    }


    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Detail
        : _AbstractionBase
    {

        public const long UID = 0x65FABFAE;

        public Detail()
            : base(UID, 2)
        {
        }

        public override string CNT => "detail";
        public override string CN => "详细介绍";
        public override string DE => "Einzelheit";
        public override string EN => "Detail";
        public override string ES => "detalle";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Specefically
        : _AbstractionBase
    {

        public const long UID = 0xAFD83AE;

        public Specefically()
            : base(UID, 1)
        {
        }

        public override string CNT => "specefically";
        public override string CN => "特别是";
        public override string DE => "konkret";
        public override string EN => "specefically";
        public override string ES => "específicamente";
    }
}
