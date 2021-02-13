using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFCSecurity
{
    /// <summary>
    /// mko, 24.4.2018
    /// Basic structure of customer business objects
    /// mko, 9.5.2018 
    /// Massive refactoring.
    /// mko, 11.5.2018
    /// Added property ActiveAsMemberOfcustomerGroup
    /// mko, 25.5.2018
    /// Now based upon contracts IPerson, ICustomerGroups
    /// </summary>
    public interface ICustomer : DFCObjects.Common.IPerson, DFCObjects.Common.ICustomerGroups
    {
        /// <summary>
        /// Name of Customer
        /// </summary>
        string Name { get; }

        /// <summary>
        /// List of all projects associated of all customer groups where customer is associated.
        /// </summary>
        IEnumerable<ATMO.DFC.Tree.IProject> AllCustomerProjects { get; }

        /// <summary>
        /// Lists all projects of a special customer group.
        /// </summary>
        /// <param name="GroupName"></param>
        /// <returns></returns>
        IEnumerable<ATMO.DFC.Tree.IProject> ProjectsOfCustomerGroup(DFCObjects.Common.ICustomerGroup Group);

        /// <summary>
        /// mko, 11.5.2018
        /// When logged in an internal user or customer, how is assigned to multiple customer groups, this property defines 
        /// the customer group, choosen by user during login. All BOM's of project associated with this customer group for user 
        /// are accesessible.
        /// </summary>
        DFCObjects.Common.ICustomerGroup ActiveAsMemberOfCustomerGroup
        {
            get;
            set;
        }
    }
}
