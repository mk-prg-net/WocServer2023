using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.Tools
{
    public static class LongExt
    {
        /// <summary>
        /// mko, 10.2.2020
        /// 
        /// Verallgemeinerung des Session- Id Generators
        /// </summary>
        /// <returns></returns>
        public static long NewGuid()
        {
            // mko: Startwert für den Zufalls- Generator erzeugen
            Random rnd = new Random((int)DateTime.Now.Ticks);

            var b8 = new byte[8];
            rnd.NextBytes(b8);

            // mko, 15.11.2018
            // Long- Guid erzeugen durch auswürfeln der Stellen
            long sid = (b8[0]
                | ((long)b8[1] << 8)
                | ((long)b8[2] << 16)
                | ((long)b8[3] << 24)
                | ((long)b8[4] << 32)
                | ((long)b8[5] << 40)
                | ((long)b8[6] << 48)
                | ((long)b8[7] << 56)) & 0xFFFFFFFFL;

            return sid;
        }
    }
}
