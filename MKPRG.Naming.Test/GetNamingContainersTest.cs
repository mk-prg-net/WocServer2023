using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Globalization;
using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

using DTF =  MKPRG.Tracing.DocuTerms.Formatter;
using DT = MKPRG.Tracing.DocuTerms;

using DTP = MKPRG.Tracing.DocuTerms.Parser;


namespace MKPRG.Naming.Test
{
    [TestClass]
    public class GetNamingContainersTest
    {
        DT.IComposer pnL;
        DT.IFormater fmt; // = new Formatter.IndentedTextFormatter(Parser.Fn._, RC.NC);

        [TestInitialize]
        public void Init()
        {
            pnL = new DT.Composer();            
        }


        [TestMethod]
        public void GetMKPRGNamingContainers()
        {
            var ntools = new ANC.Tools();
            var getNamingContainers = ntools.GetNamingContainers("MKPRG.Naming", true);

            Assert.IsTrue(getNamingContainers.succeded);

        }

        [TestMethod]
        public void GetNYTNamingContainers()
        {
            var ntools = new ANC.Tools();
            var getNamingContainers = ntools.GetNamingContainers("MKPRG.Naming.NYT.Keywords", true);

            Assert.IsTrue(getNamingContainers.succeded);


        }

    }
}
