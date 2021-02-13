using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ActiveDirectory.Errors
{
    /// <summary>
    /// mko, 16.7.2020
    /// Unbekannter Wert
    /// </summary>
    public class UserAccountNotFound : NamingBase
    {
        public const long UID = 0xA729540A;

        public UserAccountNotFound()
            : base(UID)
        {
        }

        public override string CNT => "userAccountNotFoundInAD";
        public override string CN => EN;
        public override string DE => "Das Benutzerkonto ist im Active Directory nicht auffindbar";
        public override string EN => "The user account cannot be found in Active Directory";
        public override string ES => "La cuenta de usuario no se encuentra en el Active Directory";
    }

    /// <summary>
    /// mko, 16.7.2020
    /// </summary>
    public class DomainNotFound : NamingBase
    {
        public const long UID = 0x6667DA46;

        public DomainNotFound()
            : base(UID)
        {
        }

        public override string CNT => "domainNotFoundInAD";
        public override string CN => EN;
        public override string DE => "Das Active Directory Domäne existiert nicht oder ist nicht erreichbar";
        public override string EN => "The Active Directory domain does not exist or is not accessible";
        public override string ES => "El dominio de Active Directory no existe o no es accesible";
    }


    /// <summary>
    /// mko, 16.7.2020
    /// Unbekannter Wert
    /// </summary>
    public class AccessToADForestIsRestrictedOrNotPossible : NamingBase
    {
        public const long UID = 0xF541757A;

        public AccessToADForestIsRestrictedOrNotPossible()
            : base(UID)
        {
        }

        public override string CNT => "accessToADForestIsRestrictedOrNotPossible";
        public override string CN => EN;
        public override string DE => "Zugriff auf AD Gesamtstruktur ist eingeschränkt oder nicht möglich";
        public override string EN => "Access to AD Forest is restricted or not possible";
        public override string ES => "El acceso a la estructura general está restringido o no es posible";
    }

}
