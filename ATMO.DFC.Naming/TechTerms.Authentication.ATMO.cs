using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Authentication.ATMO
{
    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class CustomerGroup
        : NamingBase
    {
        public const long UID = 0x529B018F;

        public CustomerGroup()
            : base(UID)
        {
        }

        public override string CNT => "custGroup";
        public override string CN => EN;
        public override string DE => "Kundengruppe";
        public override string EN => "Customer group";
        public override string ES => "grupo de clientes";
    }

    /// <summary>
    /// mko, 19.6.2020
    /// </summary>
    public class UserClass
        : NamingBase
    {
        public const long UID = 0x5F107BE5;

        public UserClass()
            : base(UID)
        {
        }

        public override string CNT => "userClass";
        public override string CN => EN;
        public override string DE => "Benutzerklasse";
        public override string EN => "User class";
        public override string ES => "clase de usuario";
    }


    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class UserClassCustomer
        : NamingBase
    {
        public const long UID = 0xAB95EFA9;

        public UserClassCustomer()
            : base(UID)
        {
        }

        public override string CNT => "customer";
        public override string CN => EN;
        public override string DE => "Kunde";
        public override string EN => "Customer";
        public override string ES => "cliente ";

    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class UserClassCoWorker
        : NamingBase
    {
        public const long UID = 0x72925875;

        public UserClassCoWorker()
            : base(UID)
        {
        }

        public override string CNT => "coWorker";
        public override string CN => EN;
        public override string DE => "Mitarbeiter";
        public override string EN => "CoWorker";
        public override string ES => "Empleados";

    }


    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class DFCRoleNumber
        : NamingBase
    {
        public const long UID = 0x9D77FF4B;

        public DFCRoleNumber()
            : base(UID)
        {
        }

        public override string CNT => "dfcRoleNumber";
        public override string CN => EN;
        public override string DE => "DFC Rollennummer";
        public override string EN => "DFC role number";
        public override string ES => EN;
    }


    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class LoginGettingCustomerGroupAssignedToUser
        : NamingBase
    {
        public const long UID = 0xF2BEF933;

        public LoginGettingCustomerGroupAssignedToUser()
            : base(UID)
        {
        }

        public override string CNT => "getCustGroupDuringLogin";
        public override string CN => EN;
        public override string DE => "Abrufen der dem User zugewiesenen Kundengruppe";
        public override string EN => "Getting the customer group assigned to the user";
        public override string ES => "Recuperar el grupo de clientes asignado al usuario";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class LoginStepGetCustomer
        : NamingBase
    {
        public const long UID = 0xEF469FD0;

        public LoginStepGetCustomer()
            : base(UID)
        {
        }

        public override string CNT => "getCustomerDuringLogin";
        public override string CN => EN;
        public override string DE => "Prüfen, ob User Kunde ist";
        public override string EN => "Check whether User is a customer";
        public override string ES => "Compruebe si el usuario es un cliente";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class LoginStepGetCoWorker
        : NamingBase
    {
        public const long UID = 0x3E7C4132;

        public LoginStepGetCoWorker()
            : base(UID)
        {
        }

        public override string CNT => "getCoWorkerDuringLogin";
        public override string CN => EN;
        public override string DE => "Prüfen, ob User Mitarbeiter ist";
        public override string EN => "Check whether User is a coworker";
        public override string ES => "Compruebe si el usuario es un empleado";
    }



    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class ErrorNotCustomerNorATMOEmployee
        : NamingBase
    {
        public const long UID = 0xEFEFFE58;

        public ErrorNotCustomerNorATMOEmployee()
            : base(UID)
        {
        }

        public override string CNT => "errNotCustomerNorEmployee";
        public override string CN => EN;
        public override string DE => "Die UserId kann weder einem ATMO- Kunden, noch einem ATMO- Mitarbeiter zugeordnet werden. Möglicherweise ist er deaktiviert.";
        public override string EN => "User is not a Customer nor an ATMO Employee! May be disabled?";
        public override string ES => "El ID de usuario no puede ser asignado a un cliente o a un empleado del ATMO. Puede ser desactivado.";
    }
}
