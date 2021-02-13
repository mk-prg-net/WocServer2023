using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 7.8.2020
/// ATMO Materialwirtschaft
/// MSTAE- Materialstatus Einkauf
/// </summary>
namespace MKPRG.Naming.TechTerms.ATMO.SAP.Materials.MSTAE
{

    public class None : NamingBase
    {
        public const long UID = 0x808B66F8;

        public None()
            : base(UID)
        {
        }

        public override string CNT => "none";
        public override string CN => EN;
        public override string DE => "kein MSTAE festgelegt";
        public override string EN => "no MSTAE defined";
        public override string ES => "no se ha definido el MSTAE";
    }


    /// <summary>
    /// Teil darf zukünftig nicht mehr verbaut werden, da von Lieferant abgekündigt
    /// </summary>
    public class _00_Terminated : NamingBase
    {
        public const long UID = 0xE1949D2F;

        public _00_Terminated()
            : base(UID)
        {
        }

        public override string CNT => "terminated00";
        public override string CN => EN;
        public override string DE => "00- Abgekündigte Komponente";
        public override string EN => "00- Termiated";
        public override string ES => "00- Descontinuado";
    }

    /// <summary>
    /// Teil aktuell nicht einsetzbar- wird von allen SAP- Funktionen nicht akzeptiert
    /// </summary>
    public class _01_Blocked : NamingBase
    {
        public const long UID = 0x9429729C;

        public _01_Blocked()
            : base(UID)
        {
        }

        public override string CNT => "blocked01";
        public override string CN => EN;
        public override string DE => "01- Gesperrt für alle SAP- Funktionen";
        public override string EN => "01- Blocked for all SAP functions";
        public override string ES => "01- Bloqueado para todas las funciones de SAP";
    }

    /// <summary>
    /// 
    /// </summary>
    public class _03_Expired_NotOrderable : NamingBase
    {
        public const long UID = 0x88BF85BE;

        public _03_Expired_NotOrderable()
            : base(UID)
        {
        }

        public override string CNT => "expires03";
        public override string CN => EN;
        public override string DE => "03- Läuft aus, nicht mehr bestellbar";
        public override string EN => "03- Expires, NotOrderable";
        public override string ES => "03- Expira, no se puede pedir";
    }

    /// <summary>
    /// 
    /// </summary>
    public class _09_PlantSpecificMaintenance : NamingBase
    {
        public const long UID = 0x789BA2A7;

        public _09_PlantSpecificMaintenance()
            : base(UID)
        {
        }

        public override string CNT => "plantSpecific09";
        public override string CN => EN;
        public override string DE => "09- Werksspezifische Pflege";
        public override string EN => "09- Plant-specific maintenance";
        public override string ES => "09- Mantenimiento específico de la planta";
    }

    /// <summary>
    /// 
    /// </summary>
    public class _11_AutomaticExchangeBySAP : NamingBase
    {
        public const long UID = 0x98A4ABC7;

        public _11_AutomaticExchangeBySAP()
            : base(UID)
        {
        }

        public override string CNT => "automaticExchangeBySAP11";
        public override string CN => EN;
        public override string DE => "11- Automatischer Austausch: Material wird durch ein kompatibles Material automatisch in SAP- Stücklisten ersetzt";
        public override string EN => "11- Automatic exchange: material is automatically replaced by a compatible material in SAP parts lists";
        public override string ES => "11- Intercambio automático: el material se sustituye automáticamente por un material compatible en las listas de piezas de SAP";
    }

    /// <summary>
    /// 
    /// </summary>
    public class _13_UseUpRemainingStocks : NamingBase
    {
        public const long UID = 0xE289BAA5;

        public _13_UseUpRemainingStocks()
            : base(UID)
        {
        }

        public override string CNT => "useUpRemainingStocks13";
        public override string CN => EN;
        public override string DE => "13- Restbestände aufbrauchen";
        public override string EN => "13- use up remainders";
        public override string ES => "13- usar los restos...";
    }

    /// <summary>
    /// 
    /// </summary>
    public class _49_Expired_CanBeOrdered : NamingBase
    {
        public const long UID = 0xB5BC03E6;

        public _49_Expired_CanBeOrdered()
            : base(UID)
        {
        }

        public override string CNT => "expires49";
        public override string CN => EN;
        public override string DE => "49- Läuft aus, bestellbar";
        public override string EN => "49- Expires, can be ordered";
        public override string ES => "49- Expira, disponible para su pedido";
    }



}
