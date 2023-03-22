using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//<unit_header>
//----------------------------------------------------------------
//
// Martin Korneffel: IT Beratung/Softwareentwicklung
// Stuttgart, den 27.5.2016
//
//  Projekt.......: mko.RPN
//  Name..........: Parser.cs
//  Aufgabe/Fkt...: Ein String mit einem Ausdruck in der RPN- Notation 
//                  (Reverse Polish Notation => Eingabesyntax der programmierbaren 
//                   HP- Taschenrechner aus den 1970-er Jahren)
//                  wie 2 3 ADD 5 MUL <=> (2 + 3) * 5 
//                  wird eingelesen, analysiert und im Falle eines korrekten 
//                  Ausdruckes ausgewertet.
//                  Der Parser ist generisch implementiert, und kann 
//                  zum Einlesen bliebiger Typ 2- Sprachenterme verwendet werden.
//                  Das Motiv zur Entwicklung war ein System von Filter und Sortierausdrücken
//                  in Uri's zu implementieren. Sollen z.B. in einem Webkatalog für 
//                  astronomische Daten nach allen Riesenplaneten gesucht werden,
//                  (schwerer als 2 Erdmassen), mit mehr als 10 Monden, sortiert nach dem 
//                  Sonnenabstand, könnte dies in einem URL wie folgt kodiert werden:
//                  http://<host:port>/Kepler/Planets/Index?filter=2 EM dblMax MassRng 10 intMax MoonCount asc OrderBySemiMajorAxisLength     
//                  2 EM dblMax MassRng             -> MassRange(mko.Newton.Mass.Earthmass(2), mko.Newton.Mass.Kilogramm(double.MaxValue)
//                  10 intMax MoonCount             -> MoonCount(10, int.MaxValue)
//                  asc OrderBySemiMajorAxisLength  -> OrderBySemiMajoRAxisLength(false)
// 
//                  Ab 6.4.2017 werden die Methoden TokenizeRPN un TokenizePN angeboten
//                  TokenizePN kann dabei insbesondere eine Quelltext in der polnischen Notation
//                  einlesen, und gibt diesen dann als Liste von Tokens in der umgekehrt polnischen
//                  Notation zurück
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
//  Datum.........: 6.4.2017
//  Änderungen....: Die Methoden TokenizeRPN und TokenizePN wurden implementiert.
//                  TokenizePN kann dabei insbesondere eine Quelltext in der polnischen Notation
//                  einlesen, und gibt diesen dann als Liste von Tokens in der umgekehrt polnischen
//                  Notation zurück.
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 11.3.2018
//  Änderungen....: Neue Version ParserV2, die eine Reimplementierung der Klasse Parser aus den Vorversionen
//                  darstellt.
//                  Reimplementierung des Parsers. Vorversion dokumentierte unzureichend die beim Parsen 
//                  eine PN/RPN Strings gefundenen Fehler. Konfiguration des Parsers war auf vielfältige 
//                  weise möglich.
//                  1) Verinfachte Konfiguration (Evaluator- Tabelle nur noch im Konstruktor injezieren)
//                  2) Rückmeldung von syntaktischen Fehlern im Parser in strukturierter Form
//                  3) Anstatt mit Exceptions mit mko.Logging.RC Fehler rückmelden
//  
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 3.3.2030
//  Änderungen....: Parser kann jetzt Funktionen Verarbeiten, welche anstatt die Parameter zu reduzieren
//                  wie .Add 2 3 -> 5, vermehren können, wie z.B. LastYear -> Now-1Yera Now.
//                  Dazu musste die Parserschleife modifiziert werden.
//                  Der TokenBuffer des Parsers enthält nach dem Evaluieren alle geparsten und generierten 
//                  Tokens. 
//                  Achtung: Die CountEvaluated sind im Falle von generierenden Tokens unter Umständen falsch!!
//
//  Autor.........: Martin Korneffel (mko)
//  Datum.........: 5.3.2020
//  Änderungen....: Neue Version ParserV3. Auf die Generierung von TokenBuffer etc. wird jetzt verzichtet.
//                  Das Tokenizing findet jetzt integriert in der Parse- Methode statt.
//</unit_history>
//</unit_header>        



