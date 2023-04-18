using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Operators
{
    /// <summary>
    /// mko, 8.7.2021
    /// Festlegung, Definition.
    /// </summary>
    public class Definition
        : NamingBase
    {
        public const long UID = 0xE140299F;

        public Definition()
            : base(UID)
        {
        }

        public override string CNT => "definition";
        public override string CN => "定义";
        public override string DE => "Definition";
        public override string EN => "Definition";
        public override string ES => "Definición";

        public override string Glyph => Glyphs.Math.Definition;
    }




    /// <summary>
    /// mko, 5.10.2020
    /// Wertebereich einer Funktion
    /// </summary>
    public class Domain
        : NamingBase
    {
        public const long UID = 0x8E628398;

        public Domain()
            : base(UID)
        {
        }

        public override string CNT => "domain";
        public override string CN => "定义区域";
        public override string DE => "Definitionsbereich";
        public override string EN => "domain";
        public override string ES => "Definición de área";

        public override string Glyph => Glyphs.Math.Functions.Domain;
    }

    /// <summary>
    /// Definitionsbereich einer Funktion
    /// </summary>
    public class CoDomain
        : NamingBase
    {
        public const long UID = 0x27DA7574;

        public CoDomain()
            : base(UID)
        {
        }

        public override string CNT => "codomain";
        public override string CN => "价值范围";
        public override string DE => "Wertebereich";
        public override string EN => "cdomain";
        public override string ES => "codominio";

        public override string Glyph => Glyphs.Math.Functions.CoDomain;
    }

    /// <summary>
    /// mko, 14.9.2020
    /// Grundbegriff der mathematischen Funktion/Zuordnung
    /// </summary>
    public class Mapping
        : NamingBase
    {
        public const long UID = 0xD246DDBB;

        public Mapping()
            : base(UID)
        {
        }

        public override string CNT => "mapping";
        public override string CN => "作业";
        public override string DE => "Zuordnung";
        public override string EN => "Mapping";
        public override string ES => "Mapeo";

        public override string Glyph => Glyphs.Math.Functions.Mapping;
    }

    /// <summary>
    /// mko, 14.9.2020
    /// </summary>
    public class MapTo
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xB57F0B4E;

        public static MapTo I { get; } = new MapTo();

        public MapTo()
            : base(UID)
        {
        }

        public override string CNT => "mapTo";
        public override string CN => "映射到";
        public override string DE => "wird eindeutig zugewiesen an";
        public override string EN => "map to";
        public override string ES => "está claramente asignado a";

        public override string Glyph => Glyphs.ArrowsAndLines.arrO;
    }

    public class FunctionName
        : NamingBase
    {
        public const long UID = 0x97BA793D;

        public FunctionName()
            : base(UID)
        {
        }

        public override string CNT => "funcName";
        public override string CN => "功能名称";
        public override string DE => "Funktionsname";
        public override string EN => "function name";
        public override string ES => "Nombre de la función";

        public override string Glyph => Glyphs.Math.Functions.FunctionF;
    }

}
