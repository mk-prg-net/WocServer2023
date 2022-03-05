using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static mko.Algo.Listprocessing.Fn;

using ANC = MKPRG.Naming;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

using MKPRG.Tracing.DocuTerms.Parser;

namespace MKPRG.Tracing.DocuTerms
{

    /// <summary>
    /// mko, 26.3.2018
    /// 
    /// mko, 3.3.2020
    /// Composer- Funktionen, die statt eines Namens eine Naming- Container ID annehmen, implementiert.
    /// 
    /// mko, 9.8.2021
    /// Weitestgehend reimplementiert auf Basis strenger typisierter Docu- Terme
    /// </summary>
    public partial class Composer
        : IComposer
    {
        IFn fn;
        IFormater fmt;
        IReadOnlyDictionary<long, ANC.INaming> NC;
        ANC.NamingHelper NH;


        public Composer(IFn fn, IReadOnlyDictionary<long, ANC.INaming> NC, ANC.NamingHelper NH, IFormater fmt)
        {
            this.fn = fn;
            this.NC = NC;
            this.NH = NH;
            this.fmt = fmt;
        }

        public Composer()
        {
            fn = Fn._;
            NC = RC.NC;
            NH = new ANC.NamingHelper(NC);
            fmt = RC.fmtPN;
        }

        public Composer(IFormater fmt)
            : this()
        {
            this.fmt = fmt;
        }


        public Composer(IFn fn)
            : this()
        {
            this.fn = fn;
        }

        public Composer(IFn fn, IFormater fmt)
            : this()
        {
            this.fn = fn;
            this.fmt = fmt;
        }

        // ----------------------------------------------------------------------------------------------------
        // Hilfsmethoden zum Formatieren von Werten

        /// <summary>
        /// Formatiert einen bool- Wert als docuEntity
        /// </summary>
        /// <param name="b"></param>
        /// <returns></returns>
        IBoolean CreateBoolEntity(bool b)
            => new Boolean(b);

        /// <summary>
        /// Formatiert einen Integer als DocuEntity
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        IInteger CreateIntEntity(int i)
            => new Integer(i);

        /// <summary>
        /// Formatiert einen Long als DocuEntity
        /// 
        /// mko, 12.10.2020
        /// Long wird nicht mehr al Text, sondern als DT.Long Objekt genereiert
        /// </summary>
        /// <param name="lng"></param>
        /// <returns></returns>
        IInteger CreateLongEntity(long lng)
            //=> txt(lng.ToString() + "L");
            => new Integer(lng);


        /// <summary>
        /// Formatiert einen Double als DokuEntity
        /// </summary>
        /// <param name="dbl"></param>
        /// <returns></returns>
        IDouble CreateDblEntity(double dbl)
            => new Double(dbl);


        /// <summary>
        /// mko, 15.6.2020
        /// Steht für einen beliebigen Wert einer Eigenschaft im Kontext des IsSubtree- Vergleiches
        /// </summary>
        /// <returns></returns>
        public IWildCard _v()
            => new WildCard();

        /// <summary>
        /// Wildcard für einen Namen. 
        /// Wenn
        /// </summary>
        public long _n
            => TTD.Types.WildCard.UID;

        /// <summary>
        /// mko, 17.6.2020
        /// Steht für einen beliebigen Wert einer Eigenschaft im Kontext des IsSubtree- Vergleiches.
        /// Es gilt die Einschränkung, das der Werd irgenwo den angegebenen SubTree enthält.
        /// </summary>
        /// <param name="subTree"></param>
        /// <returns></returns>
        public IWildCard _v(IDocuEntity subTree)
            => new WildCard(subTree);

        public IBoolean boolean(bool b)
            => new Boolean(b);

        public IInteger integer(long i)
            => new Integer(i);

        public IDouble dbl(double d)
            => new Double(d);

        public IString str(string s)
            => new String(s);

        /// <summary>
        /// 24.6.2020
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public INID NID(long nid)
            => new NID(nid);

        /// <summary>
        /// mko, 22.7.2020
        /// Erzeugt eine Kreuztabelle als DocuTerm- Struktur.
        /// Die HTML- Formatter rendern diese z.B. in einer HTML- Tabelle.
        /// </summary>
        /// <returns></returns>
        public IXTabGeneratorDefCols XTab()
        {
            return new XTabGeneratorCols(this);
        }



        /// <summary>
        /// mko
        /// Erzeugt ein Datumswert
        /// 
        /// mko, 15.6.2020
        /// Datumswerte werden jetzt als nummerische Triple (Jahr, Monat, Tag) definiert
        /// 
        /// mko, 9.8.2021
        /// Reimplementiert mit neuen DT- Klassen
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public IDate date(DateTime dat)
            => new DTDate(dat.Year, dat.Month, dat.Day);

        public IDate date(int year, int month, int day)
            => new DTDate(year, month, day);


        /// <summary>
        /// mko, 15.6.2020
        /// Ab heute wird die Zeit nicht mehr als String- Konstante, sondern als Tripel aus drei Integer- Konstanten
        /// gespeichert (Stunde, Minute, Sekunde). Hierdurch sollen auch Auswertungen möglich werden, wie z.B.
        /// alle Meldungen ab 11:00 Uhr.
        /// 
        /// mko, 9.8.2021
        /// Reimplementiert mit neuen, schlanken DT. Klassen.
        /// </summary>
        /// <param name="dat"></param>
        /// <param name="showMilliseconds"></param>
        /// <returns></returns>
        public ITime time(TimeSpan dat, bool showMilliseconds = false)
            => new DTTime(dat.Hours, dat.Minutes, dat.Seconds, showMilliseconds ? dat.Milliseconds : 0);

        public ITime time(int hour, int minutes, int sec, int milliseconds = 0)
            => new DTTime(hour, minutes, sec, milliseconds);


        /// <summary>
        /// mko, 4.12.2020
        /// Fall text == null behandelt
        /// 
        /// mko, 9.8.2021
        /// Reimplementiert mit neuen, schlanken DT- Klassen
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ITxt txt(string text)
        {
            if (text != null)
            {
                var strArr = text.Replace("#", " ").Split(L(' ').ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(r => new String(r)).ToArray();
                return new Txt(strArr);
            }
            else
            {

                return new Txt();
            }
        }

        /// <summary>
        /// mko, 6.7.2021
        /// Gegen null- Wert in versionStr gesichert.
        /// </summary>
        /// <param name="versionStr"></param>
        /// <returns></returns>
        public IVer ver(string versionStr)
            => new Ver(versionStr);

        // == KillIfNot ====== ====== ======

        /// <summary>
        /// Dokuentity wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillListElementIfNot KillListMemberIfNot(bool Condition, Func<IListMember> docuEntityFactory)
            => new KillListMemberIfNot(Condition, docuEntityFactory);


        /// <summary>
        /// mko, 24.7.2018
        /// DokuEntity wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillListElementIfNot KillListMemberIf(bool Condition, Func<IListMember> docuEntityFactory)
            => new KillListMemberIfNot(!Condition, docuEntityFactory);


        /// <summary>
        /// mko, 17.6.2020
        /// Event- Parameter wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="createEventParam"></param>
        /// <returns></returns>
        public IKillEventParamIfNot KillEventParamIfNot(bool Condition, Func<IEventParameter> createEventParam)
            => new KillEventParamIfNot(Condition, createEventParam);


        /// <summary>
        /// mko, 17.6.2020
        /// Event- Parameter wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillEventParamIfNot KillEventParamIf(bool Condition, Func<IEventParameter> createEventParam)
            => new KillEventParamIfNot(!Condition, createEventParam);


        /// <summary>
        /// mko, 18.6.2020
        /// InstanceMember wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="createInstanceMember"></param>
        /// <returns></returns>
        public IKillInstanceMemberIfNot KillInstanceMemberIfNot(bool Condition, Func<IInstanceMember> createInstanceMember)
            => new KillInstanceMemberIfNot(Condition, createInstanceMember);

        /// <summary>
        /// mko, 17.6.2020
        /// InstanceMember wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillInstanceMemberIfNot KillInstanceMemberIf(bool Condition, Func<IInstanceMember> createInstanceMember)
            => new KillInstanceMemberIfNot(!Condition, createInstanceMember);


        /// <summary>
        /// mko, 18.6.2020
        /// InstanceMember wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillMethodPrarmeterIfNot KillMethodParamIf(bool Condition, Func<IMethodParameter> createInstanceMember)
            => new KillMethodParameterIfNot(!Condition, createInstanceMember);


        /// <summary>
        /// mko, 18.6.2020
        /// InstanceMember wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="createInstanceMember"></param>
        /// <returns></returns>
        public IKillMethodPrarmeterIfNot KillMethodParamIfNot(bool Condition, Func<IMethodParameter> createInstanceMember)
            => new KillMethodParameterIfNot(Condition, createInstanceMember);

        // == IfElse ====== ====== ======

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        //public IListMember IfElse(bool Condition, Func<IListMember> memberIfTrue, Func<IListMember> memberIfFalse)
        //    => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        public IReturnValue IfElseRet(bool Condition, Func<IReturnValue> memberIfTrue, Func<IReturnValue> memberIfFalse)
            => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="propIfTrue"></param>
        /// <param name="propIfFalse"></param>
        /// <returns></returns>
        public IProperty IfElseProp(bool Condition, Func<IProperty> propIfTrue, Func<IProperty> propIfFalse)
            => Condition ? propIfTrue() : propIfFalse();


        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        public IPropertyValue IfElsePropVal(bool Condition, Func<IPropertyValue> memberIfTrue, Func<IPropertyValue> memberIfFalse)
            => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="eventIfTrue"></param>
        /// <param name="eventIfFalse"></param>
        /// <returns></returns>
        public IEvent IfElseEvent(bool Condition, Func<IEvent> eventIfTrue, Func<IEvent> eventIfFalse)
            => Condition ? eventIfTrue() : eventIfFalse();

        /// <summary>
        /// mko, 18.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        public IEventParameter IfElseEventParam(bool Condition, Func<IEventParameter> memberIfTrue, Func<IEventParameter> memberIfFalse)
            => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="instIfTrue"></param>
        /// <param name="instIfFalse"></param>
        /// <returns></returns>
        public IInstance IfElseInstance(bool Condition, Func<IInstance> instIfTrue, Func<IInstance> instIfFalse)
            => Condition ? instIfTrue() : instIfFalse();

        /// <summary>
        /// mko, 8.7.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        public IInstanceMember IfElseInstMember(bool Condition, Func<IInstanceMember> memberIfTrue, Func<IInstanceMember> memberIfFalse)
            => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="methIfTrue"></param>
        /// <param name="methIfFalse"></param>
        /// <returns></returns>
        public IMethod IfElseMethod(bool Condition, Func<IMethod> methIfTrue, Func<IMethod> methIfFalse)
            => Condition ? methIfTrue() : methIfFalse();

        /// <summary>
        /// mko, 10.8.2021
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="methIfTrue"></param>
        /// <param name="methIfFalse"></param>
        /// <returns></returns>
        public IMethodParameter IfElseMethodParam(bool Condition, Func<IMethodParameter> methIfTrue, Func<IMethodParameter> methIfFalse)
            => Condition ? methIfTrue() : methIfFalse();


        // == Embed ====== ====== ======

        /// <summary>
        /// Embeds properties in current instance or method parameter list
        /// Note that 
        ///    thisMethod(p1, ... px, px+1 = embed(entities), px+2 ...) 
        /// is not the same like 
        ///    thisMethod(p1, ... px, px+1 = List(entities), px+2 ...).
        ///    
        /// List then will be a parameter of Method, and entities are childs of list:
        /// thisMethod             
        ///    +--> List (Parameters as List)
        ///           +--> p1
        ///           : 
        ///           +--> px
        ///           +--> px+1 = List
        ///           |             +-->> entities
        ///           +--> px+2
        ///           :
        /// Instead of after calling thisMethod(.., embed(entities), ..) entities are direct parameters of thisMethod
        /// thisMethod             
        ///    +--> List (Parameters as List)
        ///           +--> p1
        ///           : 
        ///           +--> px
        ///           +-->> entities        
        ///           +--> px+2
        ///           :
        ///           
        /// Note: embeding will be done in CreateMemebers
        /// 
        /// mko, 6.7.2021
        /// 
        /// mko, 9.8.2021
        /// Reimplementiert mit neuen, schlanken und Nullwertfesten DT- Klassen
        /// 
        /// </summary>
        /// <param name="memberToEmbed"></param>
        /// <returns></returns>
        public IListMembersToEmbed EmbedListMembers(params IListMember[] memberToEmbed)
            => new ListMembersToEmbedClass(memberToEmbed);

        /// <summary>
        /// mko, 10.8.2021
        /// </summary>
        /// <param name="memberToEmbed"></param>
        /// <returns></returns>
        public IInstanceMembersToEmbed EmbedInstanceMembers(params IInstanceMember[] memberToEmbed)
            => new InstanceMembersToEmbedClass(memberToEmbed);


        /// <summary>
        /// mko, 10.8.2021
        /// </summary>
        /// <param name="parameterToEmbed"></param>
        /// <returns></returns>
        public IMethodParametersToEmbed EmbedMethodParameters(params IMethodParameter[] parameterToEmbed)
            => new MethodParametersToEmbedClass(parameterToEmbed);

        // ↯↯↯
        // ↯↯↯ Events ↯↯↯ ↯↯↯ ↯↯↯ 
        // ↯↯↯

        /// <summary>
        ///  Allgemeines Event
        ///  
        /// mko, 4.12.2020
        /// Fall value == null behandelt
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent e(string name, IEventParameter value)
            => new EventWithNameAsString(name, value);


        /// <summary>
        /// mko, 4.12.2020
        /// Fall value == null behandelt
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent e(IString name, IEventParameter value)
            => new EventWithNameAsString(name?.ValueAsString ?? String.NameIsNull.ValueAsString, value);


        public IEvent e(string name)
            => new EventWithNameAsString(name);

        public IEvent e(IString name)
            => new EventWithNameAsString(name?.ValueAsString ?? String.NameIsNull.ValueAsString);

        /// <summary>
        /// mko, 3.3.2020
        /// Eventname ist einen DokuTermID. Diese kann über Naming- Container in verschiedenen Sprachen präsnetiert werden.
        /// </summary>
        /// <param name="DID">DokuTermID</param>
        /// <returns></returns>
        public IEvent e(long nid)
            => new EventWithNameAsNID(NID(nid));

        public IEvent e(INID nid)
            => new EventWithNameAsNID(nid);

        /// <summary>
        /// mko, 3.3.2020
        /// Eventname ist einen Name Container ID. Diese kann über Naming- Container in verschiedenen Sprachen präsnetiert werden.
        /// 
        /// mko, 4.12.2020
        /// Fall value == null behandelt
        /// </summary>
        /// <param name="NID">Name Container ID</param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent e(long nid, IEventParameter value)
            => new EventWithNameAsNID(NID(nid), value);

        /// <summary>
        /// mko, 4.12.2020
        /// Fall value == null behandelt
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent e(INID nid, IEventParameter value)
            => new EventWithNameAsNID(nid, value);

        /// <summary>
        /// mko, 10.4.2018
        /// Creates a event with paramters, encapsulated in a list.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent ePrms(string name, params IEventParameter[] pn)
            => new EventWithNameAsString(name, List(pn));

        IEventParameter CreateListWithResultProperty(IPropertyValue pVal)
            => List(p(TTD.MetaData.Result.UID, pVal));

        public IEvent eEnd(IEventParameter value)
            => e(TTD.Event.End.UID, value);

        //public IEvent eEnd(IKillEventParamIfNot killIfNot)
        //    => e(TTD.Event.End.UID, killIfNot);

        public IEvent eEnd()
            => e(TTD.Event.End.UID);

        public IEvent eEnd(string value)
            => eEnd(CreateListWithResultProperty(txt(value)));

        public IEvent eEnd(long nid)
            => eEnd(CreateListWithResultProperty(NID(nid)));

        public IEvent eSucceeded(IEventParameter value)
            => e(TTD.Event.Succeeded.UID, value);

        //public IEvent eSucceeded(IKillEventParamIfNot killIfNot)
        //    => e(TTD.Event.Succeeded.UID, killIfNot);

        public IEvent eSucceeded()
            => e(TTD.Event.Succeeded.UID);

        public IEvent eSucceeded(string value)
            => eSucceeded(CreateListWithResultProperty(txt(value)));

        public IEvent eSucceeded(long value)
            => eSucceeded(CreateListWithResultProperty(NID(value)));

        /// <summary>
        /// mko, 2.4.2019
        /// eFails mit KillIfNot Filtersemantik
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eFails(IEventParameter value)
            => e(TTD.Event.Fails.UID, value);

        //public IEvent eFails(IKillEventParamIfNot killIfNot)
        //    => e(TTD.Event.Fails.UID, killIfNot);

        public IEvent eFails()
            => e(TTD.Event.Fails.UID);

        public IEvent eFails(string value)
            => eFails(CreateListWithResultProperty(txt(value)));

        public IEvent eFails(long nid)
            => eFails(CreateListWithResultProperty(NID(nid)));


        /// <summary>
        /// mko, 18.6.2020
        /// Häufig sind Rückgabewerte von Unterprogrammen zu dokumentieren, die als allgemeine IDocuEntity vorliegen. Diese sind dann in eine 
        /// eFails oder eSucceeded zu kapseln. Ist der IDocuEntity eine Mehtode oder ein Eigenschaftswert, dann müssen diese zunächst in eine
        /// Instanz gekapselt werden, die einen IEventParameter darstellt. Diese Funktion führt dies aus
        /// 
        /// mko, 29.6.2021
        /// Fall docuTerm == null hinzugefügt
        /// </summary>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        public IEventParameter EncapsulateAsEventParameter(IDocuEntity docuTerm)
        {
            if (docuTerm == null)
            {
                return Instance.NullValue;
            }
            else if (docuTerm is IEventParameter)
                return (IEventParameter)docuTerm;
            else if (docuTerm is IEvent eventP)
            {
                return i(TTD.MetaData.Result.UID, eventP);
            }
            else if (docuTerm is IMethod method)
                // Methode in einer Instanz einkapseln
                return i(TTD.MetaData.Result.UID, method);
            else if (docuTerm is IProperty prop)
                return i(TTD.MetaData.Result.UID, prop);
            else if (docuTerm is IPropertyValue propVal)
                // Eigenschaftswert in einer Eigenschaft innerhalb einer Instanz einkapseln
                return i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, propVal));
            else if (docuTerm is IReturn ret)
                return i(TTD.MetaData.Result.UID, m(TT.Runtime.CalledUpFunction.UID, ret));
            else if (docuTerm is ITxt txt)
                return CreateListWithResultProperty(txt); // i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (ITxt)docuTerm));
            else if (docuTerm is String str)
                return CreateListWithResultProperty(str); // i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (String)docuTerm));
            else if (docuTerm is Integer integer)
                return CreateListWithResultProperty(integer);
            else if (docuTerm is Boolean b)
                return CreateListWithResultProperty(b);
            else if (docuTerm is Double d)
                return CreateListWithResultProperty(d);
            else if (docuTerm is IDate dat)
                return CreateListWithResultProperty(dat);
            else if (docuTerm is ITime time)
                return CreateListWithResultProperty(time);
            else
                return i(TTD.MetaData.Result.UID, eFails(TTD.Composer.Errors.CantEncapsulateAsEventParameter.UID));
        }


