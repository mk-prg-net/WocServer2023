using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.StateMachine.ATMO
{


    public class StatusChange
    : NamingBase
    {
        public const long UID = 0x4FE7276E;

        public StatusChange()
            : base(UID)
        {
        }

        public override string CNT => "statusChange";
        public override string CN => EN;
        public override string DE => "Statuswechsel";
        public override string EN => "Status change";
        public override string ES => "cambio de estado";
    }


    public class StatusChangeOriginator
        : NamingBase
    {
        public const long UID = 0x81876C13;

        public StatusChangeOriginator()
            : base(UID)
        {
        }

        public override string CNT => "statusChangeOriginator";
        public override string CN => EN;
        public override string DE => "Statusänderer";
        public override string EN => "Initiator of status change";
        public override string ES => "Iniciador del cambio de estado";
    }

    public class StatusLastChanged
    : NamingBase
    {
        public const long UID = 0xD8F389C;

        public StatusLastChanged()
            : base(UID)
        {
        }

        public override string CNT => "statusLastChaged";
        public override string CN => EN;
        public override string DE => "Datum der letzten Statusänderung";
        public override string EN => "Date of the last status change";
        public override string ES => "Fecha del último cambio de estado";
    }

}
