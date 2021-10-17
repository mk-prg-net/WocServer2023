using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 10.5.2021
    /// </summary>
    public class PluralFormConversionError
        : InterfaceConversionErrorBase,
        IPluralForm
    {
        public long PluralFormOfNameInSingluarNID => TechTerms.Sets.NullValue.UID;

        public PluralFormConversionError(
                InterfaceConversionErrorTypes ErrorType,
                long NID,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            )
            : base(ErrorType, NID, CNT, CN, DE, EN, ES)
        { }
    }
}
