using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Authorization.ATMO
{
    /// <summary>
    /// mko, 1.7.2020
    /// Sicherheitsmerkmale von DFT- Tree Objekten
    /// </summary>
    public class SecurityFeatures
        : NamingBase
    {
        public const long UID = 0x12FA0768;

        public SecurityFeatures()
            : base(UID)
        {
        }

        public override string CNT => "secF";
        public override string CN => "安全功能";
        public override string DE => "Sicherheitsmerkmale";
        public override string EN => "Security features";
        public override string ES => "Características de seguridad";
    }

    /// <summary>
    /// mko, 1.7.2020
    /// </summary>
    public class ListOfReleasedProjects
        : NamingBase
    {
        public const long UID = 0x60B804FB;

        public ListOfReleasedProjects()
            : base(UID)
        {
        }

        public override string CNT => "releasedProjects";
        public override string CN => "已启动的项目清单";
        public override string DE => "Liste der freigeschaltete Projekte";
        public override string EN => "List of released Projects";
        public override string ES => "Lista de proyectos liberados";
        public override string Glyph => $"{Glyphs.Math.OpenSet}{Glyphs.DFC.Project}{Glyphs.Math.Ellipsis}{Glyphs.Math.CloseSet}";
    }

    /// <summary>
    /// mko, 1.7.2020
    /// </summary>
    public class CustomerView
        : NamingBase
    {
        public const long UID = 0xA3DFA7E9;

        public CustomerView()
            : base(UID)
        {
        }

        public override string CNT => "customerView";
        public override string CN => "顾客观";
        public override string DE => "Kundensicht";
        public override string EN => "Customer View";
        public override string ES => "Vista del cliente";
    }


    /// <summary>
    /// mko, 1.7.2020
    /// Recht, Preisinformationen einzusehen
    /// </summary>
    public class CanSeePrices
        : NamingBase
    {
        public const long UID = 0x5D5201BC;

        public CanSeePrices()
            : base(UID)
        {
        }

        public override string CNT => "canSeePrices";
        public override string CN => "出租车看价格";
        public override string DE => "Darf Preisangaben einsehen";
        public override string EN => "can see prices";
        public override string ES => "Puede ver los precios";
    }

    /// <summary>
    /// mko, 1.7.2020
    /// Darf DokuChecks durchführen
    /// </summary>
    public class CanPerformDokuChecks
        : NamingBase
    {
        public const long UID = 0x7E8B51B2;

        public CanPerformDokuChecks()
            : base(UID)
        {
        }

        public override string CNT => "CanPerformDokuChecks";
        public override string CN => "被授权执行DokuCheck";
        public override string DE => "Ist berechtigt, Dokuchecks auszuführen";
        public override string EN => "Is authorized to perform a DokuCheck";
        public override string ES => "Está autorizado a realizar un DokuCheck";
    }
}
