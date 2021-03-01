using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.CSSQL.Test
{
    class Bosch106 : Table
    {
        public Bosch106(string Aliasname) : base("Bosch106", Aliasname)
        {
            MatNr = new ColName(this, "Materialnummer");
            ProjectNumber = new ColName(this, "PSPNr");
            ZeichnungsNr = new ColName(this, "ZeichnungsNr");
            STLStatus = new ColName(this, "STLTStat");
            Qty = new ColName(this, "Qty");
        }

        public Bosch106() : this(null) { }       

        public ColName MatNr { get; }

        public ColName ProjectNumber { get; }

        public ColName ZeichnungsNr { get; }

        public ColName STLStatus { get; }

        public ColName Qty { get; }


    }


    class PathTab : Table
    {

        public PathTab()
            : base("Path")
        {
            DocId = new ColName(TableName, "DocId");
            XType = new ColName(TableName, "XType");
            Created = new ColName(TableName, "Created");
            UserState = new ColName(TableName, "UserState");
            Statuschange = new ColName(TableName, "StatusChanged");
            StatusChangeOriginator = new ColName(TableName, "StatusChangeOriginator");
        }


        public ColName DocId { get; }
        public ColName XType { get; }
        public ColName Created { get; }
        public ColName UserState { get; }
        public ColName Statuschange { get; }
        public ColName StatusChangeOriginator { get; }

    }
}
