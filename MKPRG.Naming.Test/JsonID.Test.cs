using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace MKPRG.Naming.Test
{
    [TestClass]
    public class JsonIDTest
    {
#if ZOTAC
        const string outPath = @"C:\Users\Marti\source\repos\WocServer2021\MKPRG.Naming.Test\JsonIds.json";
#else 
        const string outPath = @"C:\Users\marti_000\source\repos\WocServer2023\WocServer2023\MKPRG.Naming.Test\JsonIds.json";
#endif

        [TestMethod]
        public void TestMethod1()
        {
            var ncTools = new Tools();

            var getJsonId = ncTools.GetNamingIdsAsJSON("MKPRG.Naming");

            Assert.IsTrue(getJsonId.RC.succeeded);

            if (System.IO.File.Exists(outPath))
            {
                System.IO.File.Delete(outPath);
            }

            using (System.IO.StreamWriter writer = new System.IO.StreamWriter(outPath))
            {
                writer.WriteLine(getJsonId.JsonID);
                writer.Flush();
            }
        }
    }
}