using mko.Logging;


namespace mko.RPN
{
    /// <summary>
    /// mko, 11.3.2018
    /// Reimplementierung des Parsers. Vorversion dokumentierte unzureichend die beim Parsen 
    /// eine PN/RPN Strings gefundenen Fehler. Konfiguration des Parsers war auf vielfältige 
    /// weise möglich.
    /// 1) Verinfachte Konfiguration (Evaluator- Tabelle nur noch im Konstruktor injezieren)
    /// 2) Rückmeldung von syntaktischen Fehlern im Parser in strukturierter Form
    /// 3) Anstatt mit Exceptions mit mko.Logging.RC Fehler rückmelden
    /// </summary>
    public class ParserV3
    {
        /// <summary>
        /// mko, 4.3.2020
        /// Liefert das Ergebnis der Evaluierung eines pn/rpn- Ausdruckes mittels Parser
        /// 
        /// mko, 5.3.2020
        /// TokenBuffer etc. entfernt. Konzentration jetzt auf das reine Parsen und evaluieren.
        /// 
        /// mko, 25.6.2020
        /// Eigenschaft LastParserException hinzugefügt, um die Exceptions inklusive ihrer Detaildaten zurückzugeben
        /// </summary>
        public class Result
        {
            public Result(RC<IToken[]> ResultOfTokenizer, Stack<IToken> Stack, int indexOfLastProcessedToken)
            {
                this.ResultOfTokenizer = ResultOfTokenizer;
                this.Stack = Stack;
                this.IndexOfLastProcessedToken = IndexOfLastProcessedToken;
            }

            public Result(RC<IToken[]> ResultOfTokenizer, Stack<IToken> Stack, int indexOfLastProcessedToken, Exception LastException)
            {
                this.ResultOfTokenizer = ResultOfTokenizer;
                this.Stack = Stack;
                this.IndexOfLastProcessedToken = IndexOfLastProcessedToken;
                this.LastParserException = LastException;
            }


            public int IndexOfLastProcessedToken { get; }

            /// <summary>
            /// Ergebnis des Tokenizers
            /// </summary>
            public RC<IToken[]> ResultOfTokenizer { get; }

            /// <summary>
            /// Stack, used for evaluation of pn/rpn term
            /// </summary>
            public Stack<IToken> Stack { get; }

            /// <summary>
            /// Falls ein Fehler auftrat, dann kann hier die zuletzt 
            /// </summary>
            public Exception LastParserException { get; }


            /// <summary>
            /// Initialwert, es fand noch keine Evaluierung statt
            /// </summary>
            public static Result NullObject
            {
                get
                {
                    if (_NullObject == null)
                    {
                        _NullObject = new Result(null, null, 0);
                    }
                    return _NullObject;
                }

            }
            public static Result _NullObject;

        }


        /// <summary>
        /// Konstruktor
        /// </summary>
        /// <param name="tokenizer">Achtung: kann ein tokenizer für polish notation oder reverse polish notation sein</param>
        /// <param name="evalTab">Tabelle, die einem Funktionsnamen einen Evaluator zuordnet</param>
        public ParserV3(IReadOnlyDictionary<string, IEval> FuncEvaluators)
        {
            this.FuncEvaluators = FuncEvaluators;
        }

        IReadOnlyDictionary<string, IEval> FuncEvaluators;

