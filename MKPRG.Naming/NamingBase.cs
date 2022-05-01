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
        public NamingBase(long uid)
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


        /// <summary>
        /// Naming- ID
        /// Diese entspricht im Woc- Kontext einer WocID
        /// </summary>
        public long ID { get; }

        /// <summary>
        /// Name/Meldung als regulärer Name (keine Lerraumzeichen) in *CamelBack* Notation
        /// </summary>
        public abstract string CNT { get; }

        /// <summary>
        /// Name/ Meldung in Deutsch
        /// </summary>
        public abstract string DE { get; }

        /// <summary>
        /// Name/Meldung in Englisch
        /// </summary>
        public abstract string EN { get; }

        /// <summary>
        /// Name/Meldung in Spanisch
        /// </summary>
        public abstract string ES { get; }

        /// <summary>
        /// Name/ Meldung in Chinesisch
        /// </summary>
        public abstract string CN { get; }

        /// <summary>
        /// mko, 26.1.2021
        /// Als Standard wird ein geschütztes Leerraumzeichen ausgegeben.
        /// </summary>
        public virtual string Glyph => "&nbsp;";

        /// <summary>
        /// WocId
        /// </summary>
        public long WocId => ID;

        /// <summary>
        /// Woc- Version. Bei jeder Änderung ist die um eins zu erhöhen.
        /// </summary>
        public int WocVersion { get; }

        /// <summary>
        /// Klassifizierung des Wocs. Hier entweder *TechTerm* oder *DocuEntity*
        /// </summary>
        public long WocTypeId { get; }

        /// <summary>
        /// Liste der Verweise auf andere Woc's. Die Verweise bilden letztendlich ein semantisches Netz.
        /// </summary>
        public IEnumerable<(long RefTypeId, long WocId)> WocRefs { get; }

        /// <summary>
        /// Referenz auf das Woc, welches den Ersteller beschreibt
        /// </summary>
        public long WocAuthorId { get; }

        /// <summary>
        /// Referenz auf das Woc, welches den Knoten definiert, auf dem Änderungen (= erstellen
        /// neuer Versionen) für das Woc erlaubt sind.
        /// Hier in der Regel DLL.
        /// </summary>
        public long WocNodeId { get; }

        /// <summary>
        /// Ausgabe des Namens/Meldung in der gewünschten Sprache
        /// </summary>
        /// <param name="lng"></param>
        /// <returns></returns>
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
