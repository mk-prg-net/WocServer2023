using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Pronouns
{

    public interface IPronoun
        : INaming
    { }

    /// <summary>
    /// mko, 6.4.2021
    /// </summary>
    public class Pronoun
        : NamingBase
    {

        public const long UID = 0x9C2E2336;

        public Pronoun()
            : base(UID)
        {
        }

        public override string CNT => "pronoun";
        public override string CN => EN;
        public override string DE => "Pronomen";
        public override string EN => "pronoun";
        public override string ES => "Pronombre";
    }


    namespace Personal
    {

        public interface IPersonalPronoun
            : IPronoun
        { }

        /// <summary>
        /// mko, 6.4.2021
        /// </summary>
        public class PersonalPronoun
            : NamingBase
        {

            public const long UID = 0x92AA627D;

            public PersonalPronoun()
                : base(UID)
            {
            }

            public override string CNT => "personalPronoun";
            public override string CN => EN;
            public override string DE => "Personalpronomen";
            public override string EN => "personla pronoun";
            public override string ES => "Pronombre personal";
        }


        /// <summary>
        /// mko, 6.4.2021
        /// </summary>
        public class Me
            : NamingBase,
            IPersonalPronoun
        {

            public const long UID = 0xE5D584C0;

            public Me()
                : base(UID)
            {
            }

            public override string CNT => "me";
            public override string CN => "我";
            public override string DE => "Ich";
            public override string EN => "I";
            public override string ES => "I";
        }


        public class You
            : NamingBase,
            IPersonalPronoun
        {

            public const long UID = 0x3C56CEB6;

            public You()
                : base(UID)
            {
            }

            public override string CNT => "you";
            public override string CN => "你";
            public override string DE => "Du";
            public override string EN => "you";
            public override string ES => "usted";
        }
    }

    namespace Question
    {
        public interface IQuestionPronoun
            : IPronoun
        {

        }


        /// <summary>
        /// mko, 6.4.2021
        /// </summary>
        public class Who
            : NamingBase,
            IQuestionPronoun
        {

            public const long UID = 0xDA2701F8;

            public static IPronoun I = new Which();

            public Who()
                : base(UID)
            {
            }

            public override string CNT => "who";
            public override string CN => "谁";
            public override string DE => "wer";
            public override string EN => "who";
            public override string ES => "que";
        }


        /// <summary>
        /// mko, 6.4.2021
        /// </summary>
        public class Which
            : NamingBase,
            IQuestionPronoun
        {

            public const long UID = 0xD51724DA;

            public static IPronoun I = new Which();

            public Which()
                : base(UID)
            {
            }

            public override string CNT => "which";
            public override string CN => "其中";
            public override string DE => "welcher";
            public override string EN => "which";
            public override string ES => "que";
        }
    }
}
