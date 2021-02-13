using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Transactions
{
    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class Transaction
    : NamingBase
    {

        public const long UID = 0x937CAE2F;

        public Transaction()
            : base(UID)
        {
        }

        public override string CNT => "transaction";
        public override string CN => EN;
        public override string DE => "Transaktion";
        public override string EN => "Transaction";
        public override string ES => "Transacción";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class CommitTransaction
        : NamingBase
    {

        public const long UID = 0x934A9216;

        public CommitTransaction()
            : base(UID)
        {
        }

        public override string CNT => "commitTransaction";
        public override string CN => EN;
        public override string DE => "Transaktion verbindlich durchführen";
        public override string EN => "Commit Transaction";
        public override string ES => "cometer Transacción";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class RolebackTransaction
        : NamingBase
    {

        public const long UID = 0xC3E8AEE9;

        public RolebackTransaction()
            : base(UID)
        {
        }

        public override string CNT => "rolebackTransaction";
        public override string CN => EN;
        public override string DE => "Transaktion annulieren";
        public override string EN => "roleback Transaction";
        public override string ES => "transacción de retroceso";
    }


}