        /// <summary>
        /// Transaktion noch nicht fertig gestellt.
        /// </summary>
        /// <returns></returns>
        public IEvent eNotCompleted()
            => e(TTD.Event.NotCompleted.UID);

        //public IEvent eNotCompleted(IKillEventParamIfNot killIfNot)
        //    => e(TTD.Event.NotCompleted.UID, killIfNot);

        public IEvent eNotCompleted(IEventParameter value)
            => e(TTD.Event.NotCompleted.UID, value);

        public IEvent eNotCompleted(string value)
            => eNotCompleted(CreateListWithResultProperty(txt(value)));

        public IEvent eNotCompleted(long nid)
            => eNotCompleted(CreateListWithResultProperty(NID(nid)));

        /// <summary>
        /// Informationen zu einer Zustandsänderung
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eInfo(IEventParameter value)
            => e(TTD.Event.Info.UID, value);

        //public IEvent eInfo(IKillEventParamIfNot killIfNot)
        //    => e(TTD.Event.Info.UID, killIfNot);

        public IEvent eInfo()
            => e(TTD.Event.Info.UID);

        public IEvent eInfo(string value)
            => eInfo(CreateListWithResultProperty(txt(value)));

        public IEvent eInfo(long nid)
            => eInfo(CreateListWithResultProperty(NID(nid)));

