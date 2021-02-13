using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;


//using TechTerms = MKPRG.Tracing.DocuTerms.Composer.TechTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 28.2.2019
    /// Fasst Mehtoden zusammen, mit denen unterhalb eines DocuEntity nach Substrukturen gesucht werden kann
    /// </summary>
    public static partial class DocuEntityHlp
    {
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
                        if (eventA.EventParameter == null && eventB.EventParameter == null)
                            // Beide Events ohne Parameter -> Gleichheit
                            return true;
                        else if (eventA.EventParameter == null && eventB.EventParameter != null)
                            // Pattern als Event ohne Parameter ist auch in Events mit  
                            // Parameter enthalten -> ist SubTree
                            return true;
                        else if (eventA.EventParameter != null && eventB.EventParameter == null)
                            // Das Pattern ist restriktiver als das zu untersuchende Event-> kein SubTree
                            return false;                        
                        else
                            return eventA.EventParameter.IsEqualTo(eventB.EventParameter);
                    }
                    else return false;
                }
                else
                {
                    if (a is NID nidA && b is NID nidB)
                    {
                        return nidA.NamingId == nidB.NamingId;
                    }
                    else if (a is IReturn retA && b is IReturn retB)
                    {
                        return retA.ReturnValue.IsEqualTo(retB.ReturnValue);
                    }
                    else if (a is Integer intA && b is Integer intB)
                    {
                        return intA.ValueAsLong == intB.ValueAsLong;
                    }
                    else if (a is Double dblA && b is Double dblB)
                    {
                        return dblA.Value == dblB.Value;
                    }
                    else if (a is Boolean boolA && b is Boolean boolB)
                    {
                        return boolA.ValueAsBool == boolB.ValueAsBool;
                    }
                    else if (a is String strA && b is String strB)
                    {
                        return strA.Value == strB.Value;
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
                                areEqual &= pair.A.Value.Equals(pair.B.Value);
                                if (!areEqual)
                                    break;
                            }
                        }

                        return areEqual;
                    }
                    else if (a is DTDate aDat && b is DTDate bDat)
                    {
                        return aDat.Year == bDat.Year && aDat.Month == bDat.Month && aDat.Day == bDat.Day;
                    }
                    else if (a is DTTime aTime && b is DTTime bTime)
                    {
                        return aTime.Hour == bTime.Hour && aTime.Minutes == bTime.Minutes && aTime.Seconds == bTime.Seconds && aTime.Milliseconds == bTime.Milliseconds;
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
                                areEqual &= pair.A.Value.Equals(pair.B.Value);
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
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <param name="tree"></param>
        /// <param name="searchAnywhere"></param>
        /// <returns></returns>
        public static bool IsSubTreeOf(
            this IDocuEntity subTreePattern,
            IDocuEntity tree,
            bool searchAnywhere = true)
        {
            var res = false;

            if (tree != null)
            {
                var a = subTreePattern;
                var b = tree;

                if (a is IWildCard wc)
                {
                    // Hat die Wilccard eine Subtree- Einschränkung
                    if (wc.Childs.Any())
                    {
                        res = wc.Childs.First().IsSubTreeOf(tree);
                    }
                    else
                    {
                        res = true;
                    }
                }
                else if (a.EntityType == b.EntityType)
                {
                    if (a.IsNamed() && b.IsNamed())
                    {
                        // Namen auf Gleichheit prüfen
                        if (!a.AreOfSameName(b))
                        {
                            // Unterschiedliche Namen => keine Gleichheit
                            res = false;
                        }
                        else if (a is IMethod aMeth && b is IMethod bMeth)
                        {
                            res = PartiallyEqualMethodParameters(aMeth.Parameters, bMeth.Parameters);
                        }
                        else if (a is IInstance aInstance && b is IInstance bInstance)
                        {
                            res = PartiallyEqualInstanceMembers(aInstance.InstanceMembers, bInstance.InstanceMembers);
                        }
                        else if (a is IProperty propA && b is IProperty propB)
                        {
                            // Properties müssen im Unterschied zu Events immer einen Propertyvalue 
                            // enthalten. Im Pattern kann der PropertyValue durch einen Wildcard ersetzt
                            // werden.
                            res = propA.PropertyValue.IsSubTreeOf(propB.PropertyValue);
                        }
                        else if (a is IEvent eventA && b is IEvent eventB)
                        {
                            if (eventA.EventParameter == null && eventB.EventParameter == null)
                                // Beide Events ohne Parameter -> Gleichheit
                                res = true;
                            else if (eventA.EventParameter == null && eventB.EventParameter != null)
                                // Pattern als Event ohne Parameter ist auch in Events mit  
                                // Parameter enthalten -> ist SubTree
                                res = true;
                            else if (eventA.EventParameter != null && eventB.EventParameter == null)
                                // Das Pattern ist restriktiver als das zu untersuchende Event-> kein SubTree
                                res = false;
                            else
                                // Prüfen, ob Parameter vom Pattern im Parameter vom Event enthalten ist
                                res = eventA.EventParameter.IsSubTreeOf(eventB.EventParameter);
                        }
                        else res = false;
                    }
                    else
                    {
                        if (a is NID nidA && b is NID nidB)
                        {
                            res = nidA.NamingId == nidB.NamingId;
                        }
                        else if (a is IReturn retA && b is IReturn retB)
                        {
                            // Returns müssen im Unterschied zu Events einen ReturnValue
                            // enthalten. 
                            res = retA.ReturnValue.IsSubTreeOf(retB.ReturnValue);
                        }
                        else if (a is Integer intA && b is Integer intB)
                        {
                            res = intA.ValueAsLong == intB.ValueAsLong;
                        }
                        else if (a is Double dblA && b is Double dblB)
                        {
                            res = dblA.Value == dblB.Value;
                        }
                        else if (a is Boolean boolA && b is Boolean boolB)
                        {
                            res = boolA.ValueAsBool == boolB.ValueAsBool;
                        }
                        else if (a is String strA && b is String strB)
                        {
                            res = strA.Value == strB.Value;
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
                                    areEqual &= pair.A.Value.Equals(pair.B.Value);
                                    if (!areEqual)
                                        break;
                                }
                            }

                            res = areEqual;
                        }
                        else if (a is DTDate aDat && b is DTDate bDat)
                        {
                            res = aDat.Year == bDat.Year && aDat.Month == bDat.Month && aDat.Day == bDat.Day;
                        }
                        else if (a is DTTime aTime && b is DTTime bTime)
                        {
                            res = aTime.Hour == bTime.Hour && aTime.Minutes == bTime.Minutes && aTime.Seconds == bTime.Seconds && aTime.Milliseconds == bTime.Milliseconds;
                        }
                        else if (a is IDTList listA && b is IDTList listB)
                        {
                            // Liste A muss in Liste B enthalten sein
                            bool areEqual = listA.ListMembers.Length <= listA.ListMembers.Length;

                            if (areEqual)
                            {
                                // Vergleich unter strikter Beachtung der Reihenfolge der Parameter
                                var contentAB = listA.ListMembers.Zip(listB.ListMembers, (A, B) => (A, B));

                                foreach (var pair in contentAB)
                                {
                                    areEqual &= pair.A.IsSubTreeOf(pair.B);
                                    if (!areEqual)
                                        break;
                                }
                            }

                            res = areEqual;
                        }
                        else
                        {
                            res = false;
                        }
                    }
                }
                else if (a is NID nidA_ && b is String str_B)
                {
                    // mko, 14.7.2020
                    res = RCV3.NC[nidA_.NamingId].CNT == str_B.Value;
                }
                else if (a is String strA_ && b is NID nid_B)
                {
                    // mko, 14.7.2020
                    res = strA_.Value == RCV3.NC[nid_B.NamingId].CNT;
                }


                if (!res && searchAnywhere)
                {
                    foreach (var child in tree.Childs)
                    {
                        if (subTreePattern.IsSubTreeOf(child))
                        {
                            res = true;
                            break;
                        }
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
        static bool PartiallyEqualInstanceMembers(IInstanceMember[] aMembers, IInstanceMember[] bMembers)
        {
            // Haben Muster und zu untersuchende Instanz beide keine Parameterliste, dann sind sie gleich
            bool areEqual = aMembers == null && bMembers == null;

            if (!areEqual && aMembers == null)
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
                        if (a.IsSubTreeOf(b))
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
        static bool PartiallyEqualMethodParameters(IMethodParameter[] aParams, IMethodParameter[] bParams)
        {
            // Haben Muster und zu untersuchende Methode beide keine Parameterliste, dann sind sie gleich
            bool isSubTree = aParams == null && bParams == null;

            // Wildcards spielen beim Vergleich von Parameterlisten keine Rolle.
            if (!isSubTree && aParams == null)
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
                            isSub = subC.IsSubTreeOf(tc, false);

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
        /// mko, 29.3.2019
        /// Eine Baumstruktur als Teilbaum (Muster) in einem anderen Baume suchen. Wenn das Muster auf einem Zweig im anderen Baum passt, die Wurzel dieses Zweiges
        /// zurückgeben.
        /// Die Suche erfolgt top-down.
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <param name="treeRoot"></param>
        /// <param name="searchAnywhere">Wenn false, dann muss der Baumn mit dem Teilbaumabschnitt beginnen. sonst wird nach dem ersten Teilbaum linksrekursiv gesucht</param>
        /// <param name="PropertyValueWildCard">Bei Properties im subTreePattern mit diesem Wert muss nur der Eigenschaftsname übereinstimmen, nicht jedoch der Wert</param>
        /// <returns></returns>
        public static RCV3sV<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)> AsSubTreeOf(
            this IDocuEntity subTreePattern,
            IDocuEntity treeRoot,
            IComposer pnL,
            bool searchAnywhere = true,
            long deepth = 0,
            IDocuEntity subTreeParent = null)
        {
            var ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Failed(value: (null, null, -1), ErrorDescription: pnL.ReturnNotCompleted("SearchAsSubTreeOf"));

            if (subTreePattern.IsSubTreeOf(treeRoot, false))
            {
                ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Ok(value: (treeRoot, subTreeParent, deepth));
            }
            else if (searchAnywhere)
            {
                // Der Treenode selbst stimmt nicht mit dem subTreePattern überein.
                // Weiter in der Tiefe des Baumes nach einem Subtree suchen

                var TreeRootChilds = treeRoot.IsNamed() ? treeRoot.Childs.Skip(1) : treeRoot.Childs;

                if (TreeRootChilds.Any())
                {
                    foreach (var subC in TreeRootChilds)
                    {
                        ret = AsSubTreeOf(subTreePattern, subC, pnL, true, deepth + 1, treeRoot);

                        if (ret.Succeeded)
                        {
                            break;
                        }
                    }
                }
                else
                {
                    // Im Baum wurde keine Übereinstimmung mit der Teilbaumstruktur gefunden
                    ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Failed(
                        value: (null, null, -1),
                        ErrorDescription: pnL.ReturnSearchFailsEmptyResult(pnL.EncapsulateAsPropertyValue(subTreePattern)));
                }
            }
            else
            {


                // Im Baum wurde keine Übereinstimmung mit der Teilbaumstruktur gefunden
                ret = RCV3sV<(IDocuEntity, IDocuEntity, long)>.Failed(
                    value: (null, null, -1),
                    ErrorDescription: pnL.ReturnSearchFailsEmptyResult(pnL.EncapsulateAsPropertyValue(subTreePattern)));
            }

            return ret;
        }

        /// <summary>
        /// mko, 1.4.2019
        /// Sucht alle Teilbäume in einem Baum mit gegebener Struktur.
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <param name="treeRoot"></param>
        /// <param name="pnL"></param>
        /// <param name="deepth"></param>
        /// <param name="PropertyValueWildCard">Bei Properties im subTreePattern mit diesem Wert muss nur der Eigenschaftsname übereinstimmen, nicht jedoch der Wert</param>
        /// <returns></returns>
        public static RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>> AsSubTreeOf_AllOccurrences
            (this IDocuEntity subTreePattern,
            IDocuEntity treeRoot,
            IComposer pnL,
            long deepth = 0,
            IDocuEntity parent = null)
        {
            Debug.Assert(pnL != null);

            var matches = new List<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>();

            var ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(
                value: matches,
                ErrorDescription: pnL.ReturnNotCompleted(
                                        "AsSubTreeOf_AllOccurrences",
                                        pnL.KillIf(subTreePattern == null, () => pnL.p(ANC.TechTerms.Trees.SubTree.UID, pnL.EncapsulateAsPropertyValue(subTreePattern))),
                                        pnL.KillIf(treeRoot == null, () => pnL.p(ANC.TechTerms.Trees.Root.UID, pnL.EncapsulateAsPropertyValue(treeRoot))),
                                        pnL.p(ANC.TechTerms.Trees.Level.UID, deepth)));

            try
            {

                // Prüfen von Vorbedingungen, die in der komplexen Umgebung des Aufrufes nicht notwendigerweise erfüllt sein müssen
                // da treeRoot und subTreePattern Ergebnisse von vorausgegangenen Berechnungen sein können.
                // Deshalb ist Debug.Assert hier nicht ausreichend.
                if (subTreePattern == null)
                {
                    return RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(
                        value: matches,
                        ErrorDescription: pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(ANC.DocuTerms.MetaData.Arg.UID, ANC.TechTerms.Trees.SubTree.UID)));
                }
                else if (treeRoot == null)
                {
                    return RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(
                        value: matches,
                        ErrorDescription: pnL.ReturnValidatePreconditionNotNullFailed(pnL.p(ANC.DocuTerms.MetaData.Arg.UID, ANC.TechTerms.Trees.Root.UID)));
                }
                else
                {

                    var first = subTreePattern.AsSubTreeOf(treeRoot, pnL, false, deepth, parent);
                    if (first.Succeeded)
                    {
                        matches.Add(first.Value);
                    }

                    if (first.Succeeded || !first.Succeeded && pnL.ReturnSearchFailsEmptyResult().IsSubTreeOf(first.MessageEntity, true))
                    {
                        ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Ok(matches);

                        // Weitere Teilbäume innerhalb des aktuellen Teilbaumes suchen
                        foreach (var child in treeRoot.Childs)
                        {
                            var getAllSubtrees = subTreePattern.AsSubTreeOf_AllOccurrences(child, pnL, deepth + 1, treeRoot);
                            if (getAllSubtrees.Succeeded)
                            {
                                matches.AddRange(getAllSubtrees.Value);
                            }
                            else
                            {
                                ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(matches, getAllSubtrees.ToPlx());
                                break;
                            }
                        }

                        if (ret.Succeeded)
                        {
                            // Rückgabewert mit den allen Treffern aktualisieren
                            ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Ok(matches);
                        }
                    }
                    else
                    {
                        // Fall: In der Suche ging was schief
                        ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(value: matches, ErrorDescription: first.ToPlx());
                    }

                }
            }
            catch (Exception ex)
            {
                ret = RCV3sV<IEnumerable<(IDocuEntity subTree, IDocuEntity subTreeParent, long depth)>>.Failed(value: matches, ErrorDescription: TraceHlp.FlattenExceptionMessagesPN(ex));
            }

            return ret;
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
            {
                foreach (var child in tree.Childs)
                {
                    // mko, 12.11.2018
                    // Now more robust in case of empty child lists
                    //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                    if (node.IsEqualTo(child))
                    {
                        instance = child;
                    }
                }

                if (instance == null)
                {
                    // Search for instance a level deeper
                    foreach (var child in tree.Childs)
                    {
                        instance = node.FindIn(child);
                        if (instance != null)
                        {
                            // Found an Instance
                            break;
                        }
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Finds a given docu entity in a given tree or not. Search is top/down. Returns the first matching entity.
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
            {
                instance = node;
            }
            else
            {
                foreach (var child in tree.Childs)
                {
                    // mko, 12.11.2018
                    // Now more robust in case of empty child lists
                    //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                    if (node.IsEqualTo(child))
                    {
                        treeNodeParent = tree;
                        instance = child;
                        deepth += 1;
                        break;
                    }
                }

                if (instance == null)
                {
                    // Search for instance a level deeper
                    foreach (var child in tree.Childs)
                    {
                        (instance, treeNodeParent, deepth) = node.FindNodeAndPositionIn(child, deepth + 1);
                        if (instance != null)
                        {
                            // Found an Instance
                            break;
                        }
                    }
                }
            }

            return (instance, treeNodeParent, deepth);
        }


        /// <summary>
        /// mko, 11.3.2019
        /// Returns all nodes matching entity.
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

            if (entity.IsEqualTo(tree))
                matches.Add((tree, depth));

            foreach (var child in tree.Childs)
            {
                entity.FindAllIn(child, matches, depth + 1);
            }

            return matches;

        }


        /// <summary>
        /// mko, 18.4.2018
        /// Search for a farest descendant of an entity with defined type and name.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IDocuEntity FindNamedEntity(this IDocuEntity root, DocuEntityTypes dType, string name)
        {
            IDocuEntity instance = null;
            foreach (var child in root.Childs)
            {
                // mko, 12.11.2018
                // Now more robust in case of empty child lists
                //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                if (child.IsEntityOfTypeWithName(dType, name))
                {
                    instance = child;
                }
            }

            if (instance == null)
            {
                // Search for instance a level deeper
                foreach (var child in root.Childs)
                {
                    instance = FindNamedEntity(child, dType, name);
                    if (instance != null)
                    {
                        // Found an Instance
                        break;
                    }
                }
            }

            return instance;
        }

        /// <summary>
        /// mko, 18.8.2018
        /// Search for a child of an entity with defined type and name. Depth of Search will be restricted.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="root"></param>
        /// <returns></returns>
        public static IDocuEntity FindNamedEntity(this IDocuEntity root, DocuEntityTypes dType, string name, int UpToLevel)
        {
            IDocuEntity instance = null;
            foreach (var child in root.Childs)
            {
                // mko, 12.11.2018
                // Now more robust in case of emptiy child lists
                //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                if (child.IsEntityOfTypeWithName(dType, name))
                {
                    instance = child;
                }
            }

            UpToLevel--;

            if (instance == null && UpToLevel > 0)
            {
                // Search for instance a level deeper
                foreach (var child in root.Childs)
                {
                    instance = FindNamedEntity(child, dType, name, UpToLevel);
                    if (instance != null)
                    {
                        // Found an Instance
                        break;
                    }
                }
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
            var instances = new List<IDocuEntity>();
            foreach (var child in root.Childs)
            {
                // mko, 12.11.2018
                // Now more robust in case of emptiy child lists
                //if (child.EntityType == dType && child.Childs.FirstOrDefault()?.Value == name)
                if (child.IsEntityOfTypeWithName(dType, name))
                {
                    instances.Add(child);
                }
            }

            // Search for instance a level deeper
            foreach (var child in root.Childs)
            {
                instances.AddRange(FindAllNamedEntities(child, dType, name));
            }

            return instances;
        }
    }
}
