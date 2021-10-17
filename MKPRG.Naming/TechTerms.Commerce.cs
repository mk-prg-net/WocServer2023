using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Commerce
{
    public class Procurement
        : NamingBase
    {
        public const long UID = 0x2A829972;

        public Procurement()
            : base(UID)
        {
        }

        public override string CNT => "procurment";
        public override string CN => "采购";
        public override string DE => "Beschaffung";
        public override string EN => "Procurment";
        public override string ES => "Adquisiciones";

        public override string Glyph => Glyphs.Commerce.Basket;
    }

    public class Order
        : NamingBase
    {
        public const long UID = 0xD149F55;

        public Order()
            : base(UID)
        {
        }

        public override string CNT => "order";
        public override string CN => "采购";
        public override string DE => "Bestellung";
        public override string EN => "Order";
        public override string ES => "Pida";

        public override string Glyph => Glyphs.Commerce.Basket;
    }

}
