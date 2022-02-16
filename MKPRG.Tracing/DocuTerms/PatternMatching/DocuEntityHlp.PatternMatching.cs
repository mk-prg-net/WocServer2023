using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


//using TechTerms = ATMO.mko.Logging.PNDocuTerms.DocuEntities.Composer.TechTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 28.2.2019
    /// Fasst Mehtoden zusammen, mit denen unterhalb eines DocuEntity nach Substrukturen gesucht werden kann
    /// </summary>
    public static partial class DocuEntityHlp
    {
        
        // mko, 30.7.2021
        // Kampf den Nullwerten: Ab jetzt sind alle DocuTerme stets vollständig initialisiert. 
        // Falls kein Wert bei der Intitialisierung übermittelt wird, dann ist stets ein Default- Wert einzusetzten
        // </summary>
        // <param name="docuTerm"></param>

        //public static bool IsUndefinedDocuTerm(this IInstance i)
        //    => i.HasName(TTD.Types.UndefinedDocuTerm.UID);

        //public static bool IsUndefinedDocuTerm(this IMethod m)
        //    => m.HasName(TTD.Types.UndefinedDocuTerm.UID);

        //public static bool IsUndefinedDocuTerm(this IProperty p)
        //    => p.HasName(TTD.Types.UndefinedDocuTerm.UID);

        //public static bool IsUndefinedDocuTerm(this IEvent e)
        //    => e.HasName(TTD.Types.UndefinedDocuTerm.UID);


        /// <summary>
        /// mko, 28.2.2019
        /// 
        /// mko, 15.6.2020
        /// Namens und Wertevergleiche berücksichtigen jetzt, das Strings durch eine sprachneutrale NID dargestellt werden können.
        /// Der Vergleich berücksichtigt jetzt die strengere Typisierung der DocuTerms (Boolean, Integer etc.)
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns>true if a and b of same entity type and immediate content of a is the same as b</returns>
        public static bool IsEqualTo(this IDocuEntity a, IDocuEntity b)
        {
            if (a.EntityType == DocuEntityTypes.WildCard)
            {
                return true;
            }
            if (a.EntityType == b.EntityType)
            {
                if (a.IsNamed() && b.IsNamed() && a.AreOfSameName(b))
                {
                    if (!a.AreOfSameName(b))
                    {
                        // Unterschiedliche Namen => keine Gleichheit
                        return false;
                    }
                    else if (a is IMethod aMeth && b is IMethod bMeth)
                    {
                        return EqualMethodParameter(aMeth.Parameters, bMeth.Parameters);
                    }
                    else if (a is IInstance aInstance && b is IInstance bInstance)
                    {
                        return EqualInstanceMembers(aInstance.InstanceMembers, bInstance.InstanceMembers);
                    }
                    else if (a is IProperty propA && b is IProperty propB)
                    {
                        return propA.PropertyValue.IsEqualTo(propB.PropertyValue);
                    }
                    else if (a is IEvent eventA && b is IEvent eventB)
                    {
                        if (eventA.IsSetToDefaultValue && eventB.IsSetToDefaultValue)
                            // Beide Events ohne Parameter -> Gleichheit
                            return true;
                        else if (eventA.IsSetToDefaultValue && !eventB.IsSetToDefaultValue)
                            // Pattern als Event ohne Parameter ist auch in Events mit  
                            // Parameter enthalten -> ist SubTree
                            return true;
                        else if (!eventA.IsSetToDefaultValue && eventB.IsSetToDefaultValue)
                            // Das Pattern ist restriktiver als das zu untersuchende Event-> kein SubTree
                            return false;
                        else
                            return eventA.EventParameter.IsEqualTo(eventB.EventParameter);
                    }
                    else return false;
                }
                else
                {
                    if (a is INID nidA && b is INID nidB)
                    {
                        return nidA.NamingId == nidB.NamingId;
                    }
                    else if (a is IReturn retA && b is IReturn retB)
                    {
                        return retA.ReturnValue.IsEqualTo(retB.ReturnValue);
                    }
                    else if (a is IInteger intA && b is IInteger intB)
                    {
                        return intA.ValueAsLong == intB.ValueAsLong;
                    }
                    else if (a is IDouble dblA && b is IDouble dblB)
                    {
                        return dblA.ValueAsDouble == dblB.ValueAsDouble;
                    }
                    else if (a is IBoolean boolA && b is IBoolean boolB)
                    {
                        return boolA.ValueAsBool == boolB.ValueAsBool;
                    }
                    else if (a is IString strA && b is IString strB)
                    {
                        return strA.ValueAsString.Equals(strB.ValueAsString);
                    }
                    else if (a is ITxt txtA && b is ITxt txtB)
                    {
                        bool areEqual = txtA.Words.Length == txtA.Words.Length;

                        if (areEqual)
                        {
                            // Vergleich unter strikter Beachtung der Reihenfolge der Parameter
                            var contentAB = txtA.Words.Zip(txtB.Words, (A, B) => (A, B));

                            foreach (var pair in contentAB)
                            {
                                areEqual &= pair.A.ValueAsString.Equals(pair.B.ValueAsString);
                                if (!areEqual)
                                    break;
                            }
                        }

                        return areEqual;
                    }
                    else if (a is IDate aDat && b is IDate bDat)
                    {
                        return aDat.Year == bDat.Year && aDat.Month == bDat.Month && aDat.Day == bDat.Day;
                    }
                    else if (a is ITime aTime && b is ITime bTime)
                    {
                        return aTime.Hour == bTime.Hour && aTime.Minutes == bTime.Minutes && aTime.Seconds == bTime.Seconds && aTime.Milliseconds == bTime.Milliseconds;
                    }
                    else if (a is IVer aver && b is IVer bver)
                    {
                        return aver.VersionString.ToUpper().Equals(bver.VersionString.ToUpper());
                    }
                    else if (a is IDTList listA && b is IDTList listB)
                    {
                        bool areEqual = listA.ListMembers.Length == listA.ListMembers.Length;

                        if (areEqual)
                        {
                            // Vergleich unter strikter Beachtung der Reihenfolge der Parameter
                            var contentAB = listA.ListMembers.Zip(listB.ListMembers, (A, B) => (A, B));

                            foreach (var pair in contentAB)
                            {
                                areEqual &= pair.A.IsEqualTo(pair.B);
                                if (!areEqual)
                                    break;
                            }
                        }

                        return areEqual;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            else return false;
        }

        public static bool EqualMethodParameter(IMethodParameter[] aParams, IMethodParameter[] bParams)
        {
            // 22.6.2020
            // Alle Inhalte auf Gleichheit prüfen. 
            // Die Reihenfolge der Kindelemente ist dabei für Instanzmember ohne Bedeutung. Jedoch spielt die Reihenfolge der 
            // Kindelemente von Methoden, wo sie den Parametern und Rückgabewerten entsprechen, sehr wohl eine Rolle!                                
            // Für einfache Methoden, deren Parameterliste fix und eindeutig sind, könnte die Reihenfolge außer Betracht gelassen werden:
            // (m kleiner (L (p a 5) (p b 7)))  == (m kleiner (L (p b 7) (p a 5))) 
            // Hier kann Eigenschaft a stets als erster Wert, und b als zweiter angenommen werden, unabhängig von der Reihenfolge im DokuTerm.
            //
            // Jedoch ist dies in komplexeren Beispielen nicht mehr allgemein gültig. Z.B. sind die 2D Transformationen wie Rotation und 
            // Translation nicht kommutativ. Werden Folgen solcher Transformationen als Parameter einer Methode durch DokuTerms 
            // präsentiert, dann ist die Reihenfolge unbedingt zu beachten!
            //
            // (m Apply2DTrafos 
            //    (L 
            //       (m rot (L (p a 45)))
            //       (m translate (L (p x 10) (p y 10)))
            //    )
            //  )
            // 
            // (m Apply2DTrafos 
            //    (L 
            //       (m translate (L (p x 10) (p y 10)))
            //       (m rot (L (p a 45)))
            //    )
            //  )
            // 

            // Haben beide Methoden leere Parameterlisten, dann sind sie gleich
            bool areEqual = aParams == null && bParams == null;

            if (!areEqual)
            {

                areEqual = aParams.Length == bParams.Length;

                if (areEqual)
                {
                    // Vergleich unter strikter Beachtung der Reihenfolge der Parameter
                    var contentAB = aParams.Zip(bParams, (A, B) => (A, B));

                    foreach (var pair in contentAB)
                    {
                        areEqual &= pair.A.IsEqualTo(pair.B);
                        if (!areEqual)
                            break;
                    }
                }
            }

            return areEqual;
        }

        /// <summary>
        /// mko, 22.6.2020
        /// Die Reihenfolge von instance- Membern spielt keine Rolle. Jedoch müssen in beiden Instanzen alle Instanzmember vorhanden und identisch sein.
        /// </summary>
        /// <param name="aMembers"></param>
        /// <param name="bMembers"></param>
        /// <returns></returns>
        public static bool EqualInstanceMembers(IInstanceMember[] aMembers, IInstanceMember[] bMembers)
        {
            // Beide Instanzen müssen die gleiche Anzahl an Membern haben
            bool areEqual = aMembers == null && bMembers == null;

            if (!areEqual)
            {
                areEqual = aMembers.Length == bMembers.Length;

                if (areEqual)
                {
                    var bList = new List<IListMember>(bMembers);
                    foreach (var a in aMembers)
                    {
                        bool found = false;
                        foreach (var b in bList)
                        {
                            if (a.IsEqualTo(b))
                            {
                                // Wurde ein identischer Member in b gefunden, dann wird dieser aus der bList entfernt,
                                // und der Vergleich gilt als für a erfolgreich abgeschlossen.
                                bList.Remove(b);
                                found = true;
                                break;
                            }
                        }

                        if (!found)
                        {
                            // konnte zue einem Parameter aus aMembers kein gleicher in bMembers gefunden werden,
                            // dann sind die beiden Parameterlisten ungleich. Der Vergleich kann mit negativem 
                            // Ergebnis beendet werden.
                            areEqual = false;
                            break;
                        }
                    }

                    // Alle Member aus B müssen Membern aus A zugeordnet worden sein
                    areEqual &= !bList.Any();
                }
            }

            return areEqual;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Returns true, if a is an embeded subtree in b.
        /// Order of childs is important but not the absolute position:
        /// e.g.
        ///     If X := a(b(d f)) and Y:= a(b(c d e f) g(h i)) are trees,
        ///     Then X is subtree of Y
        /// 
        /// e.g. (Subtree- Nodes are marked with *)
        /// 
        /// a*
        /// +-b*
        /// | +-c
        /// | +-d*  -+
        /// | +-e    +- d and f has an offset, but the order of d and f is preserved
        /// | +-f*  -+
        /// |
        /// +-g
        ///   +-h
        ///   +-i
        ///   
        /// The subTreePattern can also match in a deeper level of tree.
        /// e.g.
        ///    If X:= a(b(d f)) and Y:= q(r (a(b(c d e f) g(h i))))
        ///    Then X is a subtree of Y
        /// 
        /// mko, 22.5.2019
        /// There was a problem with nested nodes of the same type. 
        /// If a node X of type A, which is not the subtree you are looking for, 
        /// has nested a node Y of type A, which represents the subtree you are looking for, 
        /// then in older versions the search for the subtree is already set at X, 
        /// and Y is no longer found.  
        /// Now the search for the subtree continues with the children of X.
        ///
        /// mko, 15.6.2020
        /// 
        /// mko, 7.12.2020
        /// Abgesichert gegen Fall tree == null
        /// 
        /// mko, 15.3.2021
        /// Namensvergleiche benannter Entities auf Vergleiche mit Wildcards erweitert.
        /// 
        /// mko, 12.4.2021  
        /// Beschränkung der Suchtiefe eingebaut.
        /// 
        /// mko, 2.8.2021
        /// Aufgesplitet in eine Schaar von IsSubTreeOf(...) Methoden, wobei der erste Paramter
        /// eine von `IDocuEntity` abgeleitet Schnittstelle, und der zweite Parameter algemein vom 
        /// Typ `IDocuEntity` ist.
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <returns></returns>
        //private static bool IsPutIntoAppropiateIsSubTreeOfTestWith(
        //    this IDocuEntity subTreePattern,
        //    IDocuEntity tree,
        //    bool searchAnywhere = true,
        //    int Deepth = int.MaxValue)
        //{
        //    var res = false;

        //    if (tree != null)
        //    {
        //        var a = subTreePattern;
        //        var b = tree;

        //        if (a is IWildCard wc)
        //        {
        //            res = wc.IsSubTreeOf(b, searchAnywhere, Deepth);
        //        }
        //        else if (!(a is INoSubTrees) && b is INoSubTrees)
        //        {
        //            // SubTreePattern ist komplexer aufgebaut ist als zu untersuchender Tree.
        //            // z.B. Instance.IsSubTreeOf(Double)
        //            // Daraus folgt, das SubTreePattern kein SubTree von Tree sein kann.
        //            res = false;
        //        }
        //        else if (a is INoSubTrees)
        //        {
        //            // Simple Terme- die Klassifizierung dient der Laufzeitoptimierung

        //            if (a is INID nidA)
        //            {
        //                res = nidA.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            }
        //            else if (a is IInteger intA)
        //            {
        //                res = intA.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            }
        //            else if (a is IDouble dblA)
        //            {
        //                res = dblA.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            }
        //            else if (a is IBoolean boolA)
        //            {
        //                res = boolA.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            }
        //            else if (a is IString strA)
        //            {
        //                res = strA.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            }
        //            else if (a is IDate aDat)
        //            {
        //                res = aDat.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            }
        //            else if (a is ITime aTime)
        //            {
        //                res = aTime.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            }
        //            else if (a is IVer aver)
        //            {
        //                res = aver.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            }
        //        }

        //        else if (a is IReturn retA) // && b is IReturn retB)
        //        {
        //            res = retA.IsSubTreeOf(b, searchAnywhere, Deepth);
        //            //res = retA.ReturnValue.IsSubTreeOf(retB.ReturnValue, false);
        //        }
        //        else if (a is ITxt txtA)
        //        {
        //            res = txtA.IsSubTreeOf(tree, searchAnywhere, Deepth);
        //        }
        //        else if (a is IInstance i)
        //        {
        //            res = i.IsSubTreeOf(b, searchAnywhere, Deepth);
        //        }
        //        else if (a is IMethod m)
        //        {
        //            res = m.IsSubTreeOf(b, searchAnywhere, Deepth);
        //        }
        //        else if (a is IReturn ret)
        //        {
        //            res = ret.IsSubTreeOf(b, searchAnywhere, Deepth);
        //        }
        //        else if (a is IProperty p)
        //        {
        //            res = p.IsSubTreeOf(b, searchAnywhere, Deepth);
        //        }
        //        else if (a is IEvent e)
        //        {
        //            res = e.IsSubTreeOf(b, searchAnywhere, Deepth);
        //        }
        //        else if (a is IDTList lst)
        //        {
        //            res = lst.IsSubTreeOf(b, searchAnywhere, Deepth);
        //        }

        //        // Weitersuchen, wenn noch keine Übereinstimmung festgestellt wurde, und auch 
        //        // die Übereinstimmung mit Strukturen in tieferen Schichten legitim wäre
        //        //if (!res && searchAnywhere)
        //        //{

        //        //    SearchInDeeperLevels(
        //        //        (_tree, deepth) => a.IsPutIntoAppropiateIsSubTreeOfTestWith(_tree, true, deepth),
        //        //        b,
        //        //        Deepth);

        //        //}

        //    }

        //    return res;
        //}

        /// <summary>
        /// mko, 2.8.2021
        /// Mapped einen Entity- Type auf einen passenden IsSubTree- Testfunktion
        /// </summary>
        private static Dictionary<DocuEntityTypes,
                                  Func<IDocuEntity,
                                       IDocuEntity,
                                       IDocuEntity,
                                       Action<IDocuEntity, IDocuEntity, int>,
                                       bool,
                                       bool,
                                       int,
                                       int,
                                       bool>> IsSubTreeTestForEntityType
            = new Dictionary<DocuEntityTypes,
                             Func<IDocuEntity,
                                  IDocuEntity,
                                  IDocuEntity,
                                  Action<IDocuEntity, IDocuEntity, int>,
                                  bool,
                                  bool,
                                  int,
                                  int,
                                  bool>>
        {
            {DocuEntityTypes.Bool, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IBoolean)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Date, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IDate)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Event, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IEvent)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Float, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IDouble)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Instance, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IInstance)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Int, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IInteger)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.List, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IDTList)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Method, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IMethod)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Name, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => false },
            {DocuEntityTypes.NID, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((INID)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.none, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => false },
            {DocuEntityTypes.Property, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IProperty)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.PropertySet, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => false },
            {DocuEntityTypes.ReturnValue, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IReturn)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.String, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IString)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Text, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((ITxt)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Time, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((ITime)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.Version, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IVer)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
            {DocuEntityTypes.WildCard, (sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) => IsSubTreeOf((IWildCard)sub, tree, parentTree, action, fin, any, currentLevel, maxLevel) },
        };


        /// <summary>
        /// mko, 11.8.2021
        /// 
        /// Allgemeine 
        /// </summary>
        /// <param name="sub"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnyWhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IDocuEntity sub,
            IDocuEntity tree,
            bool searchAnyWhere = true,
            int maxLevel = int.MaxValue)
            => IsSubTreeTestForEntityType[sub.EntityType](sub, tree, tree, (m, p, d) => { }, true, searchAnyWhere, 0, maxLevel);


        /// <summary>
        /// mko, 3.8.2021
        /// 
        /// mko, 4.8.2021
        /// deepth umbenannt in maxLevel
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnyWhere"></param>
        /// <param name="maxLevel">Tiefste Ebene, bis in die gesucht wird</param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IWildCard wc,
            IDocuEntity tree,
            bool searchAnyWhere = true,
            int maxLevel = int.MaxValue)
            => IsSubTreeOf(wc, tree, tree, (m, p, d) => { }, true, searchAnyWhere, 0, maxLevel);

        /// <summary>
        /// mko, 2.8.2021
        /// 
        /// mko, 3.8.2021
        /// Erweitert um Parameter `doSomethingIfPatternMatches` und `finishSearchAfterPatternMatched`
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnyWhere"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IWildCard wc,
            IDocuEntity tree,
            IDocuEntity parentTree,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnyWhere,
            int currentLevel = 0,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                // Hat die Wilccard eine Subtree- Einschränkung
                if (wc.HasSubTreeConstraint)
                {
                    //res = wc.SubTreeConstraint.IsPutIntoAppropiateIsSubTreeOfTestWith(tree);
                    res = IsSubTreeTestForEntityType[wc.SubTreeConstraint.EntityType]
                        (wc.SubTreeConstraint,
                        tree,
                        parentTree,
                        doSomethingIfPatternMatches,
                        true, // finishSearchAfterPatternMatched: Ist der Subtreeconstraint irgendwo erfüllt, dann kann die Analyse abgebrochen werden. Deshalb true
                        true, // SearchAnywhere: Der Subttree- constraint kann irgend ein Nachfolger zum aktuellen Knoten sein. Deshalb true
                        currentLevel,
                        maxLevel);
                }
                else
                {
                    res = true;
                }

                if (res)
                {
                    doSomethingIfPatternMatches(tree, parentTree, currentLevel);
                }
            }

            return res;
        }

        /// <summary>
        /// mko, 3.8.2021
        /// </summary>
        /// <param name="nidAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this INID nidAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                nidAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, maxLevel);

        /// <summary>
        /// mko, 2.8.2021
        /// </summary>
        /// <param name="nidAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this INID nidAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finish = true;
                if (tree is INID nidB)
                {
                    res = nidAsSubTreePattern.NamingId == nidB.NamingId;

                    if (res)
                    {
                        doSomethingIfPatternMatches(nidB, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }

                }
                else if (tree is IString str_B)
                {
                    // mko, 14.7.2020
                    // Annahme: Solche Vergleiche passieren in der Regel, wenn ein Tree mit einem geparsten verglichen wird.
                    //          Der geparste Tree ist dabei mit der CNT benannt.
                    res = RC.NC[nidAsSubTreePattern.NamingId].CNT == str_B.ValueAsString;

                    if (res)
                    {
                        doSomethingIfPatternMatches(str_B, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                }

                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    // tree ist komplex
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _level, _max) => nidAsSubTreePattern.IsSubTreeOf
                                (_tree,
                                _parent,
                                 doSomethingIfPatternMatches,
                                 finishSearchAfterPatternMatched,
                                 searchAnywhere,
                                 _level,
                                 _max),
                            tree,
                            currentLevel,
                            maxLevel);
                }
            }

            return res;
        }

        /// <summary>
        /// mko, 3.8.2021
        /// </summary>
        /// <param name="intAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IInteger intAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                intAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);

        /// <summary>
        /// mko, 2.8.2021
        /// </summary>
        /// <param name="intAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IInteger intAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finish = true;
                if (tree is IInteger iB)
                {
                    res = intAsSubTreePattern.ValueAsLong == iB.ValueAsLong;

                    if (res)
                    {
                        doSomethingIfPatternMatches(tree, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }

                }

                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    // tree ist komplex
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _level, _max) => intAsSubTreePattern.IsSubTreeOf(
                                                _tree,
                                                _parent,
                                                doSomethingIfPatternMatches,
                                                finishSearchAfterPatternMatched,
                                                searchAnywhere,
                                                _level,
                                                _max),
                            tree,
                            currentLevel,
                            maxLevel);
                }

            }

            return res;
        }

        /// <summary>
        /// mko, 3.8.2021
        /// </summary>
        /// <param name="dblAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IDouble dblAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                dblAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);

        /// <summary>
        /// mko, 2.8.2021
        /// </summary>
        /// <param name="dblAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IDouble dblAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finish = true;

                if (tree is IDouble dblB)
                {
                    res = dblAsSubTreePattern.ValueAsDouble == dblB.ValueAsDouble;

                    if (res)
                    {
                        doSomethingIfPatternMatches(dblB, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                }

                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    // tree ist komplex
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _level, _max) => dblAsSubTreePattern.IsSubTreeOf(
                                                    _tree,
                                                    _parent,
                                                    doSomethingIfPatternMatches,
                                                    finishSearchAfterPatternMatched,
                                                    searchAnywhere,
                                                    _level,
                                                    _max),
                            tree,
                            currentLevel,
                            maxLevel);
                }

            }

            return res;
        }

        /// <summary>
        /// mko, 3.8.2021
        /// </summary>
        /// <param name="boolAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IBoolean boolAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                boolAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);

        /// <summary>
        /// mko, 2.8.2021
        /// </summary>
        /// <param name="boolAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IBoolean boolAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finish = true;

                if (tree is IBoolean boolB)
                {
                    res = boolAsSubTreePattern.ValueAsBool == boolB.ValueAsBool;

                    if (res)
                    {
                        doSomethingIfPatternMatches(boolB, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                }

                if (!(tree is INoSubTrees) &&  searchAnywhere && !(finish && res))
                {
                    // tree ist komplex
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _level, _max) => boolAsSubTreePattern.IsSubTreeOf(
                                                _tree,
                                                _parent,
                                                doSomethingIfPatternMatches,
                                                finishSearchAfterPatternMatched,
                                                searchAnywhere,
                                                _level,
                                                _max),
                            tree,
                            currentLevel,
                            maxLevel);
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 2.8.2021
        /// </summary>
        /// <param name="strAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IString strAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                strAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);

        /// <summary>
        /// mko, 2.8.2021
        /// Verarbeitet auch gemischte Terme
        /// </summary>
        /// <param name="strAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IString strAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;
            var finish = true;
            if (tree != null && currentLevel <= maxLevel)
            {
                if (tree is IString strB)
                {
                    res = strAsSubTreePattern.ValueAsString.Equals(strB.ValueAsString);

                    if (res)
                    {
                        doSomethingIfPatternMatches(strB, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }

                }
                else if (tree is INID nid_B)
                {
                    // mko, 14.7.2020
                    res = strAsSubTreePattern.ValueAsString.Equals(RC.NC[nid_B.NamingId].CNT);

                    if (res)
                    {
                        doSomethingIfPatternMatches(nid_B, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                }
                else if (tree is ITxt txt)
                {
                    // mko, 30.7.2021
                    // Wenn der String als subTreePattern in einem Text als Wort gefunden wird, dann ist 
                    // die SubTree- Bedingung erfüllt
                    foreach (var word in txt.Words)
                    {
                        if (strAsSubTreePattern.ValueAsString.ToUpper().Equals(word.ValueAsString.ToUpper()))
                        {
                            res = true;

                            doSomethingIfPatternMatches(word, parent, currentLevel);
                            finish = finishSearchAfterPatternMatched;

                            break;
                        }
                    }
                }


                if (!(tree is INoSubTrees) &&  searchAnywhere && !(finish && res))
                {
                    // tree ist komplex
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _level, _max) => strAsSubTreePattern.IsSubTreeOf(
                                                    _tree,
                                                    _parent,
                                                    doSomethingIfPatternMatches,
                                                    finishSearchAfterPatternMatched,
                                                    searchAnywhere,
                                                    _level,
                                                    _max),
                            tree,
                            currentLevel,
                            maxLevel);
                }
            }

            return res;
        }

        /// <summary>
        /// mko, 3.8.2021
        /// </summary>
        /// <param name="dateAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IDate dateAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
            =>
                dateAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, currentLevel, maxLevel);

        /// <summary>
        /// mko, 2.8.2021
        /// </summary>
        /// <param name="dateAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IDate dateAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;
            var finish = true;

            if (tree != null && currentLevel <= maxLevel)
            {
                if (tree is IDate bDat)
                {
                    res = dateAsSubTreePattern.Year == bDat.Year
                        && dateAsSubTreePattern.Month == bDat.Month
                        && dateAsSubTreePattern.Day == bDat.Day;

                    if (res)
                    {
                        doSomethingIfPatternMatches(bDat, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                }

                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    // tree ist komplex
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _level, _max) => dateAsSubTreePattern.IsSubTreeOf(
                                                _tree,
                                                _parent,
                                                doSomethingIfPatternMatches,
                                                finishSearchAfterPatternMatched,
                                                searchAnywhere,
                                                _level,
                                                _max),
                            tree,
                            currentLevel,
                            maxLevel);
                }
            }

            return res;
        }

        /// <summary>
        /// mko, 3.8.2021
        /// </summary>
        /// <param name="timeAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this ITime timeAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                timeAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);

        /// <summary>
        /// mko, 2.8.2021
        /// </summary>
        /// <param name="timeAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this ITime timeAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finish = true;
                if (tree is ITime bTime)
                {
                    res = timeAsSubTreePattern.Hour == bTime.Hour
                        && timeAsSubTreePattern.Minutes == bTime.Minutes
                        && timeAsSubTreePattern.Seconds == bTime.Seconds
                        && timeAsSubTreePattern.Milliseconds == bTime.Milliseconds;

                    if (res)
                    {
                        doSomethingIfPatternMatches(bTime, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }

                }

                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    // tree ist komplex
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _level, _max) => timeAsSubTreePattern.IsSubTreeOf(
                                                    _tree,
                                                    _parent,
                                                    doSomethingIfPatternMatches,
                                                    finishSearchAfterPatternMatched,
                                                    searchAnywhere,
                                                    _level,
                                                    _max),
                            tree,
                            currentLevel,
                            maxLevel);
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 3.8.2021
        /// 
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="verAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IVer verAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int Deepth = int.MaxValue)
            =>
                verAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, Deepth);

        /// <summary>
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="verAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="parentTree"></param>
        /// <param name="doSomethingIfPatternMatches"></param>
        /// <param name="finishSearchAfterPatternMatched"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="currentLevel"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IVer verAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parentTree,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finish = true;

                if (tree is IVer bVer)
                {
                    res = verAsSubTreePattern.VersionString.Equals(bVer.VersionString);

                    if (res)
                    {
                        doSomethingIfPatternMatches(bVer, parentTree, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                }

                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    // tree ist komplex
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _level, _maxLevel) => verAsSubTreePattern.IsSubTreeOf(
                                                    _tree,
                                                    _parent,
                                                    doSomethingIfPatternMatches,
                                                    finishSearchAfterPatternMatched,
                                                    searchAnywhere,
                                                    _level,
                                                    _maxLevel),
                            tree,
                            currentLevel,
                            maxLevel);
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 28.7.2021
        /// 
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="instanceAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IInstance instanceAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                instanceAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);

        /// <summary>
        /// mko, 28.7.2021
        /// 
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="instanceAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IInstance instanceAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parentTree,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                //var a = subTreePattern;
                //var b = tree;
                var finish = true;

                if (tree is IInstance instance && instanceAsSubTreePattern.AreOfSameName(instance))
                {
                    res = PartiallyEqualInstanceMembers(
                        instanceAsSubTreePattern,
                        instance,
                        currentLevel,
                        maxLevel,
                        doSomethingIfPatternMatches,
                        finishSearchAfterPatternMatched);

                    if (res)
                    {
                        doSomethingIfPatternMatches(instance, parentTree, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                }

                // Weitersuchen, wenn noch keine Übereinstimmung festgestellt wurde, und auch 
                // die Übereinstimmung mit Strukturen in tieferen Schichten legitim wäre
                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    res = SearchInDeeperLevels(
                            (_tree, _parentTree, _level, _maxLevel) => instanceAsSubTreePattern.IsSubTreeOf(
                                                    _tree,
                                                    _parentTree,
                                                    doSomethingIfPatternMatches,
                                                    finishSearchAfterPatternMatched,
                                                    true,
                                                    _level,
                                                    _maxLevel),
                            tree,
                            currentLevel,
                            maxLevel);
                }
            }

            return res;
        }

        /// <summary>
        /// mko, 3.8.2021
        /// </summary>
        /// <param name="methodAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IMethod methodAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                methodAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);

        /// <summary>
        /// mko, 28.6.2021
        /// </summary>
        /// <param name="methodAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IMethod methodAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity Parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finishSearch = true;

                if (tree is IMethod bMeth && methodAsSubTreePattern.AreOfSameName(bMeth))
                {
                    res = PartiallyEqualMethodParameters(
                        methodAsSubTreePattern,
                        bMeth,
                        currentLevel,
                        maxLevel,
                        doSomethingIfPatternMatches,
                        finishSearchAfterPatternMatched);

                    if (res)
                    {
                        doSomethingIfPatternMatches(bMeth, Parent, currentLevel);
                        finishSearch = finishSearchAfterPatternMatched;
                    }
                }

                // Weitersuchen, wenn noch keine Übereinstimmung festgestellt wurde, und auch 
                // die Übereinstimmung mit Strukturen in tieferen Schichten legitim wäre
                // Weitersuchen, wenn noch keine Übereinstimmung festgestellt wurde, und auch 
                // die Übereinstimmung mit Strukturen in tieferen Schichten legitim wäre
                if (!res && searchAnywhere)
                {
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _current, _max)
                                => methodAsSubTreePattern.IsSubTreeOf(
                                    _tree,
                                    _parent,
                                    doSomethingIfPatternMatches,
                                    finishSearchAfterPatternMatched,
                                    true,
                                    _current,
                                    _max),
                            tree,
                            currentLevel,
                            maxLevel);
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 3.8.2021
        /// Überladung der vollständigen Implementierung von `IsSubTreeOf`, um neue Parameter im alten Knotext
        /// zu verbergen
        /// </summary>
        /// <param name="propertyAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IProperty propertyAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                propertyAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);



        /// <summary>
        /// mko, 28.7.2021
        /// 
        /// mko, 3.8.2021
        /// Erweitert auf die Parameter `doSomethingIfPatternMatches`und  `finishSearchAfterPatternMatched`,
        /// um mit dieser Funktion auch `AsSubTreeOf`zu implementieren etc..
        /// </summary>
        /// <param name="propertyAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="doSomethingIfPatternMatches"></param>
        /// <param name="finishSearchAfterPatternMatched"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IProperty propertyAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity Parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int curentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && curentLevel <= maxLevel)
            {
                //var a = subTreePattern;
                //var b = tree;

                var finishSearch = true;

                if (tree is IProperty p && propertyAsSubTreePattern.AreOfSameName(p))
                {
                    // Properties müssen im Unterschied zu Events immer einen Propertyvalue 
                    // enthalten. Im Pattern kann der PropertyValue durch einen Wildcard ersetzt
                    // werden.

                    //res = propertyAsSubTreePattern.PropertyValue.IsPutIntoAppropiateIsSubTreeOfTestWith(p.PropertyValue, false);
                    res = IsSubTreeTestForEntityType[propertyAsSubTreePattern.PropertyValue.EntityType]
                          (
                            propertyAsSubTreePattern.PropertyValue,
                            p.PropertyValue,
                            p,
                            doSomethingIfPatternMatches,
                            finishSearchAfterPatternMatched,
                            searchAnywhere,
                            curentLevel + 1,
                            maxLevel);

                    if (res)
                    {
                        doSomethingIfPatternMatches(p, Parent, curentLevel);
                        finishSearch = finishSearchAfterPatternMatched;
                    }
                }

                // Weitersuchen, wenn noch keine Übereinstimmung festgestellt wurde, und auch 
                // die Übereinstimmung mit Strukturen in tieferen Schichten legitim wäre.
                // Prüfen, ob die Tiefe nicht beschränkt ist
                if (!(tree is INoSubTrees) && searchAnywhere && !(finishSearch && res))
                {
                    res = SearchInDeeperLevels(
                            (_tree, _parent, _currentLevel, _maxLevel)
                                => propertyAsSubTreePattern.IsSubTreeOf(
                                    _tree,
                                    _parent,
                                    doSomethingIfPatternMatches,
                                    finishSearchAfterPatternMatched,
                                    true,
                                    _currentLevel,
                                    _maxLevel),
                            tree,
                            curentLevel,
                            maxLevel);
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 3.8.2021
        /// 
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="eventAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IEvent eventAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                eventAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);

        /// <summary>
        /// mko, 28.7.2021
        /// </summary>
        /// <param name="eventAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IEvent eventAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finish = true;

                if (tree is IEvent e && eventAsSubTreePattern.AreOfSameName(e))
                {

                    if (eventAsSubTreePattern.IsSetToDefaultValue)
                    {
                        // Wenn das subTreePattern keinen Eventparameter hat, dann ist es immer ein 
                        // SubTree von Tree, da beide Events sind, und ein event ohne Parameter (= Defaultwert)
                        // Teil eines Events mit Parameter ist
                        res = true;
                    }
                    else if (!eventAsSubTreePattern.IsSetToDefaultValue && e.IsSetToDefaultValue)
                    {
                        // Das Pattern ist restriktiver als das zu untersuchende Event-> kein SubTree
                        res = false;
                    }
                    else
                    {
                        // Prüfen, ob Parameter vom Pattern im Parameter vom Event enthalten ist
                        //res = eventAsSubTreePattern.EventParameter.IsPutIntoAppropiateIsSubTreeOfTestWith(e.EventParameter);
                        res = IsSubTreeTestForEntityType[eventAsSubTreePattern.EventParameter.EntityType](
                                eventAsSubTreePattern.EventParameter,
                                e.EventParameter,
                                parent,
                                doSomethingIfPatternMatches,
                                finishSearchAfterPatternMatched,
                                searchAnywhere,
                                currentLevel,
                                maxLevel);
                    }

                    if (res)
                    {
                        doSomethingIfPatternMatches(e, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }

                }

                // Weitersuchen, wenn noch keine Übereinstimmung festgestellt wurde, und auch 
                // die Übereinstimmung mit Strukturen in tieferen Schichten legitim wäre
                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    res = SearchInDeeperLevels(
                        (_tree, _parent, _currentLevel, _maxLevel) => eventAsSubTreePattern.IsSubTreeOf(
                                                _tree,
                                                _parent,
                                                doSomethingIfPatternMatches,
                                                finishSearchAfterPatternMatched,
                                                true,
                                                _currentLevel,
                                                _maxLevel),
                        tree,
                        currentLevel,
                        maxLevel);
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 3.8.2021
        /// </summary>
        /// <param name="dtListAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="_maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IDTList dtListAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int _maxLevel = int.MaxValue
            )
            =>
                dtListAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, _maxLevel);


        /// <summary>
        /// mko, 30.7.2021
        /// </summary>
        /// <param name="dtListAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IDTList dtListAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue
            )
        {
            bool res = false;

            if (tree != null && currentLevel <= maxLevel)
            {
                var finish = true;
                if (tree is IDTList listB)
                {
                    // Liste A muss in Liste B enthalten sein
                    bool areEqual = dtListAsSubTreePattern.ListMembers.Length <= listB.ListMembers.Length;

                    if (areEqual)
                    {
                        // Vergleich unter strikter Beachtung der Reihenfolge der Parameter
                        var contentAB = dtListAsSubTreePattern.ListMembers.Zip(listB.ListMembers, (A, B) => (A, B));

                        foreach (var pair in contentAB)
                        {
                            //areEqual &= pair.A.IsPutIntoAppropiateIsSubTreeOfTestWith(pair.B);
                            areEqual &= IsSubTreeTestForEntityType[pair.A.EntityType](
                                pair.A,
                                pair.B,
                                listB,
                                doSomethingIfPatternMatches,
                                finishSearchAfterPatternMatched,
                                searchAnywhere,
                                currentLevel + 1,
                                maxLevel);

                            if (!areEqual)
                            {
                                break;
                            }
                        }
                    }

                    res = areEqual;

                    if (res)
                    {
                        doSomethingIfPatternMatches(listB, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }

                }

                // Weitersuchen, wenn noch keine Übereinstimmung festgestellt wurde, und auch 
                // die Übereinstimmung mit Strukturen in tieferen Schichten legitim wäre
                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    res = SearchInDeeperLevels(
                        (_tree, _parent, _level, _max) => dtListAsSubTreePattern.IsSubTreeOf(
                                                _tree,
                                                _parent,
                                                doSomethingIfPatternMatches,
                                                finishSearchAfterPatternMatched,
                                                true,
                                                _level,
                                                _max),
                        tree,
                        currentLevel,
                        maxLevel);
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 4.8.2021
        /// 
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="retAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IReturn retAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int Deepth = int.MaxValue
            )
            =>
                retAsSubTreePattern.IsSubTreeOf(
                    tree,
                    tree,
                    (s, p, l) => { },
                    true,
                    searchAnywhere,
                    Deepth);


        /// <summary>
        /// mko, 30.7.2021
        /// 
        /// mko, 4.8.2021
        /// 
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="retAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IReturn retAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int curentLevel,
            int maxLevel = int.MaxValue
            )
        {
            var res = false;

            if (tree != null)
            {
                var finish = true;

                if (tree is IReturn r)
                {

                    // Returns müssen im Unterschied zu Events einen ReturnValue
                    // enthalten. 
                    // mko, 30.7.2021: Ist das noch so?

                    if (retAsSubTreePattern.IsSetToDefaultValue)
                    {
                        // Wenn das subTreePattern keinen ReturnValue hat, dann ist es immer ein 
                        // SubTree von Tree, da beide Returns sind, und ein Return ohne Parameter (= Defaultwert)
                        // Teil eines Returns mit Parameter ist
                        res = true;
                        doSomethingIfPatternMatches(r, parent, curentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                    else if (!retAsSubTreePattern.IsSetToDefaultValue && r.IsSetToDefaultValue)
                    {
                        // Das Pattern ist restriktiver als das zu untersuchende Return-> kein SubTree
                        res = false;
                    }
                    else
                    {
                        // Prüfen, ob Parameter vom Pattern im Parameter vom Return enthalten ist
                        // mko, 15.3.2021
                        // IsSubTreeOf(..., false) definiert, da sonst z.B. 
                        // ret(eSucceeded()).IsSubTreeOf(ret(eFails(....eSucceeded()))

                        //res = retAsSubTreePattern.ReturnValue.IsPutIntoAppropiateIsSubTreeOfTestWith(r.ReturnValue, false);
                        res = IsSubTreeTestForEntityType[retAsSubTreePattern.ReturnValue.EntityType](
                            retAsSubTreePattern.ReturnValue,
                            r.ReturnValue,
                            r,
                            doSomethingIfPatternMatches,
                            finishSearchAfterPatternMatched,
                            false,
                            curentLevel + 1,
                            maxLevel);

                        if (res)
                        {
                            doSomethingIfPatternMatches(r, parent, curentLevel);
                            finish = finishSearchAfterPatternMatched;
                        }
                    }

                }

                // Weitersuchen, wenn noch keine Übereinstimmung festgestellt wurde, und auch 
                // die Übereinstimmung mit Strukturen in tieferen Schichten legitim wäre
                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    res = SearchInDeeperLevels(
                        (_tree, _parent, _level, _max) => retAsSubTreePattern.IsSubTreeOf(
                                                _tree,
                                                _parent,
                                                doSomethingIfPatternMatches,
                                                finishSearchAfterPatternMatched,
                                                true,
                                                _level,
                                                _max),
                        tree,
                        curentLevel,
                        maxLevel);
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="txtAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this ITxt txtAsSubTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true,
            int maxLevel = int.MaxValue)
            =>
                txtAsSubTreePattern.IsSubTreeOf(tree, tree, (s, p, l) => { }, true, searchAnywhere, 0, maxLevel);


        /// <summary>
        /// mko, 5.8.2021
        /// </summary>
        /// <param name="txtAsSubTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="parent"></param>
        /// <param name="doSomethingIfPatternMatches"></param>
        /// <param name="finishSearchAfterPatternMatched"></param>
        /// <param name="searchAnywhere"></param>
        /// <param name="currentLevel"></param>
        /// <param name="maxLevel"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this ITxt txtAsSubTreePattern,
            IDocuEntity tree,
            IDocuEntity parent,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched,
            bool searchAnywhere,
            int currentLevel,
            int maxLevel = int.MaxValue)
        {
            var res = false;

            if (tree != null)
            {
                var finish = true;
                if (tree is ITxt txtB)
                {
                    var txtA = txtAsSubTreePattern;
                    bool areEqual = txtA.Words.Length == txtA.Words.Length;

                    if (areEqual)
                    {
                        // Vergleich unter strikter Beachtung der Reihenfolge der Parameter
                        var contentAB = txtA.Words.Zip(txtB.Words, (A, B) => (A, B));

                        foreach (var pair in contentAB)
                        {
                            areEqual &= pair.A.ValueAsString.Equals(pair.B.ValueAsString);
                            if (!areEqual)
                                break;
                        }
                    }

                    res = areEqual;

                    if (res)
                    {
                        doSomethingIfPatternMatches(txtB, parent, currentLevel);
                        finish = finishSearchAfterPatternMatched;
                    }
                }

                if (!(tree is INoSubTrees) && searchAnywhere && !(finish && res))
                {
                    res = SearchInDeeperLevels(
                        (_tree, _parent, _level, _maxLevel) => txtAsSubTreePattern.IsSubTreeOf(
                                                _tree,
                                                _parent,
                                                doSomethingIfPatternMatches,
                                                finishSearchAfterPatternMatched,
                                                true,
                                                _level,
                                                _maxLevel),
                        tree,
                        currentLevel,
                        maxLevel);

                }
            }

            return res;

        }


        /// <summary>
        /// mko, 30.7.2021
        /// 
        /// mko, 4.8.2021
        /// 
        /// </summary>
        /// <param name="isSubTreeOf"></param>
        /// <param name="tree"></param>
        /// <param name="Deepth"></param>
        /// <returns></returns>
        private static bool SearchInDeeperLevels(
            Func<IDocuEntity, IDocuEntity, int, int, bool> isSubTreeOf,
            IDocuEntity tree,
            //IDocuEntity parentTree,
            int currentLevel,
            int maxLevel)
        {
            var res = false;

            // Prüfen, ob die Tiefe nicht beschränkt ist
            if (currentLevel <= maxLevel)
            {
                // Nur DocuTerme betrachten, die komplexe Unterstrukturen wie IInstance aufnehmen 
                // können (String, NID etc. gehören z.B. nicht dazu)

                if (tree is IInstance i)
                {
                    // Es wird davon ausgegangen, das die Instanz immer Instanzmember hat
                    // (wenn keine, dann eine leere Liste als Default- Wert)
                    foreach (var member in i.InstanceMembers)
                    {
                        if (isSubTreeOf(member, i, currentLevel + 1, maxLevel))
                        {
                            res = true;
                            break;
                        }
                    }
                }
                else if (tree is IMethod m)
                {
                    // Es wird davon ausgegangen, das die Methode immer Parameter hat
                    // (wenn keine, dann eine leere Liste als Default- Wert)
                    foreach (var parameter in m.Parameters)
                    {
                        if (isSubTreeOf(parameter, m, currentLevel + 1, maxLevel))
                        {
                            res = true;
                            break;
                        }
                    }
                }
                else if (tree is IDTList lst)
                {
                    // Es wird davon ausgegangen, das die Liste immer Listmember hat
                    // (wenn keine, dann eine leere Liste als Default- Wert)
                    foreach (var lstMember in lst.ListMembers)
                    {
                        if (isSubTreeOf(lstMember, lst, currentLevel + 1, maxLevel))
                        {
                            res = true;
                            break;
                        }
                    }
                }
                else if (tree is IProperty p)
                {
                    // Es wird davon ausgegangen, das die Eigenschaft immer einen Wert hat
                    // (wenn keinen, dann einen Default- Wert)
                    res = isSubTreeOf(p.PropertyValue, p, currentLevel + 1, maxLevel);
                }
                else if (tree is IEvent e)
                {
                    // Es wird davon ausgegangen, dass das Event immer einen Wert hat
                    // (wenn keinen, dann einen Default- Wert)
                    res = isSubTreeOf(e.EventParameter, e, currentLevel, maxLevel);
                }
                else if (tree is IReturn ret)
                {
                    // Es wird davon ausgegangen, das das Return immer einen Wert hat
                    // (wenn keinen, dann einen Default- Wert)
                    res = isSubTreeOf(ret.ReturnValue, ret, currentLevel + 1, maxLevel);
                }
                else if (tree is ITxt txt)
                {
                    foreach (var word in txt.Words)
                    {
                        // Es wird davon ausgegangen, das das Return immer einen Wert hat
                        // (wenn keinen, dann einen Default- Wert)
                        res = isSubTreeOf(word, txt, currentLevel + 1, maxLevel);
                    }
                }
            }

            return res;
        }


        /// <summary>
        /// mko, 30.6.2020
        /// Prüft, ob die in einem Instanzpattern definierte Memberliste in der Memberliste der zu untersuchenden 
        /// Instanz enthalten ist.
        /// </summary>
        /// <param name="aMembers"></param>
        /// <param name="bMembers"></param>
        /// <returns></returns>
        static bool PartiallyEqualInstanceMembers(
            IInstance InstanceA,
            IInstance InstanceB,
            int currentLevel,
            int maxLevel,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched = true)
        {

            var aMembers = InstanceA.InstanceMembers != null ? InstanceA.InstanceMembers : new IInstanceMember[] { };
            var bMembers = InstanceB.InstanceMembers != null ? InstanceB.InstanceMembers : new IInstanceMember[] { };

            // Haben Muster und zu untersuchende Instanz beide keine Parameterliste, dann sind sie gleich
            bool areEqual = !aMembers.Any() && !bMembers.Any();

            if (!areEqual && !aMembers.Any())
            {
                // Das Pattern hat keien Parameterliste -> Stimmt in jedem Fall mit der Instanz überein
                areEqual = true;
            }
            else if (!areEqual && aMembers.Length > bMembers.Length)
            {
                // Die rechte Instanz muss mindestens soviele Member haben wie das Pattern (rechte enthält Pattern).
                areEqual = false;
            }
            else if (!areEqual)
            {
                // Vergleich der nichtleeren Memberlisten (Reihenfolge ist ohne Bedeutung)

                // Annahme: Memberlisten sind gleich
                areEqual = true;
                var bList = new List<IListMember>(bMembers);
                foreach (var a in aMembers)
                {
                    bool found = false;
                    foreach (var b in bList)
                    {
                        //if (a.IsPutIntoAppropiateIsSubTreeOfTestWith(b))
                        if (IsSubTreeTestForEntityType[a.EntityType]
                            (a, b, InstanceB, doSomethingIfPatternMatches, finishSearchAfterPatternMatched, false, currentLevel + 1, maxLevel))
                        {
                            // Wurde ein identischer Member in b gefunden, dann wird dieser aus der bList entfernt,
                            // und der Vergleich gilt als für a erfolgreich abgeschlossen.
                            bList.Remove(b);
                            found = true;
                            break;
                        }
                    }

                    if (!found)
                    {
                        // konnte zu einem Parameter aus aMembers kein gleicher in bMembers gefunden werden,
                        // dann sind die beiden Parameterlisten ungleich. Der Vergleich kann mit negativem 
                        // Ergebnis beendet werden.
                        areEqual = false;
                        break;
                    }
                }
            }

            return areEqual;
        }

        /// <summary>
        /// mko, 20.6.2020
        /// Prüft, ob die in einem Methodenpattern definierte Parameterliste in der Parameterliste der zu untersuchenden 
        /// Methode enthalten ist.
        /// </summary>
        /// <param name="aParams"></param>
        /// <param name="bParams"></param>
        /// <returns></returns>
        static bool PartiallyEqualMethodParameters(
            IMethod methodA,
            IMethod methodB,
            int currentLevel,
            int maxLevel,
            Action<IDocuEntity, IDocuEntity, int> doSomethingIfPatternMatches,
            bool finishSearchAfterPatternMatched = true)
        {
            var aParams = methodB.Parameters != null ? methodA.Parameters : new IMethodParameter[] { };
            var bParams = methodB.Parameters != null ? methodB.Parameters : new IMethodParameter[] { };

            // Haben Muster und zu untersuchende Methode beide keine Parameterliste, dann sind sie gleich
            bool isSubTree = !aParams.Any() && !bParams.Any();

            // Wildcards spielen beim Vergleich von Parameterlisten keine Rolle.
            if (!isSubTree && !aParams.Any())
            {
                // Das Pattern hat keien Parameterliste -> Stimmt in jedem Fall mit der Methode überein
                isSubTree = true;
            }
            else if (!isSubTree && aParams.Length > bParams.Length)
            {
                // Die Parameterliste des Patterns is größer als die der zu untersuchenden Methode -> 
                // Pattern kann damit nicht mehr in der Methode enthalten sein.
                isSubTree = false;
            }
            else if (!isSubTree)
            {
                // Vergleich der Parameterlisten (Reihenfolge ist von Bedeutung)
                var treeEnum = bParams.GetEnumerator();

                // all subC must be contained in treeRootChilds
                var foundSubTreeNodes = 0;
                foreach (var subC in aParams)
                {
                    bool isSub = false;

                    // compare subChild child by child with treeChildren until subChild matches or all treeChildrens are checked.
                    if (treeEnum.MoveNext())
                    {
                        do
                        {
                            var tc = (IMethodParameter)treeEnum.Current;

                            // hier false notwendig, damit sichergestellt wird, das Wurzel vom Subtree
                            // mit der Wurzel vom Tree übereinstimmen müssen.
                            //isSub = subC.IsPutIntoAppropiateIsSubTreeOfTestWith(tc, false);
                            isSub = IsSubTreeTestForEntityType[subC.EntityType]
                                (subC, tc, methodB, doSomethingIfPatternMatches, finishSearchAfterPatternMatched, false, currentLevel + 1, maxLevel);

                        } while (!isSub && treeEnum.MoveNext());
                    }

                    // break, if current subTreeChild node does not match with any child node of tree
                    if (isSub)
                        foundSubTreeNodes++;
                }

                // sicherstellen, das alle Kindknoten des subTrees im Tree enthalten sind
                isSubTree = foundSubTreeNodes == aParams.Count();
            }

            return isSubTree;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Returns true, if e is named with given name, and is of given type
        /// </summary>
        /// <param name="e"></param>
        /// <param name="eType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool IsEntityOfTypeWithName(this IDocuEntity e, DocuEntityTypes eType, string name)
            => e.EntityType == eType && e.IsNamed() && e.Name() == name;


        /// <summary>
        /// mko, 28.2.2019
        /// Finds a given docu entity in a given tree or not. Search is top/down. Returns the first matching entity.
        /// </summary>
        /// <param name="node">docuEntity to find</param>
        /// <param name="tree">tree where to find the docuEntitiy</param>
        /// <returns>docuEntity as embedded node in tree</returns>
        public static IDocuEntity FindIn(this IDocuEntity node, IDocuEntity tree)
        {
            IDocuEntity instance = null;

            if (node.IsEqualTo(tree))
                instance = node;
            else
                IsSubTreeTestForEntityType[node.EntityType](
                    node,
                    tree,
                    tree,
                    (s, p, l) => instance = s,
                    true, true, 0, int.MaxValue);

            return instance;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Finds a given docu entity in a given tree or not. Search is top/down. Returns the first matching entity.
        /// 
        /// mko, 6.8.2021
        /// Reimplementiert mit streng typisierten Funktionen.
        /// </summary>
        /// <param name="node">docuEntity to find</param>
        /// <param name="tree">tree where to find the docuEntitiy</param>
        /// <returns>docuEntity as embedded node in tree</returns>
        public static (IDocuEntity treeNode, IDocuEntity treeNodeParent, long deepth) FindNodeAndPositionIn(this IDocuEntity node, IDocuEntity tree, long deepth = 0)
        {
            IDocuEntity instance = null;
            IDocuEntity treeNodeParent = null;
            long _deepth = deepth;

            if (node.IsEqualTo(tree))
                instance = node;
            else
                IsSubTreeTestForEntityType[node.EntityType](
                    node,
                    tree,
                    tree,
                    (s, p, l) => { instance = s; treeNodeParent = p; _deepth = deepth; },
                    true, true, 0, int.MaxValue);

            return (instance, treeNodeParent, deepth);
        }


        /// <summary>
        /// mko, 11.3.2019
        /// Returns all nodes matching entity.
        /// 
        /// mko, 6.8.2021
        /// Reimplementiert mit streng typisierten Funktionen
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="tree"></param>
        /// <param name="matches"></param>
        /// <param name="depth">Rekursionstiefe</param>
        /// <returns>Liste aus (IDocEntity, depth) Wertepaare. Depth gibt an, wieviele Stufen im Baum unterhalb des 
        /// Entities das passende Element gefunden wurde</returns>
        public static List<(IDocuEntity node, int depth)> FindAllIn(this IDocuEntity entity, IDocuEntity tree, List<(IDocuEntity node, int depth)> matches = null, int depth = 0)
        {
            if (matches == null)
            {
                matches = new List<(IDocuEntity node, int depth)>();
            }

            IsSubTreeTestForEntityType[entity.EntityType](
                entity,
                tree,
                tree,
                (s, p, l) => matches.Add((s, l)),
                false,
                true,
                0,
                int.MaxValue);

            return matches;
        }

        /// <summary>
        /// mko, 18.4.2018
        /// Search for a farest descendant of an entity with defined type and name.
        /// 
        /// mko, 6.8.2021
        /// Reimplementiert mittels SearchInDeeperLevels
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IDocuEntity FindNamedEntity(this IDocuEntity root, DocuEntityTypes dType, string name)
        {
            IDocuEntity instance = null;

            if (root.IsNamed() && root.EntityType == dType && root.HasName(name))
            {
                instance = root;
            }
            else
            {
                SearchInDeeperLevels(
                        (s, p, l, m) =>
                        {
                            var e = FindNamedEntity(s, dType, name);
                            if(e != null)
                            {
                                instance = e;
                            }
                            return (e != null);

                        },
                        root,
                        0,
                        int.MaxValue
                    );
            }

            return instance;
        }

        /// <summary>
        /// mko, 18.8.2018
        /// Search for a child of an entity with defined type and name. Depth of Search will be restricted.
        /// 
        /// mko, 6.8.2021
        /// Reimplementiert mittels `SearchInDeeperLevels`
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IDocuEntity FindNamedEntity(this IDocuEntity root, DocuEntityTypes dType, string name, int UpToLevel)
        {
            IDocuEntity instance = null;


            if (root.IsNamed() && root.EntityType == dType && root.HasName(name))
            {
                instance = root;
            }
            else if(UpToLevel >= 0)
            {
                SearchInDeeperLevels(
                        (s, p, l, m) =>
                        {
                            var e = FindNamedEntity(s, dType, name, UpToLevel - 1);
                            if (e != null)
                            {
                                instance = e;
                            }
                            return (e != null);
                        },
                        root,
                        0,
                        UpToLevel
                    );
            }

            return instance;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Search for all descendants of an entity with defined type and name.
        /// </summary>
        /// <param name="root"></param>
        /// <param name="dType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static IEnumerable<IDocuEntity> FindAllNamedEntities(this IDocuEntity root, DocuEntityTypes dType, string name)
        {
            var res = new List<IDocuEntity>();
            

            if (root.IsNamed() && root.EntityType == dType && root.HasName(name))
            {
                res.Add(root);
            }
            else
            {
                SearchInDeeperLevels(
                        (s, p, l, m) =>
                        {
                            res.AddRange(FindAllNamedEntities(s, dType, name));

                            // Durch `return false` wird in `SearchInDeeperLevels` sichergestellt, das alle Zweige im Baum
                            // untersucht werden
                            return false;

                        },
                        root,
                        0,
                        int.MaxValue
                    );
            }

            return res;
        }
    }
}
