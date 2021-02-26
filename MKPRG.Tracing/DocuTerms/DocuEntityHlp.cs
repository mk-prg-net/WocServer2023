using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

using MKPRG.Tracing.DocuTerms;

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
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DocuEntityLinqDeco AsLinq(this IDocuEntity entity)
        {
            return new DocuEntityLinqDeco(entity);
        }


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
            return Name(entity, lng, RC.NC, RC.pnL);
        }

        /// <summary>
        /// mko, 9.6.2020
        /// Wenn der Name als NamingId (NID) definiert wurde, dann kann seine Aussprache in einer wählbaren Sprache abgerufen werden.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lng"></param>
        /// <param name="NC"></param>
        /// <returns></returns>
        public static string Name(this IDocuEntity entity, ANC.Language lng, IReadOnlyDictionary<long, ANC.INaming> NC, IComposer pnL)
        {
            // check, if Name exists
            var first = entity.Childs.FirstOrDefault();

            TraceHlp.ThrowArgExIfNot(
                entity.IsNamed(),
                pnL.ReturnValidatePreconditionFailedWithDetails(
                     pnL.i(TTD.Types.DocuTerm.UID,
                        pnL.p(TTD.MetaData.Type.UID, entity.EntityType.ToString())),
                     pnL.m(TT.Operators.Relations.IsOfType.UID,
                        pnL.p_NID(TTD.MetaData.Arg.UID, TTD.Types.NamedDocuTerm.UID),
                        pnL.ret(pnL.eFails(TTD.Parser.Errors.NamedTermExpected.UID)))));

                
            TraceHlp.ThrowArgExIfNot(
                first != null && (first is String || first is NID),
                pnL.ReturnValidatePreconditionFailedWithDetails(
                     pnL.i(TTD.Types.DocuTerm.UID,
                        pnL.p(TTD.MetaData.Type.UID, entity.EntityType.ToString())),
                     pnL.m(TT.Operators.Sets.Exists.UID,
                        pnL.p_NID(TTD.MetaData.Arg.UID, TTD.MetaData.Name.UID),
                        pnL.ret(pnL.eFails(TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID)))));

            string name = "";
            if (first is String str)
            {
                name = str.Value;
            }
            else
            {
                var nid = (NID)first;
                name = NC[nid.NamingId].NameIn(lng);
            }

            return name;
        }

        /// <summary>
        /// mko, 26.1.2021
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="NC"></param>
        /// <returns></returns>
        public static string Glyph(this IDocuEntity entity, IReadOnlyDictionary<long, ANC.INaming> NC, IComposer pnL)
        {
            // check, if Name exists
            var first = entity.Childs.FirstOrDefault();

            TraceHlp.ThrowArgExIfNot(
                entity.IsNamed(),
                pnL.ReturnValidatePreconditionFailedWithDetails(
                     pnL.i(TTD.Types.DocuTerm.UID,
                        pnL.p(TTD.MetaData.Type.UID, entity.EntityType.ToString())),
                     pnL.m(TT.Operators.Relations.IsOfType.UID,
                        pnL.p_NID(TTD.MetaData.Arg.UID, TTD.Types.NamedDocuTerm.UID),
                        pnL.ret(pnL.eFails(TTD.Parser.Errors.NamedTermExpected.UID)))));


            TraceHlp.ThrowArgExIfNot(
                first != null && (first is String || first is NID),
                pnL.ReturnValidatePreconditionFailedWithDetails(
                     pnL.i(TTD.Types.DocuTerm.UID,
                        pnL.p(TTD.MetaData.Type.UID, entity.EntityType.ToString())),
                     pnL.m(TT.Operators.Sets.Exists.UID,
                        pnL.p_NID(TTD.MetaData.Arg.UID, TTD.MetaData.Name.UID),
                        pnL.ret(pnL.eFails(TTD.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID)))));

            string glyph = "&nbsp;";
            if (first is NID nid)
            { 
                glyph = NC[nid.NamingId].Glyph;
            }

            return glyph;
        }


        /// <summary>
        /// mko, 29.6.2020
        /// Automatisiert den effizienten Vergleich der Namen benannter DokuTerme
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="other"></param>
        /// <returns></returns>
        public static bool AreOfSameName(this IDocuEntity entity, IDocuEntity other, IComposer pnL)
        {
            if (!entity.IsNamed() || !other.IsNamed())
                return false;
            else if(entity.Childs.First() is NID nidA && other.Childs.First() is NID nidB)
            {
                // Direkter Vergleich der NID's (exakt und effizient)
                return nidA.NamingId == nidB.NamingId;
            }
            else
            {
                return entity.Name() == other.Name();
            }
        }

        /// <summary>
        /// mko, 29.6.2020
        /// Vregleicht den Namen eines Dokuterms mit einer gegebenen NID (Naming ID). Die Naming- Id kann dabei in eine Wunschsprache 
        /// aufgelöst werden, wenn der beannte DokuTerm mit einem String und nicht mit einer NID benannt wurde.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="nid"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static bool HasName(this IDocuEntity entity, long nid, IComposer pnL, ANC.Language lng = ANC.Language.CNT)
        {
            if (!entity.IsNamed())
                return false;
            else if (entity.Childs.First() is NID nidA)
            {
                // Direkter Vergleich der NID's (exakt und effizient)
                return nidA.NamingId == nid;
            }
            else
            {
                return entity.Name() == RC.NC[nid].NameIn(lng);
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
        public static bool HasName(this IDocuEntity entity, string name, IComposer pnL)
        {
            if (!entity.IsNamed())
                return false;
            else
            {
                return entity.Name() == name;
            }
        }



        static HashSet<DocuEntityTypes> UnnamedDocuEntityTypes = new HashSet<DocuEntityTypes>()
        {
            DocuEntityTypes.Date,
            //DocuEntityTypes.Event,
            DocuEntityTypes.List,
            DocuEntityTypes.ReturnValue,
            DocuEntityTypes.String,
            DocuEntityTypes.Text,
            DocuEntityTypes.Time,
            DocuEntityTypes.Version,

            // mko, 9.6.2020
            DocuEntityTypes.Bool,
            DocuEntityTypes.Int,
            DocuEntityTypes.Float,
            DocuEntityTypes.NID,

            // mko, 15.6.2020
            DocuEntityTypes.WildCard
        };

        /// <summary>
        /// mko, 28.2.2019
        /// Returns true, if entiy is a named entity (e.g. Method, Instance...)
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool IsNamed(this IDocuEntity entity)
        {
            return !UnnamedDocuEntityTypes.Contains(entity.EntityType);
        }

        /// <summary>
        /// mko, ?
        /// Checks if Entity has Value
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static bool HasValue(this IDocuEntity entity)
            => entity.Childs.Count() > 1;

        public static IDocuEntity EntityValue(this IDocuEntity entity)
        {
            return entity.Childs.Skip(1)?.FirstOrDefault();
        }

        public static IDocuEntity EntityValue(this IDocuEntity entity, int no)
        {
            return entity.Childs.Skip(1 + no)?.FirstOrDefault();
        }

        /// <summary>
        /// mko, 18.4.2018
        /// 
        /// mko, 29.6.2020
        /// Texte, die als NID oder String verpackt sind, werden jetzt automatisch aufgelöst und zurückgegeben.
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static string GetText(this IDocuEntity entity, ANC.Language lng = ANC.Language.CNT)
        {
            if (entity is NID nidA)
            {
                // Direkter Vergleich der NID's (exakt und effizient)
                return RC.NC[nidA.NamingId].NameIn(lng);
            }
            else if (entity is String str)
            {
                return str.Value;
            }
            else if (entity is ITxt txt)
            {
                return string.Join(" ", txt.Words.Select(r => r.Value));
            }
            else TraceHlp.ThrowArgEx(RC.pnL.m("GetText", RC.pnL.ret(RC.pnL.eFails(ANC.DocuTerms.Parser.Errors.Name_NidOrStringTokenForNameExpected.UID))));

            return null;
        }


        /// <summary>
        /// mko, 29.6.2020
        /// List informelle Beschreibungen zu einem Event aus, die diesem via Composer- Generatorfunktionen hinzugefügt wurden.
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="lng"></param>
        /// <returns></returns>
        public static string GetEventTextValue(this IEvent entity, IComposer pnL, ANC.Language lng = ANC.Language.CNT)
        {
            if(entity.EntityValue() is IDTList list
                && list.ListMembers.FirstOrDefault() is IProperty  p
               && p.HasName(TTD.MetaData.Result.UID, pnL))
            {
                return p.PropertyValue.GetText(lng);
            } else
            {
                TraceHlp.ThrowArgEx(RC.pnL.m("GetEventTextValue", RC.pnL.eFails(TTD.Parser.Errors.Event_EventParameterAsTextExpected.UID)));
                return "";
            }
        }

        public static long GetEventIntValue(this IEvent entity, IComposer pnL,  ANC.Language lng = ANC.Language.CNT)
        {
            if (entity.EntityValue() is IDTList list
                && list.ListMembers.FirstOrDefault() is IProperty p
               && p.HasName(TTD.MetaData.Result.UID, pnL)
               && p.PropertyValue is Integer i)
            {
                return i.ValueAsLong;
            }
            else
            {
                TraceHlp.ThrowArgEx(RC.pnL.m("GetEventIntValue", RC.pnL.eFails(TTD.Parser.Errors.Event_EventParameterAsTextExpected.UID)));
                return 0;
            }
        }

        public static double GetEventDblValue(this IEvent entity, IComposer pnL, ANC.Language lng = ANC.Language.CNT)
        {
            if (entity.EntityValue() is IDTList list
                && list.ListMembers.FirstOrDefault() is IProperty p
               && p.HasName(TTD.MetaData.Result.UID, pnL)
               && p.PropertyValue is Double dbl)
            {
                return dbl.Value;
            }
            else
            {
                TraceHlp.ThrowArgEx(RC.pnL.m("GetEventDblValue", RC.pnL.eFails(TTD.Parser.Errors.Event_EventParameterAsTextExpected.UID)));
                return 0.0;
            }
        }

        public static bool GetEventBoolValue(this IEvent entity, IComposer pnL, ANC.Language lng = ANC.Language.CNT)
        {
            if (entity.EntityValue() is IDTList list
                && list.ListMembers.FirstOrDefault() is IProperty p
               && p.HasName(TTD.MetaData.Result.UID, pnL)
               && p.PropertyValue is Boolean b)
            {
                return b.ValueAsBool;
            }
            else
            {
                TraceHlp.ThrowArgEx(RC.pnL.m("GetEventBoolValue", RC.pnL.eFails(TTD.Parser.Errors.Event_EventParameterAsTextExpected.UID)));
                return false;
            }
        }



        /// <summary>
        /// mko, 3.7.2019
        /// Instanzmember sind in einer DokuEntity-Liste eingeschlossen. Diese Methode holt die Member aus der Dokuentity- Liste und liefert 
        /// sie in Form eines Enumerable zurück.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IEnumerable<IDocuEntity> GetInstanceMembers(this IDocuEntity entity, IComposer pnL)
        {
            TraceHlp.ThrowArgExIfNot(entity.EntityType == DocuEntityTypes.Instance, pnL.eFails(TTD.Parser.Errors.InstanceExpected.UID));

            var members = entity.Childs.Skip(1).FirstOrDefault()?.Childs;
            if (members == null)
            {
                return new IDocuEntity[] { };
            }
            else
            {
                return members;
            }
        }

        /// <summary>
        /// mko, 3.7.2019
        /// Methodenmember sind in einer DokuEntity-Liste eingeschlossen. Diese Methode holt die Member aus der Dokuentity- Liste und liefert 
        /// sie in Form eines Enumerable zurück.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static IEnumerable<IMethodParameter> GetMethodMembers(this IDocuEntity entity, IComposer pnL)
        {
            TraceHlp.ThrowArgExIfNot(entity.EntityType == DocuEntityTypes.Method, pnL.eFails(TTD.Parser.Errors.MethodExpected.UID));

            var m = (IMethod)entity;
            
            //var members = entity.Childs.Skip(1).FirstOrDefault()?.Childs;
            if (!m.Parameters.Any())
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
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static DateTime GetDate(this IDocuEntity entity, IComposer pnL)
        {
            TraceHlp.ThrowArgExIfNot(entity.EntityType == DocuEntityTypes.Date, pnL.eFails("doc entity is not a date!"));

            var d = (IDate)entity;

            return new DateTime(d.Year, d.Month, d.Day);

        }


        /// <summary>
        /// mko, 23.4.2018
        /// Returns the value of Version Entity, contained in a Entity. 
        /// 
        /// mko, 14.7.2020
        /// Zugriff auf den VErsionsstring jetzt streng typisiert.
        /// </summary>
        /// <param name="ElemWithVersion"></param>
        /// <returns></returns>
        public static RC<string> GetVersion(this IDocuEntity ElemWithVersion)
        {
            RC<string> rc = RC<string>.Failed(null);

            if (ElemWithVersion.EntityType != DocuEntityTypes.Instance || ElemWithVersion.EntityType != DocuEntityTypes.Method)
            {
                rc = RC<string>.Failed(null, "Only instances (#i) or methods (#m) can contains a version element");
            }

            foreach (var child in ElemWithVersion.Childs.Skip(1).First().Childs)
            {
                if (child is IVer ver)
                {                    
                    rc = RC<string>.Ok(ver.VersionString);
                    break;
                }
            }

            return rc;
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
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public static EventTypes GetEventType(this IDocuEntity entity, IComposer pnL)
        {
            TraceHlp.ThrowArgExIfNot(entity.EntityType == DocuEntityTypes.Event, pnL.eFails("Entity is not a event"));
            if (MapStringToEventType.ContainsKey(entity.Name(pnL)))
                return MapStringToEventType[entity.Name(pnL)];
            else
                return EventTypes.none;
        }

    }

}