        /// <summary>
        /// Signalisiert den Start einer Transaktion
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eStart(IEventParameter value)
            => e(TTD.Event.Start.UID, value);

        //public IEvent eStart(IKillEventParamIfNot killIfNot)
        //    => e(TTD.Event.Start.UID, killIfNot);

        public IEvent eStart()
            => e(TTD.Event.Start.UID);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        public IEvent eStart(string info)
            => eStart(CreateListWithResultProperty(txt(info)));

        public IEvent eStart(long nid)
            => eStart(CreateListWithResultProperty(NID(nid)));

        /// <summary>
        /// Warnt vor kritischem Zustand während einer Transaktion
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eWarn(IEventParameter value)
            => e(TTD.Event.Warn.UID, value);

        //public IEvent eWarn(IKillEventParamIfNot killIfNot)
        //    => e(TTD.Event.Warn.UID, killIfNot);

        public IEvent eWarn()
            => e(TTD.Event.Warn.UID);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eWarn(string value)
            => eWarn(CreateListWithResultProperty(txt(value)));


        public IEvent eWarn(long nid)
            => eWarn(CreateListWithResultProperty(NID(nid)));

        // 🜎🜎🜎
        // 🜎🜎🜎 Instanzen 🜎🜎🜎 🜎🜎🜎 🜎🜎🜎 
        // 🜎🜎🜎

