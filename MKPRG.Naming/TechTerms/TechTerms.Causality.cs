using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Causality
{
    public class Causality : NamingBase
    {
        public const long UID = 0x2F1B361A;

        public Causality()
            : base(UID)
        { }

        public override string CN => "因果关系";
        public override string CNT => "causality";
        public override string DE => "Kausalität (Ursache-Wirkung)";
        public override string EN => "Causality";
        public override string ES => "Causalidad";
    }

    public class Causal : NamingBase, Grammar.Adverbs.IAdverb
    {
        public const long UID = 0xFE0B9998;

        public Causal()
            : base(UID)
        { }

        public override string CN => "因";
        public override string CNT => "causal";
        public override string DE => "ursächlich";
        public override string EN => "causal";
        public override string ES => "causalmente";
    }


    public class Cause : NamingBase
    {
        public const long UID = 0xE0AC7FBF;

        public Cause()
            : base(UID)
        { }

        public override string CN => "因";
        public override string CNT => "cause";
        public override string DE => "Ursache";
        public override string EN => "cause";
        public override string ES => "Causa";
    }


    public class Causes : PluralForm
    {
        public const long UID = 0xB35694CB;

        public Causes()
            : base(UID)
        { }

        public override string CN => "原因";
        public override string CNT => "causes";
        public override string DE => "Ursachen";
        public override string EN => "causes";
        public override string ES => "Causas";

        public override long PluralFormOfNameInSingluarNID => Cause.UID;
    }

    public class Caused : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x9F1A8DB3;

        public Caused()
            : base(UID)
        { }

        public override string CN => "原因";
        public override string CNT => "causes";
        public override string DE => "verursacht";
        public override string EN => "caused";
        public override string ES => "causado";
    }

    public class HasCaused : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xC4D6146D;

        public HasCaused()
            : base(UID)
        { }

        public override string CN => "原因";
        public override string CNT => "hasCauses";
        public override string DE => "verursachte";
        public override string EN => "caused";
        public override string ES => "causado";
    }

    public class WasCausedBy : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xE4D0618B;

        public WasCausedBy()
            : base(UID)
        { }

        public override string CN => "是由以下原因造成的";
        public override string CNT => "wasCausedBy";
        public override string DE => "wurde verursacht durch";
        public override string EN => "was caused by";
        public override string ES => "fue causada por";
    }

    public class CanCause : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x5DA1BC54;

        public CanCause()
            : base(UID)
        { }

        public override string CN => "可能会导致";
        public override string CNT => "canCause";
        public override string DE => "kann verursachen";
        public override string EN => "can cause";
        public override string ES => "puede causar";
    }

    public class CantCause : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0x40926C96;

        public CantCause()
            : base(UID)
        { }

        public override string CN => "不能导致";
        public override string CNT => "cantCause";
        public override string DE => "kann nicht verursachen";
        public override string EN => "can't cause";
        public override string ES => "no puede causar";
    }






    public class ForTheFollowingReason : PluralForm
    {
        public const long UID = 0x9DC19339;

        public ForTheFollowingReason()
            : base(UID)
        { }

        public override string CN => "原因如下";
        public override string CNT => "ForTheFollowingReason";
        public override string DE => "aus folgendem Grund";
        public override string EN => "for the following reason";
        public override string ES => "por la siguiente razón";
    }


    public class ForTheFollowingReasons : PluralForm
    {
        public const long UID = 0xF738D92;

        public ForTheFollowingReasons()
            : base(UID)
        { }

        public override string CN => "原因如下";
        public override string CNT => "forTheFollowingReasons";
        public override string DE => "aus folgenden Gründen";
        public override string EN => "for the following reasons";
        public override string ES => "por las siguientes razones";

        public override long PluralFormOfNameInSingluarNID => ForTheFollowingReason.UID;
    }

    public class Effect : NamingBase
    {
        public const long UID = 0x400A1669;

        public Effect()
            : base(UID)
        { }

        public override string CN => "效果";
        public override string CNT => "effect";
        public override string DE => "Wirkung";
        public override string EN => "Effect";
        public override string ES => "Efecto";
    }


    public class Consequences : PluralForm
    {
        public const long UID = 0x30983215;

        public Consequences()
            : base(UID)
        { }

        public override string CN => "的后果";
        public override string CNT => "consequences";
        public override string DE => "Folgen";
        public override string EN => "Consequences";
        public override string ES => "consecuencias";

        public override long PluralFormOfNameInSingluarNID => Effect.UID;
    }
}
