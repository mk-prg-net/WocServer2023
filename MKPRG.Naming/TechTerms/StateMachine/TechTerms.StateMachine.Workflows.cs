using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.StateMachine.WorkFlows
{

    /// <summary>
    /// mko, 3.8.20
    /// </summary>
    public class Workflow
        : NamingBase
    {
        public const long UID = 0xAB5FC422;

        public Workflow()
            : base(UID)
        { }

        public override string CN => "工作流程";
        public override string CNT => "workflow";
        public override string DE => EN;
        public override string EN => "Workflow";
        public override string ES => EN;
    }


    /// <summary>
    /// mko, 4.9.20
    /// </summary>
    public class WorkflowList
        : NamingBase
    {
        public const long UID = 0xFD0E8482;

        public WorkflowList()
            : base(UID)
        { }

        public override string CN => "工作流程";
        public override string CNT => "workflows";
        public override string DE => "Liste der Workflows";
        public override string EN => "Workflows";
        public override string ES => "Workflows";
    }


    /// <summary>
    /// mko, 3.8.20
    /// </summary>
    public class Completed
        : NamingBase, Grammar.IFinishedActivity
    {
        public const long UID = 0x979B30F6;

        public static Completed I { get; } = new Completed();

        public Completed()
            : base(UID)
        { }

        public override string CN => "空白";
        public override string CNT => "completed";
        public override string DE => "abgelschlossen";
        public override string EN => CNT;
        public override string ES => "Completado";
    }
}
