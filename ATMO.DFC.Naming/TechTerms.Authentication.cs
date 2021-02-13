using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Authentication
{
    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Login
    : NamingBase
    {
        public const long UID = 0x77D58298;

        public Login()
            : base(UID)
        {
        }

        public override string CNT => "login";
        public override string CN => EN;
        public override string DE => "Anmeldung";
        public override string EN => "Login";
        public override string ES => "Login";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class LoginAuthenticateUser
    : NamingBase
    {
        public const long UID = 0x62C97E03;

        public LoginAuthenticateUser()
            : base(UID)
        {
        }

        public override string CNT => "authenticateUser";
        public override string CN => EN;
        public override string DE => "Identität des Benutzers feststellen";
        public override string EN => "authenticate User";
        public override string ES => "autentificar el usuario";
    }

    /// <summary>
    /// mko, 19.6.2020
    /// </summary>
    public class LoginGettingRolesAssignedToUser
        : NamingBase
    {
        public const long UID = 0x76744EDF;

        public LoginGettingRolesAssignedToUser()
            : base(UID)
        {
        }

        public override string CNT => "getUserRoles";
        public override string CN => EN;
        public override string DE => "Abrufen der dem User zugewiesenen Rollen";
        public override string EN => "Getting the roles assigned to the user";
        public override string ES => "Recuperar las funciones asignadas al usuario";
    }


    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Logout
    : NamingBase
    {
        public const long UID = 0x39A465E1;

        public Logout()
            : base(UID)
        {
        }

        public override string CNT => "logout";
        public override string CN => EN;
        public override string DE => "Abmeldung";
        public override string EN => "Logout";
        public override string ES => "logout";
    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class User
        : NamingBase
    {
        public const long UID = 0xD3E7F51E;

        public User()
            : base(UID)
        {
        }

        public override string CNT => "user";
        public override string CN => EN;
        public override string DE => "Benutzer";
        public override string EN => "User";
        public override string ES => "Usuario";
    }


    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class UserId
        : NamingBase
    {
        public const long UID = 0x95FB8B28;

        public UserId()
            : base(UID)
        {
        }

        public override string CNT => "userId";
        public override string CN => EN;
        public override string DE => EN;
        public override string EN => "User Id";
        public override string ES => EN;
    }


    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Role
        : NamingBase
    {
        public const long UID = 0xBA75EB8D;

        public Role()
            : base(UID)
        {
        }

        public override string CNT => "role";

        public override string CN => EN;

        public override string DE => "Rolle";

        public override string EN => "role";

        public override string ES => "El papel de la aplicación";
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class IsMemberOfRole
        : NamingBase
    {
        public const long UID = 0x92804CAB;

        public IsMemberOfRole()
            : base(UID)
        {
        }

        public override string CNT => "isMemberOfRole";

        public override string CN => EN;

        public override string DE => "Mitglied der Rolle";

        public override string EN => "Is Member of Role";

        public override string ES => "Es miembro de la función de solicitud";
    }


}
