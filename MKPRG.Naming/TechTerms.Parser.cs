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

        public override string CN => "解析器";
        public override string CNT => "parser";
        public override string DE => "Parser";
        public override string EN => CNT;
        public override string ES => CNT;
    }


    /// mko, 19.6.2020
    /// Prozess des Parsens eines Ausdruckes in einer formalen Sprache
    /// </summary>
    public class Parse
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x5B70734;

        public static Parse I { get; } = new Parse();

        public Parse()
            : base(UID)
        { }

        public override string CN => "解析";
        public override string CNT => "parse";
        public override string DE => "parsen";
        public override string EN => CNT;
        public override string ES => "analizar";
    }


    public class CantBeParsed
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0xC2588A3B;

        public CantBeParsed()
            : base(UID)
        { }

        public override string CN => "不能被解析";
        public override string CNT => "cantBeParsed";
        public override string DE => "kann nicht geparsed werden";
        public override string EN => "cant be parsed";
        public override string ES => "no puede ser analizado";
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

        public override string CN => "形式语言";
        public override string CNT => "language";
        public override string DE => "formale Sprache";
        public override string EN => "formal language";
        public override string ES => "lenguaje formal";
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

        public override string CN => "记号器";
        public override string CNT => "tokenizer";
        public override string DE => "Tokenizer";
        public override string EN => "Tokenizer";
        public override string ES => "tokenizador";
    }

    /// mko, 19.6.2020
    /// Text einer Sprache in Tokens auflösen.
    /// </summary>
    public class Tokenize
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x5453E050;

        public static Tokenize I { get; } = new Tokenize();

        public Tokenize()
            : base(UID)
        { }

        public override string CN => "代号化";
        public override string CNT => "tokenize";
        public override string DE => "tokenisieren";
        public override string EN => CNT;
        public override string ES => "tokenizar";
    }


    public class CantBeTokenized
        : NamingBase, Grammar.IModalPhrase
    {
        public const long UID = 0xA8076D4D;

        public CantBeTokenized()
            : base(UID)
        { }

        public override string CN => "不能被标记化";
        public override string CNT => "cantBetokenized";
        public override string DE => "kann nicht tokenisiert werden";
        public override string EN => "cant be tokenized";
        public override string ES => "no puede ser tokenizado";
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

        public override string CN => "象征性";
        public override string CNT => "token";
        public override string DE => "Token";
        public override string EN => CNT;
        public override string ES => "ficha";
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

        public override string CN => "句法错误";
        public override string CNT => "syntaxError";
        public override string DE => "syntaktischer Fehler";
        public override string EN => "Syntax Error";
        public override string ES => "error de sintaxis";

        public override string Glyph => Glyphs.Software.SyntaxError;

    }

}
