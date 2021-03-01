using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 12.6.2018
    /// 
    /// mko, 24.7.2018
    /// Extended with properties FirstName, LastName, Title, Language
    /// </summary>
    public class XUser
    {
        public long ID { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Title { get; set; }
        public long Language { get; set; }

    }
}
