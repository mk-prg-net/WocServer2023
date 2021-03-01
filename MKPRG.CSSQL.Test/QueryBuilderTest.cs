using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Diagnostics;

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

        [TestMethod]
        public void QueryBuilder_SQL()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();

            var sql = new SQL<DFCObjects.Common.IBaugruppe>();
            var tab = new Bosch106();

            var res = sql.Select(
                            sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                            sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                            sql.Map(tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v))                           
                        .From(tab)
                        .Where(sql.Eq(tab.ProjectNumber, "P.2998"))
                        .By(tab.ProjectNumber)
                        .By(tab.Qty)
                        .done();


            var bg = new DFCObjects.Common.ATMOBauGruppe();
            var readerMockUp = new ReaderMockUp(
                (tab.MatNr.N, "0100000000"),
                (tab.ProjectNumber.N, "P.2998"),
                (tab.ZeichnungsNr.N, "1234")
            );

            res.RecordToBoMapper.SetPropertiesOf(bg, readerMockUp);

            Assert.AreEqual("0100000000", bg.MatNr);
            Assert.AreEqual("P.2998", bg.ProjectPSPNr);
            Assert.AreEqual("1234", bg.DrawingNr);


            var query = res.QueryAsSql;
        }

        [TestMethod]
        public void QueryBuilder_MSSQL()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();

            var sql = new SQL<ATMO.DFC.Tree.Doc.DocInfo>(SQL.Dialect.MSSql);
            var tab = new PathTab();

            var res = sql.Select(
                            sql.Map(tab.DocId, (p, v) => p.DocId = (long)v),
                            sql.Map(tab.Created, (p, v) => p.CreationTime = (DateTime)v))
                        .From(tab)
                        .Where(
                            sql.And(
                                sql.Eq(tab.DocId, 1234L),
                                sql.Gt(tab.Created, new DateTime(2019, 12, 31)))
                            )
                        .By(tab.Statuschange)
                        .By(tab.StatusChangeOriginator)
                        .done();


        }



        [TestMethod]
        public void QueryBuilder_IfThen()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();

            var sql = new SQL<DFCObjects.Common.IBaugruppe>();
            var tab = new Bosch106();

            var res = sql.Select(
                            sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                            sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                            sql.Map(tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v))
                        .From(tab)
                        .Where(true ? sql.Eq(tab.ProjectNumber, "P.2998") : sql.Eq(tab.ProjectNumber, "P.1998"))
                        .By(tab.ProjectNumber)
                        .By(tab.Qty)
                        .done();


            var res2 = sql.Select(
                            sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                            sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                            sql.Map(tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v))
                        .From(tab)
                        .Where(false ? sql.Eq(tab.ProjectNumber, "P.2998"): sql.Eq(tab.ProjectNumber, "P.1998"))
                        .By(tab.ProjectNumber)
                        .By(tab.Qty)
                        .done();


            var res3 = sql.Select(
                sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                sql.Map(tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v))
            .From(tab)
            .SwitchedWhere(
                (false, sql.Eq(tab.ProjectNumber, "P.2998")), 
                (true, sql.Eq(tab.ProjectNumber, "P.1998")))
            .By(tab.ProjectNumber)
            .By(tab.Qty)
            .done();



            var res4 = sql.Select(
                sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                sql.Map(tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v))
            .From(tab)
            .SwitchedWhere(
                (true, sql.Eq(tab.ProjectNumber, sql.Txt("P.2998"))),
                (true, sql.Eq(tab.ProjectNumber, sql.Txt("P.1998"))))
            .By(tab.ProjectNumber)
            .By(tab.Qty)
            .done();


            var res5 = sql.Select(
                sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                sql.Map(tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v))
            .From(tab)
            .Where(
                sql.And(
                (true ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.2998")) : sql.Nop()),
                (true ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.1998")) : sql.Nop())))
            .By(tab.ProjectNumber)
            .By(tab.Qty)
            .done();

            var res6 = sql.Select(
                sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                sql.Map(tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v))
            .From(tab)
            .Where(
                sql.And(
                (false ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.2998")) : sql.Nop()),
                (true ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.1998")) : sql.Nop())))
            .By(tab.ProjectNumber)
            .By(tab.Qty)
            .done();

            var res7 = sql.Select(
                sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                sql.Map(tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v))
            .From(tab)
            .Where(
                sql.And(
                (false ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.2998")) : sql.Nop()),
                (false ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.1998")) : sql.Nop())))
            .By(tab.ProjectNumber)
            .By(tab.Qty)
            .done();


            var res8 = sql.Select(
                sql.Map(tab.MatNr, (p, v) => p.MatNr = (string)v),
                sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                sql.MapIf(false, tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v, p => p.DrawingNr = "-"))
            .From(tab)
            .Where(
                sql.And(
                (false ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.2998")) : sql.Nop()),
                (false ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.1998")) : sql.Nop())))
            .By(tab.ProjectNumber)
            .By(tab.Qty)
            .done();


            var res9 = sql.Select(
                sql.MapIf(false, tab.MatNr, (p, v) => p.MatNr = (string)v, p => p.MatNr = "0000000000"),
                sql.Map(tab.ProjectNumber, (p, v) => p.ProjectPSPNr = (string)v),
                sql.MapIf(false, tab.ZeichnungsNr, (p, v) => p.DrawingNr = (string)v, p => p.DrawingNr = "-"))
            .From(tab)
            .Where(
                sql.And(
                (false ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.2998")) : sql.Nop()),
                (false ? (IColXpr)sql.Eq(tab.ProjectNumber, sql.Txt("P.1998")) : sql.Nop())))
            .By(tab.ProjectNumber)
            .By(tab.Qty)
            .done();

        }




        [TestMethod]
        public void QueryBuilder_SQLAggregate()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();            

            var sql = new SQL<DFCObjects.Common.IBaugruppe>();
            var tab = new Bosch106();
            long _count = 0L;
            var res = sql.Select(
                            sql.Map(sql.Count(tab.MatNr), (p, v) => _count = (long)v))
                        .From(tab)
                        .Where(sql.Eq(tab.ProjectNumber, sql.Txt("P.2998")))
                        .By(tab.ProjectNumber)
                        .By(tab.Qty)
                        .done();


            res = sql.Select(
                            sql.Map(sql.Max(tab.MatNr), (p, v) => _count = (long)v),
                            sql.Map(sql.Min(tab.MatNr), (p, v) => _count = (long)v))
                        .From(tab)
                        .Where(sql.Eq(tab.ProjectNumber, sql.Txt("P.2998")))
                        .By(tab.ProjectNumber)
                        .By(tab.Qty)
                        .done();
        }



        class SFCStat
        {
            public string FromDept { get; set; }
            public string ToDept { get; set; }
            public DateTime Created { get; set; }
            public DateTime StatusChanged { get; set; }
            public DFCSecurity.Site Site { get; set; }
            public DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates UserState { get; set; }
        }

        [TestMethod]
        public void QueryBuilder_Equi_Join()
        {

            var sql = new SQL<SFCStat>();
            var tabSFC = new DFC3.DB.Tables.SFC("SFC");
            var tabDeptFrom = new DFC3.DB.Tables.Dept("DeptFrom");
            var tabDeptTo = new DFC3.DB.Tables.Dept("DeptTo");
            var tabSites = new DFC3.DB.Tables.Site("AtmoSite");
            var tabUserStates = new DFC3.DB.Tables.SFCUserstate("UState");


            var q = sql.Select(
                        sql.Map(tabDeptFrom.Department.FQN, (bo, v) => bo.FromDept = GetSave(v, "<unknown>")),
                        sql.Map(tabDeptTo.Department.FQN, (bo, v) => bo.ToDept = GetSave(v, "<unknown>")),
                        sql.Map(tabSites.Id.FQN, (bo, v) => bo.Site = GetSave(v, DFCSecurity.Site.none)),
                        sql.Map(tabSFC.CreationTime.FQN, (bo, v) => bo.Created = GetSave(v, DateTime.MinValue)),
                        sql.Map(tabSFC.StatusChanged.FQN, (bo, v) => bo.StatusChanged = GetSave(v, DateTime.MinValue)),
                        sql.Map(tabUserStates.Id.FQN, (bo, v) => bo.UserState = GetSave(v, DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates.none))
                    )
                    .EqJoinFrom(
                        (tabSFC.DeptFromId, tabDeptFrom.Id),
                        (tabSFC.DeptToId, tabDeptTo.Id),
                        (tabSFC.SiteId, tabSites.Id),
                        (tabSFC.UserStateId, tabUserStates.Id))
                    .Where(sql.Eq(tabUserStates.Id.FQN, sql.Int((int)DZAUtilities_Dictionaries.GlobalDictionaries.DfcDocStates.open)))
                    .done();

        }
        [TestMethod]
        public void QueryBuilder_Join()
        {

            var sql = new SQL<DFC3.DB.Bo.MaraPjBo>();
            var tabPrj = new DFC3.DB.Tables.Projektliste2();
            var tabMaraPj = new DFC3.DB.Tables.MaraPj();


            var q = sql.Select(
                        sql.Map(tabMaraPj.MatNr.FQN, (bo, v) => bo.MatNr = GetSave(v, "<unknown>"))
                    )
                    .JoinFrom(
                        (tabPrj, tabMaraPj, 
                            sql.And(
                                sql.Eq(tabPrj.PrjNr.FQN, tabMaraPj.PjNr.FQN), 
                                sql.Eq(tabPrj.StatNr.FQN, tabMaraPj.StatNr.FQN))))     
                    .Where(sql.Eq(tabMaraPj.MatNr, "0843AY1282"))
                    .done();
        }




        [TestMethod]
        public void QueryBuilder_LEFT_Outer_Join()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();

            var sql = new SQL<DFCObjects.Common.IBaugruppe>();
            var tabA = new Bosch106();
            var tabB = new Bosch106();

            var res = sql.Select(
                            sql.Map(tabA.MatNr.FQN, (p, v) => p.MatNr = (string)v),
                            sql.Map(tabA.ProjectNumber.FQN, (p, v) => p.ProjectPSPNr = (string)v),
                            sql.Map(tabB.ZeichnungsNr.FQN, (p, v) => p.DrawingNr = (string)v))
                        .LeftOuterJoin(tabA, tabA.MatNr, tabB, tabB.MatNr)
                        .Where(sql.Eq(tabA.ProjectNumber, sql.Txt("P.2998")))
                        .By(tabA.ProjectNumber)                        
                        .done();

            Debug.WriteLine(res.QueryAsSql);

        }


        [TestMethod]
        public void UpdateQueryBuilder_SQL()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();

            var sql = new SQL<DFCObjects.Common.IBaugruppe>();
            var tab = new Bosch106();

            var res = sql.Update(tab,  
                            sql.Set(tab.MatNr, sql.Txt("A123456789")), 
                            sql.Set(tab.Qty, sql.Int(100)),
                            sql.Set(tab.STLStatus, sql.Bool(true))
                            )                        
                        .Where(sql.Eq(tab.ProjectNumber, sql.Txt("P.2998")))
                        .done();

            var query = res.QueryAsSql;
        }

        [TestMethod]
        public void InsertQueryBuilder_SQL()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();

            var sql = new SQL<int>();
            var tab = new Bosch106();

            var res = sql.Insert(tab,
                            sql.NewVal(tab.MatNr, sql.Txt("A123456789")),
                            sql.NewVal(tab.Qty, sql.Int(100)),
                            sql.NewVal(tab.STLStatus, sql.Bool(true)),
                            sql.NewVal(tab.ZeichnungsNr, sql.Date(DateTime.Now)));
                        

            var query = res.QueryAsSql;
        }


        [TestMethod]
        public void DeleteQueryBuilder_SQL()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();

            var sql = new SQL<int>();
            var tab = new Bosch106();

            // Alle Zeilen einer 
            var res = sql.Delete().From(tab).done();


            var query = res.QueryAsSql;

            res = sql.Delete().From(tab)
                            .Where(
                                sql.And(
                                    sql.Eq(tab.MatNr, sql.Txt("1234567890")),
                                    sql.Eq(tab.STLStatus, sql.Bool(false)))).done();


            query = res.QueryAsSql;
        }

        [TestMethod]
        public void TruncateQueryBuilder_SQL()
        {
            var dataReaderMoq = new Moq.Mock<System.Data.IDataReader>();

            //dataReaderMoq.Setup(r => r.Read()).Returns();

            var sql = new SQL<int>();
            var tab = new Bosch106();

            // Alle Zeilen einer 
            var res = sql.Truncate().From(tab);

            var query = res.QueryAsSql;

        }


    }
}
