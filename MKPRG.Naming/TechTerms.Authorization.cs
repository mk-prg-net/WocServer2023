using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Authorization
{
    /// <summary>
    /// mko, 5.3.2021
    /// Check access permissions
    /// </summary>
    public class CheckAccessPermissions
        : NamingBase
    {
        public const long UID = 0xADA85482;

        public CheckAccessPermissions()
            : base(UID)
        {
        }

        public override string CNT => "checkAccessPermissions";
        public override string CN => "检查访问权限";
        public override string DE => "Zugriffsberechtigungen prüfen";
        public override string EN => "Check access permissions";
        public override string ES => "Comprobar los permisos de acceso";

    }


    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class AccessRight
        : NamingBase
    {
        public const long UID = 0x8E176B0A;

        public AccessRight()
            : base(UID)
        {
        }

        public override string CNT => "accessRight";
        public override string CN => "访问权";
        public override string DE => "Zugriffsrecht";
        public override string EN => "Access right";
        public override string ES => "Derecho de acceso";

    }

    /// <summary>
    /// mko, 13.7.2020
    /// </summary>
    public class ExecutionRight
        : NamingBase
    {
        public const long UID = 0x8FCD57C7;

        public ExecutionRight()
            : base(UID)
        {
        }

        public override string CNT => "executionRight";
        public override string CN => "执行权";
        public override string DE => "Ausführungsrecht";
        public override string EN => "Execution Right";
        public override string ES => "Derecho de ejecución";

    }



    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Granted
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xC10583AD;

        public Granted()
            : base(UID)
        {
        }

        public override string CNT => "granted";
        public override string CN => "授予";
        public override string DE => "erlaubt";
        public override string EN => "granted";
        public override string ES => "concedido";

        public override string Glyph => Glyphs.Authorization.Granted;
    }

    /// <summary>
    /// mko, 18.5.2021
    /// </summary>
    public class Allowed
    : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xF8C6E26F;

        public Allowed()
            : base(UID)
        {
        }

        public override string CNT => "allowed";
        public override string CN => "允许的";
        public override string DE => "erlaubt";
        public override string EN => "allowed";
        public override string ES => "permitido";

        public override string Glyph => Glyphs.Authorization.Granted;
    }

    public class NotAllowed
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x9779052B;

        public NotAllowed()
            : base(UID)
        {
        }

        public override string CNT => "notAllowed";
        public override string CN => "不允许";
        public override string DE => "nicht erlaubt";
        public override string EN => "not allowed";
        public override string ES => "no se permite";

        public override string Glyph => Glyphs.Authorization.Forbidden;
    }





    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Denied
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x6871C6CB;

        public static Denied I { get; } = new Denied();

        public Denied()
            : base(UID)
        {
        }

        public override string CNT => "denied";
        public override string CN => "驳回";
        public override string DE => "verboten";
        public override string EN => "denied";
        public override string ES => "denegado";

        public override string Glyph => Glyphs.Authorization.Forbidden;

    }

    public class Release
        : NamingBase, Grammar.IInProgressActivity
    {

        public const long UID = 0x3DC760A9;

        public Release()
            : base(UID)
        {
        }

        public override string CNT => "release";
        public override string CN => "释放";
        public override string DE => "gebe frei";
        public override string EN => "release";
        public override string ES => "desalojar";

        public override string Glyph => Glyphs.Authorization.Granted;
    }

    /// <summary>
    /// mko, 9.4.2021
    /// </summary>
    public class WasReleased
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x26A41F76;

        public WasReleased()
            : base(UID)
        {
        }

        public override string CNT => "wasReleased";
        public override string CN => "被释放";
        public override string DE => "wurde freigegeben";
        public override string EN => "was released";
        public override string ES => "fue lanzado";

        public override string Glyph => Glyphs.Authorization.Granted;


    }

    /// <summary>
    /// mko, 9.4.2021
    /// </summary>
    public class WasNotReleased
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x242A13F2;

        public WasNotReleased()
            : base(UID)
        {
        }

        public override string CNT => "wasNotReleased";
        public override string CN => "未发布";
        public override string DE => "wurde nicht freigegeben";
        public override string EN => "was not released";
        public override string ES => "no fue liberado";

        public override string Glyph => Glyphs.Authorization.Forbidden;

    }

    /// <summary>
    /// mko, 9.4.2021
    /// </summary>
    public class WillBeReleased
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x49E0D7D6;

        public WillBeReleased()
            : base(UID)
        {
        }

        public override string CNT => "willBeReleased";
        public override string CN => "被释放";
        public override string DE => "wird freigegeben";
        public override string EN => "is released";
        public override string ES => "se libera";

        public override string Glyph => Glyphs.Authorization.Granted;


    }

    /// <summary>
    /// mko, 9.4.2021
    /// </summary>
    public class WillNotBeReleased
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x9D9015B6;

        public WillNotBeReleased()
            : base(UID)
        {
        }

        public override string CNT => "willNotBeReleased";
        public override string CN => "不予公布";
        public override string DE => "wird nicht freigegeben";
        public override string EN => "will not be released";
        public override string ES => "no será liberado";

        public override string Glyph => Glyphs.Authorization.Forbidden;

    }



    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class AccessDenied
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0xCDA15AE7;

        public AccessDenied()
            : base(UID)
        {
        }

        public override string CNT => "accessDenied";
        public override string CN => "拒绝访问";
        public override string DE => "Zugriff verboten";
        public override string EN => "access denied";
        public override string ES => "acceso denegado";

        public override string Glyph => Glyphs.Authorization.Access + Glyphs.Authorization.Forbidden;
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class ExecDenied
        : NamingBase, Grammar.IInProgressActivity
    {
        public const long UID = 0x505CC44F;

        public ExecDenied()
            : base(UID)
        {
        }

        public override string CNT => "execDenied";
        public override string CN => "禁止执行";
        public override string DE => "Ausführung verboten";
        public override string EN => "Execution prohibited";
        public override string ES => "Prohibida la ejecución";

        public override string Glyph => Glyphs.Runtime.Execute + Glyphs.Authorization.Forbidden;
    }





    /// <summary>
    /// Access Control List
    /// </summary>
    public class ACL
        : NamingBase
    {
        public const long UID = 0x253000FD;

        public ACL()
            : base(UID)
        {
        }

        public override string CNT => "acl";
        public override string CN => "访问控制列表";
        public override string DE => "Zugriffssteuereungsliste";
        public override string EN => "Access Control List";
        public override string ES => "Lista de control de acceso";

        public override string Glyph => Glyphs.Sets.List + "§";
    }




}
