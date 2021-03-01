using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using ATMO.mko.Logging;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;
using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.DocuEntityHlp;

namespace ATMO.mko.QueryBuilder.Test
{
    [TestClass]
    public class QueryResults
    {

        Composer pnL;
        Results.PlxQueryResultAnalyzer analyzer;
        Results.PlxQueryResultDescriptionFactory factory;


        public QueryResults()
        {
            pnL = new Composer();
            factory = new Results.PlxQueryResultDescriptionFactory(pnL);            
        }

        /// <summary>
        /// mko, 26.10.2018
        /// </summary>
        [TestMethod]
        public void PlxResultCreate()
        {
            {
                var plx = factory.CreateQueryExecutionFailed(pnL.txt("Mist"));

                analyzer = new Results.PlxQueryResultAnalyzer(pnL, plx);

                Assert.IsTrue(analyzer.ExecFails);
                Assert.AreEqual("Mist", analyzer.ExecMsg.GetText());
            }

            {
                var plx = factory.CreateQueryResultEmpty();

                analyzer = new Results.PlxQueryResultAnalyzer(pnL, plx);

                Assert.IsFalse(analyzer.ExecFails);
                Assert.IsTrue(analyzer.EmptyResultset);                
                
            }

            {
                var plx = factory.CreateQueryResultEmpty();

                analyzer = new Results.PlxQueryResultAnalyzer(pnL, plx);

                Assert.IsFalse(analyzer.ExecFails);
                Assert.IsTrue(analyzer.EmptyResultset);

            }

            {
                var plx = factory.CreateQueryResultEmpty(pnL.List(pnL.p("Zusatzeigenschaft", pnL.txt("Hallo Welt"))));

                analyzer = new Results.PlxQueryResultAnalyzer(pnL, plx);

                Assert.IsFalse(analyzer.ExecFails);
                Assert.IsTrue(analyzer.EmptyResultset);
            }

            // mko, 2.7.2020
            // Folgende Konstruktionen sind unzulässig
            //{
            //    var plx = factory.CreateQueryResultEmpty(pnL.KillIf(true, () => pnL.List(pnL.p("Zusatzeigenschaft", pnL.txt("Hallo Welt")))));

            //    analyzer = new Results.PlxQueryResultAnalyzer(pnL, plx);

            //    Assert.IsFalse(analyzer.ExecFails);
            //    Assert.IsTrue(analyzer.EmptyResultset);

            //}

            //{
            //    var plx = factory.CreateQueryResultEmpty(pnL.KillIf(false, () => pnL.p("Zusatzeigenschaft", pnL.txt("Hallo Welt"))));

            //    analyzer = new Results.PlxQueryResultAnalyzer(pnL, plx);

            //    Assert.IsFalse(analyzer.ExecFails);
            //    Assert.IsTrue(analyzer.EmptyResultset);

            //}





            {
                var plx = factory.CreateQueryResultOk(222, pnL.i("Details", pnL.eInfo("Alles super")));

                analyzer = new Results.PlxQueryResultAnalyzer(pnL, plx);

                Assert.IsFalse(analyzer.ExecFails);
                Assert.IsFalse(analyzer.EmptyResultset);

                Assert.AreEqual(222L, analyzer.Count);
            }

            //{
            //    var plx = pnL.i("1ste", pnL.m("f1", pnL.ret(factory.CreateQueryResultOk(222, pnL.i("Details", pnL.eInfo("Alles super"))))));

            //    analyzer = new Results.PlxQueryResultAnalyzer(pnL, plx);

            //    Assert.IsFalse(analyzer.ExecFails);
            //    Assert.IsFalse(analyzer.EmptyResultset);

            //    Assert.AreEqual(222L, analyzer.Count);
            //}

        }
    }
}
