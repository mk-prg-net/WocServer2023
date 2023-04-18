using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Access.Datasources
{
    /// <summary>
    /// mko, 18.6.2020
    /// Bezeichner für eine Datenquelle 
    /// </summary>
    public class DataSource : NamingBase
    {
        public const long UID = 0x4FFC99DC;

        public DataSource()
            : base(UID)
        {
        }

        public override string CNT => "datasource";
        public override string CN => "数据源";
        public override string DE => "Datenquelle";
        public override string EN => "Datasource";
        public override string ES => "Fuente de datos";

        public override string Glyph => Glyphs.Computer.DataTape;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Datenfeld 
    /// </summary>
    public class DataField : NamingBase
    {
        public const long UID = 0x470A03A8;

        public DataField()
            : base(UID)
        {
        }

        public override string CNT => "dataField";
        public override string CN => "数据字段";
        public override string DE => "Datenfeld";
        public override string EN => "data field";
        public override string ES => EN;

        public override string Glyph => Glyphs.Sets.Records;
    }


    /// <summary>
    /// mko, 18.6.2020
    /// Attribut- Wertepaar 
    /// </summary>
    public class AttributeValuePair : NamingBase
    {
        public const long UID = 0x807C7EED;

        public AttributeValuePair()
            : base(UID)
        {
        }

        public override string CNT => "attribValue";
        public override string CN => "属性值对";
        public override string DE => "Attribut- Wertepaar";
        public override string EN => "attribute- value pair";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Attributname
    /// </summary>
    public class AttributeName : NamingBase
    {
        public const long UID = 0x91A66CA0;

        public AttributeName()
            : base(UID)
        {
        }

        public override string CNT => "attribName";
        public override string CN => "属性名";
        public override string DE => "Attributname";
        public override string EN => "attribute name";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// Attributwert
    /// </summary>
    public class AttributeValue : NamingBase
    {
        public const long UID = 0x404A306B;

        public AttributeValue()
            : base(UID)
        {
        }

        public override string CNT => "attribValue";
        public override string CN => "属性值";
        public override string DE => "Attributwert";
        public override string EN => "attribute value";
        public override string ES => EN;
    }



    public class DataIntegrity : NamingBase
    {
        public const long UID = 0xFBA05CF;

        public DataIntegrity()
            : base(UID)
        {
        }

        public override string CNT => "dataIntegrity";
        public override string CN => "数据完整性";
        public override string DE => "Dateninterität";
        public override string EN => "Data integrity";
        public override string ES => "Integridad de los datos";
    }

    public class DataInconsistency : NamingBase
    {
        public const long UID = 0x1C7AF80C;

        public DataInconsistency()
            : base(UID)
        {
        }

        public override string CNT => "dataInconsistency";
        public override string CN => "数据不一致";
        public override string DE => "Dateninkonsitenz";
        public override string EN => "Data inconsistency";
        public override string ES => "Inconsistencia de los datos";

        public override string Glyph => $"{Glyphs.Computer.DataTape}{Glyphs.Signalization.Warning}";
    }

    public class DataIntegrityCheck : NamingBase
    {
        public const long UID = 0x2290D015;

        public DataIntegrityCheck()
            : base(UID)
        {
        }

        public override string CNT => "dataIntegrityCheck";
        public override string CN => "数据完整性检查";
        public override string DE => "Prüfung der Datenintegrität";
        public override string EN => "Data integrity check";
        public override string ES => "Comprobación de la integridad de los datos";

        public override string Glyph => $"{Glyphs.Computer.DataTape}{Glyphs.Validation.Check}";

    }


    public class DataConsistencyCheck : NamingBase
    {
        public const long UID = 0x44105F51;

        public DataConsistencyCheck()
            : base(UID)
        {
        }

        public override string CNT => "dataConsistencyCheck";
        public override string CN => "数据一致性检查";
        public override string DE => "Datenkonsitenzprüfung";
        public override string EN => "Data consistency check";
        public override string ES => "Comprobación de la consistencia de los datos";

        public override string Glyph => $"{Glyphs.Computer.DataTape}{Glyphs.Validation.Check}";
    }



}
