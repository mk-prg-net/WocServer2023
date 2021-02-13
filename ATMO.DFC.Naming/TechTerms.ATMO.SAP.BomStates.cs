using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ATMO.SAP.BomStates
{
    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class _01_Initialized : NamingBase
    {
        public const long UID = 0x8C39CAFD;

        public _01_Initialized()
            : base(UID)
        {
        }

        public override string CNT => "initialized_01";
        public override string CN => EN;
        public override string DE => "01: Stücklistenkopf wurde angelegt, Stückliste enthält jedoch noch keine Positionen";
        public override string EN => "01: BOM header was created, but the bill of material does not yet contain any items";
        public override string ES => "01: Se creó la cabecera de la lista de materiales, pero la lista de materiales aún no contiene ninguna posición";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class _02_InProcessByExternal : NamingBase
    {
        public const long UID = 0xE70DD471;

        public _02_InProcessByExternal()
            : base(UID)
        {
        }

        public override string CNT => "inProcessByExternal_02";
        public override string CN => EN;
        public override string DE => "02: Stückliste wird von einem externen Mitarbeiter bearbeitet, für andere zur Bearbeitung gesperrt";
        public override string EN => "02: Bill of material is processed by an external employee, locked for processing by others";
        public override string ES => "02: La lista de materiales es procesada por un empleado externo, bloqueada para ser procesada por otros";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class _03_CreatedByExternal : NamingBase
    {
        public const long UID = 0x8386A5A5;

        public _03_CreatedByExternal()
            : base(UID)
        {
        }

        public override string CNT => "createdByExternal_03";
        public override string CN => EN;
        public override string DE => "03: Stückliste wird von einem externen Mitarbeiter bearbeitet, für andere zur Bearbeitung gesperrt";
        public override string EN => "03: Bill of material was created by an external employee";
        public override string ES => "03: La lista de materiales fue creada por un empleado externo";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class _04_InProcessByInternal : NamingBase
    {
        public const long UID = 0x78831660;

        public _04_InProcessByInternal()
            : base(UID)
        {
        }

        public override string CNT => "inProcessByInternal_04";
        public override string CN => EN;
        public override string DE => "04: Stückliste wird von einem internen Mitarbeiter bearbeitet, für andere zur Bearbeitung gesperrt";
        public override string EN => "04: Bill of material is processed by an internal employee, locked for processing by others";
        public override string ES => "04: La lista de materiales es procesada por un empleado interno, bloqueada para ser procesada por otros";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class _05_CreatedByInternal : NamingBase
    {
        public const long UID = 0x5B2A7F14;

        public _05_CreatedByInternal()
            : base(UID)
        {
        }

        public override string CNT => "createdByInternal_05";
        public override string CN => EN;
        public override string DE => "05: Stückliste wird von einem internen Mitarbeiter bearbeitet, für andere zur Bearbeitung gesperrt";
        public override string EN => "05: Parts list was created by an internal employee";
        public override string ES => "05: La lista de piezas fue creada por un empleado interno";
    }

    /// <summary>
    /// mko, 3.12.2020
    /// </summary>
    public class _03_05_Created : NamingBase
    {
        public const long UID = 0xEA1112CA;

        public _03_05_Created()
            : base(UID)
        {
        }

        public override string CNT => "createdByInternalOrExternal_03_05";
        public override string CN => EN;
        public override string DE => "03 oder 05: Stückliste wird von einem internen oder externen Mitarbeiter bearbeitet, für andere zur Bearbeitung gesperrt";
        public override string EN => "03 or 05: Parts list was created by an internal or external employee";
        public override string ES => "03 o 05: La lista de materiales está siendo procesada por un empleado interno o externo, bloqueada para ser procesada por otros.";
    }


    /// <summary>
    /// mko, 29.9.2020
    /// Neuer Zustand, der den ProcurementState (entstsanden aus Aufteilung von SAP- BomStatus in BomStatus und ProcurementStatus) erweitert.
    /// </summary>
    public class NoCurrentOrders : NamingBase
    {
        public const long UID = 0xA72D3C6;

        public NoCurrentOrders()
            : base(UID)
        {
        }

        public override string CNT => "noCurrentOrders";
        public override string CN => EN;
        public override string DE => "Keine leufenden Bestellungen";
        public override string EN => "No current orders";
        public override string ES => "No hay pedidos actuales";
    }


    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class _06_StartOrdering : NamingBase
    {
        public const long UID = 0xE2344076;

        public _06_StartOrdering()
            : base(UID)
        {
        }

        public override string CNT => "startOrdering_06";
        public override string CN => EN;
        public override string DE => "06: Beschaffungsprozess wurde begonnen, Änderungen an der Stückliste sind nicht möglich";
        public override string EN => "06: Procurement process was started, changes to the bill of material are not possible";
        public override string ES => "06: Se inició el proceso de adquisición, no es posible modificar la lista de materiales";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class _07_InOrdering : NamingBase
    {
        public const long UID = 0x220CEE9;

        public _07_InOrdering()
            : base(UID)
        {
        }

        public override string CNT => "inOrdering_07";
        public override string CN => EN;
        public override string DE => "07: Beschaffungsprozess wurde begonnen, Änderungen an der Stückliste sind nicht möglich";
        public override string EN => "07: Bill of material is in procurement, changes to the bill of material are not possible";
        public override string ES => "07: La lista de materiales está en proceso de adquisición, no se pueden hacer cambios en la lista de materiales";
    }

    /// <summary>
    /// mko, 28.7.2020
    /// </summary>
    public class _08_OrderingCompleted : NamingBase
    {
        public const long UID = 0x7FADD3B3;

        public _08_OrderingCompleted()
            : base(UID)
        {
        }

        public override string CNT => "orderingCompleted_08";
        public override string CN => EN;
        public override string DE => "08: Die Beschaffung ist abgeschlossen, die Stückliste kann wieder bearbeitet werden";
        public override string EN => "08: Procurement is complete, the bill of material can be processed again";
        public override string ES => "08: La adquisición está completa, la lista de materiales puede ser procesada de nuevo";
    }
}