        /// <summary>
        /// Erzeugt eine benannte Instanz
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IInstance i(string name, params IInstanceMember[] pn)
            => new InstanceWithNameAsString(name, pn);

        /// <summary>
        /// mko, 3.3.2020
        /// Erzeugt eine Instanz, wobei der Name eine Naming- Container ID ist. 
        /// Damit kann der Instanzname von Formatern kulturspezifisch ausgegeben werden.
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IInstance i(long nid, params IInstanceMember[] pn)
            => new InstanceWithNameAsNID(NID(nid), pn);

        /// <summary>
        /// mko, 15.3.2021
        /// 
        /// Der Instanznamen kann durch einen Wildcard verallgemeinert werden. Dies ermöglicht mächtigere Musterausdrücke für 
        /// Pattern- Matching
        /// 
        /// mko, 9.8.2021
        /// Gibt es nicht mehr. WildCards für Namen sind jetzt eine speziele NID, welche die Funktion _n() liefert.
        /// 
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        //public IInstance i(IWildCard wc, params IInstanceMember[] pn)
        //       => CreateDocuTermWithMemberList(
        //           wc,
        //           (_wc, _pn) => new Instance(fmt, _wc, _pn),
        //           _wc => new Instance(fmt, _wc),
        //           nid => new Instance(fmt, new NID(fmt, nid)),
        //           _pn => KillInstanceMemberIfContNotMetFilter(_pn),
        //           pn);


