using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 7.3.2019
/// Allgemeine Statusbeschreibungen
/// </summary>
namespace MKPRG.Naming.DocuTerms.StateDescription
{
    /// <summary>
    /// Details eines Zustandes
    /// </summary>
    public class Details
        : NamingBase
    {
        public const long UID = 0xF5957636;

        public Details()
            : base(UID)
        {
        }

        public override string CNT => "details";
        public override string CN => "细节";
        public override string DE => "Details";
        public override string EN => "Details";
        public override string ES => "Detalles";

        public override string Glyph => Glyphs.DataAndDocuments.SemanticMarkup.DetailInformations;
    }

    /// <summary>
    /// Kurzbeschreibung des aktuellen Zustandes
    /// </summary>
    public class WhatsUp
        : NamingBase
    {

        public const long UID = 0x346F10F0;

        public WhatsUp()
            : base(UID)
        {
        }

        public override string CNT => "WhatsUp";
        public override string CN => "什么事";
        public override string DE => "Was ist los?";
        public override string EN => "Whats up?";
        public override string ES => "¿Qué pasa?";

        public override string Glyph => Glyphs.Signalization.Attention;
    }

    /// <summary>
    /// Kurzbeschreibung des aktuellen Zustandes
    /// </summary>
    public class Why
        : NamingBase
    {
        public const long UID = 0xD2DCFAF7;

        public Why()
            : base(UID)
        {
        }

        public override string CNT => "Why";
        public override string CN => "何以";
        public override string DE => "Warum?";
        public override string EN => "Why?";
        public override string ES => "Por qué";

        public override string Glyph => Glyphs.Signalization.PleaseNote;
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class CurrentState
    : NamingBase
    {
        public const long UID = 0x280724F2;

        public CurrentState()
            : base(UID)
        {
        }

        public override string CNT => "currentState";
        public override string CN => "现状";
        public override string DE => "aktueller Zustand";
        public override string EN => "current State";
        public override string ES => "estado actual";

        public override string Glyph => Glyphs.Automaton.ActiveState;
    }

    /// <summary>
    /// mko, 22.11.2018
    /// Beschreibt den von einer Prozedur/Funktion am Ende erreichten Zustand. Wenn die 
    /// Funktion nicht die erwarteten Ergebnisse liefern konnte, werden hier z.B. die
    /// Ursachen beschrieben.
    /// 
    /// </summary>
    public class FinStateDescr
    : NamingBase
    {
        public const long UID = 0x7C1A4E42;

        public FinStateDescr()
            : base(UID)
        {
        }

        public override string CNT => "finStateDescr";
        public override string CN => "最后状态说明";
        public override string DE => "Beschreibung des Endzustandes";
        public override string EN => "Description of final state";
        public override string ES => "Descripción del estado final";

        public override string Glyph => Glyphs.Workflows.FinalState;
    }

    /// <summary>
    /// mko, 9.7.2020
    /// </summary>
    public class FinalState
        : NamingBase
    {
        public const long UID = 0xA1762B66;

        public FinalState()
            : base(UID)
        {
        }

        public override string CNT => "finalState";
        public override string CN => "最后状态";
        public override string DE => "Endzustand";
        public override string EN => "Final state";
        public override string ES => "Estado final";

        public override string Glyph => Glyphs.Workflows.FinalState;
    }

    /// <summary>
    /// mko, 9.7.2020
    /// </summary>
    public class Contradiction
        : NamingBase
    {
        public const long UID = 0xBE379C68;

        public Contradiction()
            : base(UID)
        {
        }

        public override string CNT => "contradiction";
        public override string CN => "矛盾";
        public override string DE => "Widerspruch";
        public override string EN => "Contradiction";
        public override string ES => "Contradicción";

        public override string Glyph => Glyphs.Weather.flash;
    }
}
