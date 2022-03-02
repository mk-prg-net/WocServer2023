using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

using System.Linq;
using MKPRG.Tracing.DocuTerms;

using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

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


            fmt = new IndentedTextFormatter(Parser.Fn._, RC.NC);
        }

        [TestMethod]
        public void SubTreeOf()
        {

            //var _ = pnL.txt(TechTerms.Dfc.TTL.PropValWildCard);

            // mko, 2.3.2020: Idee: wenn ein UID übergeben wird als long, dann wird dieser vom 
            // Composer automatisch in einen DocuTermId gewandelt.
            // SubTree vergleicht stets auf Basis der CNT, falls der Namen aus einem DocuTerm besteht
            // -> Kompatibilität zu altem Code
            // mko, 3.3.2020
            // SubTree vergleicht stets dynamisch erstellte Bäume.
            // Wird ein RPN- Term aus einem String geparst, dann muss dieser als Namen NID's oder CNT benutzen.
            var tree = pnL.i(TT.ATMO.DFC.Project.UID,
                            pnL.p(TT.ATMO.DFC.Project.UID, "2998"),
                            pnL.p(TT.ATMO.DFC.Station.UID, pnL.ReturnSearchWarnEmptyResult(
                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                        pnL.p("col", "Project"),
                                                        pnL.p("val", "2998")))));

            //var fmt = new PNFormater(fn: , NC: RCV3.NC, RPNUrlSaveEncode: true);
            var treeStr = fmt.Print(tree);

            var getParsed = PNDocuTerms.Parser.Parser.Parse20_06(treeStr, PNDocuTerms.Fn._, pnL);
            Assert.IsTrue(getParsed.Succeeded);

            var parsedTree = getParsed.Value;

            var subTree = pnL.ReturnSearchWarnEmptyResult(
                                                pnL.m(TT.Operators.Relations.Eq.UID,
                                                        pnL.p("col", "Project"),
                                                        pnL.p("val", "2998")));


            bool res = subTree.IsSubTreeOf(tree, true);
            Assert.IsTrue(res);

            res = subTree.IsSubTreeOf(parsedTree, true);
            Assert.IsTrue(res);


            var subTree2 = pnL.ReturnSearchWarnEmptyResult(
                                               pnL.m(TT.Operators.Relations.Eq.UID,
                                                       pnL.p("col", "Project"),
                                                       pnL.p("val", "2999")));


            res = subTree2.IsSubTreeOf(tree, true);
            Assert.IsFalse(res);

            var subTree2_1 = pnL.ReturnSearchWarnEmptyResult(
                                               pnL.m(TT.Operators.Relations.Eq.UID,
                                                       pnL.p("col", "Project"),
                                                       pnL.p("val", pnL._v())));


            res = subTree2_1.IsSubTreeOf(tree: tree);
            Assert.IsTrue(res);


            var subTree3 = pnL.ReturnSearchWarnEmptyResult();


            res = subTree3.IsSubTreeOf(tree, true);
            Assert.IsTrue(res);


            var tree2 = pnL.i("User",
                               pnL.p("NTUser", "MNK3FE"),
                               pnL.ReturnFetch(
                                   false,
                                   TT.Access.Datasources.WellKnown.ActiveDirectory.UID,
                                   pnL.m(TT.Operators.Relations.Eq.UID, pnL.p("NTUser", "MNK3FE"))));

            // mko, 6.9.2021
            // Methode mit einem StringName "fetch" und Methode mit einem NidName "fetch" sind nun explizit nicht mehr gleich!
            var subtree4 = pnL.m(TT.Access.Fetch.UID);
            Assert.IsTrue(subtree4.IsSubTreeOf(tree2, true));

            var subtree5 = pnL.m(TT.Access.Fetch.UID, pnL.ret(pnL._v()));
            Assert.IsTrue(subtree5.IsSubTreeOf(tree2, true));


            Assert.IsTrue(pnL.m(TT.Access.Fetch.UID, pnL.ret(pnL.eFails())).IsSubTreeOf(tree2, true));
            Assert.IsTrue(pnL.m(TT.Access.Fetch.UID,
                                pnL.p(TT.Search.Key.UID,
                                    pnL.m(TT.Operators.Relations.Eq.UID,
                                        pnL.p("NTUser", "ME"))),
                                pnL.ret(pnL.eFails()))
                        .IsSubTreeOf(tree2, true));

            // Ungleich, weil ungleiche Werte
            Assert.IsFalse(pnL.m(TT.Access.Fetch.UID,
                            pnL.p(TT.Search.Key.UID,
                                pnL.m(TT.Operators.Relations.Eq.UID,
                                    pnL.p("NTUser", "YOU"))),
                            pnL.ret(pnL.eFails()))
                        .IsSubTreeOf(tree2, true));

            // Gleich, weil Wert von NTUser Property ist jetzt ein WildCard
            //Assert.IsTrue(pnL.m(TechTerms.Access.fetch, pnL.p("NTUser", _), pnL.ret(pnL.eFails())).IsSubTreeOf(tree: tree2, PropertyValueWildCard: _));
            Assert.IsTrue(pnL.m(TT.Access.Fetch.UID,
                            pnL.p(TT.Search.Key.UID,
                                pnL.m(TT.Operators.Relations.Eq.UID,
                                    pnL.p("NTUser", pnL._v()))),
                            pnL.ret(pnL.eFails()))
                        .IsSubTreeOf(tree2, true));

            // Ungleich, weil ungleiche Struktur
            Assert.IsFalse(pnL.m(TT.Access.Fetch.UID,
                            pnL.p(TT.Search.Key.UID,
                                pnL.m(TT.Operators.Relations.Eq.UID,
                                    pnL.p("NTUser", "ME"))),
                            pnL.eFails())
                        .IsSubTreeOf(tree2, true));

            // mko, 22.5.2019
            // gesuchter Subtree eFails MissingDrawing in einem Subtree eFails .... Vorher wurde bereits beim 
            // übergeordneten eFails die Suche gestoppt. Jetzt werden auch die Kindknoten weiter untersucht.
            string MatNo = "0804DS9536";

            var testRet = pnL.ReturnFetchWithDetails(false,
                                pnL.i("Table", pnL.p("Name", "Path")),
                                pnL.List(pnL.eFails(TT.ATMO.DocuCheck.MissingDrawing.UID), pnL.eWarn(TT.ATMO.DocuCheck.ATDExistsButNoDrawingNo.UID)),
                                pnL.m(TT.Operators.Relations.Eq.UID, pnL.p(TT.ATMO.DFC.MatNo.UID, MatNo)),
                                pnL.m(TT.Operators.Relations.Eq.UID, pnL.p_NID(TT.ATMO.DFC.DrawingNo.UID, TT.Sets.EmptySet.UID)),
                                pnL.m(TT.Operators.Relations.Eq.UID, pnL.p_NID(TT.ATMO.DFC.SAPDocType.UID, TT.ATMO.DFC.ATD.UID)));

            Assert.IsTrue(pnL.eWarn(TT.ATMO.DocuCheck.ATDExistsButNoDrawingNo.UID).IsSubTreeOf(testRet, true));
            Assert.IsTrue(pnL.eFails(TT.ATMO.DocuCheck.MissingDrawing.UID).IsSubTreeOf(testRet, true));
            Assert.IsTrue(pnL.eFails().IsSubTreeOf(testRet, true));
            Assert.IsTrue(pnL.eWarn().IsSubTreeOf(testRet, true));


            var iMsg = pnL.i("Install",
                                        pnL.m("setup",
                                            pnL.i("DFC2",
                                                pnL.ver("20.1.14"),
                                                pnL.p("newVer", pnL.ver("20.1.14"))),
                                            pnL.eSucceeded()));
            // Versionsnummer extrahieren

            var getVer = pnL.p("newVer", pnL._v()).AsSubTreeOf(iMsg, pnL);
            Assert.IsTrue(getVer.Succeeded);
            Assert.IsTrue(getVer.Value.subTree is IProperty p && p.PropertyValue is IVer v && v.VersionString == "20.1.14");

        }


        [TestMethod]
        public void WildCards()
        {

            var tree = pnL.i(TT.ATMO.DFC.ApplicationNameDFCClient.UID,
                    pnL.p(TT.ATMO.DFC.ProjectList.UID,
                        pnL.List(
                            pnL.i(TT.ATMO.DFC.Project.UID,
                                pnL.p(TT.ATMO.DFC.ProjectNo.UID, "P.2998"),
                                pnL.p(TT.ATMO.DFC.StationList.UID,
                                    pnL.List(
                                        pnL.i(TT.ATMO.DFC.Station.UID,
                                            pnL.p(TT.ATMO.DFC.StationNo.UID, 10),
                                            pnL.p(TT.ATMO.DFC.ProcessModulList.UID,
                                                pnL.List(
                                                        pnL.i(TT.ATMO.DFC.ProcessModul.UID,
                                                            pnL.p(TT.ATMO.DFC.ProcessmoduleNo.UID, 1),
                                                            pnL.i(TT.ATMO.DFC.BOM.UID,
                                                                pnL.p_NID(TT.ATMO.DFC.BOMType.UID, ANC.TechTerms.ATMO.DFC.BOMTypeME.UID),
                                                                pnL.p(TT.ATMO.DFC.BOMPosList.UID,
                                                                    pnL.List(
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 10)
                                                                        ),
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 20)
                                                                        ),
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 30)
                                                                        ))))),
                                                        pnL.i(TT.ATMO.DFC.ProcessModul.UID,
                                                            pnL.p(TT.ATMO.DFC.ProcessmoduleNo.UID, 2),
                                                            pnL.i(TT.ATMO.DFC.BOM.UID,
                                                                pnL.p_NID(TT.ATMO.DFC.BOMType.UID, ANC.TechTerms.ATMO.DFC.BOMTypeME.UID),
                                                                pnL.p(TT.ATMO.DFC.BOMPosList.UID,
                                                                    pnL.List(
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 100)
                                                                        ),
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 110)
                                                                        )))))))),
                                        pnL.i(TT.ATMO.DFC.Station.UID,
                                            pnL.p(TT.ATMO.DFC.StationNo.UID, 20),
                                            pnL.p(TT.ATMO.DFC.ProcessModulList.UID,
                                                pnL.List(
                                                        pnL.i(TT.ATMO.DFC.ProcessModul.UID,
                                                            pnL.p(TT.ATMO.DFC.ProcessmoduleNo.UID, 1),
                                                            pnL.i(TT.ATMO.DFC.BOM.UID,
                                                                pnL.p_NID(TT.ATMO.DFC.BOMType.UID, ANC.TechTerms.ATMO.DFC.BOMTypeME.UID),
                                                                pnL.p(TT.ATMO.DFC.BOMPosList.UID,
                                                                    pnL.List(
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 5)
                                                                        ),
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 10)
                                                                        ),
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 15)
                                                                        ))))))))))),
                            pnL.i(TT.ATMO.DFC.Project.UID,
                                pnL.p(TT.ATMO.DFC.ProjectNo.UID, "M.1234567"),
                                pnL.p(TT.ATMO.DFC.StationList.UID,
                                    pnL.List(
                                        pnL.i(TT.ATMO.DFC.Station.UID,
                                            pnL.p(TT.ATMO.DFC.StationNo.UID, 10),
                                            pnL.p(TT.ATMO.DFC.ProcessModulList.UID,
                                                pnL.List(
                                                        pnL.i(TT.ATMO.DFC.ProcessModul.UID,
                                                            pnL.p(TT.ATMO.DFC.ProcessmoduleNo.UID, 1),
                                                            pnL.i(TT.ATMO.DFC.BOM.UID,
                                                                pnL.p_NID(TT.ATMO.DFC.BOMType.UID, ANC.TechTerms.ATMO.DFC.BOMTypeME.UID),
                                                                pnL.p(TT.ATMO.DFC.BOMPosList.UID,
                                                                    pnL.List(
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 10)
                                                                        ),
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 20)
                                                                        ),
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 30)
                                                                        )))))))),
                                        pnL.i(TT.ATMO.DFC.Station.UID,
                                            pnL.p(TT.ATMO.DFC.StationNo.UID, 20),
                                            pnL.p(TT.ATMO.DFC.ProcessModulList.UID,
                                                pnL.List(
                                                        pnL.i(TT.ATMO.DFC.ProcessModul.UID,
                                                            pnL.p(TT.ATMO.DFC.ProcessmoduleNo.UID, 1),
                                                            pnL.i(TT.ATMO.DFC.BOM.UID,
                                                                pnL.p_NID(TT.ATMO.DFC.BOMType.UID, ANC.TechTerms.ATMO.DFC.BOMTypeME.UID),
                                                                pnL.p(TT.ATMO.DFC.BOMPosList.UID,
                                                                    pnL.List(
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 5)
                                                                        ),
                                                                        pnL.i(TT.ATMO.DFC.BOMPos.UID,
                                                                            pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 15)
                                                                        ))))))))))))));


            var res = pnL.p(TT.ATMO.DFC.ProcessmoduleNo.UID, pnL._v()).IsSubTreeOf(tree);
            Assert.IsTrue(res);


            // Prüfen auf Subtree, der eine Bestimmt eigenschaft hat, deren Wert einer Einschränkungen genügen muss
            // p\*\p\NID\TT.ATMO.DFC.BOMPosNo.UID
            // \\NID\TT.ATMO.DFC.BOMPosList.UID
            res = pnL.p(TT.ATMO.DFC.BOMPosList.UID, pnL._v(pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 5))).IsSubTreeOf(tree);
            Assert.IsTrue(res);

            res = pnL.p(TT.ATMO.DFC.BOMPosList.UID, pnL._v(pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 6))).IsSubTreeOf(tree);
            Assert.IsFalse(res);


            // Isolieren eines Teilbaumes
            var sub = pnL.p(TT.ATMO.DFC.ProcessmoduleNo.UID, pnL._v()).AsSubTreeOf(tree, pnL);

            var allSubs = pnL.p(TT.ATMO.DFC.ProcessmoduleNo.UID, pnL._v()).AsSubTreeOf_AllOccurrences(tree, pnL);
            Assert.AreEqual(5, allSubs.Value.Count());

            sub = pnL.i(TT.ATMO.DFC.BOM.UID,
                      pnL.p(TT.ATMO.DFC.BOMPosList.UID,
                        pnL._v(pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 5))))
                    .AsSubTreeOf(tree, pnL);

            allSubs = pnL.i(TT.ATMO.DFC.BOM.UID,
                        pnL.p(TT.ATMO.DFC.BOMPosList.UID,
                            pnL._v(pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 5))))
                    .AsSubTreeOf_AllOccurrences(tree, pnL);

            Assert.AreEqual(2, allSubs.Value.Count());

            allSubs = pnL.i(TT.ATMO.DFC.BOM.UID,
                        pnL.p(TT.ATMO.DFC.BOMPosList.UID,
                            pnL._v(pnL.p(TT.ATMO.DFC.BOMPosNo.UID, 10))))
                     .AsSubTreeOf_AllOccurrences(tree, pnL);

            Assert.AreEqual(3, allSubs.Value.Count());

        }

        [TestMethod]
        public void FindAll()
        {
            //var _ = pnL.txt(TechTerms.Dfc.TTL.PropValWildCard);

            var tree = pnL.i("Compound",
                                    pnL.p("Err1", pnL.ReturnFetch(false, pnL.txt("AD"),
                                                        pnL.m(TT.Operators.Relations.Eq.UID, pnL.p("Name", "SantaC")))),
                                    pnL.p("Err2", pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(pnL.txt("SantaC"))),
                                    pnL.p("Err1", pnL.ReturnFetch(false, pnL.txt("AD"),
                                                        pnL.m(TT.Operators.Relations.Eq.UID, pnL.p("Name", "SantaF")))),
                                    pnL.p("Err3", pnL.ReturnFetchWithDetails(false, pnL.txt("AD"),
                                                        pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(pnL.txt("SantaC")),
                                                        pnL.m(TT.Operators.Relations.Eq.UID, pnL.p("Name", "SantaC")))));

            var str = fmt.Print(tree);

            var Err1 = pnL.p("Err1", pnL._v()).AsSubTreeOf(treeRoot: tree, pnL: pnL);
            Assert.IsTrue(Err1.Succeeded);
            Assert.IsTrue(pnL.p("Err1", pnL.ReturnFetch(
                                                false,
                                                pnL.txt("AD"),
                                                pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p("Name", "SantaC"))))
                                            .IsSubTreeOf(Err1.Value.subTree, true));


            var Err1_1 = pnL.p("Err1", pnL._v()).AsSubTreeOf_AllOccurrences(treeRoot: tree, pnL: pnL);
            Assert.IsTrue(Err1_1.Succeeded);
            //Assert.IsTrue(dct.p("Err1", dct.ReturnFetch(false, dct.p("ConnectedTo", "AD"), dct.p("Name", "SantaC"))).IsSubTreeOf(Err1_1.Value..subTree));




            var res = pnL.eFails().FindAllIn(tree);

            var tree2 = pnL.p("Err3", pnL.ReturnFetchWithDetails(
                                        false,
                                        pnL.txt("AD"),
                                        pnL.ReturnAuthenticationUserIsNoCustomerNorAtmoEmployee(pnL.txt("SantaC")),
                                        pnL.m(ANC.TechTerms.Operators.Relations.Eq.UID, pnL.p("Name", "SantaC"))));
            var res2 = pnL.eFails().FindAllIn(tree2);

            var getErr = pnL.eFails().AsSubTreeOf(tree, pnL);

            var getinfo = pnL.m("login").AsSubTreeOf(tree, pnL);

            var getAllErrors = pnL.eFails().AsSubTreeOf_AllOccurrences(tree, pnL);
            Assert.AreEqual(5, getAllErrors.Value.Count());

            //var getAllLogins = pnL.m("login").AsSubTreeOf_AllOccurrences(tree, pnL);
            //Assert.AreEqual(2, getAllLogins.Value.Count());


            var txt = $" #i DFCSecurity.V1.<Create>d__4.Create  #_  #e fails  #p msg  #i FinStateDescr  #_  #p semCtx  #$ Authentication #.  #m login  #_  #p UserId  #$ MNK3FE #.  #r  #e  #NID {new ATMO.DFC.Naming.DocuTerms.Event.Fails().ID}  #.  #.  #.";
            var parseDocuterm = Parser.Parser.Parse20_06(txt, Parser.Fn._, pnL);

            Assert.IsTrue(parseDocuterm.Succeeded);



        }
    }
}
