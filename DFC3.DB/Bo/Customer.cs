using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.QueryBuilder;
using DFC.UpDowngrades;
using DFCObjects.Common;


namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 25.10.2018
    /// </summary>
    public class Customer : IUserPersonalData
    {
        public string CustGroupId { get; set; }

        public string Username { get; set; }

        public string RASUserId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EMail { get; set; }

        public string Phone { get; set; }

        public string Department { get; set; }

        public string CostCenter { get; set; }

        public DateTime LastLogin { get; set; }

        public int RequestCount { get; set; }

        public IVersionDescriptor DFC2FixAssignedVersion { get; set; }

        public IVersionDescriptor DFC2CurrentUsedVersion { get; set; }
    }
}
