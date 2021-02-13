using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ATMO.SAP.Materials.OriginOfParts
{
    /// <summary>
    /// mko, 22.9.2020
    /// MTART = ANFE: in Atmo gefertigte Baugruppen und Einzelteile
    /// </summary>
    public class SelfMade : NamingBase
    {
        public const long UID = 0xD6AD3BEE;

        public SelfMade()
            : base(UID)
        {
        }

        public override string CNT => "selfMade";
        public override string CN => EN;
        public override string DE => "ANFE";
        public override string EN => "self made Part (ANFE)";
        public override string ES => "hecho a sí mismo (ANFE)";
    }

    /// <summary>
    /// mko, 22.9.2020
    /// MTART = KATA: Teile aus dem Einkaufskatalog
    /// </summary>
    public class Catalog : NamingBase
    {
        public const long UID = 0x399F5210;

        public Catalog()
            : base(UID)
        {
        }

        public override string CNT => "catalogPart";
        public override string CN => EN;
        public override string DE => "Einkaufsteil aus dem Katalog (KATA)";
        public override string EN => "Purchase part from the catalogue (KATA)";
        public override string ES => "Comprando una parte del catálogo (KATA)";
    }

    /// <summary>
    /// mko, 22.9.2020
    /// MTART = DOKU: Teil stammt aus dem Bereich Projektdokumentation und stellt
    /// in der Regel ein Manual dar.
    /// 
    /// Stellt eine eigene Materialnummernklasse dar. Entspricht der Klasse MatClass.ManualDokuMat !    
    /// </summary>    
    public class Documentation : NamingBase
    {
        public const long UID = 0xBC49FE1;

        public Documentation()
            : base(UID)
        {
        }

        public override string CNT => "documentation";
        public override string CN => EN;
        public override string DE => "Projektdokumentation, Handbuch, Bedienungsanleitung (DOKU)";
        public override string EN => "Project documentation, manual, operating instructions (DOKU)";
        public override string ES => "Documentación del proyecto, manual, instrucciones de funcionamiento (DOKU)";
    }

    /// <summary>
    /// mko, 22.9.2020
    /// MTART = ANGB: Teile stammt aus einem Angebot eines Lieferanten    
    /// </summary>
    public class Offer : NamingBase
    {
        public const long UID = 0x763D06A6;

        public Offer()
            : base(UID)
        {
        }

        public override string CNT => "offer";
        public override string CN => EN;
        public override string DE => "Angebot (ANGB)";
        public override string EN => "Offer (ANGB)";
        public override string ES => "Oferta (ANG)";
    }

    /// <summary>
    /// mko, 22.9.2020
    /// MTART = DIEN: Teil stammt aus dem Bereich der Dienstleistungen im Rahmen einer Projektrealisierung.  
    /// Merkiert eingekauften Dienstleistung.
    /// Stellt eine eigene Materialklasse dar. Entspricht der Klasse MatClass.Service
    /// </summary>
    public class Service : NamingBase
    {
        public const long UID = 0x75D02F32E;

        public Service()
            : base(UID)
        {
        }

        public override string CNT => "service";
        public override string CN => EN;
        public override string DE => "eingekauften Dienstleistung (DIEN)";
        public override string EN => "purchased service (DIEN)";
        public override string ES => "servicio comprado (DIEN)";
    }

    /// <summary>
    /// mko, 22.9.2020
    /// MTART = VERP: Teil beschreibt Verpackungsmaterial für den Transport
    /// Stellt eine eigene Materialklasse dar. Entspricht der Klasse MatClass.Packaging
    /// </summary>
    public class Packaging : NamingBase
    {
        public const long UID = 0x43C27C6F;

        public Packaging()
            : base(UID)
        {
        }

        public override string CNT => "packaging";
        public override string CN => EN;
        public override string DE => "Verpackungsmaterial für den Transport (VERP)";
        public override string EN => "Packaging material for transport (VERP)";
        public override string ES => "Material de embalaje para el transporte (VERP)";
    }
}
