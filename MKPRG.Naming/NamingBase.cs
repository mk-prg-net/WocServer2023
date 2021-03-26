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
        : Woc.Types.BaseTypes.Structure.IPlainText
    {
        internal NamingBase(long uid)
        {
            ID = uid;            
        }

        /// <summary>
        /// mko, 25.3.2021
        /// Konstruktor für die Anlage von Naming- Kontainern als PlainText- Wocs
        /// </summary>
        /// <param name="uid"></param>
        /// <param name="WocTypeId"></param>
        /// <param name="WocNodeId"></param>
        /// <param name="WocRefs"></param>
        internal NamingBase(
            long uid, 
            int WocVersion,
            long WocTypeId, 
            long WocAuthorId,
            long WocNodeId, 
            params (long RefTypeId, long WocId)[] WocRefs)
        {
            ID = uid;

            this.WocVersion = WocVersion;

            this.WocTypeId = WocTypeId;

            this.WocAuthorId = WocAuthorId;
            this.WocNodeId = WocNodeId;

            this.WocRefs = WocRefs;
        }



        public long ID { get; }

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

        public long WocId => ID;

        public int WocVersion { get; }

        public long WocTypeId { get; }

        public IEnumerable<(long RefTypeId, long WocId)> WocRefs { get; }

        public long WocAuthorId { get; }

        public long WocNodeId { get; }

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
