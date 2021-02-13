using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.HTML
{
    partial class HTMLDocument
    {

        /// <summary>
        /// mko, 4.1.2021
        /// Datumsstempel
        /// </summary>
        public HTMLDocument time
        {
            get
            {
                t("time");
                return this;
            }
        }

        /// <summary>
        /// mko, 4.1.2021
        /// </summary>
        /// <param name="Year"></param>
        /// <param name="Month"></param>
        /// <param name="Day"></param>
        /// <returns></returns>
        public HTMLDocument time_with_dateAttrib(int Year, int Month, int Day)
        {
            tWithAttribs("time", $"date='{Year.ToString("D4")}-{Month.ToString("D2")}-{Day.ToString("D2")}'");
            return this;
        }

        public HTMLDocument time_with_timeAttrib(int Hour, int Minutes, int Seconds)
        {
            tWithAttribs("time", $"date='{Hour.ToString("D2")}:{Minutes.ToString("D2")}:{Seconds.ToString("D2")}'");
            return this;
        }
    }
}
