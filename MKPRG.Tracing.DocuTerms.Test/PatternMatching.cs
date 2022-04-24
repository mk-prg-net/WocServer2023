using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using System.Linq;
using MKPRG.Tracing.DocuTerms;

using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

using MKPRG.Tracing.DocuTerms.Formatter;

using static MKPRG.Tracing.DocuTerms.DocuEntityHlp;


namespace MKPRG.Tracing.DocuTerms.Test
{
    [TestClass]
    public class PatternMatching
    {
        IComposer pnL = new Composer();

        IFormater fmt; // = new Formatter.IndentedTextFormatter(Parser.Fn._, RC.NC);

        /// <summary>
        /// mko, 2.3.2020
        /// Ordnet einer long UID einen EventName- Naming Objekt zu.
        /// </summary>
        IReadOnlyDictionary<long, ANC.INaming> MapUIDToName;


        /// <summary>
        /// mko, 2.3.2020
        /// Sprachtabellen erzeugt
        /// </summary>
        [TestInitialize]
        public void Init()
        {
            var ntools = new ANC.Tools();
            var getEventNames = ntools.GetNamingDictOf("MKPRG.Naming", new Composer());

            TraceHlp.ThrowArgExIfNot(getEventNames.Succeeded, getEventNames.ToPlx());

            MapUIDToName = getEventNames.Value;


            fmt = new IndentedTextFormatter(Parser.Fn._, RC.NC, ANC.Language.CNT);
            
        }

        [TestMethod]
        public void SubTreeOf()
        {
            // mko, 2.3.2020: Idee: wenn ein UID übergeben wird als long, dann wird dieser vom 
            // Composer automatisch in einen DocuTermId gewandelt.
            // SubTree vergleicht stets auf Basis der CNT, falls der Namen aus einem DocuTerm besteht
            // -> Kompatibilität zu altem Code
            // mko, 3.3.2020
            // SubTree vergleicht stets dynamisch erstellte Bäume.
            // Wird ein RPN- Term aus einem String geparst, dann muss dieser als Namen NID's oder CNT benutzen.
            var tree = pnL.i(TT.Sequences.Command.UID,
                            pnL.p_NID(TTD.MetaData.Name.UID, TT.Access.Copy.UID),
                            pnL.p(TT.SendReceive.From.UID, "0x10"),
                            pnL.p(TT.SendReceive.To.UID, "0xFF"),
                            pnL.p(TT.Authentication.Authenticate.UID, pnL.boolean(true)),
                            pnL.p(TT.Authorization.Granted.UID, pnL.boolean(false)),
                            pnL.p(TT.Metrology.DimensionsAnWeights.IT.FileSize.UID, pnL.List(
                                    pnL.p(TTD.MetaData.Val.UID, 100),
                                    pnL.p_NID(TT.Metrology.Unit.UID, TT.Metrology.DimensionsAnWeights.IT.Byte.UID))),
                            pnL.m(TT.Runtime.Execute.UID,
                                    pnL.ReturnAfterSuccess(TT.Access.Copy.UID)));

            // {-000001181 MKPRG.Tracing.Parser.Parse20_06 -> #i cmd #_ #p Name #m IndentedTextFormatter_Print #_ #r #e fails System.InvalidCastException: Das Objekt des Typs "MKPRG.Tracing.DocuTerms.Parser.NIDToken" kann nicht in Typ "MKPRG.Tracing.DocuTerms.NID" umgewandelt werden.
            // bei MKPRG.Tracing.DocuTerms.IndentedTextFormatter.Print(IDocuEntity entity, Int32 Indentation, StringBuilder bld) in C: \Users\marti_000\source\repos\WocServer2021\MKPRG.Tracing\DocuTerms\Formater\IndentedTextFormatter.cs:Zeile 611.
            // #.#p from #$ 0x10 #. #p to #$ 0xFF #. #p fetch #_ #p val #m IndentedTextFormatter_Print #_ #r #e fails System.InvalidCastException: Das Objekt des Typs "MKPRG.Tracing.DocuTerms.Parser.IntegerToken" kann nicht in Typ "MKPRG.Tracing.DocuTerms.Integer" umgewandelt werden.
            // bei MKPRG.Tracing.DocuTerms.IndentedTextFormatter.Print(IDocuEntity entity, Int32 Indentation, StringBuilder bld) in C: \Users\marti_000\source\repos\WocServer2021\MKPRG.Tracing\DocuTerms\Formater\IndentedTextFormatter.cs:Zeile 623. 
            // #.#p unit #m IndentedTextFormatter_Print #_ #r #e fails System.InvalidCastException: Das Objekt des Typs "MKPRG.Tracing.DocuTerms.Parser.NIDToken" kann nicht in Typ "MKPRG.Tracing.DocuTerms.NID" umgewandelt werden.
            // bei MKPRG.Tracing.DocuTerms.IndentedTextFormatter.Print(IDocuEntity entity, Int32 Indentation, StringBuilder bld) in C: \Users\marti_000\source\repos\WocServer2021\MKPRG.Tracing\DocuTerms\Formater\IndentedTextFormatter.cs:Zeile 611. 
            // #.#. #m exec #_ #i finStateDescr #_ #m copy #_ #r #e succeeded #. #. #. #. }

            //var fmt = new PNFormater(fn: , NC: RCV3.NC, RPNUrlSaveEncode: true);
            var treeStr = fmt.Print(tree);

            var getParsed = Parser.Parser.Parse20_06(treeStr, Parser.Fn._, pnL);
            Assert.IsTrue(getParsed.Succeeded);

            var parsedTree = getParsed.Value;

            // Methode als SubTree bestimmen
            var subTreeM = pnL.m(TT.Runtime.Execute.UID,
                                    pnL.ReturnAfterSuccess(TT.Access.Copy.UID));


            bool res = subTreeM.IsSubTreeOf(tree, true);
            Assert.IsTrue(res);

            res = subTreeM.IsSubTreeOf(parsedTree, true);
            Assert.IsTrue(res);


            // Gesamte instanz als Subtree bestimmen
            var subTreeI = pnL.i(TT.Sequences.Command.UID);

            res = subTreeI.IsSubTreeOf(tree, true);
            Assert.IsTrue(res);

            res = subTreeI.IsSubTreeOf(parsedTree, true);
            Assert.IsTrue(res);


            // Eigenschaft mit einem Namen als Subtree bestimmen. Wert ist Wildcard

            var subTreeP = pnL.p(TT.SendReceive.From.UID, pnL._v());

            res = subTreeP.IsSubTreeOf(tree, true);
            Assert.IsTrue(res);

            res = subTreeP.IsSubTreeOf(parsedTree, true);
            Assert.IsTrue(res);

            // Eigenschaft, die kein Subtree ist

            subTreeP = pnL.p(TT.SendReceive.From.UID, "0x11");

            res = subTreeP.IsSubTreeOf(tree, true);
            Assert.IsFalse(res);

            res = subTreeP.IsSubTreeOf(parsedTree, true);
            Assert.IsFalse(res);

            // Eigenschaft mit einem Wert bestimmen. Name ist Wildcard

            subTreeP = pnL.p(pnL._n, "0x10");

            res = subTreeP.IsSubTreeOf(tree, true);
            Assert.IsTrue(res);

            res = subTreeP.IsSubTreeOf(parsedTree, true);
            Assert.IsTrue(res);

            var catches = subTreeP.AsSubTreeOf(tree, pnL);
            Assert.IsTrue(catches.Succeeded);

            var (subTreeFound, subTreeParent, deepth) = catches.Value;

            Assert.IsTrue(subTreeFound is IPropertyWithNameAsNID);

            var pFrom = subTreeFound as IPropertyWithNameAsNID;

            Assert.AreEqual(1, deepth);

            Assert.AreEqual(TT.SendReceive.From.UID, pFrom.DocuTermNid.NamingId);


            res = subTreeP.IsSubTreeOf(parsedTree, true);
            Assert.IsTrue(res);



        }



