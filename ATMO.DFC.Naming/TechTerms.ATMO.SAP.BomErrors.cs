using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ATMO.SAP.BomErrors
{
    public class IncorrectlyClassifiedMaterial
        : NamingBase
    {
        public const long UID = 0xEEF400F6;

        public IncorrectlyClassifiedMaterial()
            : base(UID)
        {
        }

        public override string CNT => "bomErrIncorrectlyClassifedMat";
        public override string CN => EN;
        public override string DE => "Die Stücklistenposition ist bezüglich der Materialklasse falsch klassifiziert worden.";
        public override string EN => "The parts list item has been incorrectly classified with regard to the material class.";
        public override string ES => "La posición de la lista de materiales se ha clasificado incorrectamente con respecto a la clase de material.";
    }

    public class BomAtAtmoStructuralError
        : NamingBase
    {
        public const long UID = 0xCA27F659;

        public BomAtAtmoStructuralError()
            : base(UID)
        {
        }

        public override string CNT => "bomAtAtmoStructuralError";
        public override string CN => EN;
        public override string DE => "Fehlerhafte Bom@Atmo Stücklistenstruktur";
        public override string EN => "Faulty Bom@Atmo parts list structure";
        public override string ES => "Estructura incorrecta de la lista de materiales de Bom@Atmo";
    }


    public class MainMechAssyMissing
    : NamingBase
    {
        public const long UID = 0x26665ABD;

        public MainMechAssyMissing()
            : base(UID)
        {
        }

        public override string CNT => "mainMechAssyMissing";
        public override string CN => EN;
        public override string DE => "Die mechanische Hauptbaugruppe fehlt";
        public override string EN => "The main mechanical assembly is missing";
        public override string ES => "Falta el ensamblaje mecánico principal";
    }

    public class MainElectAssyMissing
        : NamingBase
    {
        public const long UID = 0x15014E0A;

        public MainElectAssyMissing()
            : base(UID)
        {
        }

        public override string CNT => "mainElectAssyMissing";
        public override string CN => EN;
        public override string DE => "Die elektrische Hauptbaugruppe fehlt";
        public override string EN => "The main electrical assembly is missing";
        public override string ES => "Falta el ensamblaje eléctrico principal";
    }



}
