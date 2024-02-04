using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

using MKPRG.Tracing.DocuTerms;
using MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 7.3.2018
    /// </summary>
    public static partial class DocuEntityHlp
    {

        /// <summary>
        /// mko, 27.2.2019
        /// Verpackt DocuEntity in einen Decorator, über den es mittels Linq- artiger Ausdrücke 
        /// untersucht werden kann.
        /// 
        /// mko, 10.9.2021
        /// Entfernt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public static DocuEntityLinqDeco AsLinq(this IDocuEntity entity)
        //{
        //    return new DocuEntityLinqDeco(entity);
        //}


        public static HashSet<DocuEntityTypes> ValidPropertyValueTypes = new HashSet<DocuEntityTypes>()
        {
                DocuEntityTypes.Instance,
                DocuEntityTypes.Text,

                // mko, 26.3.2018: Strings allowed again
                DocuEntityTypes.String,
                DocuEntityTypes.List,

                // mko, 9.6.2020
                DocuEntityTypes.Bool,
                DocuEntityTypes.Int,
                DocuEntityTypes.NID,
                DocuEntityTypes.Float,

                // mko, 15.6.2020
                DocuEntityTypes.WildCard
        };

        public static bool IsValidPropertyValue(DocuEntityTypes type)
        {
            return ValidPropertyValueTypes.Contains(type);
        }


        public static bool IsValidMethodParameterType(DocuEntityTypes type)
        {
            // mko, 21.12.2018
            // Parameter eine Methode müssen, falls vorhanden, in einer Liste eingeschlossen sein   
            return type == DocuEntityTypes.List;

        }

        static HashSet<DocuEntityTypes> ValidMethodParameterListTypes = new HashSet<DocuEntityTypes>()
        {
            DocuEntityTypes.Property,
            DocuEntityTypes.ReturnValue,
            DocuEntityTypes.Event,
            //DocuEntityTypes.List,
            //DocuEntityTypes.Time,
            //DocuEntityTypes.Version,
            //DocuEntityTypes.Date
        };

        public static bool IsValidMethodParameterListMember(DocuEntityTypes type)
        {
            // mko, 18.10.2018
            // mko, 21.12.2018
            // IsValidPropertyValue(type) ersetzt durch type == DocuEntityTypes.Property
            // erweitert um || type == DocuEntityTypes.ReturnValue || type == DocuEntityTypes.Event;
            //return IsValidPropertyValue(type) || type == DocuEntityTypes.ReturnValue || type == DocuEntityTypes.Event;

            return ValidMethodParameterListTypes.Contains(type);
        }


        public static bool IsValidInstanceMember(DocuEntityTypes type)
        {
            return type == DocuEntityTypes.List;
        }

        static HashSet<DocuEntityTypes> ValidListMembers = new HashSet<DocuEntityTypes>()
        {
            DocuEntityTypes.List,
            DocuEntityTypes.Instance,
            DocuEntityTypes.Property,
            DocuEntityTypes.PropertySet,
            DocuEntityTypes.Method,
            DocuEntityTypes.Version,
            DocuEntityTypes.ReturnValue,
            DocuEntityTypes.Date,
            DocuEntityTypes.Time,
            DocuEntityTypes.Event
        };

        /// <summary>
        /// mko, 5.6.2019
        /// Date und Time als gültige Listenelemente zugelassen
        /// </summary>
        /// <param name="type"></param>
        /// <returns></returns>
        public static bool IsValidListMember(DocuEntityTypes type)
        {
            return ValidListMembers.Contains(type);
        }


        /// <summary>
        /// mko, 9.6.2020
        /// Wenn der Name als NamingId (NID) definiert wurde, dann kann seine Aussprache in einer wählbaren Sprache abgerufen werden.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lng"></param>
        public static string Name(this IDocuEntity entity, ANC.Language lng = ANC.Language.CNT)
        {
            return Name(entity, lng, RC.NC);
        }

        /// <summary>
        /// mko, 9.6.2020
        /// Wenn der Name als NamingId (NID) definiert wurde, dann kann seine Aussprache in einer wählbaren Sprache abgerufen werden.
        /// 
        /// mko, 
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lng"></param>
        /// <param name="NC"></param>
        /// <returns></returns>
        public static string Name(this IDocuEntity entity, ANC.Language lng, IReadOnlyDictionary<long, ANC.INaming> NC)
        {
            // check, if Name exists

            TraceHlp.ThrowArgExIfNot(entity.IsNamed(),
                RC.pnL.ReturnAfterFailureWithDetails(
                    "DocuEntityHlp_Name",
                    RC.pnL.i(TTD.Formatting.Errors.TriedToRequestANameOfAnEntityThatIsUnnamed.UID),
                    RC.pnL.p(TTD.Types.DocuTerms.UID, RC.pnL.EncapsulateAsPropertyValue(entity))));

            var name = "";

            if (entity is IDocuEntityWithNameAsNid dtNid)
                name = NC[dtNid.DocuTermNid.NamingId].NameIn(lng);
            else if (entity is IDocuTermWithNameAsString dtStr)
                name = dtStr.DocuTermName;
            else
                TraceHlp.ThrowArgEx(RC.pnL.i(TTD.Formatting.Errors.TriedToRequestANameOfAnEntityThatLacksInterfacesForNameAccess.UID));

            return name;
        }

        /// <summary>
        /// mko, 26.1.2021
        /// 
        /// mko, 28.7.2021
        /// Reimplementiert in streng typisierter Form
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="NC"></param>
        /// <returns></returns>
        public static string Glyph(this IDocuEntity entity, IReadOnlyDictionary<long, ANC.INaming> NC)
        {
            TraceHlp.ThrowArgExIfNot(entity.IsNamed(),
                RC.pnL.ReturnAfterFailureWithDetails(
                    "DocuEntityHlp_Glyph",
                    RC.pnL.i(TTD.Formatting.Errors.TriedToRequestANameOfAnEntityThatIsUnnamed.UID),
                    RC.pnL.p(TTD.Types.DocuTerms.UID, RC.pnL.EncapsulateAsPropertyValue(entity))));

            var glyph = "&nbsp;";

            if (entity is IDocuEntityWithNameAsNid dtNid && NC[dtNid.DocuTermNid.NamingId] is IGlyph gy)
                glyph = gy.Glyph;

            return glyph;
        }


        /// <summary>
        /// mko, 29.6.2020
        /// Automatisiert den effizienten Vergleich der Namen benannter DokuTerme
        /// 
        /// mko, 15.3.2021
        /// Namensvergleich mit WildCards implementiert
        /// 
        /// mko, 28.7.2021
        /// In streng typisierte Form umgeschrieben. Wildcards für Namen sind ab jetzt durch Naming- ID's implementiert
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool AreOfSameName(this IDocuEntity entity, IDocuEntity other)
        {
            var isOfSameName = false;

            if (!entity.IsNamed() || !other.IsNamed())
            {
                isOfSameName = false;
            }
            else if (entity is IDocuEntityWithNameAsNid nidWc && nidWc.DocuTermNid.NamingId == TTD.Types.WildCard.UID)
            {
                // Eine Wildcard pass immer
                isOfSameName = true;
            }
            else if (other is IDocuEntityWithNameAsNid otherNidWc && otherNidWc.DocuTermNid.NamingId == TTD.Types.WildCard.UID)
            {
                // Eine Wildcard pass immer
                isOfSameName = true;
            }
            else if (entity is IDocuEntityWithNameAsNid nidA && other is IDocuEntityWithNameAsNid nidB)
            {
                isOfSameName = nidA.DocuTermNid.NamingId == nidB.DocuTermNid.NamingId;
            }
            else if (entity is IDocuTermWithNameAsString dtStrA && other is IDocuTermWithNameAsString dtStrB)
            {
                isOfSameName = dtStrA.DocuTermName == dtStrB.DocuTermName;
            }
            else if (entity is IDocuEntityWithNameAsNid nidAA && other is IDocuTermWithNameAsString stStr)
            {
                // Bei DocuTermen, die mit unterschiedlichen Bennenungstechnologien implementiert sind,
                // Erfolgt der Namensvergleich auf Basis der Kulturneutralen Darstellung (CNT)
                isOfSameName = RC.NC[nidAA.DocuTermNid.NamingId].CNT == stStr.DocuTermName;
            }
            else if (entity is IDocuTermWithNameAsString stStr2 && other is IDocuEntityWithNameAsNid nidBB)
            {
                // Bei DocuTermen, die mit unterschiedlichen Bennenungstechnologien implementiert sind,
                // Erfolgt der Namensvergleich auf Basis der Kulturneutralen Darstellung (CNT)
                isOfSameName = RC.NC[nidBB.DocuTermNid.NamingId].CNT == stStr2.DocuTermName;
            }



            return isOfSameName;
        }

        /// <summary>
        /// mko, 29.6.2020
        /// Vergleicht den Namen eines `Docuterms` mit einer gegebenen NID (Naming ID). Die Naming- Id kann dabei in eine Wunschsprache 
        /// aufgelöst werden, wenn der beannte DocuTerm mit einem String und nicht mit einer NID benannt wurde.
        /// 
        /// mko, 4.8.2021
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="nid"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static bool HasName(this IDocuEntity entity, long nid, ANC.Language lng = ANC.Language.CNT)
        {
            if (!entity.IsNamed())
                return false;
            else if (entity is IDocuEntityWithNameAsNid dtWithNid)
            {
                return dtWithNid.DocuTermNid.NamingId == nid;
            }
            else if (entity is IDocuTermWithNameAsString dtWithStr)
            {
                return dtWithStr.Equals(RC.NC[nid].NameIn(lng));
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// mko, 29.6.2020
        /// Vergleicht den Namen eines Dokuterms mit einem gegebenen, dargestellt durch einen String.
        /// Achtung: die Groß/Kleinschreibung ist von Bedeutung.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        public static bool HasName(this IDocuEntity entity, string name)
        {
            if (!entity.IsNamed())
                return false;
            else
            {
                return entity.Name() == name;
            }
        }



        //static HashSet<DocuEntityTypes> UnnamedDocuEntityTypes = new HashSet<DocuEntityTypes>()
        //{
        //    DocuEntityTypes.Date,
        //    //DocuEntityTypes.Event,
        //    DocuEntityTypes.List,
        //    DocuEntityTypes.ReturnValue,
        //    DocuEntityTypes.String,
        //    DocuEntityTypes.Text,
        //    DocuEntityTypes.Time,
        //    DocuEntityTypes.Version,

        //    // mko, 9.6.2020
        //    DocuEntityTypes.Bool,
        //    DocuEntityTypes.Int,
        //    DocuEntityTypes.Float,
        //    DocuEntityTypes.NID,

        //    // mko, 15.6.2020
        //    DocuEntityTypes.WildCard
        //};

        /// <summary>
        /// mko, 28.2.2019
        /// Returns true, if entiy is a named entity (e.g. Method, Instance...)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsNamed(this IDocuEntity entity)
            =>
                entity is IDocuEntityWithNameAsNid || entity is IDocuTermWithNameAsString;

        //{
        //    //return !UnnamedDocuEntityTypes.Contains(entity.EntityType);
        //}

        /// <summary>
        /// mko, ?
        /// Checks if Entity has Value
        /// 
        /// mko, 27.7.2021
        /// Streng typisiert reimplementiert
        /// 
        /// mko, 28.9.2021
        /// Zur allgemeinen Form für IDocuEntity erklärt
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool HasValue(this IDocuEntity entity)
        //=> entity.Childs.Count() > 1;
        {
            bool ret = false;

            if (entity is IInstance i)
                ret = i.HasValue();
            else if (entity is IMethod m)
                ret = m.HasValue();
            else if (entity is IDTList lst)
                ret = lst.HasValue();
            else if (entity is IProperty p)
                ret = p.HasValue();
            else if (entity is IReturn r)
                ret = r.HasValue();
            else if (entity is IEvent e)
                ret = e.HasValue();
            else if (entity is IVer ver)
                ret = true;
            else if (entity is IDouble dbl)
                ret = true;
            else if (entity is IInteger integer)
                ret = true;
            else if (entity is IBoolean boolean)
                ret = true;
            else if (entity is IString str)
                ret = true;
            else if (entity is INID nid)
                ret = true;

            return ret;
        }

        /// <summary>
        /// mko, 28.9.2021
        /// Spezielle Form für Instanzen
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        public static bool HasValue(this IInstance i)
            => i.InstanceMembers?.Any() ?? false;

        /// <summary>
        /// mko, 28.9.2021
        /// Spezielle Form für Methoden
        /// </summary>
        /// <param name="m"></param>
        /// <returns></returns>
        public static bool HasValue(this IMethod m)
            => m.Parameters?.Any() ?? false;

        /// <summary>
        /// mko, 28.9.2021
        /// Spezielle Form für Listen
        /// </summary>
        /// <param name="lst"></param>
        /// <returns></returns>
        public static bool HasValue(this IDTList lst)
            => lst.ListMembers?.Any() ?? false;

        /// <summary>
        /// mko, 28.9.2021
        /// Spezielle Form für Eigenschaften
        /// </summary>
        /// <param name="p"></param>
        /// <returns></returns>
        public static bool HasValue(this IProperty p)
            => !(p.PropertyValue is INID pvNid && pvNid.NamingId == TTD.Types.UndefinedPropertyValue.UID);

        /// <summary>
        /// mko, 28.9.2021
        /// Spezielle Form für Rückgabewerte
        /// </summary>
        /// <param name="r"></param>
        /// <returns></returns>
        public static bool HasValue(this IReturn r)
            => !(r.ReturnValue is IInstanceWithNameAsNid iNid && iNid.DocuTermNid.NamingId == TTD.Types.UndefinedReturnValue.UID);

        /// <summary>
        /// mko, 28.9.2021
        /// Spezielle Form für Ereignisse.
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        public static bool HasValue(this IEvent e)
            => !(e.EventParameter is IInstanceWithNameAsNid iNid && iNid.DocuTermNid.NamingId == TTD.Types.UndefinedEventParameter.UID);


        public static IDocuEntity EntityValue(this IDocuEntity entity)
        {
            //return entity.Childs.Skip(1)?.FirstOrDefault();
            IDocuEntity ret = RC.pnL.NID(TTD.Types.UndefinedDocuTerm.UID);

            if (entity is IInstance i)
                ret = RC.pnL.List(i.InstanceMembers);
            else if (entity is IMethod m)
                ret = RC.pnL.List(m.Parameters);
            else if (entity is IDTList lst)
                ret = RC.pnL.List(lst.ListMembers);
            else if (entity is IProperty p)
                ret = p.PropertyValue;
            else if (entity is IReturn r)
                ret = r.ReturnValue;
            else if (entity is IEvent e)
                ret = e.EventParameter;
            else if (entity is IVer ver)
                ret = ver;
            else if (entity is IDouble dbl)
                ret = dbl;
            else if (entity is IInteger integer)
                ret = integer;
            else if (entity is IBoolean boolean)
                ret = boolean;
            else if (entity is IString str)
                ret = str;
            else if (entity is INID nid)
                ret = nid;

            return ret;

        }

        /// <summary>
        /// mko, 27.7.2021
        /// Streng typisiert reimplementiert.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="no"></param>
        /// <returns></returns>
        public static IDocuEntity EntityValue(this IDocuEntity entity, int no)
        {
            IDocuEntity ret = RC.pnL.NID(TTD.Types.UndefinedDocuTerm.UID);

            if (entity is IInstance i)
            {
                if (i.InstanceMembers.Length > no)
                {
                    ret = i.InstanceMembers[no];
                }
            }
            else if (entity is IMethod m)
            {
                // Wäre in Zukunft zu überdenken, ob hier nicht der Rückgabewert einer Funktion stehen sollte
                if (m.Parameters.Length > no)
                {
                    ret = m.Parameters[no];
                }
            }
            else if (entity is IDTList lst)
            {
                // Wäre in Zukunft zu überdenken, ob hier nicht der Rückgabewert einer Funktion stehen sollte
                if (lst.ListMembers.Length > no)
                {
                    ret = lst.ListMembers[no];
                }
            }
            else if (entity is IProperty p)
                ret = p.PropertyValue;
            else if (entity is IReturn r)
                ret = r.ReturnValue;
            else if (entity is IEvent e)
                ret = e.EventParameter;
            else if (entity is IVer ver)
                ret = ver;
            else if (entity is IDouble dbl)
                ret = dbl;
            else if (entity is IInteger integer)
                ret = integer;
            else if (entity is IBoolean boolean)
                ret = boolean;
            else if (entity is IString str)
                ret = str;
            else if (entity is INID nid)
                ret = nid;

            return ret;
            //return entity.Childs.Skip(1 + no)?.FirstOrDefault();
        }

        /// <summary>
        /// mko, 18.4.2018
        /// 
        /// mko, 29.6.2020
        /// Texte, die als NID oder String verpackt sind, werden jetzt automatisch aufgelöst und zurückgegeben.
        /// 
        /// mko, 27.7.2021
        /// angepasst an neue, streng typisierte Implementierung.
        /// 
        /// mko, 27.9.2021
        /// Falls keien Textdaten gefunden werden, dann wird nicht mehr eine Ausnahme geworfen, sondern stattdessen eine Fehlermeldung ausgegeben.
        /// 
        /// mko, 30.9.2021
        /// GetText wird auf IPropertyValues eingeschränkt (vorher IDocuEntity)
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public static string GetText(this IDocuEntity entity, ANC.Language lng = ANC.Language.CNT)
        public static string GetText(this IPropertyValue entity, ANC.Language lng = ANC.Language.CNT)
        {
            var NH = new ANC.NamingHelper(RC.NC, lng);
            if (entity is INID nidA)
            {
                // Direkter Vergleich der NID's (exakt und effizient)
                return NH._(nidA.NamingId);
            }
            else if (entity is IString str)
            {
                return str.ValueAsString;
            }
            else if (entity is ITxt txt)
            {
                return string.Join(" ", txt.Words.Select(r => r.ValueAsString));
            }
            else
            {
                //TraceHlp.ThrowArgEx(RC.pnL.m("GetText", RC.pnL.ret(RC.pnL.eFails(TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID))));                
                return $"{NH.glyph(TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID)} {NH._(TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID)}";
            }

            //return null;
        }

        /// <summary>
        /// mko, 29.6.2020
        /// Liest informelle Beschreibungen zu einem Event aus, die diesem via Composer- Generatorfunktionen hinzugefügt wurden.
        /// 
        /// Diese Werte sind immer in Eigenschaften mit dem Namen **Result** gekapselt, die wiederum in einer Liste gekapselt sind:
        /// #_ #p Result ... #.
        /// 
        /// </summary>
        /// <param name="dtEvent"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static string GetEventTextValue(this IEvent dtEvent, ANC.Language lng = ANC.Language.CNT)
        {
            if (dtEvent.EventParameter is IDTList list
                && list.ListMembers.FirstOrDefault() is IProperty p
               && p.HasName(TTD.MetaData.Result.UID))
            {
                return p.PropertyValue.GetText(lng);
            }
            else
            {
                TraceHlp.ThrowArgEx(RC.pnL.m("GetEventTextValue", RC.pnL.eFails("Event contains no textual description")));
                return "";
            }
        }

        public static long GetEventIntValue(this IEvent dtEvent)
        {
            if (dtEvent.EventParameter is IDTList list
                && list.ListMembers.FirstOrDefault() is IProperty p
               && p.HasName(TTD.MetaData.Result.UID)
               && p.PropertyValue is Integer i)
            {
                return i.ValueAsLong;
            }
            else
            {
                TraceHlp.ThrowArgEx(RC.pnL.m("GetEventIntValue", RC.pnL.eFails("Event has no integer value")));
                return 0;
            }
        }

        public static double GetEventDblValue(this IEvent dtEvent)
        {
            if (dtEvent.EventParameter is IDTList list
                && list.ListMembers.FirstOrDefault() is IProperty p
               && p.HasName(TTD.MetaData.Result.UID)
               && p.PropertyValue is Double dbl)
            {
                return dbl.ValueAsDouble;
            }
            else
            {
                TraceHlp.ThrowArgEx(RC.pnL.m("GetEventDblValue", RC.pnL.eFails("Event has no double value")));
                return 0.0;
            }
        }

        public static bool GetEventBoolValue(this IEvent dtEvent)
        {
            if (dtEvent.EventParameter is IDTList list
                && list.ListMembers.FirstOrDefault() is IProperty p
               && p.HasName(TTD.MetaData.Result.UID)
               && p.PropertyValue is Boolean b)
            {
                return b.ValueAsBool;
            }
            else
            {
                TraceHlp.ThrowArgEx(RC.pnL.m("GetEventBoolValue", RC.pnL.eFails("Event has no bool value")));
                return false;
            }
        }

        /// <summary>
        /// mko, 3.7.2019
        /// Instanzmember sind in einer DokuEntity-Liste eingeschlossen. Diese Methode holt die Member aus der Dokuentity- Liste und liefert 
        /// sie in Form eines Enumerable zurück.
        /// 
        /// mko, 27.7.2021
        /// Strenger typisiert: 
        /// - Parameter jetzt vom Typ `IInstance` (vorher `IDocuEntity`)
        /// - Rückgabetyp jetzt `IEnumerable<IInstanceMember>` (vorher `IEnumerable<IDocuEntity>`)
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IEnumerable<IInstanceMember> GetInstanceMembers(this IInstance entity)
        {
            //TraceHlp.ThrowArgExIfNot(entity.EntityType == DocuEntityTypes.Instance, "entity is not a instance!");
            // var members = entity.Childs.Skip(1).FirstOrDefault()?.Childs;

            if (entity.InstanceMembers == null)
            {
                return new IInstanceMember[] { };
            }
            else
            {
                return entity.InstanceMembers;
            }
        }

        /// <summary>
        /// mko, 3.7.2019
        /// Methodenmember sind in einer DokuEntity-Liste eingeschlossen. Diese Methode holt die Member aus der Dokuentity- Liste und liefert 
        /// sie in Form eines Enumerable zurück.
        /// 
        /// mko, 27.7.2021
        /// Strenger typisiert: Parameter jetzt vom Typ IMethod (vorher IDocuEntity).
        /// Zudem Zugriff auf Eigenschaft **Parameters** gegen Null- Werte abgesichert mittels ??
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IEnumerable<IMethodParameter> GetMethodMembers(this IMethod m)
        {
            //var members = entity.Childs.Skip(1).FirstOrDefault()?.Childs;
            if (!m.Parameters?.Any() ?? false)
            {
                return new IMethodParameter[] { };
            }
            else
            {
                return m.Parameters;
            }
        }

        /// <summary>
        /// mko, 2.7.2019
        /// List den Datumswert aus einem Date- Element
        /// 
        /// mko, 27.7.2021
        /// Streng typisiert reimplementiert: Parametertype **IDocuEntity** in **IDate** gewandelt.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DateTime GetDate(this IDate date)
        {
            return new DateTime(date.Year, date.Month, date.Day);
        }


        private static void _GetVersion(IInstance instanceWithVersion, ref RC<string> ret)
        {
            foreach (var member in instanceWithVersion.InstanceMembers)
            {
                if (member is IVer ver)
                {
                    ret = RC<string>.Ok(ver.VersionString);
                    break;
                }
            }
        }


        /// <summary>
        /// mko, 23.4.2018
        /// Returns the value of Version Entity, contained in a Entity. 
        /// 
        /// mko, 14.7.2020
        /// Zugriff auf den VErsionsstring jetzt streng typisiert.
        /// 
        /// mko, 27.7.2021
        /// Reimplementiert für neue, streng typisierte DocuTerms
        /// </summary>
        /// <param name="instanceWithVersion"></param>
        /// <returns></returns>
        public static RC<string> GetVersion(this IInstanceWithNameAsNid instanceWithVersion)
        {
            var ret = RC<string>.Failed(value: null, ErrorDescription:  RC.pnL.ReturnFetchNotFound(TTD.Types.DocuTerms.UID, TTD.Types.Instance.UID, instanceWithVersion.DocuTermNid));

            _GetVersion(instanceWithVersion, ref ret);

            return ret;
        }

        /// <summary>
        /// mko, 23.4.2018
        /// Returns the value of Version Entity, contained in a Entity. 
        /// 
        /// mko, 14.7.2020
        /// Zugriff auf den VErsionsstring jetzt streng typisiert.
        /// 
        /// mko, 27.7.2021
        /// Reimplementiert für neue, streng typisierte DocuTerms
        /// </summary>
        /// <param name="instanceWithVersion"></param>
        /// <returns></returns>
        public static RC<string> GetVersion(this IInstanceWithNameAsString instanceWithVersion)
        {
            var ret = RC<string>.Failed(value: null, ErrorDescription: RC.pnL.ReturnFetchNotFound(TTD.Types.DocuTerms.UID, TTD.Types.Instance.UID, RC.pnL.str(instanceWithVersion.DocuTermName)));

            _GetVersion(instanceWithVersion, ref ret);

            return ret;
        }

        /// <summary>
        /// mko, 27.7.2021
        /// </summary>
        /// <param name="instanceWithVersion"></param>
        /// <param name="ret"></param>
        private static void _GetVersion(IMethod instanceWithVersion, ref RC<string> ret)
        {
            foreach (var parameter in instanceWithVersion.Parameters)
            {
                if (parameter is IVer ver)
                {
                    ret = RC<string>.Ok(ver.VersionString);
                    break;
                }
            }
        }

        /// <summary>
        /// mko, 27.7.2021
        /// </summary>
        /// <param name="methodWithVersion"></param>
        /// <returns></returns>
        public static RC<string> GetVersion(this IMethodWithNameAsNid methodWithVersion)
        {
            var ret = RC<string>.Failed(value: null, ErrorDescription: RC.pnL.ReturnFetchNotFound(TTD.Types.DocuTerms.UID, TTD.Types.Instance.UID, methodWithVersion.DocuTermNid));

            _GetVersion(methodWithVersion, ref ret);

            return ret;
        }

        /// <summary>
        /// mko, 27.7.2021
        /// </summary>
        /// <param name="methodWithVersion"></param>
        /// <returns></returns>
        public static RC<string> GetVersion(this IMethodWithNameAsString methodWithVersion)
        {
            var ret = RC<string>.Failed(value: null, ErrorDescription: RC.pnL.ReturnFetchNotFound(TTD.Types.DocuTerms.UID, TTD.Types.Instance.UID, RC.pnL.str(methodWithVersion.DocuTermName)));

            _GetVersion(methodWithVersion, ref ret);

            return ret;
        }





        public enum EventTypes
        {
            info,
            warn,
            fails,
            start,
            end,
            succeded,
            notCompleted,
            none
        }


        public static Dictionary<string, EventTypes> MapStringToEventType = new Dictionary<string, EventTypes>()
        {
            {"info", EventTypes.info },
            {"warn", EventTypes.warn },
            {"fails", EventTypes.fails },
            {"start", EventTypes.start },
            {"end", EventTypes.end },
            {"succeeded", EventTypes.succeded },
            {"notcompleted", EventTypes.notCompleted }
        };


        /// <summary>
        /// mko, 27.7.2021
        /// Ordnet einer NID einen EventType zu.
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public static EventTypes EventNidToEventType(long nid)
        {
            switch (nid)
            {
                case TTD.Event.End.UID:
                    return DocuEntityHlp.EventTypes.end;
                case TTD.Event.Fails.UID:
                    return DocuEntityHlp.EventTypes.fails;
                case TTD.Event.Info.UID:
                    return DocuEntityHlp.EventTypes.info;
                case TTD.Event.NotCompleted.UID:
                    return DocuEntityHlp.EventTypes.notCompleted;
                case TTD.Event.Start.UID:
                    return DocuEntityHlp.EventTypes.start;
                case TTD.Event.Succeeded.UID:
                    return DocuEntityHlp.EventTypes.succeded;
                case TTD.Event.Warn.UID:
                    return DocuEntityHlp.EventTypes.warn;
                default:
                    return DocuEntityHlp.EventTypes.none;
            }

        }

        /// <summary>
        /// mko, 2.7.2018
        /// 
        /// mko, 23.6.2020
        /// Reimplementiert auf Basis strenger Typisierung
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsCommonEventType(this IDocuEntity entity)
        {
            return entity is IEvent ev && ev.EventType != EventTypes.none;
            //.EntityType == DocuEntityTypes.Event && MapStringToEventType.ContainsKey(entity.Name());
        }

        /// <summary>
        /// mko, 23.6.2020
        /// Besimmt zu einem Namen als String den Event- Type.
        /// Abgesichert gegen unbekannte Event- Typen
        /// 
        /// mko, 27.7.2021
        /// Umgestellt auf neue, streng typisierte Implementierung
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EventTypes GetEventType(this IDocuEntity entity)
        {
            TraceHlp.ThrowArgExIfNot(entity.EntityType == DocuEntityTypes.Event,
                RC.pnL.m("GetEventType",
                    RC.pnL.p(TTD.MetaData.Type.UID, entity.EntityType.ToString()),
                    RC.pnL.eFails(TTD.Composer.Errors.DocuTermAsEventRequired.UID)));

            var ret = EventTypes.none;

            if (entity is IDocuEntityWithNameAsNid eventWithNameAsNid)
            {
                ret = EventNidToEventType(eventWithNameAsNid.DocuTermNid.NamingId);
            }
            else if (entity is IDocuTermWithNameAsString eventWithNameAsString)
            {
                if (MapStringToEventType.ContainsKey(entity.Name()))
                    ret = MapStringToEventType[entity.Name()];
                else
                    ret = EventTypes.none;
            }
            else
            {
                ret = EventTypes.none;
            }

            return ret;
        }

    }

}
