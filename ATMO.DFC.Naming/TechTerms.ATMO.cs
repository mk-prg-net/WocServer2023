using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ATMO
{

    public class Site
     : NamingBase
    {
        public const long UID = 0xF0D2BA57;

        public Site()
            : base(UID)
        {
        }


        public override string CNT => "site";
        public override string CN => EN;
        public override string DE => "Standort";
        public override string EN => "Site";
        public override string ES => "Localización";        
    }

    public class SiteName
         : NamingBase
    {
        public const long UID = 0x63254BB0;

        public SiteName()
            : base(UID)
        {
        }

        public override string CNT => "siteName";
        public override string CN => EN;
        public override string DE => "Standortname";
        public override string EN => "Site name";
        public override string ES => "Nombre de la ubicación";
    }


    public class CostCenter
        : NamingBase
    {

        public const long UID = 0x4A6448C4;

        public CostCenter()
            : base(UID)
        {
        }


        public override string CNT => "costCenter";
        public override string CN => EN;
        public override string DE => "Kostenstelle";
        public override string EN => "CostCenter";
        public override string ES => "Centro de costos";        
    }

    public class Department
    : NamingBase
    {

        public const long UID = 0xE48830B4;

        public Department()
            : base(UID)
        {
        }

        public override string CNT => "department";
        public override string CN => EN;
        public override string DE => "Abteilung";
        public override string EN => "Department";
        public override string ES => "Departamento";
    }

    public class CoWorker
    : NamingBase
    {

        public const long UID = 0x89AD00C;

        public CoWorker()
            : base(UID)
        {
        }

        public override string CNT => "coWorker";
        public override string CN => EN;
        public override string DE => "ATMO Mitarbeiter";
        public override string EN => "ATMO coworker";
        public override string ES => "Empleados del ATMO";
    }


    public class Customer
        : NamingBase
    {

        public const long UID = 0x1781AC;

        public Customer()
            : base(UID)
        {
        }

        public override string CNT => "customer";
        public override string CN => EN;
        public override string DE => "ATMO Kunde";
        public override string EN => "ATMO customer";
        public override string ES => "Cliente de ATMO";
    }


    public class VAB
        : NamingBase
    {

        public const long UID = 0x4B6DF00C;

        public VAB()
            : base(UID)
        {
        }

        public override string CNT => "vab";
        public override string CN => EN;
        public override string DE => "Verantwortlicher für das gesamte Projekt";
        public override string EN => "Responsible for the whole project";
        public override string ES => "Responsable de todo el proyecto";
    }

    public class VMK
    : NamingBase
    {

        public const long UID = 0xDA53E051;

        public VMK()
            : base(UID)
        {
        }

        public override string CNT => "vmk";
        public override string CN => EN;
        public override string DE => "Verantwortlicher für die mechanische Konstruktion";
        public override string EN => "Responsible for the mechanical construction";
        public override string ES => "Responsable de la construcción mecánica";
    }

    public class VSM
        : NamingBase
    {

        public const long UID = 0xFAD4B757;

        public VSM()
            : base(UID)
        {
        }

        public override string CNT => "vmk";
        public override string CN => EN;
        public override string DE => "Verantwortlicher für Software und Messtechnik";
        public override string EN => "Responsible for software and measurement technology";
        public override string ES => "Responsable del software y la tecnología de medición";
    }

    public class VDP
        : NamingBase
    {

        public const long UID = 0x3D71FA35;

        public VDP()
            : base(UID)
        {
        }

        public override string CNT => "vdp";
        public override string CN => EN;
        public override string DE => "Verantwortlicher Disponent";
        public override string EN => "Responsible dispatcher";
        public override string ES => "Despachador responsable";
    }
}
