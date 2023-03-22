//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 14.6.2016
//
//  Projekt.......: mko.RPN
//  Name..........: IntToken.cs
//  Aufgabe/Fkt...: Token für Integer
//                  
//
//
//
//
//<unit_environment>
//------------------------------------------------------------------
//  Zielmaschine..: PC 
//  Betriebssystem: Windows 7 mit .NET 4.5
//  Werkzeuge.....: Visual Studio 2013
//  Autor.........: Martin Korneffel (mko)
//  Version 1.0...: 
//
// </unit_environment>
//
//<unit_history>
//------------------------------------------------------------------
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 18.5.2017
//  Änderungen....: Erweitert um die Konvertierungsoperatoren double und int
//
//</unit_history>
//</unit_header>        

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mko.RPN
{
    /// <summary>
    /// mko, 8.6.2020
    /// Integers werden jetzt als 64bit- Werte gespeichert
    /// </summary>
    public class IntToken : NoFunctionToken
    {
        public IntToken(long Value, int CountOfEvaluatedTokens = 1)
            : base(CountOfEvaluatedTokens)
        {
            _Value = Value;            
        }

        long _Value;

        public int ValueAsInt => (int)_Value;

        public long ValueAsLong => _Value;
        
        protected override string ValueToString => _Value.ToString();

        public override bool IsInteger => true;

        public override bool IsBoolean => false;

        public override bool IsNummeric => true;

        public override IToken Copy()
        {
            return new IntToken(_Value, CountOfEvaluatedTokens);
        }

        public override string ToString()
        {
            return "int(" + ValueToString + ")";
        }

        public static implicit operator int(IntToken tok)
        {
            return tok.ValueAsInt;
        }

        /// <summary>
        /// mko, 8.6.2020
        /// </summary>
        /// <param name="tok"></param>
        public static implicit operator long(IntToken tok)
        {
            return tok.ValueAsLong;
        }

        public static implicit operator double(IntToken tok)
        {
            return tok.ValueAsInt;
        }

    }
}
