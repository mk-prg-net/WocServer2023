using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Grammar.Adverbs
{
    public interface IAdverb
        : INaming
    { }

    public interface IAdverbCausal
        : IAdverb
    {
    }


    public interface IAdverbModal
        : IAdverb
    {
    }


    public interface IAdverbInstrumantal
        : IAdverb
    {
    }

    public interface IAdverbLocal
        : IAdverb
    {
    }

    public interface IAdverbDirectional
        : IAdverb
    {
    }


    public interface IAdverbPositional
        : IAdverb
    {
    }


    public interface IAdverbTemporal
        : IAdverb
    {
    }

    /// <summary>
    /// mko, 11.5.2021
    /// </summary>
    public class Adverb
        : NamingBase
    {

        public const long UID = 0x94D6EEFB;

        public Adverb()
            : base(UID)
        {
        }

        public override string CNT => "adverb";
        public override string CN => "副词";
        public override string DE => "Adverb";
        public override string EN => "Adverb";
        public override string ES => "Adverbio";
    }

    /// <summary>
    /// mko, 12.5.2021
    /// </summary>
    public class AdverbConversationError
        : InterfaceConversionErrorBase,
        IAdverb
    {
        public AdverbConversationError
            (
                InterfaceConversionErrorTypes ErrorType,
                long NIDofAdverb,
                string CNT,
                string CN,
                string DE,
                string EN,
                string ES
            ) : base(ErrorType, NIDofAdverb, CNT, CN, DE, EN, ES) { }
    }

    /// <summary>
    /// mko, 3.9.2020
    /// </summary>
    public class Why
        : NamingBase,
        IAdverbCausal
    {

        public const long UID = 0xC79FE7AC;

        public Why()
            : base(UID)
        {
        }

        public override string CNT => "why";
        public override string CN => "何以";
        public override string DE => "warum";
        public override string EN => "why";
        public override string ES => "por qué";
    }


    public class Irrelevant
        : NamingBase, IAdverb
    {

        public const long UID = 0x8596C4B9;

        public Irrelevant()
            : base(UID)
        {
        }

        public override string CNT => "irrelevant";
        public override string CN => "不相干";
        public override string DE => "irrelevant";
        public override string EN => "irrelevant";
        public override string ES => "irrelevante";
    }

    public class Successful
        : NamingBase, IAdverb, Grammar.Adjectives.IAdjective
    {

        public const long UID = 0x41BF133F;

        public Successful()
            : base(UID)
        {
        }

        public override string CNT => "successful";
        public override string CN => "成功的";
        public override string DE => "erfolgreich";
        public override string EN => "successful";
        public override string ES => "éxito";

        public override string Glyph => Glyphs.Gestures.ThumbsUp;
    }

    public class NotSuccessful
    : NamingBase, IAdverb, Adjectives.IAdjective
    {

        public const long UID = 0x98161049;

        public NotSuccessful()
            : base(UID)
        {
        }

        public override string CNT => "notSuccessful";
        public override string CN => "不成功的";
        public override string DE => "nicht erfolgreich";
        public override string EN => "not successful";
        public override string ES => "sin éxito";

        public override string Glyph => Glyphs.Gestures.ThumbsDown;
    }

    public class Exclusively
        : NamingBase, IAdverb
    {

        public const long UID = 0x31A74F91;

        public Exclusively()
            : base(UID)
        {
        }

        public override string CNT => "only";
        public override string CN => "只有";
        public override string DE => "ausschließlich";
        public override string EN => "exclusively";
        public override string ES => "sólo";

    }
}
