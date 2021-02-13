using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.DocuEntityHlp;

using ANC = MKPRG.Naming;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 23.3.2018
    /// </summary>
    public class HTMLFormater : IFormater
    {
        IComposer pnL;

        /// <summary>
        /// mko
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="lng"></param>
        public HTMLFormater(
            IComposer pnL,
            ANC.Language lng = ANC.Language.CNT)
        {
            this.pnL = pnL;
            this.lng = lng;
            this.NC = RCV3.NC;
            htmDoc = new HTML.HTMLDocument();
        }

        /// <summary>
        /// mko
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="NC"></param>
        /// <param name="lng"></param>
        public HTMLFormater(
            IComposer pnL, 
            IReadOnlyDictionary<long, MKPRG.Naming.INaming> NC,
            ANC.Language lng = ANC.Language.CNT)
        {
            this.pnL = pnL;
            this.lng = lng;
            this.NC = NC;
            htmDoc = new HTML.HTMLDocument();
        }

        /// <summary>
        /// Allgemeines html- Dokument
        /// </summary>
        HTML.HTMLDocument htmDoc;

        /// <summary>
        /// mko, 2.3.2020
        /// Aus Gründen der Abwärtscompatibilität wird immer in der kulturneutralen Sparache ausgegeben
        /// </summary>
        ANC.Language lng = DFC.Naming.Language.CNT;

        /// <summary>
        /// Ordnet einer long UID einen EventName- Naming Objekt zu.
        /// </summary>
        IReadOnlyDictionary<long, MKPRG.Naming.INaming> NC { get; }        


        public string Print(IDocuEntity entity)
        {
            var fn = Fn._;

            return _Print(1, entity);
        }

        string NormalizeName(string name)
        {
            return name.Replace("&", "&amp;").Replace("<", "&lt;").Replace(">", "&gt;").Replace("\"", "&quot;").Replace("'", "&apos;"); //.Replace(';', ' ').Replace('#', ' ');
        }

        private string _Print(int Level, IDocuEntity entity)
        {
            string res = "";
            switch (entity.EntityType)
            {
                case DocuEntityTypes.Date:
                    {
                        var d = (IDate)entity;
                        res = $"<time date='{d.Year.ToString("D4")}-{d.Month.ToString("D2")}-{d.Day.ToString("D2")}'>{d.Year.ToString("D4")}-{d.Month.ToString("D2")}-{d.Day.ToString("D2")}</time>";
                    }
                    break;
                case DocuEntityTypes.Time:
                    {
                        var t = (ITime)entity;
                        res = $"<time date='{t.Hour.ToString("D2")}:{t.Minutes.ToString("D2")}:{t.Seconds.ToString("D2")}'>{t.Hour.ToString("D2")}:{t.Minutes.ToString("D2")}:{t.Seconds.ToString("D2")}</time>";
                    }
                    break;
                case DocuEntityTypes.Event:
                    {
                        var e = (IEvent)entity;

                        var name = NormalizeName(entity.Name(lng, NC));

                        //var color = "#000000";
                        var cssClass = "event";
                        if (entity.IsCommonEventType())
                        {
                            switch (entity.GetEventType())
                            {
                                case EventTypes.start:
                                case EventTypes.end:
                                    cssClass="eventEnd";
                                    break;
                                case EventTypes.fails:
                                    cssClass="eventFails";
                                    break;
                                case EventTypes.info:
                                    cssClass="eventInfo";
                                    break;
                                case EventTypes.succeded:
                                    cssClass="eventSucceeded";
                                    break;
                                case EventTypes.notCompleted:
                                case EventTypes.warn:
                                    cssClass= "eventWarn";
                                    break;
                                default:
                                    cssClass= "event";
                                    break;
                            }
                        } else
                        {
                            cssClass = "event";
                        }

                        if (entity.HasValue())
                        {
                            res = $"<div class=\"{cssClass}\"><h1>{name}!</h1><p>{_Print(Level + 1, e.EventParameter)}</p></div>";
                        }
                        else
                        {
                            res = $"<div class='{cssClass}'><h1>{name}!</h1></em>";
                        }
                    }
                    break;
                case DocuEntityTypes.Instance:
                    {
                        var i = (IInstance)entity;

                        var name = NormalizeName(i.Name(lng, NC));
                        var hLevel = Level > 6 ? 6 : Level;

                        if (i.InstanceMembers.Any())
                        {
                            // mko, 18.4.2018
                            // xTab <=> pivot table processing
                            if (name == "xTab")
                            {
                                res = xTabFormating(i);
                            }
                            else
                            {
                                var bld = new StringBuilder();

                                bld.Append("<div class=\"instance\">");
                                bld.Append($"<h1>{name}</h1><p>");
                                foreach(var member in i.InstanceMembers)
                                {
                                    bld.Append(_Print(Level + 1, member));
                                }
                                bld.Append("</p>");
                                bld.Append("</div>");

                                res = bld.ToString();
                            }
                        }
                        else
                        {
                            res = $"<div class=\"instance\"><h{hLevel}>{name}</h{hLevel}></div>";
                        }
                    }
                    break;
                case DocuEntityTypes.List:
                    {
                        var L = (IDTList)entity;
                        if (L.ListMembers.Any())
                        {
                            var bld = new StringBuilder();
                            bld.Append("<ol class='list'>");
                            foreach (var child in L.ListMembers)
                            {
                                bld.Append("<li>");
                                bld.Append(_Print(Level + 1, child));
                                bld.Append("</li>");
                            }
                            bld.Append("</ol>");

                            res = bld.ToString();
                        } else
                        {
                            // mko, 26.6.2020: Leere Listen werden weggelassen
                        }
                    }
                    break;
                case DocuEntityTypes.Method:
                    {
                        var bld = new StringBuilder();

                        var m = (IMethod)entity;

                        if (m.Parameters.Any())
                        {
                            bld.Append("<div class=\"method\">");
                                bld.Append($"<h1>{NormalizeName(m.Name(lng, NC))}</h1>");
                                bld.Append($"<ol>");
                                foreach (var child in m.Parameters)
                                {
                                    bld.Append("<li>");
                                    bld.Append(_Print(Level + 1, child));
                                    bld.Append("</li>");
                                }
                                bld.Append("</ol>");
                            bld.Append("</div>");
                        }
                        else
                        {
                            bld.Append($"<div class=\"method\">{NormalizeName(m.Name(lng, NC))}()</div>");
                        }
                        res = bld.ToString();
                    }
                    break;
                case DocuEntityTypes.Property:
                    {
                        var bld = new StringBuilder();

                        var p = (IProperty)entity;

                        bld.Append("<div class=\"property\">");
                        bld.Append($"<h1>{NormalizeName(p.Name(lng, NC))}</h1>");
                        bld.Append($"<div class='PropVal'>{_Print(Level + 1, p.PropertyValue)}</div>");
                        bld.Append("</div>");

                        res = bld.ToString();
                    }
                    break;
                case DocuEntityTypes.PropertySet:
                    {
                        var bld = new StringBuilder();

                        bld.Append("<div class=\"propertySet\">");
                        bld.Append($"<h1>{NormalizeName(entity.Name(lng, NC))} :=</h1>");
                        bld.Append(_Print(Level + 1, entity.EntityValue()));
                        bld.Append("<div>");

                        res = bld.ToString();
                    }
                    break;                   
                case DocuEntityTypes.NID:
                    {
                        // mko, 9.6.2020
                        // Abrufen des Namens in der Wunschsprache
                        var nid = (NID)entity;
                        res = NormalizeName(NC[nid.NamingId].NameIn(lng));
                    }
                    break;
                case DocuEntityTypes.String:
                    {

                        // mko, 2.3.2020
                        // Wenn der String ein DocuTermUID definiert ist,
                        // dann wird seine sprachspezifische Repräsentation 
                        // nachgeschlagen und angezeigt.

                        var str = ((String)entity).Value;

                        //if (MKPRG.Naming.NamingBase.IsIDAsName(str))
                        //{
                        //    var UID = MKPRG.Naming.NamingBase.ParseUID(str);

                        //    if (NamingContainer.ContainsKey(UID))
                        //    {
                        //        str = NamingContainer[UID].NameIn(lng);
                        //    }
                        //}

                        res = NormalizeName(str);
                    }
                    break;
                case DocuEntityTypes.Text:
                    {
                        var txt = (ITxt)entity;
                        var bld = new StringBuilder();
                        foreach (var word in txt.Words)
                        {
                            // mko, 9.6.2020
                            // Jedes Kind wird mit _Print verarbeitet, um so zu berücksichtigen, das einzelne Wörter eines
                            // Textes durch NID's dargestellt werden.
                            bld.Append($"{NormalizeName(_Print(Level, word))} ");
                        }
                        res = bld.ToString();
                    }
                    break;
                case DocuEntityTypes.Bool:
                    {
                        var boolVal = (Boolean)entity;

                        return boolVal.ValueAsBool
                            ? NC[ANC.DocuTerms.Boolean.True.UID].NameIn(lng)
                            : NC[ANC.DocuTerms.Boolean.False.UID].NameIn(lng);
                    }
                case DocuEntityTypes.Int:
                    {
                        var intVal = (Integer)entity;

                        return intVal.ValueAsLong.ToString();
                    }
                case DocuEntityTypes.Float:
                    {
                        var floatVal = (Double)entity;

                        return floatVal.Value.ToString();
                    }
                case DocuEntityTypes.Version:
                    // 15.11.2018
                    // Bei einer Versionsdefinition ist das erste Kind der Wert und nicht wie bei einer Eigenschaft erst der zweite
                    res = $"<dfn>{NC[ANC.TechTerms.Development.Version.UID].NameIn(lng)}</dfn> {_Print(Level + 1, entity.Childs.First())}</br>";
                    break;
                case DocuEntityTypes.ReturnValue:
                    {
                        var r = (IReturn)entity;

                        var bld = new StringBuilder();

                        bld.Append("<div class=\"return\">");

                        // mko, 8.10.2018
                        // Zugriff auf Rückgabewerte robuster gemacht.
                        if (r.ReturnValue != null)
                        {
                            bld.Append($"<dfn>&#8599;</dfn><br/>");
                            bld.Append(_Print(Level + 1, r.ReturnValue));
                        } else
                        {
                            bld.Append($"<dfn>&#8599;</dfn>");
                        }
                        
                        bld.Append("</div>");

                        res = bld.ToString();

                    }
                    break;
            }

            return res;
        }

        /// <summary>
        /// mko, 18.4.2018
        /// Formats a xTab- property as a cross table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        string xTabFormating(IInstance entity)
        {
            var res = $"<table>";

            var dim1 = (IProperty)entity.InstanceMembers.FirstOrDefault(m => m.EntityType == DocuEntityTypes.Property && m.HasName(ANC.DocuTerms.Formatting.XTab.Dim1.UID));
            if (dim1 != null && dim1.HasValue())
            {
                // First dimension as table header
                res += "<tr><th>&nbsp;</th>";
                var dim1List = dim1.PropertyValue as IDTList;
                var dim1Str = dim1List.ListMembers.Select(m => (IProperty)m); //..Childs.Select(r => r.EntityValue().GetText(lng)).ToArray();

                foreach (var c in dim1Str)
                {
                    res += $"<th>{c.PropertyValue.GetText(lng)}</th>";
                }

                res += "</tr>";

                // Second dimension as table rows

                var dim2 = (IProperty)entity.InstanceMembers.FirstOrDefault(m => m.EntityType == DocuEntityTypes.Property && m.HasName(ANC.DocuTerms.Formatting.XTab.Dim2.UID));
                var dim2List = dim2.PropertyValue as IDTList;

                var dim2Str = dim2List.ListMembers.Select(r => (IProperty)r); //.EntityValue().GetText(lng)).ToArray();

                var pValues = ((IProperty)entity.InstanceMembers
                                              .FirstOrDefault(m => m.EntityType == DocuEntityTypes.Property && m.HasName(ANC.DocuTerms.Formatting.XTab.Values.UID)));

                var valueList = (DTList)pValues.PropertyValue;

                var values = valueList.ListMembers.Select(r => (IInstance)r);

                foreach (var c2 in dim2Str)
                {
                    res += $"<tr><td>{c2.PropertyValue.GetText(lng)}</td>";

                    foreach (var c1 in dim1Str)
                    {
                        res += "<td>";

                        // i.e.: #i c1 #_ #p c2 #$ read #. #.

                        var _1 = values.FirstOrDefault(r => r.AreOfSameName(c1));
                        if (_1 != null)
                        {
                            var _2 = _1.InstanceMembers.FirstOrDefault(r => r.AreOfSameName(c2)) as IProperty; 

                            if (_2 != null)
                            {
                                res += Print(_2.PropertyValue);
                            } else
                            {
                                res += "&nbsp;";
                            }                           

                        }
                        else
                        {
                            res += "&nbsp;";
                        }

                        res += "</td>";
                    }
                    res += "<tr>";
                }
                res += "</table>";
            }
            else
            {
                res = $"Cross table structure invalid";
            }

            return res;
        }
    }
}
