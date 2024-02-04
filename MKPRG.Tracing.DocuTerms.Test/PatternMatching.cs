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
        ANC.INamingHelper NH;

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
            var getNamingContainers = ntools.GetNamingContainers("MKPRG.Naming", true);

            Assert.IsTrue(getNamingContainers.succeded);

            MapUIDToName = getNamingContainers.ncDict;

            NH = new ANC.NamingHelper(MapUIDToName, ANC.Language.CNT);
            fmt = new IndentedTextFormatter(Parser.FnRunen._, RC.NC, ANC.Language.CNT);
            
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

            //var fmt = new PNFormater(fn: , NC: RCV3.NC, RPNUrlSaveEncode: true);
            var treeStr = fmt.Print(tree);

            var getParsed = Parser.Parser.Parse20_06(treeStr, Parser.FnRunen._, pnL, NH);
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



    }
}
