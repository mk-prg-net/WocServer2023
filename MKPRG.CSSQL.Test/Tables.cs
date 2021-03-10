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
            var tab = new Table("Autoren");

            Assert.IsFalse(tab.HasAlias);
            Assert.AreEqual("Autoren", tab.TableName);
            Assert.IsNull(tab.Alias);
        }

        [TestMethod]
        public void QueryBuilder_Tables_TableWithAlias()
        {
            var tab = new Table("Autoren", "T");

            Assert.IsTrue(tab.HasAlias);
            Assert.AreEqual("Autoren", tab.TableName);
            Assert.IsNotNull(tab.Alias);
            Assert.AreEqual("T", tab.Alias);

            var btab = new TabAuthors("T");

            Assert.AreEqual("T.Id", btab.AuthorId.FQN.N);
            Assert.AreEqual("Vorname as V", btab.FirstName.Alias("V").N);
            Assert.AreEqual("T.Vorname as M", btab.FirstName.FQN.Alias("M").N);
        }


        [TestMethod]
        public void QueryBuilder_Tables_StrongTypedTableWithoutAlias()
        {
            var tab = new TabAuthors();

            Assert.IsFalse(tab.HasAlias);
            Assert.AreEqual("Autoren", tab.TableName);
            Assert.IsNull(tab.Alias);

            Assert.AreEqual("Vorname", tab.FirstName.N);
            Assert.AreEqual("Vornamen as V", tab.FirstName.Alias("V").N);
        }
    }
}
