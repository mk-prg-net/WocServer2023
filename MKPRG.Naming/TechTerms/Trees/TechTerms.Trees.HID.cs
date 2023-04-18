using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Trees.HID
{
    //"Parent Subspace is full- can't allocate new Hid"
    /// <summary>
    /// mko, 3.8.2020
    /// </summary>
    public class HidAllocationError_IndexIntervallInParentNodeExhausted
        : NamingBase
    {

        public const long UID = 0x682B3316;

        public HidAllocationError_IndexIntervallInParentNodeExhausted()
            : base(UID)
        {
        }

        public override string CNT => "hidParentSubspaceIsFull";
        public override string CN => "父节点中的hid可能的指数区间已经用尽";
        public override string DE => "Das Intervall möglicher Indizes für eine Hid im Elternknoten ist ausgeschöpft";
        public override string EN => "The interval of possible indices for a hid in the parent node is exhausted";
        public override string ES => "Se agota el intervalo de índices posibles para un hid en el nodo padre";
    }
}
