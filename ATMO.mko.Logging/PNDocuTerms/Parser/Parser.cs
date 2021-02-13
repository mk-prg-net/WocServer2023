﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;

using ANC = MKPRG.Naming;

namespace ATMO.mko.Logging.PNDocuTerms.Parser
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
        public static RCV2<DocuEntities.IDocuEntity> Parse(string pn, IFn fn, bool doRPNUrlDecode = true)
        {
            RCV2<DocuEntities.IDocuEntity> rc = null;
            DocuEntities.IDocuEntity NullEntity = null;

            var pnL = new DocuEntities.Composer();

            var ncTools = new MKPRG.Naming.Tools();
            var getNC = ncTools.GetNamingConcurrentDictOf("MKPRG.Naming", pnL);

            if (!getNC.Succeeded)
            {
                rc = RCV2<DocuEntities.IDocuEntity>.Failed(getNC.Message);
            }
            else
            {

                var fmt = new DocuEntities.PNFormater(fn, getNC.Value);
                var evalTab = new FunctionEvaluatorTable(new FunctionEvalMapperFunctor(fn, pnL));
                var _parser = new ParserV2(evalTab.FuncEvaluators);

                var rcT = BasicTokenizer.TokenizePN(pn, doRPNUrlDecode, evalTab.FuncEvaluators.Keys.ToArray());

                if (rcT.Succeeded)
                {
                    var rcp = _parser.Parse(rcT.Value);

                    PNDocuTerms.DocuEntities.IDocuEntity val = rcp.Value.Stack.Peek() is PNDocuTerms.DocuEntities.IDocuEntity
                                                                    ? (DocuEntities.IDocuEntity)rcp.Value.Stack.Peek()
                                                                    : pnL.txt(rcp.Value.Stack.Peek().ToString());

                    if (rcp.Succeeded && rcp.Value.Stack.Count == 1)
                    {
                        rc = RCV2<DocuEntities.IDocuEntity>.Ok(value: val);
                    }
                    else
                    {
                        rc = RCV2<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: "Parse failed", inner: new RCV2<IToken>(rcp));
                    }
                }
                else
                {
                    rc = RCV2<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: "Tokenizer failed");
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
        public static RCV3sV<DocuEntities.IDocuEntity> Parse18_11(string pn, IFn fn, DocuEntities.IComposer pnL, bool doRPNUrlDecode = true)
        {
            RCV3sV<DocuEntities.IDocuEntity> rc = null;
            DocuEntities.IDocuEntity NullEntity = null;

            var fmt = new DocuEntities.PNFormater(fn, new DFC.Naming.Tools().GetNamingContainerAsConcurrentDict("MKPRG.Naming"), DFC.Naming.Language.CNT);

            try
            {
                var evalTab = new FunctionEvaluatorTable(new FunctionEvalMapperFunctor(fn, pnL));
                var _parser = new ParserV2(evalTab.FuncEvaluators);

                pn = NormalizePN(pn, fn);

                var rcT = BasicTokenizer.TokenizePN(pn, doRPNUrlDecode, evalTab.FuncEvaluators.Keys.ToArray());

                if (rcT.Succeeded)
                {
                    var rcp = _parser.Parse(rcT.Value);

                    PNDocuTerms.DocuEntities.IDocuEntity val = rcp.Value.Stack.Peek() is PNDocuTerms.DocuEntities.IDocuEntity
                                                                    ? (DocuEntities.IDocuEntity)rcp.Value.Stack.Peek()
                                                                    : pnL.txt(rcp.Value.Stack.Peek().ToString());

                    if (rcp.Succeeded && rcp.Value.Stack.Count == 1)
                    {
                        rc = RCV3sV<DocuEntities.IDocuEntity>.Ok(value: val);
                    }
                    else
                    {
                        rc = RCV3sV<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.m("Parse", pnL.ret(pnL.eFails())), inner: RCV3.TranformToRCV3(rcp));
                    }
                }
                else
                {
                    rc = RCV3sV<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.i("Tokenizer", pnL.m("Tokenize", pnL.ret(pnL.eFails()))), inner: RCV3.TranformToRCV3(rcT));
                }
            }
            catch (Exception ex)
            {
                rc = RCV3sV<DocuEntities.IDocuEntity>.Failed(NullEntity, ErrorDescription: pnL.i("Parser", pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex)))));
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
        /// </summary>
        /// <param name="pn"></param>
        /// <param name="fn"></param>
        /// <param name="pnL"></param>
        /// <param name="doRPNUrlDecode"></param>
        /// <returns></returns>
        public static RCV3sV<DocuEntities.IDocuEntity> Parse20_06(
            string pn, 
            IFn fn, 
            DocuEntities.IComposer pnL,
            //DocuEntities.IFormater fmt,
            bool doRPNUrlDecode = true)
        {
            RCV3sV<DocuEntities.IDocuEntity> rc = null;
            DocuEntities.IDocuEntity NullEntity = null;

            try
            {
                var evalTab = new FunctionEvaluatorTable(new FunctionEvalMapperFunctor(fn, pnL));
                var parser = new global::mko.RPN.ParserV3(evalTab.FuncEvaluators);

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
                    rc = RCV3sV<DocuEntities.IDocuEntity>.Failed(NullEntity,
                        ErrorDescription: 
                        pnL.i(ANC.DocuTerms.StateDescription.FinStateDescr.UID, 
                            pnL.m(ANC.TechTerms.Parser.Parse.UID,
                                pnL.ret(pnL.eFails(
                                        pnL.i(ANC.TechTerms.Parser.SyntaxError.UID,
                                            pnL.KillIf(getParsed.Value.LastParserException == null || !(getParsed.Value.LastParserException is IExceptionWithDocuTermDescription), () => pnL.p(ANC.DocuTerms.StateDescription.WhatsUp.UID, getParsed.Message)),
                                            pnL.KillIf(getParsed.Value.LastParserException != null && getParsed.Value.LastParserException is IExceptionWithDocuTermDescription, () => pnL.p(ANC.DocuTerms.StateDescription.WhatsUp.UID, pnL.EncapsulateAsPropertyValue(((IExceptionWithDocuTermDescription)getParsed.Value.LastParserException)?.MessageAsDocuTerm))),                                            
                                            pnL.KillIf(getParsed.Value.ResultOfTokenizer.Succeeded, 
                                                () => pnL.p(ANC.DocuTerms.StateDescription.Why.UID,
                                                        pnL.i(ANC.TechTerms.Parser.Tokenizer.UID,                                                            
                                                            pnL.p("IxLastProcessedToken", getParsed.Value.IndexOfLastProcessedToken),
                                                            pnL.m(ANC.TechTerms.Parser.Tokenize.UID,
                                                                pnL.eFails(getParsed.Value.ResultOfTokenizer.Message)))))))))));
                }
                else
                {
                    DocuEntities.IDocuEntity val = getParsed.Value.Stack.Peek() is DocuEntities.IDocuEntity docE
                                                    ? docE
                                                    : pnL.txt(getParsed.Value.Stack.Peek().ToString());


                    rc = RCV3sV<DocuEntities.IDocuEntity>.Ok(val);
                }
            }
            catch (Exception ex)
            {
                rc = RCV3sV<DocuEntities.IDocuEntity>.Failed(NullEntity, 
                    ErrorDescription: pnL.i(ANC.TechTerms.Parser.Parser.UID, 
                                        pnL.eFails(pnL.EncapsulateAsEventParameter(TraceHlp.FlattenExceptionMessagesPN(ex)))));
            }

            return rc;
        }

    }
}
