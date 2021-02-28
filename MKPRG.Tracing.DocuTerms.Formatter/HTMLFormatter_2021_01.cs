using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


using ANC = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

using static MKPRG.Tracing.DocuTerms.DocuEntityHlp;

using Glyphs = MKPRG.Naming.Glyphs;

namespace MKPRG.Tracing.DocuTerms
{
    public class HTMLFormatter_2021_01
        : IFormater
    {
        IComposer pnL;

        /// <summary>
        /// Allgemeines html- Dokument
        /// </summary>
        HTML.HTMLDocument htmDoc;

        /// <summary>
        /// mko, 2.3.2020
        /// Aus Gründen der Abwärtscompatibilität wird immer in der kulturneutralen Sparache ausgegeben
        /// </summary>
        ANC.Language lng = ANC.Language.CNT;

        public ANC.Language Language
        {
            get
            {
                return lng;
            }

            set
            {
                lng = value;
            }
        }

        /// <summary>
        /// Ordnet einer long UID einen EventName- Naming Objekt zu.
        /// </summary>
        IReadOnlyDictionary<long, ANC.INaming> NC { get; }

        public bool ShowGlyphs { get; set; } = true;


        /// <summary>
        /// mko
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="lng"></param>
        public HTMLFormatter_2021_01(
            IComposer pnL,
            ANC.Language lng = ANC.Language.CNT)
        {
            this.pnL = pnL;
            this.lng = lng;
            this.NC = RC.NC;
            htmDoc = new HTML.HTMLDocument(pnL);
        }


        public HTMLFormatter_2021_01(
            IComposer pnL,
            IReadOnlyDictionary<long, ANC.INaming> NC,
            ANC.Language lng = ANC.Language.CNT)
        {
            this.pnL = pnL;
            this.lng = lng;
            this.NC = NC;
            htmDoc = new HTML.HTMLDocument(pnL);

        }


        public string CssStyleSheet
        {
            get;
            set;
        }

        /// <summary>
        /// Erzeugt für ein DokuEntity ein komplettesm HTML- Dokument
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public string Print(IDocuEntity entity)
        {
            htmDoc.Clear();
            htmDoc.createHeader(CssStyleSheet);

            //return _Print(1, entity);
            _Print(1, entity, htmDoc);
            return htmDoc.CloseDoc();
        }


        /// <summary>
        /// mko, 21.1.2021
        /// Erzeugt die Ausgabe in einem bereits existierenten HTML- Dokument
        /// </summary>
        /// <param name="htmDoc"></param>
        /// <param name="entity"></param>
        public void PrintOn(HTML.HTMLDocument htmDoc, IDocuEntity entity)
        {
            _Print(1, entity, htmDoc);
        }

        /// <summary>
        /// Rekursive Hilfsmethode, welche das DocuEntity in ein HTML- Dokument übersetzt.
        /// </summary>
        /// <param name="Level"></param>
        /// <param name="entity"></param>
        /// <param name="htmDoc"></param>
        private void _Print(int Level, IDocuEntity entity, HTML.HTMLDocument htmDoc)
        {
            var hLevel = Level > 6 ? 6 : Level;

            switch (entity.EntityType)
            {
                case DocuEntityTypes.Date:
                    {
                        var d = (IDate)entity;
                        htmDoc.time_with_dateAttrib(d.Year, d.Month, d.Day)
                                .txt($"{d.Year.ToString("D4")}-{d.Month.ToString("D2")}-{d.Day.ToString("D2")}").E.build();
                    }
                    break;
                case DocuEntityTypes.Time:
                    {
                        var t = (ITime)entity;

                        htmDoc.time_with_timeAttrib(t.Hour, t.Minutes, t.Seconds)
                                .txt($"{t.Hour.ToString("D2")}:{t.Minutes.ToString("D2")}:{t.Seconds.ToString("D2")}").E.build();
                    }
                    break;
                case DocuEntityTypes.Event:
                    {
                        var e = (IEvent)entity;

                        var glyph = ShowGlyphs && e.Glyph(NC) != Glyphs.Text.SPC ? $"{e.Glyph(NC)}{Glyphs.Text.SPC}" : "";


                        var name = entity.Name(lng);

                        //var color = "#000000";
                        var cssClass = "event";
                        if (entity.IsCommonEventType())
                        {
                            switch (entity.GetEventType())
                            {
                                case EventTypes.start:
                                case EventTypes.end:
                                    cssClass = "eventEnd";
                                    break;
                                case EventTypes.fails:
                                    cssClass = "eventFails";                                    
                                    break;
                                case EventTypes.info:
                                    cssClass = "eventInfo";
                                    break;
                                case EventTypes.succeded:
                                    cssClass = "eventSucceeded";                                    
                                    break;
                                case EventTypes.notCompleted:
                                case EventTypes.warn:
                                    cssClass = "eventWarn";                                    
                                    break;
                                default:
                                    cssClass = "event";
                                    break;
                            }
                        }
                        else
                        {
                            cssClass = "event";
                        }

                        if (entity.HasValue())
                        {
                            htmDoc.div_class(cssClass)
                                  .hAtLevelWithClass(hLevel, cssClass).html(glyph).txt(name).E
                                  .p.build();

                            _Print(Level + 1, e.EventParameter, htmDoc);

                            htmDoc.E.E.build();
                        }
                        else
                        {
                            htmDoc.div_class(cssClass).hAtLevelWithClass(hLevel, cssClass).html(glyph).nnbsp.txt(name).E.E.build();
                        }
                    }
                    break;
                case DocuEntityTypes.Instance:
                    {
                        var i = (IInstance)entity;

                        var glyph = ShowGlyphs && i.Glyph(NC) != Glyphs.Text.SPC ? $"{i.Glyph(NC)}{Glyphs.Text.SPC}" : "";


                        var name = i.Name(lng);

                        if (i.InstanceMembers.Any())
                        {
                            // mko, 18.4.2018
                            // xTab <=> pivot table processing
                            if (name == "xTab")
                            {
                                xTabFormating(i, htmDoc);
                            }
                            else
                            {
                                htmDoc.div_class("instance")
                                        .hAtLevelWithClass(hLevel, "instance").html(glyph).txt(name).E
                                        //.h1.txt(name).E
                                        .p.build();

                                foreach (var member in i.InstanceMembers)
                                {
                                    _Print(Level + 1, member, htmDoc);
                                }

                                htmDoc.E.E.build();
                            }
                        }
                        else
                        {
                            htmDoc.div_class("instance").hAtLevelWithClass(hLevel, "instance").html(glyph).txt(name).E.E.build();
                            //htmDoc.div_class("instance").h1.txt(name).E.E.build();
                        }
                    }
                    break;
                case DocuEntityTypes.List:
                    {
                        var L = (IDTList)entity;
                        if (L.ListMembers.Any())
                        {
                            //htmDoc.ol_class("list").build();
                            htmDoc.ol.build();

                            foreach (var child in L.ListMembers)
                            {
                                htmDoc.li.build();

                                _Print(Level + 1, child, htmDoc);

                                htmDoc.E.build();
                            }
                            htmDoc.E.build();
                        }
                        else
                        {
                            // mko, 26.6.2020: Leere Listen werden weggelassen
                        }
                    }
                    break;
                case DocuEntityTypes.Method:
                    {
                        var m = (IMethod)entity;

                        var glyph = ShowGlyphs && m.Glyph(NC) != Glyphs.Text.SPC ? $"{m.Glyph(NC)}{Glyphs.Text.SPC}" : "";

                        if (m.Parameters.Any())
                        {
                            htmDoc.div_class("method")
                                    .hAtLevelWithClass(hLevel, "method").html(glyph).txt(m.Name(lng)).E
                                    //.h1.txt(m.Name(lng, NC)).E
                                    .ol.build();

                            foreach (var child in m.Parameters)
                            {
                                htmDoc.li.build();

                                _Print(Level + 1, child, htmDoc);

                                htmDoc.E.build();
                            }

                            htmDoc.E.E.build();
                        }
                        else
                        {
                            htmDoc.div_class("method").hAtLevelWithClass(hLevel, "method").html(glyph).txt(m.Name(lng)).E.E.build();
                        }

                    }
                    break;
                case DocuEntityTypes.Property:
                    {
                        var p = (IProperty)entity;

                        var glyph = ShowGlyphs && p.Glyph(NC) != Glyphs.Text.SPC ? $"{p.Glyph(NC)}{Glyphs.Text.SPC}" : "";


                        htmDoc.div_class("property")
                                .hAtLevelWithClass(hLevel, "property").html(glyph).txt(p.Name(lng)).E
                                //.h1.txt(p.Name(lng, NC)).E
                                .div_class("propVal").build();

                        _Print(Level + 1, p.PropertyValue, htmDoc);

                        htmDoc.E.E.build();
                    }
                    break;
                case DocuEntityTypes.PropertySet:
                    {
                        htmDoc.div_class("propertySet")
                                .hAtLevelWithClass(hLevel, "propertySet").txt(entity.Name(lng)).E.build();

                        _Print(Level + 1, entity.EntityValue(), htmDoc);

                        htmDoc.E.build();
                    }
                    break;
                case DocuEntityTypes.NID:
                    {
                        // mko, 9.6.2020
                        // Abrufen des Namens in der Wunschsprache
                        var nid = (NID)entity;

                        if (ShowGlyphs && NC[nid.NamingId].Glyph != Glyphs.Text.SPC)
                        {
                            htmDoc.html($"{NC[nid.NamingId].Glyph}{Glyphs.Text.SPC}").txt(NC[nid.NamingId].NameIn(lng));
                        }
                        else
                        {
                            htmDoc.txt(NC[nid.NamingId].NameIn(lng));
                        }


                    }
                    break;
                case DocuEntityTypes.String:
                    {
                        var str = ((String)entity).Value;
                        htmDoc.txt(str);
                    }
                    break;
                case DocuEntityTypes.Text:
                    {
                        var txt = (ITxt)entity;
                        foreach (var word in txt.Words)
                        {
                            // mko, 9.6.2020
                            // Jedes Kind wird mit _Print verarbeitet, um so zu berücksichtigen, das einzelne Wörter eines
                            // Textes durch NID's dargestellt werden.
                            htmDoc.txt($"{word.Value} ");
                        }
                    }
                    break;
                case DocuEntityTypes.Bool:
                    {
                        var boolVal = (Boolean)entity;

                        htmDoc.span_class("bool").txt(boolVal.ValueAsBool
                            ? NC[TTD.Boolean.True.UID].NameIn(lng)
                            : NC[TTD.Boolean.False.UID].NameIn(lng)).E.build();

                        break;
                    }
                case DocuEntityTypes.Int:
                    {
                        var intVal = (Integer)entity;

                        htmDoc.span_class("int").txt(intVal.ValueAsLong.ToString()).E.build();
                        break;
                    }
                case DocuEntityTypes.Float:
                    {
                        var floatVal = (Double)entity;

                        htmDoc.span_class("float").txt(floatVal.Value.ToString()).E.build();
                        break;
                    }
                case DocuEntityTypes.Version:
                    // 15.11.2018
                    // Bei einer Versionsdefinition ist das erste Kind der Wert und nicht wie bei einer Eigenschaft erst der zweite
                    htmDoc.dfn_class("version").txt(NC[TT.Development.Version.UID].NameIn(lng)).E.build();

                    _Print(Level + 1, entity.Childs.First(), htmDoc);

                    htmDoc.br.build();
                    break;
                case DocuEntityTypes.ReturnValue:
                    {
                        var r = (IReturn)entity;

                        var glyph = Glyphs.Transactions.reject;


                        htmDoc.div_class("return");

                        // mko, 8.10.2018
                        // Zugriff auf Rückgabewerte robuster gemacht.
                        if (r.ReturnValue != null)
                        {
                            htmDoc.dfn.html(glyph).E.br.build();
                            _Print(Level + 1, r.ReturnValue, htmDoc);
                        }
                        else
                        {
                            htmDoc.dfn.html(glyph).E.br.build();
                        }

                        htmDoc.E.build();
                    }
                    break;
            }
        }

        /// <summary>
        /// mko, 18.4.2018
        /// Formats a xTab- property as a cross table
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        void xTabFormating(IInstance entity, HTML.HTMLDocument htmDoc)
        {
            htmDoc.table.build();

            var dim1 = (IProperty)entity.InstanceMembers.FirstOrDefault(m => m.EntityType == DocuEntityTypes.Property && m.HasName(ANC.DocuTerms.Formatting.XTab.Dim1.UID));
            if (dim1 != null && dim1.HasValue())
            {
                // First dimension as table header
                htmDoc.tr.th.nbsp.E.build();

                var dim1List = dim1.PropertyValue as IDTList;
                var dim1Str = dim1List.ListMembers.Select(m => (IProperty)m); //..Childs.Select(r => r.EntityValue().GetText(lng)).ToArray();

                foreach (var c in dim1Str)
                {
                    htmDoc.th.txt(c.PropertyValue.GetText(lng)).E.build();
                }

                htmDoc.E.build();

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
                    htmDoc.tr.td.txt(c2.PropertyValue.GetText(lng)).E.build();

                    foreach (var c1 in dim1Str)
                    {
                        htmDoc.td.build();

                        // i.e.: #i c1 #_ #p c2 #$ read #. #.

                        var _1 = values.FirstOrDefault(r => r.AreOfSameName(c1));
                        if (_1 != null)
                        {
                            var _2 = _1.InstanceMembers.FirstOrDefault(r => r.AreOfSameName(c2)) as IProperty;

                            if (_2 != null)
                            {
                                _Print(1, _2.PropertyValue, htmDoc);
                            }
                            else
                            {
                                htmDoc.nbsp.build();
                            }
                        }
                        else
                        {
                            htmDoc.nbsp.build();
                        }

                        htmDoc.E.build();

                    }
                    htmDoc.E.build();

                }
                htmDoc.E.build();
            }
            else
            {
                TraceHlp.ThrowEx(pnL.m(TT.Validation.Validate.UID,
                                    pnL.p_NID(TT.Grammar.Subject.UID, TT.Markup.Markup.UID),
                                    pnL.ret(pnL.eFails(TTD.Formatting.XTab.Errors.XTabStructInvalid.UID))));

            }
        }
    }
}
