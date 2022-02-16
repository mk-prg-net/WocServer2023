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

    /// <summary>
    /// mko, 2.3.2020
    /// 
    /// mko, 10.8.2021
    /// Angepasst an die neue, streng typisierte DocuEntity Lib
    /// </summary>
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

        ANC.NamingHelper NH { get; }

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
            this.NH = new ANC.NamingHelper(NC, lng);
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
            this.NH = new ANC.NamingHelper(NC, lng);
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

            try
            {

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


                            var name = entity.Name(lng, NC);

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


                            var name = i.Name(lng, NC);

                            if (i.InstanceMembers.Any())
                            {
                                // mko, 18.4.2018
                                // xTab <=> pivot table processing
                                if (name == "xTab")
                                {
                                    xTabFormating(i, htmDoc);
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.InProgressActivity.UID)))
                                {
                                    SentenceFormatting(i, htmDoc, Level, i.Glyph(NC));
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.FinishedActivity.UID)))
                                {
                                    SentenceFormatting(i, htmDoc, Level, i.Glyph(NC));
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.FutureActivity.UID)))
                                {
                                    SentenceFormatting(i, htmDoc, Level, i.Glyph(NC));
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.ModalQuestion.UID)))
                                {
                                    SentenceFormatting(i, htmDoc, Level, i.Glyph(NC));
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.ModalResponse.UID)))
                                {
                                    SentenceFormatting(i, htmDoc, Level, i.Glyph(NC));
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.Exclamation.UID)))
                                {
                                    SentenceFormatting(i, htmDoc, Level, i.Glyph(NC));
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.Object.UID)))
                                {
                                    ObjectFormatting(i, htmDoc, Level);
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.EnumerationOfObjects.UID)))
                                {
                                    ObjectEnumerationFormatting(htmDoc, Level, i);
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.ObjectsInPluralForm.UID)))
                                {
                                    ObjectsInPluralForm(htmDoc, Level, i);
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.ObjectsInRelationship.UID)))
                                {
                                    ObjectsInRelationship(htmDoc, Level, i);
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.Prepositions.PrepositionalObject.UID)))
                                {
                                    PrepositionalObject(htmDoc, Level, i);
                                }
                                else if (i.AreOfSameName(pnL.i(TT.Grammar.AdverbialClause.UID)))
                                {
                                    AdverbFormatting(i, htmDoc, Level);
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
                                        .hAtLevelWithClass(hLevel, "method").html(glyph).txt(m.Name(lng, NC)).E
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
                                htmDoc.div_class("method").hAtLevelWithClass(hLevel, "method").html(glyph).txt(m.Name(lng, NC)).E.E.build();
                            }

                        }
                        break;
                    case DocuEntityTypes.Property:
                        {
                            var p = (IProperty)entity;

                            var glyph = ShowGlyphs && p.Glyph(NC) != Glyphs.Text.SPC ? $"{p.Glyph(NC)}{Glyphs.Text.SPC}" : "";


                            htmDoc.div_class("property")
                                    .hAtLevelWithClass(hLevel, "property").html(glyph).txt(p.Name(lng, NC)).E
                                    //.h1.txt(p.Name(lng, NC)).E
                                    .div_class("propVal").build();

                            _Print(Level + 1, p.PropertyValue, htmDoc);

                            htmDoc.E.E.build();
                        }
                        break;
                    case DocuEntityTypes.PropertySet:
                        {
                            htmDoc.div_class("propertySet")
                                    .hAtLevelWithClass(hLevel, "propertySet").txt(entity.Name(lng, NC)).E.build();

                            _Print(Level + 1, entity.EntityValue(), htmDoc);

                            htmDoc.E.build();
                        }
                        break;
                    case DocuEntityTypes.NID:
                        {
                            // mko, 9.6.2020
                            // Abrufen des Namens in der Wunschsprache
                            var nid = (INID)entity;

                            if (ShowGlyphs && NH.htmlGlyph(nid.NamingId) != Glyphs.Text.SPC)
                            {
                                htmDoc.html($"{NH.htmlGlyph(nid.NamingId)}{Glyphs.Text.SPC}").txt(NH._(nid.NamingId));
                            }
                            else
                            {
                                htmDoc.txt(NH._(nid.NamingId));
                            }
                        }
                        break;
                    case DocuEntityTypes.String:
                        {
                            var str = ((IString)entity).ValueAsString;
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
                                htmDoc.txt($"{word.ValueAsString} ");
                            }
                        }
                        break;
                    case DocuEntityTypes.Bool:
                        {
                            var boolVal = (IBoolean)entity;

                            htmDoc.span_class("bool")
                                    .txt(
                                        boolVal.ValueAsBool
                                            ? NH._(TTD.Boolean.True.UID)
                                            : NH._(TTD.Boolean.False.UID))
                                    .E.build();

                            break;
                        }
                    case DocuEntityTypes.Int:
                        {
                            var intVal = (IInteger)entity;

                            htmDoc.span_class("int").txt(intVal.ValueAsLong.ToString()).E.build();
                            break;
                        }
                    case DocuEntityTypes.Float:
                        {
                            var floatVal = (IDouble)entity;

                            htmDoc.span_class("float").txt(floatVal.ValueAsDouble.ToString()).E.build();
                            break;
                        }
                    case DocuEntityTypes.Version:
                        {
                            var ver = (IVer)entity;

                            // 15.11.2018
                            // Bei einer Versionsdefinition ist das erste Kind der Wert und nicht wie bei einer Eigenschaft erst der zweite
                            htmDoc.dfn_class("version")
                                  .txt(NH._(TT.Development.Version.UID)).E
                                  .txt(ver.VersionString)
                                  .br
                                  .build();


                            //_Print(Level + 1, ver.VersionString, htmDoc);

                            //htmDoc.br.build();
                            break;
                        }
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
            catch (Exception ex)
            {
                htmDoc.div_class("eventFails")
                        .hAtLevel(hLevel).txt($"{NH.htmlGlyph(TTD.Formatting.Errors.FormattingError.UID)} {NH._(TTD.Formatting.Errors.FormattingError.UID)}").E
                        .p.txt(ex.Message).E
                    .E.build();
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


        /// <summary>
        /// mko, 12.4.2021
        /// Formatiert Strukturen grammatikalischer Objekte
        /// </summary>
        /// <param name="pv"></param>
        /// <param name="htmDoc"></param>
        /// <param name="Level"></param>
        void ObjectFormatting(IInstance i, HTML.HTMLDocument htmDoc, int Level)
        {
            PrintPropIfExists(TT.Abstraction.Abstraction.UID, i, 3, htmDoc);
            PrintPropIfExists(TT.Grammar.Adjectives.Adjective.UID, i, 3, htmDoc);
            PrintPropIfExists(TTD.MetaData.Name.UID, i, 3, htmDoc);
        }

        /// <summary>
        /// mko, 9.4.2021
        /// Formatiert eine Satzstruktur
        /// 
        /// mko, 5.5.2021
        /// Fromatieren einfacher Objekte ermöglicht
        /// </summary>
        /// <param name="i"></param>
        /// <param name="htmDoc"></param>
        /// <param name="Level"></param>
        /// <param name="EOSChar"></param>
        void SentenceFormatting(IInstance i, HTML.HTMLDocument htmDoc, int Level, string EOSChar)
        {
            // Ein Satz ist im allgemeinen eine folge aus *Subject* *Activity* *{Adverbials} *Object*
            // Zuerst wird versucht, diese Satzglieder auszulesen
            var getSubject = pnL.p(TT.Grammar.Subject.UID, pnL._v()).AsSubTreeOf(i, pnL);
            var getActivity = pnL.p(TT.Grammar.Verb.UID, pnL._v()).AsSubTreeOf(i, pnL);
            var getAllAdverbials = pnL.p(TT.Grammar.AdverbialClause.UID, pnL._v()).AsSubTreeOf_AllOccurrences(i, pnL);
            var getObject = pnL.p(TT.Grammar.Object.UID, pnL._v()).AsSubTreeOf(i, pnL);

            if (Language == ANC.Language.DE || Language == ANC.Language.EN || Language == ANC.Language.ES || Language == ANC.Language.CN)
            {
                // Aktuell wird die Formatierung eines semantisch sinnvollen Satzes nur für die 
                // Sprachen Deutsch, Englisch und Spanisch versucht

                if (getSubject.Succeeded && getSubject.Value.subTree is IProperty PropSubject)
                {
                    htmDoc.spc.build();
                    _Print(Level, PropSubject.PropertyValue, htmDoc);
                }

                if (getActivity.Succeeded && getActivity.Value.subTree is IProperty PropActivity)
                {
                    htmDoc.spc.build();
                    _Print(Level, PropActivity.PropertyValue, htmDoc);
                }

                if (getAllAdverbials.Succeeded)
                {
                    foreach (var tuple in getAllAdverbials.Value)
                    {
                        if (tuple.subTree is IProperty PropAdverb && PropAdverb.PropertyValue is IInstance InstanceAdverbClause)
                        {
                            PrintPropIfExists(TT.Grammar.Preposition.UID, InstanceAdverbClause, Level, htmDoc);

                            if (PrintPropIfExists(TT.Grammar.Adverbs.Adverb.UID, InstanceAdverbClause, Level, htmDoc)) { }
                            else if (PrintPropIfExists(TT.Timeline.TimeStamp.UID, InstanceAdverbClause, Level, htmDoc)) { }
                            else if (PrintPropIfExists(TT.Timeline.DateStamp.UID, InstanceAdverbClause, Level, htmDoc)) { }
                            else if (PrintPropIfExists(TT.Locations.Location.UID, InstanceAdverbClause, Level, htmDoc)) { }
                        }
                        else
                        {
                            // Fehler inline ausgeben
                            _Print(Level,
                                pnL.eFails(
                                    pnL.i(TTD.MetaData.Details.UID,
                                        pnL.p(TTD.StateDescription.WhatsUp.UID,
                                            pnL.ModalResponse(
                                                NH.mP(TT.Documents.Formatting.CantFormat.UID),
                                                pnL.DefObject(TT.Grammar.Adverbs.Adverb.UID))),
                                        pnL.p(TTD.StateDescription.Why.UID,
                                                pnL.FinishedActivityStatement(
                                                    NH.fA(TT.Search.NotFound.UID),
                                                    pnL.DefAinRelationToB(TTD.Types.Property.UID, NH.pp(TT.Operators.Sets.Contains.UID), TT.Grammar.Adverbs.Adverb.UID))))),
                                htmDoc);
                        }
                    }
                }

                if (getObject.Succeeded && getObject.Value.subTree is IProperty PropObj && PropObj.PropertyValue is IInstance objTree)
                {
                    htmDoc.spc.build();
                    _Print(Level, objTree, htmDoc);
                }

                // Satzendezeichen ausgeben
                htmDoc.html(EOSChar);

            }
            else
            {
                if (getSubject.Succeeded)
                {
                    _Print(Level, getSubject.Value.subTree, htmDoc);
                }

                if (getActivity.Succeeded)
                {
                    _Print(Level, getActivity.Value.subTree, htmDoc);
                }

                if (getAllAdverbials.Succeeded)
                {
                    foreach (var tuple in getAllAdverbials.Value)
                    {
                        if (tuple.subTree is IProperty PropAdverb && PropAdverb.PropertyValue is IInstance InstanceAdverbClause)
                        {
                            PrintPropIfExists(TT.Grammar.Preposition.UID, InstanceAdverbClause, Level, htmDoc);

                            //if (PrintPropIfExists(TT.Grammar.Adverbs.Adverb.UID, InstanceAdverbClause, Level, htmDoc)) { }
                            //else if (PrintPropIfExists(TT.Timeline.TimeStamp.UID, InstanceAdverbClause, Level, htmDoc)) { }
                            //else if (PrintPropIfExists(TT.Timeline.DateStamp.UID, InstanceAdverbClause, Level, htmDoc)) { }
                            //else if (PrintPropIfExists(TT.Locations.Location.UID, InstanceAdverbClause, Level, htmDoc)) { }

                            // mko, 27.9.2021
                            // alle möglichen Adverbein werden ausgegeben, falls vorhanden. Vorher wurde aus irgend einen Grund
                            // nue eines ausgegeben.
                            PrintPropIfExists(TT.Grammar.Adverbs.Adverb.UID, InstanceAdverbClause, Level, htmDoc);
                            PrintPropIfExists(TT.Timeline.TimeStamp.UID, InstanceAdverbClause, Level, htmDoc);
                            PrintPropIfExists(TT.Timeline.DateStamp.UID, InstanceAdverbClause, Level, htmDoc);
                            PrintPropIfExists(TT.Locations.Location.UID, InstanceAdverbClause, Level, htmDoc);

                        }
                        else
                        {
                            // Fehler inline ausgeben
                            _Print(Level,
                                pnL.eFails(
                                    pnL.i(TTD.MetaData.Details.UID,
                                        pnL.p(TTD.StateDescription.WhatsUp.UID,
                                            pnL.ModalResponse(
                                                NH.mP(TT.Documents.Formatting.CantFormat.UID),
                                                pnL.DefObject(TT.Grammar.Adverbs.Adverb.UID))),
                                        pnL.p(TTD.StateDescription.Why.UID,
                                                pnL.FinishedActivityStatement(
                                                    NH.fA(TT.Search.NotFound.UID),
                                                    pnL.DefAinRelationToB(TTD.Types.Property.UID, NH.pp(TT.Operators.Sets.Contains.UID), TT.Grammar.Adverbs.Adverb.UID))))),
                                htmDoc);
                        }
                    }
                }

                if (getObject.Succeeded)
                {
                    _Print(Level, getObject.Value.subTree, htmDoc);
                }
            }
        }

        void AdverbFormatting(IInstance i, HTML.HTMLDocument htmDoc, int Level)
        {
            PrintPropIfExists(TT.Grammar.Preposition.UID, i, Level, htmDoc);
            PrintPropIfExists(TT.Grammar.Adverbs.Adverb.UID, i, Level, htmDoc);
            PrintPropIfExists(TT.Timeline.TimeStamp.UID, i, Level, htmDoc);
            PrintPropIfExists(TT.Timeline.DateStamp.UID, i, Level, htmDoc);
            PrintPropIfExists(TT.Locations.Location.UID, i, Level, htmDoc);

        }

        /// <summary>
        /// mko, 11.5.2021
        /// Sucht Eigenschaften in einer Objektinstanz nach dem Eigenschaftname und gibt diese aus, falls sie existieren.
        /// </summary>
        /// <param name="PropNameNID"></param>
        /// <param name="objTree"></param>
        /// <param name="Level"></param>
        /// <returns></returns>
        bool PrintPropIfExists(long PropNameNID, IInstance objTree, int Level, HTML.HTMLDocument htmDoc)
        {
            var getProp = pnL.p(PropNameNID, pnL._v()).AsSubTreeOf(objTree, pnL);

            if (!getProp.Succeeded && !pnL.ReturnSearchFailsEmptyResult().IsSubTreeOf(getProp.MessageEntity))
            {
                // Fehler in der Struktur
                htmDoc.spc.build();
                _Print(Level, getProp.MessageEntity, htmDoc);
                return false;
            }
            else if (!getProp.Succeeded)
            {
                // Partikel nicht gefunden
                return false;
            }
            else if (getProp.Succeeded && getProp.Value.subTree is IProperty Prop)
            {
                htmDoc.spc.build();
                _Print(Level, Prop.PropertyValue, htmDoc);
                return true;
            }
            else
            {
                htmDoc.spc.build();
                _Print(Level, pnL.m("Get", pnL.eFails(pnL.FinishedActivityStatement(TTD.Types.Property.UID, NH.fA(TT.Grammar.Verbs.WasExpected.UID)))), htmDoc);
                return false;
            }
        }

        private void PrepositionalObject(HTML.HTMLDocument htmDoc, int Level, IInstance objTree)
        {
            // Ein Präpositionales Objket liegt vor                        

            PrintPropIfExists(TT.Grammar.Preposition.UID, objTree, Level, htmDoc);
            PrintPropIfExists(TT.Grammar.Object.UID, objTree, Level, htmDoc);
        }

        private void ObjectsInPluralForm(HTML.HTMLDocument htmDoc, int Level, IInstance objTree)
        {
            PrintPropIfExists(TT.Grammar.Preposition.UID, objTree, Level, htmDoc);
            PrintPropIfExists(TT.Metrology.Counter.UID, objTree, Level, htmDoc);
            PrintPropIfExists(TTD.MetaData.Name.UID, objTree, Level, htmDoc);
        }

        private void ObjectsInRelationship(HTML.HTMLDocument htmDoc, int Level, IInstance objTree)
        {
            PrintPropIfExists(TT.Sets.First.UID, objTree, Level, htmDoc);
            PrintPropIfExists(TT.Grammar.Preposition.UID, objTree, Level, htmDoc);
            PrintPropIfExists(TT.Sets.Second.UID, objTree, Level, htmDoc);
        }

        /// <summary>
        /// mko, 11.5.2021
        /// </summary>
        /// <param name="htmDoc"></param>
        /// <param name="Level"></param>
        /// <param name="objTree"></param>
        private void ObjectEnumerationFormatting(HTML.HTMLDocument htmDoc, int Level, IInstance objTree)
        {
            var objCount = 0;

            var getPropCounter = pnL.p(TT.Metrology.Counter.UID, pnL._v()).AsSubTreeOf(objTree, pnL);
            if (getPropCounter.Succeeded
                && getPropCounter.Value.subTree is IProperty propCounter
                && propCounter.PropertyValue is Integer counterVal)
            {
                objCount = counterVal.ValueAsInteger;
            }
            else
            {
                // Fehler inline ausgeben
                _Print(Level,
                    pnL.eFails(
                        pnL.i(TTD.MetaData.Details.UID,
                            pnL.p(TTD.StateDescription.WhatsUp.UID,
                                pnL.FinishedActivityStatement(
                                    NH.fA(TT.Documents.Formatting.CantFormat.UID),
                                    pnL.DefAinRelationToB(TT.Sets.Enumeration.UID, NH.pp(TT.Grammar.Prepositions.Of.UID), TT.Grammar.Object.UID))),
                            pnL.p(TTD.StateDescription.Why.UID,
                                pnL.FinishedActivityStatement(NH.fA(TT.Access.CantRead.UID),
                                pnL.DefAinRelationToB(TTD.Types.Property.UID,
                                NH.pp(TT.Grammar.Prepositions.Of.UID),
                                TT.Metrology.Counter.UID))))),
                    htmDoc);
            }

            var getEnumeratedObjectNames = pnL.p(TTD.MetaData.Name.UID, pnL._v()).AsSubTreeOf_AllOccurrences(objTree, pnL);

            if (getEnumeratedObjectNames.Succeeded
                && getEnumeratedObjectNames.Value.Any())
            {
                htmDoc.nbsp.build();
                foreach (IProperty PropObjectName in getEnumeratedObjectNames.Value.Select(r => r.subTree))
                {
                    var name = PropObjectName.PropertyValue.GetText(Language);
                    htmDoc.txt(name);
                    if (objCount > 1)
                    {
                        htmDoc.txt(", ");
                    }

                    objCount--;
                }
            }
        }
    }
}
