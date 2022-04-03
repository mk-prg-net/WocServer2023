using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 3.4.2022
/// 
/// Containerklassen wie in https://www.cplusplus.com/reference/stl/ beschreiben
/// </summary>
namespace MKPRG.Naming.TechTerms.Sets.Containers
{
    public class Array
        : NamingBase
    {
        public const long UID = 0x8304BA6;


        public Array()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "array";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    public class Stack
        : NamingBase
    {
        public const long UID = 0x7A87D728;


        public Stack()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "stack";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    public class Dictionary
        : NamingBase
    {
        public const long UID = 0xE34B3C84;


        public Dictionary()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "dictionary";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }
}
