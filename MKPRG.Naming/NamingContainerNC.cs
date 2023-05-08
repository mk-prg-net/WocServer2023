using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace MKPRG.Naming
{
    public class NamingContainerNC
        : NamingBase, ILangPL
    {

        public const long UID = 0x13B61721469D89C8L;

        public NamingContainerNC()
            : base(UID)
        {
        }

        public override string CNT => "NC";
        public override string CN => "命名容器";
        public override string DE => "Namenscontainer";
        public override string EN => "Naming Container";
        public override string ES => "Contenedor de nombres";

        public string PL => "Pojemnik na nazwy";

        public override string Glyph => Glyphs.NamingContainers.NamingContainer;
    }


    public class NID
        : NamingBase, ILangPL
    {

        public const long UID = 0x6381F4B1B8D74862L;

        public NID()
            : base(UID)
        {
        }

        public override string CNT => "NID";
        public override string CN => "NID: 命名容器ID";
        public override string DE => "NID: Namenscontainer ID";
        public override string EN => "NID: Naming Container ID";
        public override string ES => "NID: ID del contenedor de nombres";

        public string PL => "NID: Identyfikator pojemnika na nazwy";

        public override string Glyph => Glyphs.NamingContainers.NamingId;
    }
}
