using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.Tracing.DocuTerms
{

    /// <summary>
    /// mko, 6.4.2021
    /// Docuterms für grundlegende Satzstrukturen.
    /// Sinn und Zweck ist die Erkennbarkeit der Strukturen durch Formatter. Diese können dann für die jeweilige Ausgabesprache 
    /// valide Sätze erzeugen.
    /// </summary>
    public partial class ComposerSubTrees
    {
        // **Objekte**

        /// <summary>
        /// mko, 04.2021
        /// Beschreibt ein einzelnes Objekt
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static IInstance DefObject(
                this IComposer pnL,
                long Name
            )
        => pnL.i(TT.Grammar.Object.UID,
                    pnL.p_NID(TTD.MetaData.Name.UID, Name));

        /// <summary>
        /// mko, 17.5.2021
        /// Durch ein Adjektiv näher spezifiziertes Objekt
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="Adjective"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static IInstance DefObject(
            this IComposer pnL,
            TT.Grammar.Adjectives.IAdjective Adjective,
            long Name
        )
        => pnL.i(TT.Grammar.Object.UID,
                    pnL.p_NID(TT.Grammar.Adjectives.Adjective.UID, Adjective.ID),
                    pnL.p_NID(TTD.MetaData.Name.UID, Name));

        /// <summary>
        /// mko, 6.5.2021
        /// Beschreibt ein einzelnes Objekt
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static IInstance DefObject(
            this IComposer pnL,
            string Name
            )
        => pnL.i(TT.Grammar.Object.UID,
            pnL.p(TTD.MetaData.Name.UID, Name));

        /// <summary>
        /// mko, 17.5.2021
        /// Durch ein Adjektiv näher spezifiziertes Objekt
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="Adjective"></param>
        /// <param name="Name"></param>
        /// <returns></returns>
        public static IInstance DefObject(
            this IComposer pnL,
            TT.Grammar.Adjectives.IAdjective Adjective,
            string Name
        )
        => pnL.i(TT.Grammar.Object.UID,
                    pnL.p_NID(TT.Grammar.Adjectives.Adjective.UID, Adjective.ID),
                    pnL.p(TTD.MetaData.Name.UID, Name));


        /// <summary>
        /// mko, 4.2021
        /// Beschreibt ein einzelnes Objekt, und verweist auf den Oberbegriff.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="Name"></param>
        /// <param name="ObjectClassUID"></param>
        /// <returns></returns>
        public static IInstance DefObject(
                this IComposer pnL,
                long Name,
                long ObjectClassUID // Oberbegriff, Klasse, zu der das Objekt gehört wie User, ACL                 
            )
        => pnL.i(TT.Grammar.Object.UID,
                    pnL.p_NID(TTD.MetaData.Name.UID, Name),
                    pnL.p_NID(TT.Abstraction.Abstraction.UID, ObjectClassUID));

        /// <summary>
        /// mko, 4.2021
        /// Beschreibt ein einzelnes Objekt und verweist auf den Oberbegriff.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="Name"></param>
        /// <param name="ObjectClassUID"></param>
        /// <returns></returns>
        public static IInstance DefObject(
            this IComposer pnL,
            string Name,
            long ObjectClassUID // Z.B. User, ACL etc.            
            )
        => pnL.i(TT.Grammar.Object.UID,
                    pnL.p(TTD.MetaData.Name.UID, Name),
                    pnL.p_NID(TT.Abstraction.Abstraction.UID, ObjectClassUID));





        // **Prepositionale Objekte**

        /// <summary>
        /// mko, 4.2021
        /// Beschreibt ein Objekt und definiert den örtlichen Bezug zum Betrachter durch eine Präposition.
        /// Das Objekt selber muss bereits als DocuTerm Beschreibung vorliegen, und bestenfalls durch eine der 
        /// Generatorfunktionen wie pnL.DefObject(...) erstellt worden sein.
        /// 
        /// mko, 29.9.2021
        /// Fall, Struktur enthält nicht die geforderte Namenseigenschaft von ArgumentException Ausnahme
        /// befreit: Jestzt wird das Objekt um die geforderte Namenseigenschaft erweitert.
        /// So wird sichergestellt, das DokuTerms in jeden Fall erstellt werden, und die Erstellung 
        /// nicht durch Ausnahmen abgebrochen wird.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="preposition"></param>
        /// <param name="Object"></param>
        /// <returns></returns>
        public static IInstance DefPrepositionalObject(
                this IComposer pnL,
                TT.Grammar.Prepositions.IPre preposition,
                IInstance Object
            )
        {
            var ret = pnL.DefObject(TT.Sets.NullValue.UID, TT.Sets.NullValue.UID);

            // Prüfen, ob das Objekt eine Namenseigenschaft hat (in der dritten Ebene. 
            // 1. Ebene: Objekt, 2. Ebene: Liste der instanzmember, 3. Ebene: Instanzmember
            if (!pnL.p(TTD.MetaData.Name.UID, pnL._v()).IsSubTreeOf(Object, true, 3))
            {
                //TraceHlp.ThrowArgEx(
                //        pnL.ReturnValidatePreconditionFailed(
                //            pnL.m(TT.Operators.Sets.Contains.UID,
                //                pnL.p_NID(TTD.Types.Property.UID, TTD.MetaData.Name.UID),
                //                pnL.ret(pnL.eFails()))));

                // Namenseigenschaft mit einem Default- Namen rekonstruieren

                var cNameIsUnknown = "NameIsUnknown";
                IInstance objWithAddedName;
                if (Object is IInstanceWithNameAsNid objNid)
                {
                    objWithAddedName = pnL.i(objNid.DocuTermNid.NamingId,
                            pnL.p(TTD.MetaData.Name.UID, cNameIsUnknown),
                            pnL.EmbedInstanceMembers(objNid.InstanceMembers)
                        );
                }
                else if(Object is InstanceWithNameAsString objStr) 
                {
                    objWithAddedName = pnL.i(objStr.DocuTermName,
                            pnL.p(TTD.MetaData.Name.UID,cNameIsUnknown),
                            pnL.EmbedInstanceMembers(objStr.InstanceMembers)
                        );
                }
                else
                {
                    objWithAddedName = pnL.i(cNameIsUnknown,
                            pnL.p(TTD.MetaData.Name.UID, cNameIsUnknown),
                            pnL.EmbedInstanceMembers(Object.InstanceMembers)
                        );
                }

                ret = pnL.i(TT.Grammar.Prepositions.PrepositionalObject.UID,
                    pnL.p_NID(TT.Grammar.Preposition.UID, preposition.ID),
                    pnL.p(TT.Grammar.Object.UID, objWithAddedName)
                );
            }
            else
            {

                ret = pnL.i(TT.Grammar.Prepositions.PrepositionalObject.UID,
                        pnL.p_NID(TT.Grammar.Preposition.UID, preposition.ID),
                        pnL.p(TT.Grammar.Object.UID, Object)
                    );
            }

            return ret;
        }


        /// <summary>
        /// mko, 6.5.2021
        /// Beschreibt eine Anzahl von Objekten
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="NamingContainerOfPluralForm"></param>
        /// <returns></returns>
        public static IInstance DefObjects(
            this IComposer pnL,
            IPluralForm NamingContainerInstanceOfPluralForm)
            => pnL.i(TT.Grammar.ObjectsInPluralForm.UID,
                    pnL.p_NID(TTD.MetaData.Name.UID, NamingContainerInstanceOfPluralForm.ID));

        /// <summary>
        /// mko, 7.5.2021
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="CountOfObjects"></param>
        /// <param name="NamingContainerInstanceOfPluralForm"></param>
        /// <returns></returns>
        public static IInstance DefObjects(
            this IComposer pnL,
            int CountOfObjects,
            IPluralForm NamingContainerInstanceOfPluralForm)
            => pnL.i(TT.Grammar.ObjectsInPluralForm.UID,
                    pnL.p(TT.Metrology.Counter.UID, CountOfObjects),
                    pnL.p_NID(TTD.MetaData.Name.UID, NamingContainerInstanceOfPluralForm.ID));


        public static IInstance DefObjects(
            this IComposer pnL,
            TT.Grammar.Prepositions.IPre preposition,
            IPluralForm NamingContainerOfPluralForm)
            => pnL.i(TT.Grammar.ObjectsInPluralForm.UID,
                    pnL.p_NID(TT.Grammar.Preposition.UID, preposition.ID),
                    pnL.p_NID(TTD.MetaData.Name.UID, NamingContainerOfPluralForm.ID));


        // **Aufzählung von Objekten**

        /// <summary>
        /// mko, 6.5.2021
        /// Aufzählung von Objekten. Objekte werden als NamingIds aufgelistet
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="ObjectNids"></param>
        /// <returns></returns>
        public static IInstance DefObjectEnumeration(
                this IComposer pnL,
                params long[] ObjectNids)
            => pnL.i(TT.Grammar.EnumerationOfObjects.UID,
                    pnL.p(TT.Metrology.Counter.UID, ObjectNids.Length),
                    pnL.EmbedInstanceMembers(
                        ObjectNids.Select(r => pnL.p_NID(TTD.MetaData.Name.UID, r)).ToArray()));


        /// <summary>
        /// mko, 6.5.2021
        /// Aufzählung von Objekten. Objekte werden als Klartextnamen augelistet
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="ObjectNames"></param>
        /// <returns></returns>
        public static IInstance DefObjectEnumeration(
                this IComposer pnL,
                params string[] ObjectNames)
            => pnL.i(TT.Grammar.EnumerationOfObjects.UID,
                    pnL.p(TT.Metrology.Counter.UID, ObjectNames.Length),
                    pnL.EmbedInstanceMembers(
                        ObjectNames.Select(r => pnL.p(TTD.MetaData.Name.UID, r)).ToArray()));


        /// <summary>
        /// mko, 11.5.2021
        /// Definiert zwei Objekte, die in Beziehung stehen durch eine Präposition
        /// wie 
        ///   "Bestellung für Projekt", "Zeichnung im Zustand_InWork"
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="ObjectA"></param>
        /// <param name="prepositionToB"></param>
        /// <param name="ObjectB"></param>
        /// <returns></returns>
        public static IInstance DefAinRelationToB(
                this IComposer pnL,
                long ObjectA,
                TT.Grammar.Prepositions.IPre prepositionToB,
                long ObjectB)
            => pnL.i(TT.Grammar.ObjectsInRelationship.UID,
                    pnL.p_NID(TT.Sets.First.UID, ObjectA),
                    pnL.p_NID(TT.Grammar.Preposition.UID, prepositionToB.ID),
                    pnL.p_NID(TT.Sets.Second.UID, ObjectB));

        public static IInstance DefAinRelationToB(
                this IComposer pnL,
                IInstance ObjectA,
                TT.Grammar.Prepositions.IPre prepositionToB,
                IInstance ObjectB)
            => pnL.i(TT.Grammar.ObjectsInRelationship.UID,
                    pnL.p(TT.Sets.First.UID, ObjectA),
                    pnL.p_NID(TT.Grammar.Preposition.UID, prepositionToB.ID),
                    pnL.p(TT.Sets.Second.UID, ObjectB));

        // **Adverbien**

        /// <summary>
        /// Allgemeine Adverbiale Bestimmung
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="adverb"></param>
        /// <returns></returns>
        public static IInstance DefAdverb(this IComposer pnL, TT.Grammar.Adverbs.IAdverb adverb)
        => pnL.i(TT.Grammar.AdverbialClause.UID,
                    pnL.p_NID(TT.Grammar.Adverbs.Adverb.UID, adverb.ID)
                );

        public static IInstance DefTimeAdverb(this IComposer pnL, TT.Grammar.Prepositions.IPre preposition, ITime time)
        {
            return pnL.i(TT.Grammar.AdverbialClause.UID,
                    pnL.p_NID(TT.Grammar.Preposition.UID, preposition.ID),
                    pnL.p(TT.Timeline.TimeStamp.UID, time)
                );
        }

        public static IInstance DefDateAdverb(this IComposer pnL, TT.Grammar.Prepositions.IPre preposition, IDate date)
        {
            return pnL.i(TT.Grammar.AdverbialClause.UID,
                    pnL.p_NID(TT.Grammar.Preposition.UID, preposition.ID),
                    pnL.p(TT.Timeline.DateStamp.UID, date)
                );
        }

        public static IInstance DefLocationAdverb(this IComposer pnL, TT.Grammar.Prepositions.IPre preposition, long LocationUID)
        {
            return pnL.i(TT.Grammar.AdverbialClause.UID,
                    pnL.p_NID(TT.Grammar.Preposition.UID, preposition.ID),
                    pnL.p_NID(TT.Locations.Location.UID, LocationUID)
                );
        }

        public static IInstance DefLocationAdverb(this IComposer pnL, TT.Grammar.Prepositions.IPre preposition, string LocationTxt)
        {
            return pnL.i(TT.Grammar.AdverbialClause.UID,
                    pnL.p_NID(TT.Grammar.Preposition.UID, preposition.ID),
                    pnL.p(TT.Locations.Location.UID, LocationTxt)
                );
        }


        /// <summary>
        /// mko, 6.4.2021
        /// Aussagesatz: Subjekt wirkt auf Objekt ein
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="SubjectUID"></param>
        /// <param name="verb"></param>
        /// <param name="preposition"></param>
        /// <param name="ObjectUID"></param>
        /// <returns></returns>
        //public static IInstance SubjectPredicateObjectStatement(
        //    this IComposer pnL,
        //    long SubjectUID,
        //    TT.Grammar.IVerb verb,
        //    TT.Grammar.Prepositions.IPre preposition,
        //    IInstance ObjectUID,
        //    params IInstance[] adverbialClauses
        //    )
        //{
        //    return pnL.i(TT.Grammar.Statement.UID,
        //            pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
        //            pnL.p_NID(TT.Grammar.Verb.UID, verb.ID),
        //            pnL.EmbedMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
        //            pnL.DefPrepositionalObject(preposition, ObjectUID)
        //        );
        //}



        /// <summary>
        /// Beschreibt einen abgeschlossenen Vorgang durch einen Aussagesatz.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="ObjectUID">Objekt, auf dem die abgeschlossene Handlung stattfand</param>
        /// <param name="finishedActivity">abgeschlossene Handlung</param>
        /// <param name="adverbialClauses">adverbiale Bestimmungen wir Ort des Geschehens, Zeitpunkt des Geschehens</param>
        /// <returns></returns>
        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            long SubjectNID,
            TT.Grammar.IFinishedActivity finishedActivity,
            params IInstance[] adverbialClauses
            )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectNID),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
                );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFinishedActivity finishedActivity,
            params IInstance[] adverbialClauses
            )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
                );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            long SubjectNID,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses
            )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectNID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
            );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses
            )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
            );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses
            )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
            );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            long SubjectNID,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectNID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
                );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
                );


        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
                );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            long SubjectNID,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectNID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
                );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
                );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            TT.Grammar.IFinishedActivity finishedActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
                );


        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            long SubjectNID,
            TT.Grammar.IFinishedActivity finishedActivity,
            IInstance PrepositionalObject,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectNID),
                    pnL.p(TT.Grammar.Object.UID, PrepositionalObject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
        );

        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFinishedActivity finishedActivity,
            IInstance PrepositionalObject,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, PrepositionalObject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
        );


        public static IInstance FinishedActivityStatement(
            this IComposer pnL,
            TT.Grammar.IFinishedActivity finishedActivity,
            IInstance PrepositionalObject,
            params IInstance[] adverbialClauses
        )
        => pnL.i(TT.Grammar.FinishedActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, PrepositionalObject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, finishedActivity.ID)
        );


        // **InProgressActivity**


        /// <summary>
        /// Beschreibt einen laufenden Vorgang durch einen Aussagesatz.
        /// </summary>
        /// <param name="pnL"></param>                
        /// <param name="inProgressActivity"></param>
        /// <param name="SubjectUID">Objekt, auf dem die Handlung stattfindet</param>
        /// <param name="adverbialClauses">adverbiale Bestimmungen wir Ort des Geschehens, Zeitpunkt des Geschehens</param>
        /// <returns></returns>
        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IInProgressActivity inProgressActivity,
            params IInstance[] adverbialClauses)
        {
            return pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );
        }

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IInProgressActivity inProgressActivity,
            params IInstance[] adverbialClauses)
        {
            return pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );
        }

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );


        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
        );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
        );


        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            TT.Grammar.IInProgressActivity inProgressActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
        );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IInProgressActivity inProgressActivity,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, Object),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IInProgressActivity inProgressActivity,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, Object),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );

        public static IInstance InProgressActivityStatement(
            this IComposer pnL,
            TT.Grammar.IInProgressActivity inProgressActivity,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, Object),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, inProgressActivity.ID)
                );

        // mko, 18.5.2021
        // **Future Activity Statement**

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IFutureActivity futureActivity,
            params IInstance[] adverbialClauses)
        {
            return pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );
        }

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFutureActivity futureActivity,
            params IInstance[] adverbialClauses)
        {
            return pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );
        }

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );


        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
        );

        public static IInstance FutureStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
        );


        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            TT.Grammar.IFutureActivity futureActivity,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.FutureActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
        );

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IFutureActivity futureActivity,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, Object),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );

        public static IInstance FutureStatement(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IFutureActivity futureActivity,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, Object),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );

        public static IInstance FutureActivityStatement(
            this IComposer pnL,
            TT.Grammar.IFutureActivity futureActivity,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.InProgressActivity.UID,
                    pnL.p(TT.Grammar.Object.UID, Object),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, futureActivity.ID)
                );




        /// <summary>
        /// mko, 6.4.2021
        /// Frage nach einer Person, die etwas tut, machen soll
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="pronoun"></param>
        /// <param name="verb"></param>
        /// <param name="ObjectUID"></param>
        /// <returns></returns>
        public static IInstance PronounQuestion(
            this IComposer pnL,
            TT.Grammar.Pronouns.Question.IQuestionPronoun questionPronoun,
            TT.Grammar.Prepositions.IPre prePosition,
            long ObjectUID)
        {
            return pnL.i(TT.Grammar.Question.UID,
                    pnL.p_NID(TT.Grammar.Pronouns.Questions.PronounQuestion.UID, questionPronoun.ID),
                    pnL.p_NID(TT.Grammar.Preposition.UID, prePosition.ID),
                    pnL.p_NID(TT.Grammar.Object.UID, ObjectUID)
                );
        }


        // **ModalQuestion**

        /// <summary>
        /// mko, 6.4.2021
        /// Frage, ob etwas möglich ist (kann) 
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="modalQuestionVerb"></param>        
        /// <param name="SubjectUID"></param>
        /// <param name="adverbialClauses">adverbiale Bestimmungen wir Ort des Geschehens, Zeitpunkt des Geschehens</param>
        /// <returns></returns>
        public static IInstance ModalQuestion(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            params IInstance[] adverbialClauses)

        {
            return pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
                );
        }

        public static IInstance ModalQuestion(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            params IInstance[] adverbialClauses)

        {
            return pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
                );
        }

        public static IInstance ModalQuestion(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            IInstance PrepositionalObject,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, PrepositionalObject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
                );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            IInstance PrepositionalObject,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, PrepositionalObject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
                );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            IInstance PrepositionalObject,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Object.UID, PrepositionalObject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
                );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
                );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
                );


        public static IInstance ModalQuestion(
            this IComposer pnL,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
                );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
        );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
        );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
        );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
        );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
        );

        public static IInstance ModalQuestion(
            this IComposer pnL,
            TT.Grammar.IModalPhrase modalQuestionVerb,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)

        => pnL.i(TT.Grammar.ModalQuestion.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalQuestionVerb.ID)
        );


        // **ModalResponse**

        /// <summary>
        /// mko, 6.4.2021
        /// Modale Antwort (etwas ist möglich oder nicht). Das Objekt, auf das sich die Anwort bezieht,
        /// ist eine NID (NamingID)
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="modalResponseVerb"></param>        
        /// <param name="ObjectInstance"></param>
        /// <param name="adverbialClauses">adverbiale Bestimmungen wir Ort des Geschehens, Zeitpunkt des Geschehens</param>
        /// <returns></returns>
        public static IInstance ModalResponse(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalResponseVerb,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
                );

        public static IInstance ModalResponse(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalResponseVerb,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
                );

        public static IInstance ModalResponse(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalResponseVerb,
            IInstance PrepositionalObject,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, PrepositionalObject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
                );

        public static IInstance ModalResponse(
            this IComposer pnL,
            TT.Grammar.IModalPhrase modalResponseVerb,
            IInstance PrepositionalObject,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p(TT.Grammar.Object.UID, PrepositionalObject),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
                );

        public static IInstance ModalResponse(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
                );

        public static IInstance ModalResponse(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
                );

        public static IInstance ModalResponse(
            this IComposer pnL,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            long ObjectUID,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectUID))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
                );

        public static IInstance ModalResponse(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
        );

        public static IInstance ModalResponse(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
        );

        public static IInstance ModalResponse(
            this IComposer pnL,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            string ObjectName,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, pnL.DefObject(ObjectName))),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
        );

        public static IInstance ModalResponse(
            this IComposer pnL,
            long SubjectUID,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p_NID(TT.Grammar.Subject.UID, SubjectUID),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
        );

        public static IInstance ModalResponse(
            this IComposer pnL,
            IInstance Subject,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p(TT.Grammar.Subject.UID, Subject),
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
        );

        public static IInstance ModalResponse(
            this IComposer pnL,
            TT.Grammar.IModalPhrase modalResponseVerb,
            TT.Grammar.Prepositions.IPre preposition,
            IInstance Object,
            params IInstance[] adverbialClauses)
        => pnL.i(TT.Grammar.ModalResponse.UID,
                    pnL.p(TT.Grammar.Object.UID, pnL.DefPrepositionalObject(preposition, Object)),
                    pnL.EmbedInstanceMembers(adverbialClauses?.Select(r => pnL.p(TT.Grammar.AdverbialClause.UID, r)).ToArray()),
                    pnL.p_NID(TT.Grammar.Verb.UID, modalResponseVerb.ID)
        );

        // **Exclamation**

        /// <summary>
        /// mko, 6.4.2021
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="verb"></param>
        /// <param name="ObjectUID"></param>
        /// <returns></returns>
        public static IInstance Exclamation(
            this IComposer pnL,
            TT.Grammar.IVerb verb,
            long ObjectUID)
        {
            return pnL.i(TT.Grammar.Exclamation.UID,
                    pnL.p_NID(TT.Grammar.Verb.UID, verb.ID),
                    pnL.p_NID(TT.Grammar.Object.UID, ObjectUID)
                );
        }

        public static IInstance Exclamation(
            this IComposer pnL,
            TT.Grammar.IVerb verb,
            IInstance ObjectInstance)
        {
            return pnL.i(TT.Grammar.Exclamation.UID,

                    pnL.p_NID(TT.Grammar.Verb.UID, verb.ID),
                    pnL.p(TT.Grammar.Object.UID, ObjectInstance)
                );
        }
    }
}
