using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.RPN;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 7.3.2018
    /// 
    /// mko, 18.12.2018
    /// Decodiert automatisch Zeichenumschreibungen in Strings
    /// </summary>
    public class String
    : DocuEntity,
    IString
    {
        /// <summary>
        /// mko, 4.11.2020
        /// Hinzugefügt, um in anderen Funktionen wie txt die Fälle value==null effizient behandeln zu können.
        /// </summary>
        public String()
            : base(DocuEntityTypes.String)
        {
            ValueAsString = "";
        }

        public String(string value)
            : base(DocuEntityTypes.String)
        {
            // mko, 18.12.2018
            // Zeichenumschreibungen werden automatisch dekodiert
            // mko, 4.12.2020
            // Fall value == null behandelt.
            if (value != null)
                ValueAsString = UrlSaveStringEncoder.RPNUrlSaveStringDecodeIf(value, true);
        }

        public string ValueAsString { get; } = "";

        //public static String NameIsNull = new String(RCV3.NC[TTD.Composer.Errors.NameIsNull.UID].CNT);
        public static String NameIsNull = new String("Name is null");

    }

}
