using System;

namespace MKPRG.GUID64
{
    /// <summary>
    /// mko, 23.4.2023
    /// GUID 64 Generator als explizite Klasse definiert
    /// </summary>
    public static class GUID64Generator
    {            
        public static long NewGUID64()
        {
            var b8 = new byte[8];
            ThreadSafeRandom.NextBytes(b8);
            //rnd.NextBytes(b8);

            // mko, 15.11.2018
            // Long- Guid erzeugen durch auswürfeln der Stellen
            long guid64 = (b8[0]
                | ((long)b8[1] << 8)
                | ((long)b8[2] << 16)
                | ((long)b8[3] << 24)
                | ((long)b8[4] << 32)
                | ((long)b8[5] << 40)
                | ((long)b8[6] << 48)
                | ((long)b8[7] << 56)) & 0x7FFFFFFFFFFFFFFFL;

            return guid64;
        }
    }
}
