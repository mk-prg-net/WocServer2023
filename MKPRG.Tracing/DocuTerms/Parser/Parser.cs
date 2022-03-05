using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using ANC = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

using PN = MKPRG.Tracing.DocuTerms.Parser;
using DT = MKPRG.Tracing.DocuTerms;
using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 27.3.2018
    /// </summary>
    public static class Parser
    {
        /// <summary>
        /// mko, 16.4.2018
        /// </summary>
        //public static PNDocuTerms.DocuEntities.Composer Composer = new DocuEntities.Composer();        

        /// <summary>
        /// mko, 27.3.2018
        /// Specialiazed parser for DocEntity strings
        /// 
        /// mko, 26.4.2018
        /// Added Parameter fn to inject functionnames
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        public static RC<IDocuEntity> Parse(string pn, IFn fn, bool doRPNUrlDecode = true)
        {
            RC<IDocuEntity> rc = null;
            IDocuEntity NullEntity = null;

            var pnL = new Composer();

            var ncTools = new ANC.Tools();
            var getNC = ncTools.GetNamingConcurrentDictOf("ATMO.DFC.Naming", pnL);

            if (!getNC.Succeeded)
            {
                rc = RC<DT.IDocuEntity>.Failed(getNC.Message);
            }
            else
            {

                var fmt = new DT.PNFormater(fn, getNC.Value);
                var evalTab = new FunctionEvaluatorTable(new FunctionEvalMapperFunctor(fn, pnL));
                var _parser = new ParserV2(evalTab.FuncEvaluators);

                var rcT = BasicTokenizer.TokenizePN(pn, doRPNUrlDecode, evalTab.FuncEvaluators.Keys.ToArray());

                if (rcT.Succeeded)
                {
                    var rcp = _parser.Parse(rcT.Value);

                    DT.IDocuEntity val = rcp.Value.Stack.Count > 0 && rcp.Value.Stack.Peek() is IDocuEntity
                                                                    ? (IDocuEntity)rcp.Value.Stack.Peek()
                                                                    : rcp.Value.Stack.Count > 0 ? pnL.txt(rcp.Value.Stack.Peek().ToString()) : pnL.txt("No Result, Parser Stack is empty");

                    if (rcp.Succeeded && rcp.Value.Stack.Count == 1)
                    {
                        rc = RC<IDocuEntity>.Ok(value: val);
                    }
                    else
                    {
                        rc = RC<IDocuEntity>.Failed(NullEntity, ErrorDescription: "Parse failed", inner: new RC<ParserV2.Result>(rcp));
                    }
                }
                else
                {
                    rc = RC<IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.txt("Tokenizer failed"));
                }
            }

            return rc;

        }

        /// <summary>
        /// mko, 15.11.2018
        /// Neuer Parserfunktion mit leistungsfähigeren Rückgabewert.
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="fn"></param>
        /// <param name="pnL"></param>
        /// <returns></returns>
        public static RC<IDocuEntity> Parse18_11(string pn, IFn fn, DT.IComposer pnL, bool doRPNUrlDecode = true)
        {
            RC<IDocuEntity> rc = null;
            IDocuEntity NullEntity = null;

            var fmt = new PNFormater(fn, RC.NC, ANC.Language.CNT);

            try
            {
                var evalTab = new FunctionEvaluatorTable(new FunctionEvalMapperFunctor(fn, pnL));
                var _parser = new ParserV2(evalTab.FuncEvaluators);

                pn = NormalizePN(pn, fn);

                var rcT = BasicTokenizer.TokenizePN(pn, doRPNUrlDecode, evalTab.FuncEvaluators.Keys.ToArray());

                if (rcT.Succeeded)
                {
                    var rcp = _parser.Parse(rcT.Value);

                    DT.IDocuEntity val = rcp.Value.Stack.Peek() is IDocuEntity
                                                                    ? (IDocuEntity)rcp.Value.Stack.Peek()
                                                                    : pnL.txt(rcp.Value.Stack.Peek().ToString());

                    if (rcp.Succeeded && rcp.Value.Stack.Count == 1)
                    {
                        rc = RC<IDocuEntity>.Ok(value: val);
                    }
                    else
                    {
                        rc = RC<IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.m("Parse", pnL.ret(pnL.eFails())), inner: RC.TranformToRCV3(rcp));
                    }
                }
                else
                {
                    rc = RC<IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.i("Tokenizer", pnL.m("Tokenize", pnL.ret(pnL.eFails()))), inner: RC.TranformToRCV3(rcT));
                }
            }
            catch (Exception ex)
            {
                rc = RC<IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.i("Parser", pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex)))));
            }

            return rc;
        }

        private static string NormalizePN(string pn, IFn fn)
        {
            // mko, 25.6.2019
            // Normalisieren der Eingabe: Leerzeichen zwischen den RPN Funktionsnamen und Parametern einfügen
            // 1. Symbole durch sichere Ersatzsymbole austauschen

            pn = pn.Replace(fn.Txt, "~$1")
                   .Replace(fn.List, "~$2")
                   .Replace(fn.ListEnd, "~$3")
                   .Replace(fn.Property, "~$4")
                   .Replace(fn.Date, "~$5")
                   .Replace(fn.Event, "~$6")
                   .Replace(fn.Function, "~$7")
                   .Replace(fn.Instance, "~$8")
                   .Replace(fn.Return, "~$9")
                   .Replace(fn.Time, "~$0")
                   .Replace(fn.Version, "~$A")
                   .Replace(fn.Method, "~$B");

            // 2. Ersatztsymbole mit Whitespaces umrahmen
            pn = pn.Replace("~$1", " ~$1 ")
                   .Replace("~$2", " ~$2 ")
                   .Replace("~$3", " ~$3 ")
                   .Replace("~$4", " ~$4 ")
                   .Replace("~$5", " ~$5 ")
                   .Replace("~$6", " ~$6 ")
                   .Replace("~$7", " ~$7 ")
                   .Replace("~$8", " ~$8 ")
                   .Replace("~$9", " ~$9 ")
                   .Replace("~$0", " ~$0 ")
                   .Replace("~$A", " ~$A ")
                   .Replace("~$B", " ~$B ");

            // 3. Ersatztsymbole durch Originale austauschen
            pn = pn.Replace("~$1", fn.Txt)
                   .Replace("~$2", fn.List)
                   .Replace("~$3", fn.ListEnd)
                   .Replace("~$4", fn.Property)
                   .Replace("~$5", fn.Date)
                   .Replace("~$6", fn.Event)
                   .Replace("~$7", fn.Function)
                   .Replace("~$8", fn.Instance)
                   .Replace("~$9", fn.Return)
                   .Replace("~$0", fn.Time)
                   .Replace("~$A", fn.Version)
                   .Replace("~$B", fn.Method);
            return pn;
        }

        /// <summary>
        /// mko, 8.6.2020
        /// Diese Reimplementierung basiert auf den neuen mko.RPN.ParserV3. Für boolsche Werte, Zahlen und Datumswerte werden 
        /// jetzt streng typisierte Dokuterme erzeugt. Zudem werden Naming- Ids erkannt.
        /// 
        /// mko, 9.8.2021
        /// Eingabe pn auf null und leer hin geprüft, und Fehler behandelt.
        /// Auswertung des Parser- Ergebnisses jetzt genauer: Leere Stacks werden jetzt explizit erkannt, und eine Fehlermeldung 
        /// zurückgegeben.
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="fn"></param>
        /// <param name="pnL"></param>
        /// <param name="doRPNUrlDecode"></param>
        /// <returns></returns>
        public static RC<DT.IDocuEntity> Parse20_06(
            string pn,
            IFn fn,
            DT.IComposer pnL,
            //DocuEntities.IFormater fmt,
            bool doRPNUrlDecode = true)
        {
            RC<DT.IDocuEntity> rc = null;
            DT.IDocuEntity NullEntity = null;

            try
            {
                if (!string.IsNullOrWhiteSpace(pn))
                {

                    var evalTab = new FunctionEvaluatorTable(new FunctionEvalMapperFunctor(fn, pnL));
                    var parser = new ParserV3(evalTab.FuncEvaluators);

                    // mko, 19.11.2020
                    // Entfernen von HTML kodierten Leerzeichen
                    if (doRPNUrlDecode)
                    {
                        pn = pn.Replace("%20", " ");
                    }

                    pn = NormalizePN(pn, fn);

                    var getParsed = parser.Parse(pn, false, doRPNUrlDecode);

                    if (!getParsed.Succeeded || getParsed.Value.Stack.Count != 1)
                    {
                        rc = RC<IDocuEntity>.Failed(
                            value: NullEntity,
                            ErrorDescription:
                            pnL.i(TTD.StateDescription.FinStateDescr.UID,
                                pnL.m(TT.Parser.Parse.UID,
                                    pnL.p(TTD.MetaData.Name.UID, "Parse20_06"),
                                    pnL.ret(pnL.eFails(
                                            pnL.i(TT.Parser.SyntaxError.UID,
                                                pnL.KillInstanceMemberIf(getParsed.Value.LastParserException == null || !(getParsed.Value.LastParserException is IExceptionWithDocuTermDescription), () => pnL.p(TTD.StateDescription.WhatsUp.UID, getParsed.Message)),
                                                pnL.KillInstanceMemberIf(getParsed.Value.LastParserException != null && getParsed.Value.LastParserException is IExceptionWithDocuTermDescription, () => pnL.p(TTD.StateDescription.WhatsUp.UID, pnL.EncapsulateAsPropertyValue(((IExceptionWithDocuTermDescription)getParsed.Value.LastParserException)?.MessageAsDocuTerm))),
                                                pnL.KillInstanceMemberIf(getParsed.Value.ResultOfTokenizer.Succeeded,
                                                    () => pnL.p(TTD.StateDescription.Why.UID,
                                                            pnL.i(TT.Parser.Tokenizer.UID,
                                                                pnL.p("IxLastProcessedToken", getParsed.Value.IndexOfLastProcessedToken),
                                                                pnL.m(TT.Parser.Tokenize.UID,
                                                                    pnL.eFails(getParsed.Value.ResultOfTokenizer.Message)))))))))));
                    }
                    else
                    {
                        // Fall: auf dem Stapel liegt genau ein Ergebnis

                        DT.IDocuEntity val = null;

                        if (getParsed.Value.Stack.Peek() is DT.IDocuEntity docE)
                        {
                            val = docE;
                        }
                        else if (getParsed.Value.Stack.Peek() is global::mko.RPN.StringToken strtok)
                        {
                            val = pnL.txt(strtok.Value);
                        }
                        else if (getParsed.Value.Stack.Peek() is IToken tok)
                        {
                            val = pnL.txt(tok.ToString());
                        }

                        rc = RC<IDocuEntity>.Ok(val);
                    }
                }
                else
                {
                    // Fall: Der zu parsende DocuTerm ist leer.
                    var nh = new ANC.NamingHelper(RC.NC);

                    rc = RC<IDocuEntity>.Failed(
                                value: NullEntity,
                                ErrorDescription:                                
                                pnL.ReturnAfterFailureWithDetails(
                                    TT.Parser.Parse.UID,
                                    pnL.i(TTD.MetaData.Block.UID,
                                        pnL.p(TTD.MetaData.Name.UID, "Parse20_06"),
                                        pnL.p(TTD.MetaData.Description.UID,
                                            pnL.FinishedActivityStatement(
                                                TTD.Parser.RPNDocuTerm.UID,
                                                nh.fA(TT.Grammar.Verbs.Was.UID),
                                                pnL.DefObject(TT.Sets.EmptySet.UID))))));
                }
            }
            catch (Exception ex)
            {
                rc = RC<IDocuEntity>.Failed(
                    value: NullEntity,
                    ErrorDescription: pnL.i(TT.Parser.Parser.UID,
                                        pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex)))));
            }

            return rc;
        }

    }
}
