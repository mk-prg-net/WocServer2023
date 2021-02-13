using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.DFC.Naming.TechTerms.Access.ATMO.DFC.Errors
{
    public class MatNoDoesNotReferToExpectedType 
        : NamingBase
    {
        public const long UID = 0xF020784;

        public MatNoDoesNotReferToExpectedType()
            : base(UID)
        {
        }

        public override string CNT => "matNoDoesNotReferToExpectedType";
        public override string CN => "材料编号与预期类型不符";
        public override string DE => "Materialnummer verweist nicht auf den erwarteten Typ";
        public override string EN => "Material number does not refer to the expected type";
        public override string ES => "El número de material no se refiere al tipo esperado";
    }
}
