using MKPRG.Naming.TechTerms.Sets;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 4.2.2021
    /// Sammlung bewährter Methoden für den vereinfachten Zugriff auf 
    /// </summary>
    public class NamingHelper
        : INamingHelper
    {
        public NamingHelper(IReadOnlyDictionary<long, INaming> NC, Language lng = Language.CNT)
        {
            this.NC = NC;
            Language = lng;
        }

        /// <summary>
        /// Definiert die Ausgabesparache
        /// </summary>
        public Language Language
        {
            get; set;
        }

        IReadOnlyDictionary<long, INaming> NC;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        public string _(long NID)
        {
            var str = $"🗲 NID({NID}) ⊷ NamingContainer: {NC[TechTerms.Search.NotFound.UID].NameIn(Language)} 🗲";

            if (NC.TryGetValue(NID, out INaming nc))
            {
                str = nc.NameIn(Language);
            }

            return str;
        }

        public string _(long NID, Language lng)
        {
            var str = $"🗲 NID({NID}) ⊷ NamingContainer: {NC[TechTerms.Search.NotFound.UID].NameIn(Language)} 🗲";

            if (NC.TryGetValue(NID, out INaming nc))
            {
                str = nc.NameIn(lng);
            }

            return str;
        }


        /// <summary>
        /// mko, 10.5.2021 
        /// Ruft einen Namenscontainer als Pluralform ab.
        /// Falls der Namenscontainer keine Pluralform ist, wird ein Hilfscontainer 
        /// in PluralForm erstellt, der die Daten des angeforderten Containers bereitstellt
        /// und den Fehler beschreibt
        /// </summary>
        /// <param name="NIDofPluralForm"></param>
        /// <returns></returns>
        public IPluralForm pF(long NIDPluralForm)
        => GetPhraseFromNCSave<IPluralForm, PluralFormConversionError>
            (
                NIDPluralForm,
                (errType, nid, cnt, cn, de, en, es)
                    => new PluralFormConversionError(errType, nid, cnt, cn, de, en, es)
            );

        /// <summary>
        /// mko, 10.5.2021
        /// Allgemeine Implementierung einer Typsicheren und robusten Zugriffsfunktion auf NamingContainer,
        /// die Satzglieder darstellen.
        /// </summary>
        /// <typeparam name="I"></typeparam>
        /// <typeparam name="TErr"></typeparam>
        /// <param name="NIDFinishedActivity"></param>
        /// <param name="CreateConversationError"></param>
        /// <returns></returns>
        private I GetPhraseFromNCSave<I, TErr>(
            long NIDFinishedActivity,
            Func<InterfaceConversionErrorTypes, long, string, string, string, string, string, TErr> CreateConversationError)
            where I : INaming
            where TErr : I
        {
            I ret = default(I);

            if (!NC.ContainsKey(NIDFinishedActivity))
            {
                var nullVal = NC[TechTerms.Sets.NullValue.UID];

                ret = CreateConversationError(
                        InterfaceConversionErrorTypes.NIDisUndefined,
                        nullVal.ID,
                        nullVal.CNT,
                        nullVal is ILangCN cnLng ? cnLng.CN : nullVal.CNT,
                        nullVal is ILangDE deLng ? deLng.DE : nullVal.CNT,
                        nullVal is ILangEN enLng ? enLng.EN : nullVal.CNT,
                        nullVal is ILangES esLng ? esLng.ES : nullVal.CNT
                    );
            }
            else if (NC[NIDFinishedActivity] is I phrase)
            {
                ret = phrase;
            }
            else
            {
                var nc = NC[NIDFinishedActivity];

                ret = CreateConversationError(
                        InterfaceConversionErrorTypes.RequestedInterfaceIsNotSupportetdByNC,
                        nc.ID,
                        nc.CNT,
                        nc is ILangCN cnLng ? cnLng.CN : nc.CNT,
                        nc is ILangDE deLng ? deLng.DE : nc.CNT,
                        nc is ILangEN enLng ? enLng.EN : nc.CNT,
                        nc is ILangES esLng ? esLng.ES : nc.CNT
                    );
            }
            return ret;
        }



        /// <summary>
        /// mko, 7.4.2021
        /// Streng typisierter Zugriff auf eine Präposition
        /// </summary>
        /// <param name="NIDPreposition"></param>
        /// <returns></returns>
        public TechTerms.Grammar.Prepositions.IPre pp(long NIDPreposition)
        => GetPhraseFromNCSave<TechTerms.Grammar.Prepositions.IPre, TechTerms.Grammar.Prepositions.PreConversationError>
            (
                NIDPreposition,
                (errType, nid, cnt, cn, de, en, es)
                => new TechTerms.Grammar.Prepositions.PreConversationError(errType, nid, cnt, cn, de, en, es)
            );


        /// <summary>
        /// mko, 7.4.2021
        /// Streng typisierter Zugriff auf eine abgeschlossene Handlung
        /// </summary>
        /// <param name="NIDFinishedActivity"></param>
        /// <returns></returns>
        public TechTerms.Grammar.IFinishedActivity fA(long NIDFinishedActivity)
        => GetPhraseFromNCSave<TechTerms.Grammar.IFinishedActivity, TechTerms.Grammar.FinishedActivityConversationError>
            (
                NIDFinishedActivity,
                (errType, nid, cnt, cn, de, en, es)
                => new TechTerms.Grammar.FinishedActivityConversationError(errType, nid, cnt, cn, de, en, es)
            );


        /// <summary>
        /// mko, 7.4.2021
        /// Streng typisierter Zugriff auf eine laufende Handlung
        /// </summary>
        /// <param name="NIDInProgressActivity"></param>
        /// <returns></returns>
        public TechTerms.Grammar.IInProgressActivity pA(long NIDInProgressActivity)
         => GetPhraseFromNCSave<TechTerms.Grammar.IInProgressActivity, TechTerms.Grammar.InProgressActivityConversationError>
             (
                 NIDInProgressActivity,
                 (errType, nid, cnt, cn, de, en, es)
                 => new TechTerms.Grammar.InProgressActivityConversationError(errType, nid, cnt, cn, de, en, es)
             );

        /// <summary>
        /// mko, 1.7.2021
        /// </summary>
        /// <param name="NIDFutureActivity"></param>
        /// <returns></returns>
        public TechTerms.Grammar.IFutureActivity futurA(long NIDFutureActivity)
        => GetPhraseFromNCSave<TechTerms.Grammar.IFutureActivity, TechTerms.Grammar.FutureActivityConversationError>
            (
                NIDFutureActivity,
                (errType, nid, cnt, cn, de, en, es)
                => new TechTerms.Grammar.FutureActivityConversationError(errType, nid, cnt, cn, de, en, es)
            );

        /// <summary>
        /// mko, 7.4.2021
        /// Streng typisierter Zugriff auf eine modale Verbform (kann ..., kann nicht ...).
        /// 
        /// mko, 10.5.2021
        /// Falls der addressierte Namingcontainer nicht existiert oder keine IModalPhrase Schnittstelle
        /// implementiert, wird trotzdem eine IModalPhrase Objket zurückgegeben mit passenden Fehlerbeschreibungen.
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        public TechTerms.Grammar.IModalPhrase mP(long NIDModalPhrase)
        => GetPhraseFromNCSave<TechTerms.Grammar.IModalPhrase, TechTerms.Grammar.ModalPhraseConversionError>
            (
                NIDModalPhrase,
                (errType, nid, cnt, cn, de, en, es)
                => new TechTerms.Grammar.ModalPhraseConversionError(errType, nid, cnt, cn, de, en, es)
            );


        public TechTerms.Grammar.Adverbs.IAdverb av(long NIDAdverb)
        => GetPhraseFromNCSave<TechTerms.Grammar.Adverbs.IAdverb, TechTerms.Grammar.Adverbs.AdverbConversationError>
            (
                NIDAdverb,
                (errType, nid, cnt, cn, de, en, es)
                => new TechTerms.Grammar.Adverbs.AdverbConversationError(errType, nid, cnt, cn, de, en, es)
            );

        public TechTerms.Grammar.Adjectives.IAdjective aj(long NIDAdjective)
            => GetPhraseFromNCSave<TechTerms.Grammar.Adjectives.IAdjective, TechTerms.Grammar.Adjectives.AdjectiveConversationError>
                (
                    NIDAdjective,
                    (errType, nid, cnt, cn, de, en, es)
                    => new TechTerms.Grammar.Adjectives.AdjectiveConversationError(errType, nid, cnt, cn, de, en, es)
                );


        /// <summary>
        /// Liefert einen Glyphen als UTF16- Zeichen
        /// 
        /// mko, 21.9.2021
        /// Gegen undefinierte NID's gesichert.
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        public string glyph(long NID)
        {
            var g = "🗲";
            if (NC.TryGetValue(NID, out INaming nc) && nc is IGlyph gy)
            {
                g = Glyphs.toStr(gy.Glyph);
            }
            return g;
        }

        /// <summary>
        /// Liefert einen Glyphen als HTML- Enitität
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        public string htmlGlyph(long NID)
        {
            var entity = "🗲";
            if (NC.TryGetValue(NID, out INaming nc) && nc is IGlyph gy)
            {
                entity = gy.Glyph;
            }
            return entity;
        }
    }
}
