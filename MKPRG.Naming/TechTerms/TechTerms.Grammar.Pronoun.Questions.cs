using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// mko, 6.4.2021
/// 
/// Die Fragen nach einer verantwortlichen Person für einen Vorgang, eine Sache sind in der jeweiligen
/// Sprache sehr spezifisch. Deshalb werden hier Kombinationen aus Pronomen und Prädikaten/Objekten 
/// gelistet
/// </summary>
namespace MKPRG.Naming.TechTerms.Grammar.Pronouns.Questions
{

    public interface IPronounsQuestion
        : IPronoun
    {

    }

    /// <summary>
    /// mko, 6.4.2021
    /// </summary>
    public class PronounQuestion
        : NamingBase,
        IPronounsQuestion
    {

        public const long UID = 0xA65E069A;

        public PronounQuestion()
            : base(UID)
        {
        }

        public override string CNT => "pronounQuestion";
        public override string CN => "主题问题";
        public override string DE => "Frage nach einem Subjekt";
        public override string EN => "Question about a subject";
        public override string ES => "Pregunta de un sujeto";
    }



    /// <summary>
    /// mko, 6.4.2021
    /// </summary>
    public class WhoIsResponsible
        : NamingBase,
        IPronounsQuestion
    {

        public const long UID = 0xBAE2E72;

        public static IPronounsQuestion I = new WhoIsResponsible();

        public WhoIsResponsible()
            : base(UID)
        {
        }

        public override string CNT => "whoIsResponsible";
        public override string CN => "谁负责";
        public override string DE => "Wer ist verantwortlich";
        public override string EN => "Who is responsible for";
        public override string ES => "¿Quién es el responsable de";
    }

    /// <summary>
    /// mko, 6.4.2021
    /// </summary>
    public class WhoShouldBeNotified
        : NamingBase,
        IPronounsQuestion
    {

        public const long UID = 0x7AC97E45;

        public static IPronounsQuestion I = new WhoIsResponsible();

        public WhoShouldBeNotified()
            : base(UID)
        {
        }

        public override string CNT => "whoShouldBeNotified";
        public override string CN => "应通知谁";
        public override string DE => "Wer soll benachrichtigt werden";
        public override string EN => "Who should be notified";
        public override string ES => "Quién debe ser notificado";
    }

    /// <summary>
    /// mko, 6.4.2021
    /// </summary>
    public class WhatAreMyRights
        : NamingBase,
        IPronounsQuestion
    {

        public const long UID = 0x295D57B7;

        public static IPronounsQuestion I = new WhatAreMyRights();

        public WhatAreMyRights()
            : base(UID)
        {
        }

        public override string CNT => "whatAreMyRights";
        public override string CN => "我的权利是什么";
        public override string DE => "Welche Rechte habe ich";
        public override string EN => "What are my Rights";
        public override string ES => "¿Qué derechos tengo?";
    }
}
