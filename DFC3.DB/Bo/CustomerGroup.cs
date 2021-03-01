using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 9.11.2018
    /// dza_admin.bosch106usr_cust_grp
    /// Datafields of CustomerGroup record. 
    /// </summary>
    public class CustomerGroup 
    {
        public long ID { get; set; }
        public string CustGroupId { get; set; }
        public string CustGroupDescription { get; set; }
        public string custGroupAdmins { get; set; }
    }
}
