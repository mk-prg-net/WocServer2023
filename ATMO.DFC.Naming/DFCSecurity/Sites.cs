using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFCSecurity
{
    /// <summary>
    /// mko, 20.3.2018
    /// Enumaration of all Atmo- sites (plants/factories)
    /// 
    /// mko, 11.6.2018
    /// Renamed from Plant to Site
    /// 
    /// mko, 20.11.2018
    /// Moved from DFCSecurity to here
    /// 
    /// mko, 7.8.2020
    /// Von DFCSecurity.Defs hierher verschoben, damit in TechTems.ATMO.Sites diese Enums in die Naming- Container 
    /// aufgenommen werden können
    /// </summary>
    public enum Site
    {
        /// <summary>        
        /// E.g. custumers are assigned to Site.none.
        /// Useful in situations, wehre customer and employees are processed in the same manner. 
        /// </summary>
        none = 0,

        /// <summary>
        ///  If a permission is valid for all plants, plant will be set to all.
        /// </summary>
        all = 100000,

        /// <summary>
        /// Stuttgart Feuerbach, Germany
        /// </summary>
        ATMO_1 = 1,

        /// <summary>
        /// Madrid
        /// </summary>
        ATMO_2 = 2,

        /// <summary>
        /// Suzhou, China
        /// </summary>
        ATMO_3 = 3,

        /// <summary>
        /// Charlstone, USA
        /// </summary>
        ATMO_4 = 4,

        /// <summary>
        /// Changsha, China
        /// </summary>
        ATMO_5 = 5,

        /// <summary>
        /// Banglagore, India
        /// </summary>
        ATMO_6 = 6,

        /// <summary>
        /// Toluca, Mexico
        /// </summary>
        ATMO_7 = 7,

        /// <summary>
        /// Bursa, Turkey
        /// </summary>
        ATMO_8 = 8,

        /// <summary>
        /// Möhwald
        /// </summary>
        MH = 9,


        /// <summary>
        /// mko, 6.8.2018
        /// TEF FE
        /// </summary>
        TEF_Fep = 1000,
    }
}