        [TestMethod]
        public void FindAll()
        {
            //var _ = pnL.txt(TechTerms.Dfc.TTL.PropValWildCard);

            //var tree = pnL.i("Compound",
            //                        pnL.p("Err1", pnL.ReturnFetch(false, pnL.txt("AD"),
            //                                            pnL.m(TT.Operators.Relations.Eq.UID, pnL.p("Name", "SantaC")))),
            //                        pnL.p("Err2", pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(pnL.txt("SantaC"))),
            //                        pnL.p("Err1", pnL.ReturnFetch(false, pnL.txt("AD"),
            //                                            pnL.m(TT.Operators.Relations.Eq.UID, pnL.p("Name", "SantaF")))),
            //                        pnL.p("Err3", pnL.ReturnFetchWithDetails(false, pnL.txt("AD"),
            //                                            pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(pnL.txt("SantaC")),
            //                                            pnL.m(TT.Operators.Relations.Eq.UID, pnL.p("Name", "SantaC")))));

            //var str = fmt.Print(tree);

            //var Err1 = pnL.p("Err1", pnL._v()).AsSubTreeOf(treeRoot: tree, pnL: pnL);
            //Assert.IsTrue(Err1.Succeeded);
            //Assert.IsTrue(pnL.p("Err1", pnL.ReturnFetch(
            //                                    false,
            //                                    pnL.txt("AD"),
            //                                    pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p("Name", "SantaC"))))
            //                                .IsSubTreeOf(Err1.Value.subTree, true));


            //var Err1_1 = pnL.p("Err1", pnL._v()).AsSubTreeOf_AllOccurrences(treeRoot: tree, pnL: pnL);
            //Assert.IsTrue(Err1_1.Succeeded);
            ////Assert.IsTrue(dct.p("Err1", dct.ReturnFetch(false, dct.p("ConnectedTo", "AD"), dct.p("Name", "SantaC"))).IsSubTreeOf(Err1_1.Value..subTree));




            //var res = pnL.eFails().FindAllIn(tree);

            //var tree2 = pnL.p("Err3", pnL.ReturnFetchWithDetails(
            //                            false,
            //                            pnL.txt("AD"),
            //                            pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(pnL.txt("SantaC")),
            //                            pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p("Name", "SantaC"))));
            //var res2 = pnL.eFails().FindAllIn(tree2);

            //var getErr = pnL.eFails().AsSubTreeOf(tree, pnL);

            //var getinfo = pnL.m("login").AsSubTreeOf(tree, pnL);

            //var getAllErrors = pnL.eFails().AsSubTreeOf_AllOccurrences(tree, pnL);
            //Assert.AreEqual(5, getAllErrors.Value.Count());

            ////var getAllLogins = pnL.m("login").AsSubTreeOf_AllOccurrences(tree, pnL);
            ////Assert.AreEqual(2, getAllLogins.Value.Count());


            //var txt = $" #i DFCSecurity.V1.<Create>d__4.Create  #_  #e fails  #p msg  #i FinStateDescr  #_  #p semCtx  #$ Authentication #.  #m login  #_  #p UserId  #$ MNK3FE #.  #r  #e  #NID {new ATMO.DFC.Naming.DocuTerms.Event.Fails().ID}  #.  #.  #.";
            //var parseDocuterm = Parser.Parser.Parse20_06(txt, Parser.Fn._, pnL);

            //Assert.IsTrue(parseDocuterm.Succeeded);



        }
    }
}
