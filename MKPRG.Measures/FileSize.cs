using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Measures.Filesystem
{
    /// <summary>
    /// mko, 18.11.2019
    /// Formatierte Darstellung von Dateigrößen etc.
    /// </summary>
    public class FileSize
    {
        public enum Magnitude : long
        {
            Byte = 1L,
            Kilobyte = 1024L,
            Megabyte = 1024L * 1024L,
            Gigabyte = 1024L * 1024L * 1024L,
            Terrabyte = 1024L * 1024L * 1024L * 1024L,
            Petabyte = 1024L * 1024L * 1024L * 1024L * 1024L
        }

        /// <summary>
        /// mko, 19.11.2019
        /// Definiert Beziehungen zwischen den Größenordnungen von Speichermaßen.
        /// MagLimit ::= Größenordnung eines Speichermaßes
        /// </summary>
        (Magnitude sizeOfUnitInByte, string UnitName, Magnitude MagLimit, int Accuracy)[] MagList =
        {            
            (Magnitude.Byte,  "Byte", Magnitude.Kilobyte, 0),
            (Magnitude.Kilobyte, "KB", Magnitude.Megabyte, 3),
            (Magnitude.Megabyte, "MB", Magnitude.Gigabyte, 1),
            (Magnitude.Gigabyte, "GB", Magnitude.Terrabyte, 3),
            (Magnitude.Terrabyte, "TB", Magnitude.Petabyte, 3)
        };

        (double size, Magnitude SizeMagnitude, string Unit, string sizeFmtString) fmt(long sizeOfFileInByte, Magnitude sizeOfUnitInByte, int Accuracy, string UnitName)
                => (((double)(sizeOfFileInByte) / (long)sizeOfUnitInByte),
                    sizeOfUnitInByte,
                    UnitName,
                    ((double)(sizeOfFileInByte) / (long)sizeOfUnitInByte).ToString($"N{Accuracy}") + $" {UnitName}");

        /// <summary>
        /// Automatische Formatierung von Speichermaßen.
        /// </summary>
        /// <param name="sizeOfFileInByte"></param>
        /// <returns></returns>
        public (double size, Magnitude SizeMagnitude, string Unit, string sizeFmtString) SizeAutoFmt(long sizeOfFileInByte)
        {            

            foreach (var pair in MagList)
            {
                if(sizeOfFileInByte < (long)pair.MagLimit)
                {
                    return fmt(sizeOfFileInByte, pair.sizeOfUnitInByte, pair.Accuracy, pair.UnitName);
                }                
            }
            return fmt(sizeOfFileInByte, Magnitude.Petabyte, 3, "PB");
        }

        /// <summary>
        /// Manuelle Formatierung von Speichermaßen
        /// </summary>
        /// <param name="sizeOfFileInByte"></param>
        /// <param name="magnitude"></param>
        /// <returns></returns>
        public (double size, Magnitude SizeMagnitude, string Unit, string sizeFmtString) SizeFmt(long sizeOfFileInByte, Magnitude magnitude)
        {
            var pair = MagList.First(r => r.sizeOfUnitInByte == magnitude);
            return fmt(sizeOfFileInByte, magnitude, pair.Accuracy, pair.UnitName);
        }

    }
}
