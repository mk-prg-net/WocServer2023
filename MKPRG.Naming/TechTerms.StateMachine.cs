using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.StateMachine
{
    /// <summary>
    /// mko, 9.7.2020
    /// </summary>
    public class StateTransition
        : NamingBase
    {
        public const long UID = 0xD1ED9457;

        public StateTransition()
            : base(UID)
        {
        }

        public override string CNT => "stateTransition";
        public override string CN => EN;
        public override string DE => "Zustansübergang";
        public override string EN => "State transistion";
        public override string ES => "Transcripción de estado";
    }

    /// <summary>
    /// mko, 9.7.2020
    /// </summary>
    public class Input
        : NamingBase
    {
        public const long UID = 0x63733BC5;

        public Input()
            : base(UID)
        {
        }

        public override string CNT => "input";
        public override string CN => EN;
        public override string DE => "Eingabe";
        public override string EN => "Input";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 9.7.2020
    /// </summary>
    public class InputType
        : NamingBase
    {
        public const long UID = 0x36888EA;

        public InputType()
            : base(UID)
        {
        }

        public override string CNT => "inputType";
        public override string CN => EN;
        public override string DE => "Art der Eingabe";
        public override string EN => "Input type";
        public override string ES => EN;
    }


    public class Output
        : NamingBase
    {
        public const long UID = 0x36A1CFD5;

        public Output()
            : base(UID)
        {
        }

        public override string CNT => "output";
        public override string CN => EN;
        public override string DE => "Ausgabe";
        public override string EN => "Output";
        public override string ES => EN;
    }

    /// <summary>
    /// mko, 9.7.2020
    /// </summary>
    public class OutputType
        : NamingBase
    {
        public const long UID = 0x42A88E44;

        public OutputType()
            : base(UID)
        {
        }

        public override string CNT => "outputType";
        public override string CN => EN;
        public override string DE => "Art der Ausgabe";
        public override string EN => "Output type";
        public override string ES => EN;
    }

}
