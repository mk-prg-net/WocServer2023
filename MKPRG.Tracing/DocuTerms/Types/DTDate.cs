using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 22.7.2021
    /// Daten direkt in den Eigenschaften Year, Month, Day gepseichert, und nicht mehr als 
    /// 
    /// mko, 6.8.2021
    /// Child
    /// </summary>
    public class DTDate
        : DocuEntity,
        IDate
    {
        public DTDate(int Year, int Month, int Day)
            : base(DocuEntityTypes.Date)
        {
            this.Year = Year;
            this.Month = Month;
            this.Day = Day;
        }

        public int Year { get; } = 1900;

        public int Month { get; } = 1;

        public int Day { get; } = 1;
    }
}
