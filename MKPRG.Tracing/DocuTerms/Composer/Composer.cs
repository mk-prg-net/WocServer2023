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
    /// </summary>
    public partial class Composer 
        : IComposer
    {
        IFn fn = Fn._;

        IFormater fmt;

        public Composer()
        {
            //var NC = RCV3.NC;
            fmt = RCV3.fmtPN;
        }

        public Composer(IFormater fmt)
        {
            this.fmt = fmt;
        }


        public Composer(IFn fn)
            : this()
        {
            this.fn = fn;
        }

        public Composer(IFn fn, IFormater fmt)
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
        Boolean CreateBoolEntity(bool b)
            => new Boolean(b, fmt);

        /// <summary>
        /// Formatiert einen Integer als DocuEntity
        /// </summary>
        /// <param name="i"></param>
        /// <returns></returns>
        Integer CreateIntEntity(int i)
            => new Integer(i, fmt);

        /// <summary>
        /// Formatiert einen Long als DocuEntity
        /// 
        /// mko, 12.10.2020
        /// Long wird nicht mehr al Text, sondern als DT.Long Objekt genereiert
        /// </summary>
        /// <param name="lng"></param>
        /// <returns></returns>
        Integer CreateLongEntity(long lng)
            //=> txt(lng.ToString() + "L");
            => new Integer(lng, fmt);


        /// <summary>
        /// Formatiert einen Double als DokuEntity
        /// </summary>
        /// <param name="dbl"></param>
        /// <returns></returns>
        Double CreateDblEntity(double dbl)
            => new Double(dbl, fmt);

        /// <summary>
        /// mko
        /// Erzeugt ein Datumswert
        /// 
        /// mko, 15.6.2020
        /// Datumswerte werden jetzt als nummerische Triple (Jahr, Monat, Tag) definiert
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        public IDate date(DateTime dat)
            => new DTDate(
                    fmt,
                    new Integer(dat.Year, fmt),
                    new Integer(dat.Month, fmt),
                    new Integer(dat.Day, fmt));

        public IDate date(int year, int month, int day)
            => new DTDate(
                    fmt,
                    new Integer(year, fmt),
                    new Integer(month, fmt),
                    new Integer(day, fmt));

        /// <summary>
        /// mko, 15.6.2020
        /// Ab heute wird die Zeit nicht mehr als String- Konstante, sondern als Tripel aus drei Integer- Konstanten
        /// gespeichert (Stunde, Minute, Sekunde). Hierdurch sollen auch Auswertungen möglich werden, wie z.B.
        /// alle Meldungen ab 11:00 Uhr.
        /// </summary>
        /// <param name="dat"></param>
        /// <param name="showMilliseconds"></param>
        /// <returns></returns>
        public ITime time(TimeSpan dat, bool showMilliseconds = false)
            => new DTTime(fmt,
                new Integer(dat.Hours, fmt),
                new Integer(dat.Minutes, fmt),
                new Integer(dat.Seconds, fmt), 
                new Integer(showMilliseconds ? dat.Milliseconds : 0, fmt));

        public ITime time(int hour, int minutes, int sec, int milliseconds = 0)
            => new DTTime(fmt,
                new Integer(hour, fmt),
                new Integer(minutes, fmt),
                new Integer(sec, fmt),
                new Integer(milliseconds, fmt));


        //        new String($"{dat.Hours.ToString("D2")}:{dat.Minutes.ToString("D2")}:{dat.Seconds.ToString("D2")}"));
        /// <summary>
        /// mko, 4.12.2020
        /// Fall text == null behandelt
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public ITxt txt(string text)
        {
            if (text != null)
            {
                var str = text.Replace("#", " ").Split(L(' ').ToArray(), StringSplitOptions.RemoveEmptyEntries).Select(r => new String(r)).ToArray();
                return new Txt(fmt, str);
            }else
            {

                return new Txt(fmt, new String());
            }
        }

        public IVer ver(string versionStr)
            => new Ver(fmt, new String(versionStr));

        // == KillIfNot ====== ====== ======

        /// <summary>
        /// Dokuentity wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillIfNot KillIfNot(bool Condition, Func<IListMember> docuEntityFactory)
            => new KillIfNot(Condition, docuEntityFactory);


        /// <summary>
        /// mko, 24.7.2018
        /// DokuEntity wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillIfNot KillIf(bool Condition, Func<IListMember> docuEntityFactory)
            => new KillIfNot(!Condition, docuEntityFactory);


        /// <summary>
        /// mko, 17.6.2020
        /// Event- Parameter wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="createEventParam"></param>
        /// <returns></returns>
        public IKillEventParamIfNot KillIfNot(bool Condition, Func<IEventParameter> createEventParam)
            => new KillEventParamIfNot(Condition, createEventParam);


        /// <summary>
        /// mko, 17.6.2020
        /// Event- Parameter wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillEventParamIfNot KillIf(bool Condition, Func<IEventParameter> createEventParam)
            => new KillEventParamIfNot(!Condition, createEventParam);


        /// <summary>
        /// mko, 18.6.2020
        /// InstanceMember wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="createInstanceMember"></param>
        /// <returns></returns>
        public IKillInstanceMemberIfNot KillIfNot(bool Condition, Func<IInstanceMember> createInstanceMember)
            => new KillInstanceMemberIfNot(Condition, createInstanceMember);

        /// <summary>
        /// mko, 17.6.2020
        /// InstanceMember wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillInstanceMemberIfNot KillIf(bool Condition, Func<IInstanceMember> createInstanceMember)
            => new KillInstanceMemberIfNot(!Condition, createInstanceMember);


        /// <summary>
        /// mko, 18.6.2020
        /// InstanceMember wird nur ausgegeben, wenn die Bedingung erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        public IKillMethodPrarmeterIfNot KillIf(bool Condition, Func<IMethodParameter> createInstanceMember)
            => new KillMethodParameterIfNot(!Condition, createInstanceMember);


        /// <summary>
        /// mko, 18.6.2020
        /// InstanceMember wird nur ausgegeben, wenn die Bedingung nicht erfüllt ist.
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="createInstanceMember"></param>
        /// <returns></returns>
        public IKillMethodPrarmeterIfNot KillIfNot(bool Condition, Func<IMethodParameter> createInstanceMember)
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
        public IReturnValue IfElse(bool Condition, Func<IReturnValue> memberIfTrue, Func<IReturnValue> memberIfFalse)
            => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        public IPropertyValue IfElse(bool Condition, Func<IPropertyValue> memberIfTrue, Func<IPropertyValue> memberIfFalse)
            => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 18.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        public IEventParameter IfElse(bool Condition, Func<IEventParameter> memberIfTrue, Func<IEventParameter> memberIfFalse)
            => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 8.7.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        public IInstanceMember IfElse(bool Condition, Func<IInstanceMember> memberIfTrue, Func<IInstanceMember> memberIfFalse)
            => Condition ? memberIfTrue() : memberIfFalse();

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="eventIfTrue"></param>
        /// <param name="eventIfFalse"></param>
        /// <returns></returns>
        public IEvent IfElse(bool Condition, Func<IEvent> eventIfTrue, Func<IEvent> eventIfFalse)
            => Condition ? eventIfTrue() : eventIfFalse();

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="propIfTrue"></param>
        /// <param name="propIfFalse"></param>
        /// <returns></returns>
        public IProperty IfElse(bool Condition, Func<IProperty> propIfTrue, Func<IProperty> propIfFalse)
            => Condition ? propIfTrue() : propIfFalse();

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="instIfTrue"></param>
        /// <param name="instIfFalse"></param>
        /// <returns></returns>
        public IInstance IfElse(bool Condition, Func<IInstance> instIfTrue, Func<IInstance> instIfFalse)
            => Condition ? instIfTrue() : instIfFalse();

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="methIfTrue"></param>
        /// <param name="methIfFalse"></param>
        /// <returns></returns>
        public IMethod IfElse(bool Condition, Func<IMethod> methIfTrue, Func<IMethod> methIfFalse)
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
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IListToEmbed EmbedMembers(IListMember[] entities)
            => new ListToEmbed(entities);

        // == Events ====== ====== ======

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
            => value != null ? CreateEntity(name, value, (id, p) => new Event(fmt, id, value)) : e(name);

        /// <summary>
        /// mko, 4.12.2020
        /// Fall value == null behandelt
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent e(String name, IEventParameter value)
            =>value != null ? CreateEntity(name, value, (id, p) => new Event(fmt, id, value)) : e(name);

        public IEvent e(string name)
            => new Event(fmt, new String(name));

        public IEvent e(String name)
            => new Event(fmt, name);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="name"></param>
        /// <param name="killIfNot"></param>
        /// <returns></returns>
        public IEvent e(string name, IKillEventParamIfNot killIfNot)
        {
            if (killIfNot.Condition)
            {
                return e(name, killIfNot.EventParameter);
            }
            else
            {
                return e(name);
            }
        }

        /// <summary>
        /// mko, 3.3.2020
        /// Eventname ist einen DokuTermID. Diese kann über Naming- Container in verschiedenen Sprachen präsnetiert werden.
        /// </summary>
        /// <param name="DID">DokuTermID</param>
        /// <returns></returns>
        public IEvent e(long nid)
            => new Event(fmt, new NID(fmt, nid));

        public IEvent e(NID nid)
            => new Event(fmt, nid);

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
            => value != null ? CreateEntity(nid, value, (id, p) => new Event(fmt, id, p)) : e(nid);

        /// <summary>
        /// mko, 4.12.2020
        /// Fall value == null behandelt
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent e(NID nid, IEventParameter value)
            => value != null ? CreateEntity(nid, value, (id, p) => new Event(fmt, id, p)) : e(nid);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="killIfNot"></param>
        /// <returns></returns>
        public IEvent e(long nid, IKillEventParamIfNot killIfNot)
        {
            if (killIfNot.Condition)
            {
                return e(nid, killIfNot.EventParameter);
            }
            else
            {
                return e(nid);
            }
        }

        /// <summary>
        /// mko, 10.4.2018
        /// Creates a event with paramters, encapsulated in a list.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent ePrms(string name, params IEventParameter[] pn)
            => new Event(fmt, new String(name), List(KillIfNotFilter(pn)));


        IEventParameter CreateListWithResultProperty(IPropertyValue pVal)
            => List(p(TTD.MetaData.Result.UID, pVal));

        public IEvent eEnd(IEventParameter value)
            => e(TTD.Event.End.UID, value);

        public IEvent eEnd(IKillEventParamIfNot killIfNot)
            => e(TTD.Event.End.UID, killIfNot);

        public IEvent eEnd()
            => e(TTD.Event.End.UID);

        public IEvent eEnd(string value)
            => eEnd(CreateListWithResultProperty(txt(value)));

        public IEvent eEnd(long nid)
            => eEnd(CreateListWithResultProperty(new NID(fmt, nid)));

        public IEvent eSucceeded(IEventParameter value)
            => e(TTD.Event.Succeeded.UID, value);

        public IEvent eSucceeded(IKillEventParamIfNot killIfNot)
            => e(TTD.Event.Succeeded.UID, killIfNot);

        public IEvent eSucceeded()
            => e(TTD.Event.Succeeded.UID);

        public IEvent eSucceeded(string value)
            => eSucceeded(CreateListWithResultProperty(txt(value)));

        public IEvent eSucceeded(long value)
            => eSucceeded(CreateListWithResultProperty(new NID(fmt, value)));

        /// <summary>
        /// mko, 2.4.2019
        /// eFails mit KillIfNot Filtersemantik
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eFails(IEventParameter value)
            => e(TTD.Event.Fails.UID, value);

        public IEvent eFails(IKillEventParamIfNot killIfNot)
            => e(TTD.Event.Fails.UID, killIfNot);

        public IEvent eFails()
            => e(TTD.Event.Fails.UID);

        public IEvent eFails(string value)
            => eFails(CreateListWithResultProperty(txt(value)));

        public IEvent eFails(long nid)
            => eFails(CreateListWithResultProperty(new NID(fmt, nid)));


        /// <summary>
        /// mko, 18.6.2020
        /// Häufig sind Rückgabewerte von Unterprogrammen zu dokumentieren, die als allgemeine IDocuEntity vorliegen. Diese sind dann in eine 
        /// eFails oder eSucceeded zu kapseln. Ist der IDocuEntity eine Mehtode oder ein Eigenschaftswert, dann müssen diese zunächst in eine
        /// Instanz gekapselt werden, die einen IEventParameter darstellt. Diese Funktion führt dies aus
        /// </summary>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        //public IEventParameter EncapsulateAsEventParameter(IDocuEntity docuTerm)
        //    => IfElse(docuTerm is IEventParameter, 
        //                    () => (IEventParameter) docuTerm,
        //                    () => IfElse(docuTerm is IMethod, 
        //                        // Methode in einer Instanz einkapseln
        //                        () => i(TTD.MetaData.Result.UID, (IInstanceMember) docuTerm),
        //                        () => IfElse(docuTerm is IProperty,
        //                            () => i(TTD.MetaData.Result.UID, (IProperty)docuTerm),
        //                            () => IfElse(docuTerm is IPropertyValue,
        //                                // Eigenschaftswert in einer Eigenschaft innerhalb einer Instanz einkapseln
        //                                () => i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (IPropertyValue) docuTerm)),                                                                                            
        //                                () => IfElse(docuTerm is IReturn,
        //                                    () => i(TTD.MetaData.Result.UID, m(ANC.TechTerms.RunTime.CalledUpFunction.UID, (IReturn)docuTerm)),
        //                                    () => IfElse(docuTerm is ITxt,
        //                                        () => (IEventParameter)i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (ITxt)docuTerm)),
        //                                        () => IfElse(docuTerm is String,
        //                                            () => (IEventParameter)i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (String)docuTerm)),
        //                                            () => IfElse(docuTerm is Integer,
        //                                                () => (IEventParameter)i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (Integer)docuTerm)),
        //                                                () => IfElse(docuTerm is Boolean,
        //                                                    () => (IEventParameter)i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (Boolean)docuTerm)),
        //                                                    () => IfElse(docuTerm is Double,
        //                                                        () => (IEventParameter)i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (Double)docuTerm)),
        //                                                        () => IfElse(docuTerm is IDate,
        //                                                            () => (IEventParameter)i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (IDate)docuTerm)),
        //                                                            () => IfElse(docuTerm is ITime,
        //                                                                () => (IEventParameter)i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (ITime)docuTerm)),
        //                                                                () => i(TTD.MetaData.Result.UID, eFails("There exitst a detaild description of error, but it can't be encapsulated as docuTerm EventParameter"))
        //                                                                ))))))))))));

        public IEventParameter EncapsulateAsEventParameter(IDocuEntity docuTerm)
        {
            if (docuTerm is IEventParameter)
                return (IEventParameter)docuTerm;
            else if(docuTerm is IEvent eventP)
            {
                return i(TTD.MetaData.Result.UID, eventP);
            }
            else if (docuTerm is IMethod)
                // Methode in einer Instanz einkapseln
                return i(TTD.MetaData.Result.UID, (IMethod)docuTerm);
            else if (docuTerm is IProperty)
                return i(TTD.MetaData.Result.UID, (IProperty)docuTerm);
            else if (docuTerm is IPropertyValue)
                // Eigenschaftswert in einer Eigenschaft innerhalb einer Instanz einkapseln
                return i(TTD.MetaData.Result.UID, p(TTD.MetaData.Val.UID, (IPropertyValue)docuTerm));
            else if (docuTerm is IReturn)
                return i(TTD.MetaData.Result.UID, m(TT.Runtime.CalledUpFunction.UID, (IReturn)docuTerm));
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
                return i(TTD.MetaData.Result.UID, eFails("There exitst a detaild description of error, but it can't be encapsulated as docuTerm EventParameter"));
        }                               


        /// <summary>
        /// Transaktion noch nicht fertig gestellt.
        /// </summary>
        /// <returns></returns>
        public IEvent eNotCompleted()
            => e(TTD.Event.NotCompleted.UID);

        public IEvent eNotCompleted(IKillEventParamIfNot killIfNot)
            => e(TTD.Event.NotCompleted.UID, killIfNot);

        public IEvent eNotCompleted(IEventParameter value)
            => e(TTD.Event.NotCompleted.UID, value);

        public IEvent eNotCompleted(string value)
            => eNotCompleted(CreateListWithResultProperty(txt(value)));

        public IEvent eNotCompleted(long nid)
            => eNotCompleted(CreateListWithResultProperty(new NID(fmt, nid)));

        /// <summary>
        /// Informationen zu einer Zustandsänderung
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eInfo(IEventParameter value)
            => e(TTD.Event.Info.UID, value);

        public IEvent eInfo(IKillEventParamIfNot killIfNot)
            => e(TTD.Event.Info.UID, killIfNot);

        public IEvent eInfo()
            => e(TTD.Event.Info.UID);

        public IEvent eInfo(string value)
            => eInfo(CreateListWithResultProperty(txt(value)));

        public IEvent eInfo(long nid)
            => eInfo(CreateListWithResultProperty(new NID(fmt, nid)));

        /// <summary>
        /// Signalisiert den Start einer Transaktion
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eStart(IEventParameter value)
            => e(TTD.Event.Start.UID, value);

        public IEvent eStart(IKillEventParamIfNot killIfNot)
            => e(TTD.Event.Start.UID, killIfNot);

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
            => eStart(CreateListWithResultProperty(new NID(fmt, nid)));

        /// <summary>
        /// Warnt vor kritischem Zustand während einer Transaktion
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public IEvent eWarn(IEventParameter value)
            => e(TTD.Event.Warn.UID, value);

        public IEvent eWarn(IKillEventParamIfNot killIfNot)
            => e(TTD.Event.Warn.UID, killIfNot);

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
            => eWarn(CreateListWithResultProperty(new NID(fmt, nid)));


        /// <summary>
        /// Erzeugt eine benannte Instanz
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IInstance i(string name, params IInstanceMember[] pn)
            => pn.Any()
                ? CreateObjectWithMembers(name, (n, lst) => new Instance(fmt, new String(name), lst), pn)
                : new Instance(fmt, new String(name), new DTList(fmt));

        /// <summary>
        /// mko, 3.3.2020
        /// Erzeugt eine Instanz, wobei der Name eine Naming- Container ID ist. 
        /// Damit kann der Instanzname von Formatern kulturspezifisch ausgegeben werden.
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IInstance i(long nid, params IInstanceMember[] pn)
            => pn.Any()
                ? CreateObjectWithMembers(nid, (id, prm) => new Instance(fmt, id, prm), pn)
                : new Instance(fmt, new NID(fmt, nid), new DTList(fmt));



        /// <summary>
        /// Dokumentiert einen Methodenaufruf.
        /// </summary>
        /// <param name="name"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IMethod m(string name, params IMethodParameter[] pn)
            => pn.Any() 
                ? CreateObjectWithMembers(name, (n, lst) => new Method(fmt, n, lst), pn)
                : new Method(fmt, new String(name));

        /// <summary>
        /// mko, 3.3.2020
        /// Dokumentiert einen Methodenaufruf.
        /// </summary>
        /// <param name="NID">Naming Contaeiner ID</param>
        /// <param name="pn"></param>
        /// <returns></returns>
        public IMethod m(long nid, params IMethodParameter[] pn)
            => pn.Any()
                ? CreateObjectWithMembers(nid, (id, lst) => new Method(fmt, id, lst), pn)
                : new Method(fmt, new NID(fmt, nid));

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
            return new Return(fmt, pn);
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
            =>  pVal is IReturnValue rVal
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
        /// Rückgabewert erzeugen, dessen Wert durch sprachneutrale Naming ID definiert wird (siehe MKPRG.Naming)
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public IReturn ret_NID(long nid)
            => returnResultProperty(new NID(fmt, nid));

        // == Properties ====== ====== ======

        public IPropertyValue EncapsulateAsPropertyValue(IDocuEntity docuTerm)
        {
            if (docuTerm is IPropertyValue)
                return (IPropertyValue)docuTerm;
            else if (docuTerm is IInstanceMember)
                // Methode in einer Instanz einkapseln
                return i(TTD.MetaData.Result.UID, (IInstanceMember)docuTerm);            
            else if (docuTerm is IReturn)
                return i(TTD.MetaData.Result.UID, m(TT.Runtime.CalledUpFunction.UID, (IReturn)docuTerm));
            else
                return i(TTD.MetaData.Result.UID, eFails("There exitst a detaild description of error, but it can't be encapsulated as docuTerm PropertyValue"));
        }

        /// <summary>
        /// mko, 1.2.2021
        /// </summary>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        public IInstance EncapsulateAsInstance(IDocuEntity docuTerm)
        {
            if (docuTerm is IInstance inz)
                return inz;
            else if (docuTerm is IInstanceMember imem)
                // Methode in einer Instanz einkapseln
                return i(TTD.MetaData.Result.UID, imem);
            else if (docuTerm is IReturn ret)
                return i(TTD.MetaData.Result.UID, m(TT.Runtime.CalledUpFunction.UID, ret));
            else
                return i(TTD.MetaData.Result.UID, eFails("There exitst a detaild description of error, but it can't be encapsulated as docuTerm PropertyValue"));
        }


        //public IPropertyValue EncapsulateAsPropertyValue(IDocuEntity docuTerm)
        //    => IfElse(docuTerm is IPropertyValue,
        //                    () => (IPropertyValue)docuTerm,
        //                    () => IfElse(docuTerm is IInstanceMember,
        //                            // Methode in einer Instanz einkapseln
        //                            () => i(TTD.MetaData.Result.UID, (IInstanceMember)docuTerm),
        //                            () => IfElse(docuTerm is IReturn,
        //                                            () => (IPropertyValue)i(TTD.MetaData.Result.UID, m(DFC.Naming.TechTerms.RunTime.CalledUpFunction.UID, (IReturn)docuTerm)),
        //                                            () => i(TTD.MetaData.Result.UID, eFails("There exitst a detaild description of error, but it can't be encapsulated as docuTerm EventParameter")))));

        /// <summary>
        /// mko, 21.12.2018
        /// Erweitert um flexible Parameterliste
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, IPropertyValue Value)
                => new Property(fmt, new String(Name), Value);

        public IProperty p(String Name, IPropertyValue Value)
            => new Property(fmt, Name, Value);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, IPropertyValue Value)
            => new Property(fmt, new NID(fmt, nid), Value);

        public IProperty p(NID nid, IPropertyValue Value)
            => new Property(fmt, nid, Value);


        /// <summary>
        /// mko, 27.2.2019
        /// Eigenschaft mit bool- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, bool Value)
            => new Property(fmt, new String(Name), CreateBoolEntity(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, bool Value)
            => new Property(fmt, new NID(fmt, nid), CreateBoolEntity(Value));

        /// <summary>
        /// mko, 10.4.2018
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, string Value)
            => new Property(fmt, new String(Name), txt(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, string Value)
            => new Property(fmt, new NID(fmt, nid), txt(Value));

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit int- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, int Value)
            => new Property(fmt, new String(Name), CreateIntEntity(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, int Value)
            => new Property(fmt, new NID(fmt, nid), CreateIntEntity(Value));

        /// <summary>
        /// mko, 22.02.2019
        /// Eigenschaft mit long- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string name, long Value)
            => new Property(fmt, new String(name), CreateLongEntity(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, long Value)
            => new Property(fmt, new NID(fmt, nid), CreateLongEntity(Value));

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit float- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(string Name, double Value)
            => new Property(fmt, new String(Name), CreateDblEntity(Value));

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        public IProperty p(long nid, double Value)
            => new Property(fmt, new NID(fmt, nid), CreateDblEntity(Value));

        /// <summary>
        /// Eigenschaft erzeugen, deren Name als auch Wert durch sprachneutrale Naming ID's definiert werden (siehe MKPRG.Naming)
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="nidVal"></param>
        /// <returns></returns>
        public IProperty p_NID(long nid, long nidVal)
            => new Property(fmt, new NID(fmt, nid), new NID(fmt, nidVal));

        /// <summary>
        /// Eigenschaft erzeugen, deren Wert durch sprachneutrale Naming ID's definiert werden (siehe MKPRG.Naming)
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="nidVal"></param>
        /// <returns></returns>
        public IProperty p_NID(string Name, long nidVal)
            => new Property(fmt, new String(Name), new NID(fmt, nidVal));


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
            => new Property(fmt, new String(Name), Value);

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
            => new Property(fmt, new DocuTerms.NID(fmt, nid), Value);

        /// <summary>
        /// Erzeugt eine Liste von DocuTerms
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        public IDTList List(params IListMember[] entities)
            => new DTList(fmt, CreateListElements(entities));

        /// <summary>
        /// Erzeugt eine Liste von DocuTerms
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        //public IDocuEntity List(IEnumerable<IDocuEntity> entities) => new DocuEntity(fmt, DocuEntityTypes.List, CreateListElements(entities));


        //---------------------------------------------------------------------------------------------------
        // private members for implementation

        /// <summary>
        /// Realisiert Kill- Kommandos
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        public IDocuEntity ExecuteKillCommand(IDocuEntity details)
        {

            IDocuEntity ret = null;

            if (details != null)
            {
                if (details.EntityType == DocuEntityTypes.KillIfNot
                    && ((IKillEventParamIfNot)details).Condition)
                {
                    // nicht killen
                    ret = ((IKillEventParamIfNot)details).DocuEntity;
                }
                else if (details.EntityType == DocuEntityTypes.KillIfNot
                  && !((IKillEventParamIfNot)details).Condition)
                {
                    // killen
                    ret = null;
                }
                else
                {
                    // kein KillIfNot- Kommando
                    ret = details;
                }
            }

            return ret;
        }

        /// <summary>
        /// mko, 8.5.2018
        /// Kill all parameters where conditions is not met
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private static IListMember[] KillIfNotFilter(IListMember[] pn)
        {
            // Alle herausfiltern, bei denen die kein kill durchgeführt werden soll, bzw. die kein KillIfNot sind
            var prms = pn.Where(r => (r.EntityType == DocuEntityTypes.KillIfNot && ((IKillIfNot)r).Condition) || r.EntityType != DocuEntityTypes.KillIfNot);
            return prms.Select(r => r.EntityType == DocuEntityTypes.KillIfNot ? ((IKillIfNot)r).DocuEntity : r).ToArray();
        }


        ///// <summary>
        ///// mko, 8.5.2018
        ///// Kill all parameters where conditions is not met
        ///// </summary>
        ///// <param name="pn"></param>
        ///// <returns></returns>
        //private static IDocuEntity[] KillIfNotFilter(IEnumerable<IDocuEntity> pn)
        //{
        //    var prms = pn.Where(r => (r.EntityType == DocuEntityTypes.KillIfNot && ((IKillEventParamIfNot)r).Condition) || r.EntityType != DocuEntityTypes.KillIfNot);
        //    return prms.Select(r => r.EntityType == DocuEntityTypes.KillIfNot ? ((IKillEventParamIfNot)r).DocuEntity : r).ToArray();
        //}

        /// <summary>
        /// mko, 1.3.2019
        /// Embeds sub- lists in current pn docu entity list
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private static IListMember[] ResolveListsToEmbed(IEnumerable<IListMember> pn)            
        {
            var lst = new List<IListMember>();

            // mko, 27.2.2019
            // Auflösen aller Einbettungen
            foreach (var dt in pn)
            {
                if (dt is ListToEmbed)
                {
                    var lEmbed = (ListToEmbed)dt;
                    if (lEmbed.Childs != null)
                        lst.AddRange(lEmbed.ToEmbed);
                }
                else
                {
                    lst.Add(dt);
                }
            }

            return lst.ToArray();
        }


        private IDocuEntity CreateEntity(DocuEntityTypes docEType, string name)
        {
            return new DocuEntity(fmt, docEType, new String(name));
        }

        /// <summary>
        /// mko, 9.6.2020
        /// </summary>
        /// <param name="docEType"></param>
        /// <param name="nid"></param>
        /// <returns></returns>
        //private IDocuEntity CreateEntity(DocuEntityTypes docEType, long nid)
        //{
        //    return new DocuEntity(fmt, docEType, new NID(fmt, nid));
        //}


        /// <summary>
        /// mko, 2.4.2019
        /// Erzeugt ein DocuEntity und berücksichtigt dabei, dass das Argument ein KillIfNot sein kann.
        /// </summary>
        /// <param name="docEType"></param>
        /// <param name="Name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        //private IDocuEntity CreateEntity(string Name, IDocuEntity value)
        //{
        //    IDocuEntity entity = null;

        //    if (value is KillIfNot kill)
        //    {
        //        if (!kill.Condition)
        //        {
        //            // Muss Wert killen !
        //            entity = new DocuEntity(fmt, docEType, new String(Name));
        //        }
        //        else
        //        {
        //            // Wert bleibt erhalten
        //            entity = new DocuEntity(fmt, docEType, new String(Name), kill.DocuEntity);
        //        }
        //    }
        //    else
        //    {
        //        entity = new DocuEntity(fmt, docEType, new String(Name), value);
        //    }

        //    return entity;
        //}

        /// <summary>
        /// mko, 2.4.2019
        /// Erzeugt ein DocuEntity und berücksichtigt dabei, dass das Argument ein KillIfNot sein kann.
        /// 
        /// mko, 16.6.2020
        /// Erzeugen streng typisierter DocuEntites
        /// </summary>
        /// <param name="docEType"></param>
        /// <param name="Name"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private IRet CreateEntity<IParam, IRet>(string name, IParam value, Func<String, IParam, IRet> factory)
            where IParam : class, IDocuEntity
            where IRet : class, IDocuEntity
        {
            IRet entity = null;

            if (value is IKillEventParamIfNot kill)
            {
                if (!kill.Condition)
                {
                    // Muss killen !
                    entity = factory(new String(name), null);
                }
                else
                {
                    entity = factory(new String(name), (IParam)kill.DocuEntity);
                }
            }
            else
            {

                entity = factory(new String(name), value);
            }

            return entity;
        }

        private IRet CreateEntity<IParam, IRet>(String name, IParam value, Func<String, IParam, IRet> factory)
            where IParam : class, IDocuEntity
            where IRet : class, IDocuEntity
        {
            IRet entity = null;

            if (value is IKillEventParamIfNot kill)
            {
                if (!kill.Condition)
                {
                    // Muss killen !
                    entity = factory(name, null);
                }
                else
                {
                    entity = factory(name, (IParam)kill.DocuEntity);
                }
            }
            else
            {

                entity = factory(name, value);
            }

            return entity;
        }



        /// <summary>
        /// mko, 3.3.2020
        /// 
        /// mko, 16.6.2020
        /// Erzeugen streng typisierter DocuEntites
        /// </summary>
        /// <param name="docEType"></param>
        /// <param name="NID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        private IRet CreateEntity<IParam, IRet>(long nid, IParam value, Func<NID, IParam, IRet> factory)
            where IParam : class, IDocuEntity
            where IRet : class, IDocuEntity
        {
            IRet entity = null;

            if (value is IKillEventParamIfNot kill)
            {
                if (!kill.Condition)
                {
                    // Muss killen !
                    entity = factory(new NID(fmt, nid), null);
                }
                else
                {
                    entity = factory(new NID(fmt, nid), (IParam)kill.DocuEntity);
                }
            }
            else
            {

                entity = factory(new NID(fmt, nid), value);
            }

            return entity;
        }

        /// <summary>
        /// mko, 24.6.2020
        /// </summary>
        /// <typeparam name="IParam"></typeparam>
        /// <typeparam name="IRet"></typeparam>
        /// <param name="nid"></param>
        /// <param name="value"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        private IRet CreateEntity<IParam, IRet>(NID nid, IParam value, Func<NID, IParam, IRet> factory)
            where IParam : class, IDocuEntity
            where IRet : class, IDocuEntity
        {
            IRet entity = null;

            if (value is IKillEventParamIfNot kill)
            {
                if (!kill.Condition)
                {
                    // Muss killen !
                    entity = factory(nid, null);
                }
                else
                {
                    entity = factory(nid, (IParam)kill.DocuEntity);
                }
            }
            else
            {

                entity = factory(nid, value);
            }

            return entity;
        }



        /// <summary>
        /// mko, 21.12.2018
        /// Instanzen und Methoden haben stets in Listen verpackte Member
        /// </summary>
        /// <param name="type"></param>
        /// <param name="name"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        //private IDocuEntity CreateObjectWithMembers(DocuEntityTypes type, string name, params IListMember[] _pn)
        //{
        //    IDocuEntity res;

        //    // Member müssen stets in einer Liste verpackt werden
        //    // Achtung: beim erstellen der Liste werden Bedingt aufzunehmende Elemente und Einbettungslisten evaluiert. Die Liste 
        //    // kann dadurch schrumpfen oder wachsen.
        //    var memberList = List(_pn);

        //    // mko 21.12.2018
        //    // Parameter werden ab jetzt stets in eine Parameterliste eingekapselt
        //    //if (pn.Length > 1 || MandantoryWrapValuesInList)
        //    if (memberList.Childs.Any())
        //    {
        //        // Instanz mit Membern erzeugen
        //        res = new DocuEntity(fmt, type, new String(name), memberList);
        //    }
        //    else
        //    {
        //        // Instanz ohne Member erzeugen (leere Instanz)
        //        res = new DocuEntity(fmt, type, new String(name));
        //    }

        //    return res;
        //}

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="type"></param>
        /// <param name="NID"></param>
        /// <param name="_pn"></param>
        /// <returns></returns>
        //private IDocuEntity CreateObjectWithMembers(DocuEntityTypes type, long nid, params IListMember[] _pn)
        //{
        //    IDocuEntity res;

        //    // Member müssen stets in einer Liste verpackt werden
        //    // Achtung: beim erstellen der Liste werden bedingt aufzunehmende Elemente und Einbettungslisten evaluiert. Die Liste 
        //    // kann dadurch schrumpfen oder wachsen.
        //    var memberList = List(_pn);

        //    // mko 21.12.2018
        //    // Parameter werden ab jetzt stets in eine Parameterliste eingekapselt
        //    //if (pn.Length > 1 || MandantoryWrapValuesInList)
        //    if (memberList.Childs.Any())
        //    {
        //        // Instanz mit Membern erzeugen
        //        res = new DocuEntity(fmt, type, new NID(fmt, nid), memberList);
        //    }
        //    else
        //    {
        //        // Instanz ohne Member erzeugen (leere Instanz)
        //        res = new DocuEntity(fmt, type, new NID(fmt, nid));
        //    }

        //    return res;
        //}


        private IRet CreateObjectWithMembers<IRet, IParam>(long nid, Func<NID, IDTList, IRet> factory, params IParam[] _pn)
            where IRet : class, IDocuEntity
            where IParam : class, IListMember
        {
            IRet res;

            // Member müssen stets in einer Liste verpackt werden
            // Achtung: beim erstellen der Liste werden bedingt aufzunehmende Elemente und Einbettungslisten evaluiert. Die Liste 
            // kann dadurch schrumpfen oder wachsen.
            var memberList = List(_pn);

            // mko 21.12.2018
            // Parameter werden ab jetzt stets in eine Parameterliste eingekapselt
            //if (pn.Length > 1 || MandantoryWrapValuesInList)
            if (memberList.Childs.Any())
            {
                // Instanz mit Membern erzeugen
                res = factory(new NID(fmt, nid), memberList);
            }
            else
            {
                // Instanz ohne Member erzeugen (leere Instanz)
                res = factory(new NID(fmt, nid), new DTList(fmt));
            }

            return res;
        }

        private IRet CreateObjectWithMembers<IRet, IParam>(string name, Func<String, IDTList, IRet> factory, params IParam[] _pn)
            where IRet : class, IDocuEntity
            where IParam : class, IListMember
        {
            IRet res;

            // Member müssen stets in einer Liste verpackt werden
            // Achtung: beim erstellen der Liste werden bedingt aufzunehmende Elemente und Einbettungslisten evaluiert. Die Liste 
            // kann dadurch schrumpfen oder wachsen.
            var memberList = List(_pn);

            // mko 21.12.2018
            // Parameter werden ab jetzt stets in eine Parameterliste eingekapselt
            //if (pn.Length > 1 || MandantoryWrapValuesInList)
            if (memberList.Childs.Any())
            {
                // Instanz mit Membern erzeugen
                res = factory(new String(name), memberList);
            }
            else
            {
                // Instanz ohne Member erzeugen (leere Instanz)
                res = factory(new String(name), new DTList(fmt));
            }

            return res;
        }

        /// <summary>
        /// mko, 28.2.2019
        /// Creates list elements
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        private IListMember[] CreateListElements(IListMember[] _pn)
        {
            IListMember[] res;

            var pn = KillIfNotFilter(_pn);

            // mko 21.12.2018
            // Parameter werden ab jetzt stets in eine Parameterliste eingekapselt
            //if (pn.Length > 1 || MandantoryWrapValuesInList)
            if (pn.Any())
            {
                res = ResolveListsToEmbed(pn);
            }
            else
            {
                res = new IListMember[] { };
            }

            return res;
        }

        //private IRet[] CreateListElements<IParams, IRet>(IParams[] _pn)
        //    where IParams : class, IListMember
        //    where IRet : class, IListMember
        //{
        //    IRet[] res;

        //    var pn = KillIfNotFilter(_pn);

        //    // mko 21.12.2018
        //    // Parameter werden ab jetzt stets in eine Parameterliste eingekapselt
        //    //if (pn.Length > 1 || MandantoryWrapValuesInList)
        //    if (pn.Any())
        //    {
        //        res = ResolveListsToEmbed(pn);
        //    }
        //    else
        //    {
        //        res = new IRet[] { };
        //    }

        //    return res;
        //}

        /// <summary>
        /// mko, 2.7.2019
        /// Erstellt tiefe Kopie des Dokuentities
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public IDocuEntity CreateCopyOfEntity(IDocuEntity entity)
        {
            if (entity is String str)
            {
                return new String(str.Value);
            }
            else
            {
                return new DocuEntity(fmt, entity.EntityType, entity.Childs.Select(r => CreateCopyOfEntity(r)).ToArray());
            }
        }



        /// <summary>
        /// mko, 15.6.2020
        /// Steht für einen beliebigen Wert einer Eigenschaft im Kontext des IsSubtree- Vergleiches
        /// </summary>
        /// <returns></returns>
        public IWildCard _()
            => new WildCard(fmt);

        /// <summary>
        /// mko, 17.6.2020
        /// Steht für einen beliebigen Wert einer Eigenschaft im Kontext des IsSubtree- Vergleiches.
        /// Es gilt die Einschränkung, das der Werd irgenwo den angegebenen SubTree enthält.
        /// </summary>
        /// <param name="subTree"></param>
        /// <returns></returns>
        public IWildCard _(IDocuEntity subTree)
            => new WildCard(fmt, subTree);

        public Boolean boolean(bool b)
            => new Boolean(b, fmt);

        public Integer integer(long i)
            => new Integer(i, fmt);

        public Double dbl(double d)
            => new Double(d, fmt);

        public String str(string s)
            => new String(s);

        /// <summary>
        /// 24.6.2020
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        public NID NID(long nid)
            => new NID(fmt, nid);

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
    }
}
