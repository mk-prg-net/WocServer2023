using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Authentication.ATMO.Errors
{
    public class UserIsDisabled
        : NamingBase
    {
        public const long UID = 0xED382B6F;

        public UserIsDisabled()
            : base(UID)
        {
        }

        public override string CNT => "errUserIsDisabled";
        public override string CN => EN;
        public override string DE => "Der Benutzer wurde deaktiviert, und kann sich folglich nicht mehr anmelden.";
        public override string EN => "The user has been deactivated and can therefore no longer log in.";
        public override string ES => "El usuario ha sido desactivado y, por lo tanto, no puede conectarse.";
    }

    public class NoRoleasAreAssigedToUser
        : NamingBase
    {
        public const long UID = 0x6D4631AD;

        public NoRoleasAreAssigedToUser()
            : base(UID)
        {
        }

        public override string CNT => "errNoRolesAssigned";
        public override string CN => EN;
        public override string DE => "Dem Benutzer wurden noch keine DFC Rollen zugewiesen.";
        public override string EN => "The user has not yet been assigned any DFC roles.";
        public override string ES => "Al usuario no se le ha asignado todavía ningún DFC papel.";
    }

    public class NoCustGroupsAreAssigedToUser
    : NamingBase
    {
        public const long UID = 0xF273B453;

        public NoCustGroupsAreAssigedToUser()
            : base(UID)
        {
        }

        public override string CNT => "errNoCustGroupsAreAssigned";
        public override string CN => EN;
        public override string DE => "Dem Kunden wurden ist keine DFC Kundengruppe zugewiesen";
        public override string EN => "No DFC customer group has been assigned to the customer";
        public override string ES => "No se ha asignado ningún DFC grupo de clientes al cliente";
    }


}
