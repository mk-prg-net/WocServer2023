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
        : IDocuEntity,
        IPropertyValue
        //IEventParameter,
        //IReturnValue
    {
        /// <summary>
        /// mko, 4.11.2020
        /// Hinzugefügt, um in anderen Funktionen wie txt die Fälle value==null effizient behandeln zu können.
        /// </summary>
        public String()
        {
            Value = "";
        }

        public String(string value)
        {
            // mko, 18.12.2018
            // Zeichenumschreibungen werden automatisch dekodiert
            // mko, 4.12.2020
            // Fall value == null behandelt.
            if (value != null)
                this.Value = UrlSaveStringEncoder.RPNUrlSaveStringDecodeIf(value, true);
            else
                this.Value = "";
        }

        public DocuEntityTypes EntityType => DocuEntityTypes.String;

        public string Value { get; }

        public int CountOfEvaluatedTokens => 1;

        public IEnumerable<IDocuEntity> Childs => new IDocuEntity[]{};

        public bool IsFunctionName => true;

        public bool IsInteger => false;

        public bool IsBoolean => false;

        public bool IsNummeric => false;

        public IToken Copy()
        {
            return new String(Value);
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
