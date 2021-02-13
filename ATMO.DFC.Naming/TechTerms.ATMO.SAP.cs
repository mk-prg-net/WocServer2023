using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ATMO.SAP
{
    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class Transaktion : NamingBase
    {
        public const long UID = 0x8F809F39;

        public Transaktion()
            : base(UID)
        {
        }

        public override string CNT => "sapTransaction";
        public override string CN => EN;
        public override string DE => "SAP Transaktion";
        public override string EN => "SAP Transaction";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class ZTransaktion : NamingBase
    {
        public const long UID = 0x1733CB9F;

        public ZTransaktion()
            : base(UID)
        {
        }

        public override string CNT => "sapZTransaction";
        public override string CN => EN;
        public override string DE => "SAP Z- Transaktion";
        public override string EN => "SAP Z- Transaction";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class ProjectBuilder : NamingBase
    {
        public const long UID = 0x550D887D;

        public ProjectBuilder()
            : base(UID)
        {
        }

        public override string CNT => "sapProjectBuilder";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "SAP Project Builder";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class ZSTLM : NamingBase
    {
        public const long UID = 0xDACD43FA;

        public ZSTLM()
            : base(UID)
        {
        }

        public override string CNT => "sapZSTLM";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "SAP ZSTLM";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class ZSTLDRUCK : NamingBase
    {
        public const long UID = 0xD95D2531;

        public ZSTLDRUCK()
            : base(UID)
        {
        }

        public override string CNT => "sapZSTLDRUCK";
        public override string CN => EN;
        public override string DE => "SAP Stücklistendruck (ZSTLDRUCK)";
        public override string EN => "SAP BOM print (ZSTLDRUCK)";
        public override string ES => EN;
    }



    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class BomStatus : NamingBase
    {
        public const long UID = 0x89762167;

        public BomStatus()
            : base(UID)
        {
        }

        public override string CNT => "sapBomStatus";
        public override string CN => EN;
        public override string DE => "SAP Stücklistenstatus";
        public override string EN => "SAP BOM Status";
        public override string ES => "Estado de la lista de materiales de SAP";
    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class BomStatusNo : NamingBase
    {
        public const long UID = 0xCB23A695;

        public BomStatusNo()
            : base(UID)
        {
        }

        public override string CNT => "sapBomStatusNo";
        public override string CN => EN;
        public override string DE => "SAP Stücklistenstatusnummer";
        public override string EN => "SAP BOM Status No.";
        public override string ES => "Número de estado de la lista de materiales de SAP";
    }
}
