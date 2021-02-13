using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Operators.Tree
{
    /// <summary>
    /// mko, 19.11.2020
    /// Durchlaufen eines Baumes
    /// </summary>
    public class Traverse
        : NamingBase
    {
        public const long UID = 0xABBF5154;

        public  Traverse()
            : base(UID)
        {
        }

        public override string CNT => "traverse";
        public override string CN => EN;
        public override string DE => "durchqueren";
        public override string EN => "traverse";
        public override string ES => "atravesando";
    }

    /// <summary>
    /// Kindknoten- Ebene öffnen/aufklappen
    /// </summary>
    public class Fold
        : NamingBase
    {
        public const long UID = 0xABCB6F7A;

        public Fold()
            : base(UID)
        {
        }

        public override string CNT => "fold";
        public override string CN => EN;
        public override string DE => "zuklappen";
        public override string EN => "fold";
        public override string ES => "cerrar";
    }

    /// <summary>
    /// Kindknoten- Ebene schließen/zuklappen 
    /// </summary>
    public class Unfold
    : NamingBase
    {
        public const long UID = 0xED3514AD;

        public Unfold()
            : base(UID)
        {
        }

        public override string CNT => "unfold";
        public override string CN => EN;
        public override string DE => "aufklappen";
        public override string EN => "unfold";
        public override string ES => "abrir";
    }




}
