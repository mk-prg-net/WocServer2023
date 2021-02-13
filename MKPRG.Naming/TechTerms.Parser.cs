using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Parser
{

    /// mko, 19.6.2020
    /// Parser
    /// </summary>
    public class Parser
        : NamingBase
    {
        public const long UID = 0xC7FBA436;

        public Parser()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "parser";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// mko, 19.6.2020
    /// Prozess des Parsens eines Ausdruckes in einer formalen Sprache
    /// </summary>
    public class Parse
        : NamingBase
    {
        public const long UID = 0x5B70734;

        public Parse()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "parse";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// mko, 19.6.2020
    /// Prozess des Parsens eines Ausdruckes in einer formalen Sprache
    /// </summary>
    public class Language
        : NamingBase
    {
        public const long UID = 0x343CB5EE;

        public Language()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "language";
        public override string DE => "formale Sprache";
        public override string EN => CNT;
        public override string ES => "Idioma";
    }


    /// mko, 19.6.2020
    /// Apparat zum Auflösen von Texten in einer Formalen Sparache in Tokens
    /// </summary>
    public class Tokenizer
        : NamingBase
    {
        public const long UID = 0x2F397A2C;

        public Tokenizer()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "tokenizer";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// mko, 19.6.2020
    /// Text einer Sprache in Tokens auflösen.
    /// </summary>
    public class Tokenize
        : NamingBase
    {
        public const long UID = 0x5453E050;

        public Tokenize()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "tokenize";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    /// mko, 19.6.2020
    /// Token
    /// </summary>
    public class Token
        : NamingBase
    {
        public const long UID = 0xD179F730;

        public Token()
            : base(UID)
        { }

        public override string CN => CNT;
        public override string CNT => "token";
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }



    /// mko, 19.6.2020
    /// Text einer Sprache in Tokens auflösen.
    /// </summary>
    public class SyntaxError
        : NamingBase
    {
        public const long UID = 0x44851A8B;

        public SyntaxError()
            : base(UID)
        { }

        public override string CN => EN;
        public override string CNT => "syntaxError";
        public override string DE => "syntaktischer Fehler";
        public override string EN => "Syntax Error";
        public override string ES => EN;
    }

}
