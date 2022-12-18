using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Search
{
    public class Search
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0xC44BE765;

        public Search()
            : base(UID)
        {
        }

        public override string CNT => "search";

        public override string CN => "搜索";

        public override string DE => "suche";

        public override string EN => "search";

        public override string ES => "busca en";

        public override string Glyph => Glyphs.Math.Sets.FilterOp;

    }

    public class Filter
        : NamingBase
    {

        public const long UID = 0x71E59B6E;

        public Filter()
            : base(UID)
        {
        }

        public override string CNT => "searchFilter";
        public override string CN => "搜索过滤器";
        public override string DE => "Suchfilter";
        public override string EN => "Search filter";
        public override string ES => "Filtro de búsqueda";

        public override string Glyph => Glyphs.Math.Sets.FilterOp;

    }

    /// <summary>
    /// Id eines Datensatzes
    /// </summary>
    public class Id
        : NamingBase
    {

        public const long UID = 0x9FDB4932;

        public Id()
            : base(UID)
        {
        }

        public override string CNT => "id";
        public override string CN => EN;
        public override string DE => "ID";
        public override string EN => "ID";
        public override string ES => "ID";

        public override string Glyph => Glyphs.Authentication.ID;
    }

    /// <summary>
    /// mko, 19.6.2020
    /// Zugriffsschlüssel
    /// </summary>
    public class Key
        : NamingBase
    {

        public const long UID = 0x93966AAF;

        public Key()
            : base(UID)
        {
        }

        public override string CNT => "key";
        public override string CN => "钥匙";
        public override string DE => "Zugriffsschlüssel";
        public override string EN => "Key";
        public override string ES => "Key";

        public override string Glyph => Glyphs.DataAndDocuments.Key;
    }

    /// <summary>
    /// mko, 3.7.2020
    /// Ein gesuchtes Objekt wurde nicht gefunden 
    /// </summary>
    public class NotFound
        : NamingBase, Grammar.IFinishedActivity
    {

        public const long UID = 0x7243D72F;

        public NotFound()
            : base(UID)
        {
        }

        public override string CNT => "notFound";
        public override string CN => "未找到";
        public override string DE => "nicht gefunden";
        public override string EN => "not found";
        public override string ES => "no se encuentra";

        public override string Glyph => Glyphs.Validation.OutOfRange;
    }

}
