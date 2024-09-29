using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.TechTerms.ProjectMgmt
{
    public class ProjectManagment
        : NamingBase
    {
        public const long UID = 0x7E14FB2A;

        public ProjectManagment()
            : base(UID)
        {
        }

        public override string CNT => "projMgmt";
        public override string CN => EN;
        public override string DE => "Projektmanagement";
        public override string EN => "Project Management";
        public override string ES => "Gestión de proyectos";
    }
}
