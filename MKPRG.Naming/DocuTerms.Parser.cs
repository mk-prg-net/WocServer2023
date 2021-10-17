using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Parser
{
    public class DocuTermParser
        : NamingBase
    {
        public const long UID = 0x1FA31682;

        public DocuTermParser()
            : base(UID)
        { }

        public override string CNT => GetType().Name;

        public override string DE => "DocuTerm Textanalyse (Parser)";

        public override string EN => "DocuTerm Parser";

        public override string ES => EN;

        public override string CN => "DocuTerm 解析器";
    }

    public class RPNDocuTerm
    : NamingBase
    {
        public const long UID = 0x50731934;

        public RPNDocuTerm()
            : base(UID)
        { }

        public override string CNT => GetType().Name;

        public override string DE => "Serialisierter DocuTerm in Reverse Polish Notation (RPN)";

        public override string EN => "Serialized DocuTerm in Reverse Polish Notation (RPN)";

        public override string ES => "DocuTerm serializado en notación polaca inversa (RPN)";

        public override string CN => "串行化的DocuTerm的反向波兰语符号（RPN）。";
    }
}