        // ⎔⎔⎔
        // ⎔⎔⎔ Methoden ⎔⎔⎔ ⎔⎔⎔ ⎔⎔⎔ ⎔⎔⎔
        // ⎔⎔⎔

        /// <summary>
        /// Dokumentiert einen Methodenaufruf.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IMethod m(string name, params IMethodParameter[] pn)
            => new MethodWithNameAsString(name, pn);

        /// <summary>
        /// mko, 3.3.2020
        /// Erzeugt eine Instanz, wobei der Name eine Naming- Container ID ist. 
        /// Damit kann der Instanzname von Formatern kulturspezifisch ausgegeben werden.
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IMethod m(long nid, params IMethodParameter[] pn)
            => new MethodWithNameAsNID(NID(nid), pn);

        // ↺↺↺
        // ↺↺↺ Return ↺↺↺ ↺↺↺ ↺↺↺
        // ↺↺↺

        /// <summary>
        /// mko, 10.4.2018
        /// Returnvalue
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        //public IReturn ret(params IReturnValue[] pn)
        //{
        //    return new Return(fmt, List(pn));
        //}

        public IReturn ret(IReturnValue pn)
        {
            return new Return(pn);
        }

        /// <summary>
        /// mko, 17.6.2020
        /// Ergebnisse von Methoden werden als Docuentity ausführlich dokumentiert:
        /// Gibt eine Methode z.B. einen Integer- Wert zurück, dann wird von einer erfolgreichen Berechnung ausgegangen
        ///   -> einkapseln in eSucceeded
        /// Da Integers nur als Eigenschaftswerte erlaubt sind, wird der Integer in die allgemeine Eigenschaft "Result" 
        /// eingekapselt. Eigenschaften selber können nicht direkt Kinder eines Events sein, sondern nur Member von Listen
        ///   -> einkapseln in Liste, das wiederum Kind des Events eSucceeded ist.
        /// </summary>
        /// <param name="pVal"></param>
        /// <returns></returns>
        IReturn returnResultProperty(IPropertyValue pVal)
            //=> ret(eSucceeded(List(p(TTD.MetaData.Result.UID, pVal))));
            => pVal is IReturnValue rVal
                ? ret(rVal)
                : ret(i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, EncapsulateAsPropertyValue(pVal))));

        /// <summary>
        /// Boolean zurückgeben
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IReturn ret(bool res)
            => returnResultProperty(CreateBoolEntity(res));

        /// <summary>
        /// Integer zurückgeben
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IReturn ret(int res)
            => returnResultProperty(CreateIntEntity(res));

        /// <summary>
        /// Long zurückgeben
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IReturn ret(long res)
            => returnResultProperty(CreateLongEntity(res));

        /// <summary>
        /// String zurückgeben
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        public IReturn ret(string res)
            => returnResultProperty(txt(res));

        /// <summary>
        /// mko, 15.6.2020
        /// Rückgabewert erzeugen, dessen Wert durch sprachneutrale Naming ID definiert wird (siehe ATMO.DFC.Naming)
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public IReturn ret_NID(long nid)
            => returnResultProperty(new NID(nid));

        // == Properties ====== ====== ======

        /// <summary>
        ///        
        /// mko, 29.6.2021
        /// Fall docuTerm == null hinzugefügt.
        /// </summary>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        public IPropertyValue EncapsulateAsPropertyValue(IDocuEntity docuTerm)
        {
            if (docuTerm == null)
            {
                return NID(TT.Sets.NullValue.UID);
            }
            else if (docuTerm is IPropertyValue)
                return (IPropertyValue)docuTerm;
            else if (docuTerm is IInstanceMember)
                // Methode in einer Instanz einkapseln
                return i(TTD.MetaData.Result.UID, (IInstanceMember)docuTerm);
            else if (docuTerm is IReturn ret)
                return i(TTD.MetaData.Result.UID, m(TT.Runtime.CalledUpFunction.UID, ret));
            else
                return i(TTD.MetaData.Result.UID, eFails(TTD.Composer.Errors.CantEncapsulateAsPropertyValue.UID));
        }

        /// <summary>
        /// mko, 1.2.2021
        /// 
        /// mko, 29.6.2021
        /// Fall docuTerm == null hinzugefügt.
        /// </summary>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        public IInstance EncapsulateAsInstance(IDocuEntity docuTerm)
        {
            if (docuTerm == null)
            {
                return Instance.NullValue;
            }
            else if (docuTerm is IInstance inz)
                return inz;
            else if (docuTerm is IInstanceMember imem)
                // Methode in einer Instanz einkapseln
                return i(TTD.MetaData.Result.UID, imem);
            else if (docuTerm is IReturn ret)
                return i(TTD.MetaData.Result.UID, m(TT.Runtime.CalledUpFunction.UID, ret));
            else if (docuTerm is ITxt t)
            {
                return i(TTD.MetaData.Result.UID, p(TTD.MetaData.Msg.UID, t));
            }
            else if (docuTerm is IString str)
            {
                return i(TTD.MetaData.Result.UID, p(TTD.MetaData.Msg.UID, txt(str.ValueAsString)));
            }
            else
                return i(TTD.MetaData.Result.UID, eFails(TTD.Composer.Errors.CantEncapsulateAsInstance.UID));
        }

        // ⦾⦾⦾
        // ⦾⦾⦾ Properties ⦾⦾⦾ ⦾⦾⦾ ⦾⦾⦾
        // ⦾⦾⦾

        /// <summary>
        /// mko, 21.12.2018
        /// Erweitert um flexible Parameterliste
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, IPropertyValue Value)
                => new PropertyWithNameAsString(Name, Value);

        public IProperty p(String Name, IPropertyValue Value)
            => new PropertyWithNameAsString(Name?.ValueAsString ?? String.NameIsNull.ValueAsString, Value);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, IPropertyValue Value)
            => new PropertyWithNameAsNID(NID(nid), Value);

        public IProperty p(NID nid, IPropertyValue Value)
            => new PropertyWithNameAsNID(nid, Value);


        /// <summary>
        /// mko, 27.2.2019
        /// Eigenschaft mit bool- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, bool Value)
            => p(Name, CreateBoolEntity(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, bool Value)
            => p(nid, CreateBoolEntity(Value));

        /// <summary>
        /// mko, 10.4.2018
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, string Value)
            => p(Name, txt(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, string Value)
            => p(nid, txt(Value));

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit int- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, int Value)
            => p(Name, CreateIntEntity(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, int Value)
            => p(nid, CreateIntEntity(Value));

        /// <summary>
        /// mko, 22.02.2019
        /// Eigenschaft mit long- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string name, long Value)
            => p(name, CreateLongEntity(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, long Value)
            => p(nid, CreateLongEntity(Value));

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit float- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, double Value)
            => p(Name, CreateDblEntity(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, double Value)
            => p(nid, CreateDblEntity(Value));

        /// <summary>
        /// Eigenschaft erzeugen, deren Name als auch Wert durch sprachneutrale Naming ID's definiert werden (siehe ATMO.DFC.Naming)
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="nidVal"></param>
        /// <returns></returns>
        public IProperty p_NID(long nid, long nidVal)
            => p(nid, new NID(nidVal));

        /// <summary>
        /// Eigenschaft erzeugen, deren Wert durch sprachneutrale Naming ID's definiert werden (siehe ATMO.DFC.Naming)
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="nidVal"></param>
        /// <returns></returns>
        public IProperty p_NID(string Name, long nidVal)
            => p(Name, new NID(nidVal));


        // == pSet ====== ====== ====== 

        /// <summary>
        /// Dokumentiert das Setzen einer Eigenschaft auf einen neuen Wert
        /// 
        /// mko, 25.6.2020
        /// Aktuell wird das Konzept eines Property- Setters nicht weiterverfolgt. Deshalb wird beim Aufruf von
        /// pSet eine gewöhnliche Property erzeugt.
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty pSet(string Name, IPropertyValue Value)
            => p(Name, Value);

        /// <summary>
        /// mko 3.3.2020
        /// 
        /// mko, 25.6.2020
        /// Aktuell wird das Konzept eines Property- Setters nicht weiterverfolgt. Deshalb wird beim Aufruf von
        /// pSet eine gewöhnliche Property erzeugt.
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty pSet(long nid, IPropertyValue Value)
            => p(nid, Value);

        /// <summary>
        /// Erzeugt eine Liste von DocuEntities
        /// </summary>
        /// <param name="listMembers"></param>
        /// <returns></returns>
        public IDTList List(params IListMember[] listMembers)
            => new DTList(listMembers);

    }
}
