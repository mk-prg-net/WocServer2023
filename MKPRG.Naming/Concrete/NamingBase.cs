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
        : IGetNameSpaceOfNamingContainer,
        ILangCN, ILangDE, ILangEN, ILangES//,
        //Woc.Types.BaseTypes.Structure.IPlainText
    {
        public NamingBase(long uid)
        {
            ID = uid;            
        }

        /// <summary>
        /// mko, 25.3.2021
        /// Konstruktor für die Anlage von Naming- Containern als PlainText- Wocs
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
        /// Naming- ID, Synonym
        /// </summary>
        public long NID => ID;

        /// <summary>
        /// Name/Meldung als regulärer Name (keine Lerraumzeichen) in *CamelBack* Notation
        /// </summary>
        public abstract string CNT { get; }

        /// <summary>
        /// Name/ Meldung in Deutsch
        /// </summary>
        public virtual string DE { get => EN; }

        /// <summary>
        /// Name/Meldung in Englisch
        /// </summary>
        public abstract string EN { get; }

        /// <summary>
        /// Name/Meldung in Spanisch
        /// </summary>
        public virtual string ES { get => EN; }

        /// <summary>
        /// Name/ Meldung in Chinesisch
        /// </summary>
        public virtual string CN
        {
            get => EN;
        }

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

        // Implementierung von IGetNamespaceOfNamingContainer

        T _InitializeIfRequiredAndGet<T>(Func<T> getRetVal)
        {
            // Initialisierung aller Felder in einem Schritt aus Effizienzgründen
            if (!_IsGetNameSpaceOfNamingContainerInitialized)
            {
                var myType = GetType();
                _MyNamingContainerName = myType.Name;
                _MyNamespace = myType.Namespace;
                _MyNamespaceLevel = _MyNamespace.Count(c => c == '.');
                _IsGetNameSpaceOfNamingContainerInitialized = true;
            }

            return getRetVal();
        }

        bool _IsGetNameSpaceOfNamingContainerInitialized = false;
        string _MyNamingContainerName;
        string _MyNamespace;
        int _MyNamespaceLevel = -1;

        public string MyNamingContainerName => _InitializeIfRequiredAndGet(() => _MyNamingContainerName);

        public string MyNamespace => _InitializeIfRequiredAndGet(() => _MyNamespace);

        public int MyNameSpaceLevel => _InitializeIfRequiredAndGet(() => _MyNamespaceLevel);
        

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
