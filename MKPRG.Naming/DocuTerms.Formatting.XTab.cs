using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Formatting.XTab
{

    /// <summary>
    /// mko, 22.7.2020
    /// Bezeichner xTab- Instanz
    /// </summary>
    public class XTab
        : NamingBase
    {
        public const long UID = 0x751981DC;

        public XTab()
            : base(UID)
        {
        }

        public override string CNT => "xTab";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }



    /// <summary>
    /// mko, 22.7.2020
    /// Bezeichner für Zeilenauflistung
    /// </summary>
    public class Dim1
        : NamingBase
    {
        public const long UID = 0x652D47E;

        public Dim1()
            : base(UID)
        {
        }

        public override string CNT => "dim1";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// <summary>
    /// mko, 22.7.2020
    /// Bezeichner für Spaltenauflistung
    /// </summary>
    public class Dim2
        : NamingBase
    {
        public const long UID = 0x88168A6B;

        public Dim2()
            : base(UID)
        {
        }

        public override string CNT => "dim2";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    public class Values
        : NamingBase
    {
        public const long UID = 0xE45FD687;

        public Values()
            : base(UID)
        {
        }

        public override string CNT => "values";
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


}
