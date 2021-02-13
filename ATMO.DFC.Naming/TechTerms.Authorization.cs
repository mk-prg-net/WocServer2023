using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.Authorization
{
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
        public override string CN => EN;
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
        public override string CN => EN;
        public override string DE => "Ausführungsrecht";
        public override string EN => "Execution Right";
        public override string ES => "Derecho de ejecución";

    }



    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Granted
        : NamingBase
    {
        public const long UID = 0xC10583AD;

        public Granted()
            : base(UID)
        {
        }

        public override string CNT => "granted";
        public override string CN => EN;
        public override string DE => "erlaubt";
        public override string EN => "granted";
        public override string ES => "concedido";
        
    }

    /// <summary>
    /// mko, 18.6.2020
    /// </summary>
    public class Denied
        : NamingBase
    {
        public const long UID = 0x6871C6CB;

        public Denied()
            : base(UID)
        {
        }

        public override string CNT => "denied";
        public override string CN => EN;
        public override string DE => "verboten";
        public override string EN => "denied";
        public override string ES => "denegado";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class AccessDenied
        : NamingBase
    {
        public const long UID = 0xCDA15AE7;

        public AccessDenied()
            : base(UID)
        {
        }

        public override string CNT => "accessDenied";
        public override string CN => EN;
        public override string DE => "Zugriff verboten";
        public override string EN => "access denied";
        public override string ES => "acceso denegado";
    }

    /// <summary>
    /// mko, 4.8.2020
    /// </summary>
    public class ExecDenied
        : NamingBase
    {
        public const long UID = 0x505CC44F;

        public ExecDenied()
            : base(UID)
        {
        }

        public override string CNT => "execDenied";
        public override string CN => EN;
        public override string DE => "Ausführung verboten";
        public override string EN => "Execution prohibited";
        public override string ES => "Prohibida la ejecución";
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
        public override string CN => CNT;
        public override string DE => CNT;
        public override string EN => CNT;
        public override string ES => CNT;
    }

    

}
