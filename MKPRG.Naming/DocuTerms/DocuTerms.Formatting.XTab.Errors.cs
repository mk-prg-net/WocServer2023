using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.DocuTerms.Formatting.XTab.Errors
{
    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class XTabStructInvalid
        : NamingBase
    {

        public const long UID = 0x74D3374C;

        public XTabStructInvalid()
            : base(UID)
        {
        }

        public override string CNT => "crossTableStructInvalid";
        public override string CN => "交叉表到DocuTerms的映射不正确。";
        public override string DE => "Die Abbildung einer Kreuztabelle auf DocuTerms ist unkorrekt.";
        public override string EN => "The mapping of a crosstab on DocuTerms is incorrect";
        public override string ES => "El mapeo de una lengüeta cruzada a DocuTerms es incorrecto.";
    }

}
