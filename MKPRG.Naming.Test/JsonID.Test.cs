using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MKPRG.Naming.Test
{
    [TestClass]
    public class JsonIDTest
    {
        [TestMethod]
        public void TestMethod1()
        {
            var ncTools = new Tools();

            var getJsonId = ncTools.GetNamingIdsAsJSON("MKPRG.Naming");

            Assert.IsTrue(getJsonId.RC.succeeded);

        }
    }
}
