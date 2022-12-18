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
        public override string CN => "交易";
        public override string DE => "Transaktion";
        public override string EN => "Transaction";
        public override string ES => "Transacción";

        public override string Glyph => Glyphs.Transactions.Transaction;

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
        public override string CN => "提交交易";
        public override string DE => "Transaktion bestätigen";
        public override string EN => "Commit Transaction";
        public override string ES => "cometer Transacción";
    }

    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class RollbackTransaction
        : NamingBase
    {

        public const long UID = 0xC3E8AEE9;

        public RollbackTransaction()
            : base(UID)
        {
        }

        public override string CNT => "rollbackTransaction";
        public override string CN => "回滚交易";
        public override string DE => "Transaktion annulieren";
        public override string EN => "rollback Transaction";
        public override string ES => "Transacción de reversión";

        public override string Glyph => Glyphs.Transactions.Rollback;
    }

    /// <summary>
    /// mko, 3.2.2021
    /// </summary>
    public class CancelTransaction
        : NamingBase
    {

        public const long UID = 0xC7FCE454;

        public CancelTransaction()
            : base(UID)
        {
        }

        public override string CNT => "cancelTransaction";
        public override string CN => "取消交易";
        public override string DE => "Transaktion abbrechen";
        public override string EN => "cancel Transaction";
        public override string ES => "Cancelar la transacción";

        public override string Glyph => Glyphs.Transactions.Cancel;

    }




}
