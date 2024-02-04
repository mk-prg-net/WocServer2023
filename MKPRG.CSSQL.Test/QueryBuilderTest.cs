using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

using static MKPRG.CSSQL.TabColAccess;

namespace MKPRG.CSSQL.Test
{
    /// <summary>
    /// Summary description for QueryBuilderTest
    /// </summary>
    [TestClass]
    public class QueryBuilderTest
    {
        public QueryBuilderTest()
        {
            //
            // TODO: Add constructor logic here
            //
        }

        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test
        [TestInitialize()]
        public void MyTestInitialize()
        {

        }        

        // Use TestCleanup to run code after each test has run
        [TestCleanup()]
        public void MyTestCleanup() {
        }

        #endregion

        //[TestMethod]
        //public void QueryBuilder_SQL()
        //{
        //    var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

        //    //dataReaderMoq.Setup(r => r.Read()).Returns();

        //    var sql = new SQL<Author>();
        //    var tab = new TabAuthors();

        //    var res = sql.Select(
        //                    sql.Map(tab.FirstName, (p, v) => p.AuthorFirstName = GetSave(v, "")),
        //                    sql.Map(tab.LastName, (p, v) => p.AuthorLastName = GetSave(v, "")),
        //                    sql.Map(tab.Birthday, (p, v) => p.AuthorBrithday = GetSave(v, DateTime.MaxValue)))                           
        //                .From(tab)
        //                .Where(sql.Eq(tab.FirstName, "Martin"))
        //                .By(tab.City)
        //                .By(tab.Birthday)
        //                .done();


        //    var a = new Woc.Author();
        //    var readerMockUp = new ReaderMockUp(
        //        (tab.FirstName.N, "Martin"),
        //        (tab.LastName.N, "Korneffel"),
        //        (tab.Birthday.N, new DateTime(1968, 6, 7))                
        //    );

        //    res.RecordToBoMapper.SetPropertiesOf(a, readerMockUp);

        //    Assert.AreEqual("Martin", a.AuthorFirstName);
        //    Assert.AreEqual("Korneffel", a.AuthorLastName);
        //    Assert.AreEqual(new DateTime(1968, 6, 7), a.AuthorBrithday);


        //    var query = res.QueryAsSql;
        //}

        //[TestMethod]
        //public void QueryBuilder_MSSQL()
        //{
        //    var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

        //    //dataReaderMoq.Setup(r => r.Read()).Returns();

        //    var sql = new SQL<Author>(SQL.Dialect.MSSql);
        //    var tab = new TabAuthors();

        //    var res = sql.Select(
        //                    sql.Map(tab.FirstName, (p, v) => p.AuthorFirstName = GetSave(v, "")),
        //                    sql.Map(tab.Birthday, (p, v) => p.AuthorBrithday = GetSave(v, DateTime.MaxValue)))
        //                .From(tab)
        //                .Where(
        //                    sql.And(
        //                        sql.Eq(tab.AuthorId, 123456789L),
        //                        sql.Gt(tab.Erfahrung, 5))
        //                    )
        //                .By(tab.Erfahrung)
        //                .By(tab.Birthday)
        //                .done();


        //}       

    }
}
