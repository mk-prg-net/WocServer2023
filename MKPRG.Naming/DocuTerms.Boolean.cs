using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 9.6.2020
/// Bennenung von Wahrheitswerten etc.
/// </summary>
namespace MKPRG.Naming.DocuTerms.Boolean
{
    /// <summary>
    /// mko, 26.3.2021
    /// Namensraum der Booleans
    /// </summary>
    public class Boolean
        : NamingBase
    {
        public const long UID = 0xD7960352;

        public Boolean()
            : base(UID, 1, Wocs.DocuTerms._TypeDocuTerms.UID, Wocs.Authors.KorneffelMartin.UID, Wocs.Nodes.DLL.MkprgNamingDll.UID,
                  new (long RefTypeId, long WocId)[]
                  {
                      (Wocs._WocTypeNamespace.UID, Wocs.DocuTerms._TypeDocuTerms.UID)
                  })
        { }

        public override string CNT => "Boolean";

        public override string DE => CNT;

        public override string EN => CNT;

        public override string ES => CNT;

        public override string CN => CNT;
    }

    public abstract class _BooleanBase
        : NamingBase
    {

        public _BooleanBase(long UID, int WocVersion)
            : base(UID, WocVersion, Wocs.DocuTerms._TypeDocuTerms.UID, Wocs.Authors.KorneffelMartin.UID, Wocs.Nodes.DLL.MkprgNamingDll.UID,
                  new (long RefTypeId, long WocId)[]
                  {
                      (Wocs._WocTypeNamespace.UID, Wocs.DocuTerms._TypeDocuTerms.UID)
                  })
        { }
    }

    /// <summary>
    /// Wahrheitswert für wahr
    /// </summary>
    public class True
    : _BooleanBase
    {
        public const long UID = 0x93EA7C6B;

        public True()
            : base(UID, 1)
        { }        

        public override string CNT => EN;

        public override string DE => "wahr";

        public override string EN => "true";

        public override string ES => "verdadero";

        public override string CN => "真正";
    }

    /// <summary>
    /// Wahrheitswert für falsch
    /// </summary>
    public class False
        : _BooleanBase
    {
        public const long UID = 0x5046A757;

        public False()
            : base(UID, 1)
        { }

        public override string CNT => EN;

        public override string DE => "falsch";

        public override string EN => "false";

        public override string ES => "falso";

        public override string CN => "假的";
    }
}
