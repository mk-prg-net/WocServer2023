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
        public override string CN => "登入";
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
        public override string CN => "验证用户";
        public override string DE => "Identität des Benutzers feststellen";
        public override string EN => "authenticate User";
        public override string ES => "autentificar el usuario";
    }

    public class Authenticate
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xACC75C60;

        public Authenticate()
            : base(UID)
        {
        }

        public override string CNT => "authenticate";
        public override string CN => "验证用户";
        public override string DE => "authentifiziere";
        public override string EN => "authenticate";
        public override string ES => "autentificar";
    }

    public class HaveBeenAuthenticated
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x9EA7681D;

        public HaveBeenAuthenticated()
            : base(UID)
        {
        }

        public override string CNT => "haveBeenAuthenticated";
        public override string CN => "验证用户";
        public override string DE => "wurde authentifiziert";
        public override string EN => "have been authenticated";
        public override string ES => "sido autentificado";
    }

    public class HaveBeenNotAuthenticated
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0xA77A6499;

        public HaveBeenNotAuthenticated()
            : base(UID)
        {
        }

        public override string CNT => "haveBeenNotAuthenticated";
        public override string CN => "验证用户";
        public override string DE => "wurde nicht authentifiziert";
        public override string EN => "have been not authenticated";
        public override string ES => "no ha sido autentificado";
    }


    public class WillAuthenticate
        : NamingBase, Grammar.IFutureActivity
    {
        public const long UID = 0xDCED4A58;

        public WillAuthenticate()
            : base(UID)
        {
        }

        public override string CNT => "willAuthenticate";
        public override string CN => "将被认证";
        public override string DE => "wird authentifiziert";
        public override string EN => "will be authenticated";
        public override string ES => "está autentificado";
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
        public override string CN => "获取分配给用户的角色";
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
        public override string CN => "登出";
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
        public override string CN => "用户";
        public override string DE => "Benutzer";
        public override string EN => "User";
        public override string ES => "Usuario";

        public override string Glyph => Glyphs.Authentication.Name;
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
        public override string CN => "用户ID";
        public override string DE => EN;
        public override string EN => "User Id";
        public override string ES => EN;

        public override string Glyph => Glyphs.Authentication.ID;
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

        public override string CN => "角色";

        public override string DE => "Rolle";

        public override string EN => "role";

        public override string ES => "El papel de la aplicación";

        public override string Glyph => Glyphs.Authentication.Role;

    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class AdminRole
        : NamingBase
    {
        public const long UID = 0xB42ED9C1;

        public AdminRole()
            : base(UID)
        {
        }

        public override string CNT => "adminRole";

        public override string CN => "管理员角色";

        public override string DE => "Adminstrator-Rolle";

        public override string EN => "Adminstrator Role";

        public override string ES => "Función de administrador";

        public override string Glyph => Glyphs.Authentication.AdminRole;

    }

    /// <summary>
    /// mko, 14.4.2021
    /// </summary>
    public class MemberOfAdminRole
        : NamingBase
    {
        public const long UID = 0xE7312888;

        public MemberOfAdminRole()
            : base(UID)
        {
        }

        public override string CNT => "memberOfAdminRole";

        public override string CN => "管理角色的成员";

        public override string DE => "Mitglied der Administratoren- Rolle";

        public override string EN => "Member of the administrator role";

        public override string ES => "Miembro de la función de administrador";

        public override string Glyph => Glyphs.Authentication.User;

    }


    /// <summary>
    /// mko, 14.4.2021
    /// </summary>
    public class MembersOfAdminRole
        : NamingBase
    {
        public const long UID = 0xCA9D64ED;

        public MembersOfAdminRole()
            : base(UID)
        {
        }

        public override string CNT => "membersOfAdminRole";

        public override string CN => "管理员角色";

        public override string DE => "Mitglieder der Administratoren- Rolle";

        public override string EN => "Members of the administrator role";

        public override string ES => "Miembros de la función de administrador";

        public override string Glyph => Glyphs.Authentication.Members;

    }

    public class MemberOfNonAdminRole
    : NamingBase
    {
        public const long UID = 0x2748545E;

        public MemberOfNonAdminRole()
            : base(UID)
        {
        }

        public override string CNT => "memberOfNonAdminRole";

        public override string CN => "非管理员角色的成员";

        public override string DE => "Mitglied einer Nicht-Administrator-Rolle";

        public override string EN => "Memeber of a Non Administrator Role";

        public override string ES => "Miembro de una función no administrativa";

        public override string Glyph => Glyphs.Authentication.User;

    }


    /// <summary>
    /// mko, 14.4.2021
    /// </summary>
    public class MembersOfNonAdminRole
        : NamingBase
    {
        public const long UID = 0x45674485;

        public MembersOfNonAdminRole()
            : base(UID)
        {
        }

        public override string CNT => "membersOfNonAdminRole";

        public override string CN => "非管理员角色的成员";

        public override string DE => "Mitglieder einer Nicht-Administrator-Rolle";

        public override string EN => "Memebers of a Non Administrator Role";

        public override string ES => "Miembros de una función no administrativa";

        public override string Glyph => Glyphs.Authentication.Members;

    }


    /// <summary>
    /// mko, 14.4.2021
    /// </summary>
    public class MembersOfRole
        : NamingBase
    {
        public const long UID = 0x477E9A3B;

        public MembersOfRole()
            : base(UID)
        {
        }

        public override string CNT => "membersOfRole";

        public override string CN => "角色成员";

        public override string DE => "Mitglieder der Rolle";

        public override string EN => "Members of role";

        public override string ES => "Miembros de la función";

        public override string Glyph => Glyphs.Authentication.Members;

    }




    /// <summary>
    /// mko, 30.3.2021
    /// </summary>
    public class Roles
        : NamingBase
    {
        public const long UID = 0x9E5B4E71;

        public Roles()
            : base(UID)
        {
        }

        public override string CNT => "roles";

        public override string CN => "角色";

        public override string DE => "Rolle";

        public override string EN => "Roles";

        public override string ES => "Papeles";

        public override string Glyph => Glyphs.Authentication.Roles;

    }


    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class IsMemberOfRole
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x92804CAB;

        public IsMemberOfRole()
            : base(UID)
        {
        }

        public override string CNT => "isMemberOfRole";

        public override string CN => "是角色的成员";

        public override string DE => "ist Mitglied der Rolle";

        public override string EN => "is Member of Role";

        public override string ES => "es miembro de la función de solicitud";

        public override string Glyph => Glyphs.Sets.Operators.LeftIncludedInRight;
    }

    public class IsNotMemberOfRole
    : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x9CCA7020;

        public IsNotMemberOfRole()
            : base(UID)
        {
        }

        public override string CNT => "isNotMemberOfRole";

        public override string CN => "不是角色的成员";

        public override string DE => "ist kein Mitglied der Rolle";

        public override string EN => "is not Member of Role";

        public override string ES => "no es miembro de la función";

        public override string Glyph => Glyphs.Sets.Operators.LeftNotIncludedInRight;
    }

}
