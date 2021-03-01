using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MKPRG.CSSQL.Test
{
    [TestClass]
    public class Tables
    {
        [TestMethod]
        public void QueryBuilder_Tables_TableWithoutAlias()
        {
            var tab = new Table("Bosch106");

            Assert.IsFalse(tab.HasAlias);
            Assert.AreEqual("Bosch106", tab.TableName);
            Assert.IsNull(tab.Alias);
        }

        [TestMethod]
        public void QueryBuilder_Tables_TableWithAlias()
        {
            var tab = new Table("Bosch106", "T");

            Assert.IsTrue(tab.HasAlias);
            Assert.AreEqual("Bosch106", tab.TableName);
            Assert.IsNotNull(tab.Alias);
            Assert.AreEqual("T", tab.Alias);

            var btab = new Bosch106("T");

            Assert.AreEqual("T.Materialnummer", btab.MatNr.FQN.N);
            Assert.AreEqual("Materialnummer as M", btab.MatNr.Alias("M").N);
            Assert.AreEqual("T.Materialnummer as M", btab.MatNr.FQN.Alias("M").N);
        }


        [TestMethod]
        public void QueryBuilder_Tables_StrongTypedTableWithoutAlias()
        {
            var b106 = new Bosch106();

            Assert.IsFalse(b106.HasAlias);
            Assert.AreEqual("Bosch106", b106.TableName);
            Assert.IsNull(b106.Alias);

            Assert.AreEqual("Materialnummer", b106.MatNr.N);
            Assert.AreEqual("Materialnummer as MatNr", b106.MatNr.Alias("MatNr").N);
            

        }

    }
}
