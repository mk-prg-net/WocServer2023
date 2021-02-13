using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar
{
    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Subject
    : NamingBase
    {

        public const long UID = 0xEA9EC638;

        public Subject()
            : base(UID)
        {
        }

        public override string CNT => "subject";
        public override string CN => CNT;
        public override string DE => "Subjekt";
        public override string EN => "Subject";
        public override string ES => "Asunto";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Object
        : NamingBase
    {

        public const long UID = 0x3A9DAAFB;

        public Object()
            : base(UID)
        {
        }

        public override string CNT => "object";
        public override string CN => CNT;
        public override string DE => "Objekt";
        public override string EN => "Object";
        public override string ES => "Objeto";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Verb
    : NamingBase
    {

        public const long UID = 0x6B447D49;

        public Verb()
            : base(UID)
        {
        }

        public override string CNT => "verb";
        public override string CN => CNT;
        public override string DE => "Verb";
        public override string EN => "Verb";
        public override string ES => "Verbo";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class Preposition
        : NamingBase
    {

        public const long UID = 0x856D3CC5;

        public Preposition()
            : base(UID)
        {
        }

        public override string CNT => "prepos";
        public override string CN => CNT;
        public override string DE => "Präposition";
        public override string EN => "Preposition";
        public override string ES => "Preposición";
    }

}
