using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NaLisp = mko.NaLisp;
using System.Text.RegularExpressions;

using BG = DFCObjects.Common.IBaugruppe;

namespace MKPRG.CSSQL.Test
{
    [TestClass]
    public class WhereExpressions
    {

        SQL<DFCObjects.Common.IBaugruppe> qBG = new ATMO.mko.QueryBuilder.SQL<DFCObjects.Common.IBaugruppe>();
        Bosch106 tab;

        [TestInitialize]
        public void Init()
        {
            tab = new Bosch106();
        }


        [TestMethod]
        public void QueryBuilder_SelectExpressions()
        {

            qBG.Map(tab.MatNr, (p, v) => p.MatNr = (string)v);

            var xpr = qBG.Select(qBG.Map(tab.MatNr, (p, v) => p.MatNr = (string)v));


        }


        [TestMethod]
        public void QueryBuilder_WhereExpressions_Eq()
        {

            var xpr = new Where(qBG.Eq(tab.MatNr, qBG.Txt("0123456789")));
            Assert.AreEqual(1, xpr.Elements.Length);

            var res = NaLisp.Core.Evaluator._.Eval(xpr);
            Assert.IsTrue(res is NaLisp.Data.ConstVal<string>);

            var resStr = (NaLisp.Data.ConstVal<string>)res;
            Assert.AreEqual("  Materialnummer = '0123456789'  ", resStr.Value);
        }

        [TestMethod]
        public void QueryBuilder_WhereExpressions_Lt()
        {
            //var xpr = Where(SQL.)
        }

        
        [TestMethod]
        //public async System.Threading.Tasks.Task QueryBuilder_WhereExpressions_And()
        public void QueryBuilder_WhereExpressions_And()
        {

            var xpr = new Where(qBG.And(qBG.Eq(tab.MatNr, qBG.Txt("0123456789")), qBG.Eq(tab.ProjectNumber, tab.MatNr), qBG.Lt(tab.ZeichnungsNr, qBG.Long(1000L))));
            Assert.AreEqual(1, xpr.Elements.Length);

            //var vld =  await NaLisp.Core.Inspector._.ValidateAsync(xpr);            
            var vld = NaLisp.Core.Inspector._.Validate(xpr);

            var res = NaLisp.Core.Evaluator._.Eval(xpr);
            Assert.IsTrue(res is NaLisp.Data.ConstVal<string>);

            var resStr = (NaLisp.Data.ConstVal<string>)res;
            var str = Regex.Replace(resStr.Value, @"\s+", " ");
            Assert.AreEqual(" ( Materialnummer = '0123456789' and PSPNr = Materialnummer and ZeichnungsNr < 1000 ) ", str);
        }
    }
}