        /// <summary>
        /// mko, 11.3.2018
        /// Parst eine Liste von Tokens und evaluiert sie, falls möglich. Das Ergebnis ist wird 
        /// durch den Zustand des Stapelspeichers des Kellerautomaten repräsentiert.
        /// </summary>
        /// <param name="Tokens">Liste von Tokens. Wurde zuvor mittels BasicTokenizer.TokenizePN oder TokenizeRPN erstellt</param>
        /// <returns></returns>
        public RC<Result> Parse(string pnTerm, bool TermInReversePolishNotation = false, bool doUrlDecode = false)
        {
            RC<Result> rc = RC<Result>.Ok(Result.NullObject);

            // 1. Phase: pn- Ausdruck in eine Liste von Tokens umwandeln
            var getTokens = TermInReversePolishNotation
                        ? BasicTokenizer.TokenizeRPN(pnTerm, doUrlDecode, FuncEvaluators.Keys.ToArray())
                        : BasicTokenizer.TokenizePN(pnTerm, doUrlDecode, FuncEvaluators.Keys.ToArray());

            if (!getTokens.Succeeded)
            {
                rc = RC<Result>.Failed(new Result(getTokens, null, 0), $"Tokenizing failed of {pnTerm}");
            }
            else
            {

                var Tokens = getTokens.Value;

                // Stack der Tokens
                var stack = new Stack<IToken>();

                int ixToken = 0;

                try
                {
                    //int count; // = Tokens.Count();

                    // mko, 3.3.2020
                    // Alle Tokens werden in einen Stack umgeladen. Dies ermöglicht auch die Evaluierung 
                    // von Generator- Funktionen. Vorher war dies nicht möglich.
                    var inStack = new Stack<IToken>(Tokens.Length);

                    foreach (var tok in Tokens.Reverse())
                    {
                        inStack.Push(tok);
                    }

                    for (; inStack.Any() && rc.Succeeded; ixToken++)
                    {
                        var token = inStack.Pop();
                        
                        if (token.IsFunctionName)
                        {
                            var funcname = token.Value;
                            if (FuncEvaluators.ContainsKey(funcname))
                            {
                                var fe = FuncEvaluators[funcname];

                                int paramCount = stack.Count;
                                fe.Eval(stack);
                                if (!fe.Succesful)
                                {
                                    rc = RC<Result>.Failed(value: new Result(getTokens, stack, ixToken, fe.EvalException),
                                        ErrorDescription: $"Evaluation of {funcname} fails: {fe.EvalException}");
                                }
                                else
                                {                                    
                                    if (stack.Count > paramCount && !(stack.Peek() is ListEndToken))
                                    {
                                        // Die soeben evalutierte Funktion hat die anzahl der Parameter auf dem 
                                        // Stack vergrößert anstatt zu vermindern (=Generator)                                  
                                        // In nachfolgenden Schleifenzyklen sind diese auszuwerten.
                                        // Dabei ist zu beachten:
                                        // 1) Die generierten Werte sind keine Funktionstokens
                                        // 2) Unter den generierten Werten befinden sich Funktionstokens
                                        // 3) Unter den generierten Werten befinden sich Funktionstokens, 
                                        //    welche Generatoren sind.
                                        // Wegen 2) und 3) müssen alle zusätzlich generierten Token zurück auf das Band
                                        // geschrieben werden, damit die Evaluierung aller Funktionstokens
                                        // garantiert bleibt!

                                        while (stack.Count() > paramCount)
                                        {
                                            inStack.Push(stack.Pop());
                                        }
                                    }
                                }
                            }
                            else
                            {
                                rc = RC<Result>.Failed(value: new Result(getTokens, stack, ixToken),
                                        ErrorDescription: $"unknown function {funcname} found");
                            }
                        }
                        else
                        {
                            stack.Push(token);
                        }
                    }

                    if (rc.Succeeded)
                    {
                        // Letzten Stand des Stacks sichern nach erfolgreicher Evaluierung sichern
                        rc = RC<Result>.Ok(new Result(getTokens, stack, ixToken));
                    }

                }
                catch (Exception ex)
                {
                    rc = RC<Result>.Failed(
                        value: new Result(getTokens, stack, ixToken, ex),
                        ErrorDescription: ExceptionHelper.FlattenExceptionMessages(ex));
                }
            }

            return rc;
        }
    }
}
