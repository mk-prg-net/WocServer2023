using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 18.2.2020
    /// Basisklasse für alle Bennenungscontainer
    /// 
    /// mko, 27.2.2020
    /// Geändert auf long- GUID's (kompakter)
    /// </summary>
    public abstract class NamingBase
        : INaming
    {
        internal NamingBase(long uid)
        {
            ID = uid;
            _DocuTermId = CreateIDAsNameFor(ID);            
        }


        /// <summary>
        /// mko, 2.3.2020
        /// Erstellt für die UID eines Namens die DocuTermID
        /// </summary>
        /// <param name="UID"></param>
        /// <returns></returns>
        public static string CreateIDAsNameFor(long UID)
        {
            return $"DT_UID_{UID.ToString("X").ToUpper()}";
        }

        /// <summary>
        /// mko, 2.3.2020
        /// Prüft, ob der übergebene String die syntaktische Struktur einer DocuTermID hat.
        /// </summary>
        /// <param name="idAsName"></param>
        /// <returns></returns>
        public static bool IsIDAsName(string idAsName)
        {
            return System.Text.RegularExpressions.Regex.IsMatch(idAsName.Trim().ToUpper(), @"^(DT_UID_)[0123456789ABCDEF]+$");
        }

        /// <summary>
        /// mko, 28.2.2020
        /// Versucht, eine DocuTermID aus einem String einzulesen. Wenn das nicht gelingt,
        /// dann wird long.Min zurückgegeben.
        /// </summary>
        /// <param name="DocuTermID"></param>
        /// <returns></returns>
        public static long ParseUID(string DocuTermID)
        {
            var ret = long.MinValue;

            if(IsIDAsName(DocuTermID))
            {
                if(long.TryParse(DocuTermID.Substring(7), System.Globalization.NumberStyles.HexNumber, System.Threading.Thread.CurrentThread.CurrentCulture, out long ID))
                {
                    ret = ID;
                }
            }

            return ret;
        }

        //internal NamingBase(string guid)
        //    : this(long.Parse(guid, System.Globalization.NumberStyles.HexNumber)) { }


        string _DocuTermId;

        public long ID { get; }

        public string IDAsName => _DocuTermId;

        public abstract string CNT { get; }

        public abstract string DE { get; }

        public abstract string EN { get; }

        public abstract string ES { get; }

        public abstract string CN { get; }

        /// <summary>
        /// mko, 26.1.2021
        /// Als Standard wird ein geschütztes Leerraumzeichen ausgegeben.
        /// </summary>
        public virtual string Glyph => "&nbsp;";

        public string NameIn(Language lng)
        {
            switch (lng)
            {
                case Language.CNT:
                    return CNT;
                case Language.CN:
                    return CN;
                case Language.DE:
                    return DE;
                case Language.EN:
                    return EN;
                case Language.ES:
                    return ES;
                case Language.NID:
                    return ID.ToString();
                default:
                    return CNT;
            }
        }

    }
}
