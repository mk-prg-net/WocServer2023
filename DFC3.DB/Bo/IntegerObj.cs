using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DFC3.DB.Bo
{
    /// <summary>
    /// mko, 16.7.2018
    /// Helper object for mapping cols with integer values 
    /// </summary>
    public class IntegerObj
    {
        public int Value {
            get => _v;
            set => _v = value;
        }

        int _v;
    }

    /// <summary>
    /// mko, 16.7.2018
    /// Helper object for mapping cols with long values 
    /// </summary>
    public class LongObj
    {
        public long Value { get; set; }
    }

    /// <summary>
    /// mko, 16.7.2018
    /// Helper object for mapping cols with decimal values (count(*) etc.)
    /// </summary>
    public class DecimalObj
    {
        public decimal Value { get; set; }
    }

    public class ProjectStationNo
    {
        public long ProjectNo { get; set; }

        public long StationNo { get; set; }
    }
}
