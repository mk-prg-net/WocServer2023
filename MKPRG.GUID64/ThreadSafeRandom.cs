#nullable enable
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;

namespace MKPRG.GUID64
{
    /// <summary>
    /// mko, 23.4.2023
    /// Multithread- fester Zufallsgenerator.
    /// Details siehe hier: https://andrewlock.net/building-a-thread-safe-random-implementation-for-dotnet-framework/
    /// </summary>
    public static class ThreadSafeRandom
    {
        [ThreadStatic]
        private static Random? _local;
        private static readonly Random Global = new Random(); // 👈 Global instance used to generate seeds

        private static Random Instance
        {
            get
            {
                if (_local is null)
                {
                    int seed;
                    lock (Global) // 👈 Ensure no concurrent access to Global
                    {
                        seed = Global.Next();
                    }

                    _local = new Random(seed); // 👈 Create [ThreadStatic] instance with specific seed
                }

                return _local;
            }
        }

        public static int Next() => Instance.Next();


        /// <summary>
        /// mko, 23.4.2023
        /// </summary>
        /// <param name="bytes"></param>
        public static void NextBytes(byte[] bytes)
        {
            Instance.NextBytes(bytes);
        }

    }
}
