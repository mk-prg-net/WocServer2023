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

                return new  Txt();
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

        /// <summary>
        /// Erzeugt eine Liste von DocuEntities
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        //public IDocuEntity List(IEnumerable<IDocuEntity> entities) => new DocuEntity(fmt, DocuEntityTypes.List, CreateListElements(entities));


        // 🛠🛠🛠
        // 🛠🛠🛠 private members for implementation 🛠🛠🛠 🛠🛠🛠 🛠🛠🛠
        // 🛠🛠🛠

        /// <summary>
        /// Realisiert Kill- Kommandos
        /// 
        /// mko, 9.8.2021
        /// Rausgeschmissen aus Implementierung und Schnittstelle
        /// </summary>
        /// <param name="details"></param>
        /// <returns></returns>
        //public IDocuEntity ExecuteKillCommand(IDocuEntity details)
        //{

        //    IDocuEntity ret = null;

        //    if (details != null)
        //    {
        //        if (details.EntityType == DocuEntityTypes.KillIfNot
        //            && ((IKillEventParamIfNot)details).Condition)
        //        {
        //            // nicht killen
        //            ret = ((IKillEventParamIfNot)details).DocuEntity;
        //        }
        //        else if (details.EntityType == DocuEntityTypes.KillIfNot
        //          && !((IKillEventParamIfNot)details).Condition)
        //        {
        //            // killen
        //            ret = null;
        //        }
        //        else
        //        {
        //            // kein KillIfNot- Kommando
        //            ret = details;
        //        }
        //    }

        //    return ret;
        //}

        /// <summary>
        /// mko, 8.5.2018
        /// Kill all parameters where conditions is not met
        /// 
        /// mko, 5.7.2021
        /// Von static auf nicht static geändert.
        /// Fall pn == null jetzt berücksichtigt- es wird jetzt eine leere Liste zurückgegeben
        /// 
        /// mko, 9.8.2021
        /// Rausgeschmissen, da Implementierungsdetail, das jetzt in den Konstruktoren von IInstance... implementiert wird.
        /// 
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        //internal IListMember[] KillIfNotFilter(IListMember[] pn)
        //{
        //    var ret = new IListMember[] { };

        //    // mko, 5.7.2021
        //    // Behandlung von Null- Werten eingeführt
        //    if (pn != null)
        //    {
        //        // Alle herausfiltern, bei denen die kein kill durchgeführt werden soll, bzw. die kein KillIfNot sind
        //        var prms = pn.Where(r => (r.EntityType == DocuEntityTypes.KillIfNot && ((IKillIfNot)r).Condition) || r.EntityType != DocuEntityTypes.KillIfNot);
        //        ret = prms.Select(r => r.EntityType == DocuEntityTypes.KillIfNot
        //                                                // mko, 5.7.2021
        //                                                // Fall Member Docuentity == null berücksichtigt.
        //                                                ? (((IKillIfNot)r).DocuEntity != null ? ((IKillIfNot)r).DocuEntity : i(TT.Sets.NullValue.UID))
        //                                                : r
        //                         ).ToArray();
        //    }

        //    return ret;
        //}

        /// <summary>
        /// mko, 12.7.2021
        /// Streng typisierte Filterfunktion für bedingte Methodenparameter
        /// </summary>
        /// <typeparam name="IParam"></typeparam>
        /// <param name="pn"></param>
        /// <returns></returns>
        //internal IMethodParameter[] KillMethodParamIfCondNotMetFilter(IMethodParameter[] pn)
        //{
        //    var ret = new IMethodParameter[] { };

        //    // mko, 5.7.2021
        //    // Behandlung von Null- Werten eingeführt
        //    if (pn != null)
        //    {
        //        // Alle herausfiltern, bei denen die kein kill durchgeführt werden soll, bzw. die kein KillIfNot sind
        //        var prms = pn.Where(r => r is IKillMethodPrarmeterIfNot mp && mp.Condition || !(r is IKillMethodPrarmeterIfNot));
        //        ret = prms.Select(r => r is IKillMethodPrarmeterIfNot mp
        //                                                // mko, 5.7.2021
        //                                                // Fall Member Docuentity == null berücksichtigt.
        //                                                ? mp.MethodParameter != null ? mp.MethodParameter : p_NID(TTD.Composer.Errors.ComposerError.UID, TTD.Composer.Errors.KillIfNotParamIsNull.UID)
        //                                                : r
        //                         ).ToArray();
        //    }

        //    return ret;
        //}

        /// <summary>
        /// mko, 12.7.2021
        /// Streng typisierte Filterfunktion für bedingte Instanzmember
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        //internal IInstanceMember[] KillInstanceMemberIfContNotMetFilter(IInstanceMember[] pn)
        //{
        //    var ret = new IInstanceMember[] { };

        //    // mko, 5.7.2021
        //    // Behandlung von Null- Werten eingeführt
        //    if (pn != null)
        //    {
        //        // Alle herausfiltern, bei denen die kein kill durchgeführt werden soll, bzw. die kein KillIfNot sind
        //        var prms = pn.Where(r => r is IKillInstanceMemberIfNot mp && mp.Condition || !(r is IKillInstanceMemberIfNot));
        //        ret = prms.Select(r => r is IKillInstanceMemberIfNot mp
        //                                                // mko, 5.7.2021
        //                                                // Fall Member Docuentity == null berücksichtigt.
        //                                                ? mp.InstanceMember != null ? mp.InstanceMember : p_NID(TTD.Composer.Errors.ComposerError.UID, TTD.Composer.Errors.KillIfNotParamIsNull.UID)
        //                                                : r
        //                         ).ToArray();
        //    }

        //    return ret;
        //}

        /// <summary>
        /// mko, 12.7.2021
        /// Streng typisierte Filterfunktion für bedingte Instanzmember
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        //internal IEventParameter[] KillEventMemberIfCondNotMetFilter(IEventParameter[] pn)
        //{
        //    var ret = new IEventParameter[] { };

        //    // mko, 5.7.2021
        //    // Behandlung von Null- Werten eingeführt
        //    if (pn != null)
        //    {
        //        // Alle herausfiltern, bei denen die kein kill durchgeführt werden soll, bzw. die kein KillIfNot sind
        //        var prms = pn.Where(r => r is IKillEventParamIfNot mp && mp.Condition || !(r is IKillEventParamIfNot));
        //        ret = prms.Select(r => r is IKillEventParamIfNot mp
        //                                                // mko, 5.7.2021
        //                                                // Fall Member Docuentity == null berücksichtigt.
        //                                                ? mp.EventParameter != null ? mp.EventParameter : i(TTD.Composer.Errors.ComposerError.UID, p_NID(TTD.StateDescription.WhatsUp.UID, TTD.Composer.Errors.KillIfNotParamIsNull.UID))
        //                                                : r
        //                         ).ToArray();
        //    }

        //    return ret;
        //}

        //internal IListMember[] KillListMemberIfCondNotMetFilter(IListMember[] pn)
        //{
        //    var ret = new IListMember[] { };

        //    // mko, 5.7.2021
        //    // Behandlung von Null- Werten eingeführt
        //    if (pn != null)
        //    {
        //        // Alle herausfiltern, bei denen die kein kill durchgeführt werden soll, bzw. die kein KillIfNot sind
        //        var prms = pn.Where(r => r is IKillListElementIfNot mp && mp.Condition || !(r is IKillListElementIfNot));
        //        ret = prms.Select(r => r is IKillListElementIfNot mp
        //                                                // mko, 5.7.2021
        //                                                // Fall Member Docuentity == null berücksichtigt.
        //                                                ? mp.ListMember != null ? mp.ListMember : i(TTD.Composer.Errors.ComposerError.UID, p_NID(TTD.StateDescription.WhatsUp.UID, TTD.Composer.Errors.KillIfNotParamIsNull.UID))
        //                                                : r
        //                         ).ToArray();
        //    }

        //    return ret;
        //}







        /// <summary>
        /// mko, 1.3.2019
        /// Embeds sub- lists in current pn docu entity list
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        //private static IListMember[] ResolveListsToEmbed(IEnumerable<IListMember> pn)
        //{
        //    var lst = new List<IListMember>();

        //    // mko, 27.2.2019
        //    // Auflösen aller Einbettungen
        //    foreach (var dt in pn)
        //    {
        //        if (dt is ListMembersToEmbedClass)
        //        {
        //            var lEmbed = (ListMembersToEmbedClass)dt;
        //            if (lEmbed.Childs != null)
        //                lst.AddRange(lEmbed.ToEmbed);
        //        }
        //        else
        //        {
        //            lst.Add(dt);
        //        }
        //    }

        //    return lst.ToArray();
        //}


        /// <summary>
        /// mko, 8.7.21
        /// Gegen Nullwerte gehärtet
        /// </summary>
        /// <param name="docEType"></param>
        /// <param name="name"></param>
        /// <returns></returns>
        //private IDocuEntity CreateEntity(DocuEntityTypes docEType, string name)
        //{
        //    IDocuEntity ret = null;

        //    if (string.IsNullOrWhiteSpace(name))
        //    {
        //        ret = new DocuEntity(fmt, docEType, NID(TTD.Composer.Errors.NameIsNull.UID));
        //    }
        //    else
        //    {
        //        ret = new DocuEntity(fmt, docEType, new String(name));
        //    }

        //    return ret;
        //}

        /// <summary>
        /// mko, 2.4.2019
        /// Erzeugt ein DocuEntity und berücksichtigt dabei, dass das Argument ein KillIfNot sein kann.
        /// Methode ist 
        /// 
        /// mko, 16.6.2020
        /// Erzeugen streng typisierter DocuEntites
        /// 
        /// Funktionsweise der Komposition von KillIfNot- Termen und ihrer Evaluierung
        /// 1) Die Terme werden 
        /// 
        /// 
        /// mko, 5.7.2021
        /// Die Fälle, das der Wert (value) ein KillIfNot- Befehl ist, oder das der Wert Null ist, jetzt eindeutiger behandelt.
        /// 
        /// mko, 9.7.2021
        /// Zugriff auf den streng typisiertern Parameter eines KillIfNot- Objektes verbessert.
        /// 
        /// </summary>        
        /// <param name="name"></param>
        /// <param name="value"></param>
        /// <param name="factoryWithParam">Klassenfabrik wird eingesetzt, wenn Parameterliste erzeugt werden muss</param>
        /// <param name="factoryIfParamKilled">Klassenfabrik wird eingesetzt, wenn Parameterliste nicht definiert wurde (null), oder durch ein Kill- Kommando gelöscht wurde</param>
        /// <returns></returns>
        //private IRet EvaluateKillIfNot<TParam, TKillIfNotParam, IRet>(
        //    long nid,
        //    TKillIfNotParam killIfNot,
        //    Func<TKillIfNotParam, TParam> GetParam,
        //    Func<long, TParam, IRet> factoryWithParam,
        //    Func<long, IRet> factoryIfParamKilled)
        //    where TParam : IDocuEntity
        //    where TKillIfNotParam : class, IKillIfNot
        //    where IRet : class, IDocuEntity
        //{
        //    IRet entity = null;

        //    var nidIsDefined = NC.ContainsKey(nid);

        //    if (nidIsDefined)
        //    {
        //        if (killIfNot == null)
        //        {
        //            entity = factoryIfParamKilled(TTD.Composer.Errors.KillIfNotParamIsNull.UID);
        //        }
        //        else if (killIfNot.Condition)
        //        {
        //            entity = factoryWithParam(nid, GetParam(killIfNot));
        //        }
        //        else
        //        {
        //            entity = factoryIfParamKilled(nid);
        //        }
        //    }
        //    else
        //    {
        //        if (killIfNot == null)
        //        {
        //            entity = factoryIfParamKilled(TTD.Composer.Errors.NidIsUnknownAndKillIfNotIsNull.UID);
        //        }
        //        else if (killIfNot.Condition)
        //        {
        //            entity = factoryWithParam(TTD.Composer.Errors.NidIsUnknown.UID, GetParam(killIfNot));
        //        }
        //        else
        //        {
        //            entity = factoryIfParamKilled(TTD.Composer.Errors.NidIsUnknown.UID);
        //        }
        //    }

        //    return entity;
        //}


        /// <summary>
        /// mko, 9.8.2021
        /// </summary>
        /// <typeparam name="TParam"></typeparam>
        /// <typeparam name="TKillIfNotParam"></typeparam>
        /// <typeparam name="IRet"></typeparam>
        /// <param name="Name"></param>
        /// <param name="killIfNot"></param>
        /// <param name="GetParam"></param>
        /// <param name="factoryWithNidAndParam"></param>
        /// <param name="factoryWithStringAndParam"></param>
        /// <param name="factoryWithStringNameIfParamKilled"></param>
        /// <param name="factoryWithNidNameIfParamKilled"></param>
        /// <returns></returns>
        //private IRet EvaluateKillIfNot<TParam, TKillIfNotParam, IRet>(
        //    string Name,
        //    TKillIfNotParam killIfNot,
        //    Func<TKillIfNotParam, TParam> GetParam,
        //    Func<long, TParam, IRet> factoryWithNidAndParam,
        //    Func<string, TParam, IRet> factoryWithStringAndParam,
        //    Func<long, IRet> factoryWithNidNameIfParamKilled,
        //    Func<string, IRet> factoryWithStringNameIfParamKilled)            
        //    where TParam : IDocuEntity
        //    where TKillIfNotParam : class, IKillIfNot
        //    where IRet : class, IDocuEntity
        //{
        //    IRet entity = null;

        //    var nameIsDefined = Name != null && Name != NC[TTD.Composer.Errors.NameIsNull.UID].CNT;

        //    if (nameIsDefined)
        //    {
        //        if (killIfNot == null)
        //        {
        //            entity = factoryWithStringAndParam(Name, (TParam)NID(TTD.Composer.Errors.KillIfNotParamIsNull.UID));
        //        }
        //        else if (killIfNot.Condition)
        //        {
        //            entity = factoryWithStringAndParam(Name, GetParam(killIfNot));
        //        }
        //        else
        //        {
        //            entity = factoryWithStringNameIfParamKilled(Name);
        //        }
        //    }
        //    else
        //    {
        //        if (killIfNot == null)
        //        {
        //            entity = factoryWithNidNameIfParamKilled(TTD.Composer.Errors.NidIsUnknownAndKillIfNotIsNull.UID);
        //        }
        //        else if (killIfNot.Condition)
        //        {
        //            entity = factoryWithNidAndParam(TTD.Composer.Errors.NidIsUnknown.UID, GetParam(killIfNot));
        //        }
        //        else
        //        {
        //            entity = factoryWithNidNameIfParamKilled(TTD.Composer.Errors.NidIsUnknown.UID);
        //        }
        //    }

        //    return entity;
        //}





        //private IRet CreateEntity<TName, IParam, IRet>(
        //    Func<TName> NameGenerator,
        //    IParam value,
        //    Func<TName, IParam, IRet> factoryWithParam,
        //    Func<TName, IRet> factoryIfKilledParam)
        //    where IParam : class
        //    where IRet : class, IDocuEntity
        //{
        //    IRet entity = null;

        //    if (value == null)
        //    {
        //        entity = factoryIfKilledParam(NameGenerator());
        //    }
        //    else if (value is IKillIfNot kill)
        //    {
        //        if (!kill.Condition)
        //        {
        //            // Muss killen !
        //            entity = factoryIfKilledParam(NameGenerator());
        //        }
        //        else
        //        {
        //            entity = factoryWithParam(NameGenerator(), (IParam)kill.DocuEntity);
        //        }
        //    }
        //    else
        //    {

        //        entity = factoryWithParam(NameGenerator(), value);
        //    }

        //    return entity;
        //}




        //private IRet CreateEntity<IParam, IRet>(String name, IParam value, Func<String, IParam, IRet> factory)
        //    where IParam : class, IDocuEntity
        //    where IRet : class, IDocuEntity
        //{
        //    IRet entity = null;

        //    if (value is IKillEventParamIfNot kill)
        //    {
        //        if (!kill.Condition)
        //        {
        //            // Muss killen !
        //            entity = factory(name, null);
        //        }
        //        else
        //        {
        //            entity = factory(name, (IParam)kill.DocuEntity);
        //        }
        //    }
        //    else
        //    {

        //        entity = factory(name, value);
        //    }

        //    return entity;
        //}



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
        //private IRet CreateEntity<IParam, IRet>(long nid, IParam value, Func<NID, IParam, IRet> factory)
        //    where IParam : class, IDocuEntity
        //    where IRet : class, IDocuEntity
        //{
        //    IRet entity = null;

        //    if (value is IKillEventParamIfNot kill)
        //    {
        //        if (!kill.Condition)
        //        {
        //            // Muss killen !
        //            entity = factory(new NID(fmt, nid), null);
        //        }
        //        else
        //        {
        //            entity = factory(new NID(fmt, nid), (IParam)kill.DocuEntity);
        //        }
        //    }
        //    else
        //    {

        //        entity = factory(new NID(fmt, nid), value);
        //    }

        //    return entity;
        //}

        /// <summary>
        /// mko, 24.6.2020
        /// </summary>
        /// <typeparam name="IParam"></typeparam>
        /// <typeparam name="IRet"></typeparam>
        /// <param name="nid"></param>
        /// <param name="value"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        //private IRet CreateEntity<IParam, IRet>(NID nid, IParam value, Func<NID, IParam, IRet> factory)
        //    where IParam : class, IDocuEntity
        //    where IRet : class, IDocuEntity
        //{
        //    IRet entity = null;

        //    if (value is IKillEventParamIfNot kill)
        //    {
        //        if (!kill.Condition)
        //        {
        //            // Muss killen !
        //            entity = factory(nid, null);
        //        }
        //        else
        //        {
        //            entity = factory(nid, (IParam)kill.DocuEntity);
        //        }
        //    }
        //    else
        //    {

        //        entity = factory(nid, value);
        //    }

        //    return entity;
        //}



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


        //private IRet CreateObjectWithMembers<IRet, IParam>(long nid, Func<NID, IDTList, IRet> factory, params IParam[] _pn)
        //    where IRet : class, IDocuEntity
        //    where IParam : class, IListMember
        //{
        //    IRet res;

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
        //        res = factory(new NID(fmt, nid), memberList);
        //    }
        //    else
        //    {
        //        // Instanz ohne Member erzeugen (leere Instanz)
        //        res = factory(new NID(fmt, nid), new DTList(fmt));
        //    }

        //    return res;
        //}


        /// <summary>
        /// 
        /// In der Regel existieren vier Generator- Funktionen:
        /// f(n)
        /// f(n, w)
        /// f(i)
        /// f(i, w)
        /// 
        /// Folgende allgemeine Parameterkombinationen sind möglich:
        /// 
        /// Fälle; n==null || w == null || i n.def 
        /// 
        /// f(i, w)                 -: f(i, w)
        /// f(n, w)                 -: f(n, w)
        /// f(i n. def, w)          -: f(i_ndef, w)
        /// f(n == null, w)         -: f(i_nameIsNull, w)
        /// f(i, w == null)         -: f(i)
        /// f(n, w == null)         -: f(n)        
        /// f(i n.def, w == null)   -: Error
        /// f(n == null, w == null) -: Error
        /// 
        /// Zwei verallgemeinerte Composer- Factories, die alle Fälle korrekt behandelt
        /// CF(F/N/     cf1,    //  n => f(n), 
        ///    F/N, W/  cf2,    // (n, w) => f(n, w) 
        ///    F/I/     cf3,    // i => f(i)
        ///    F/I, W/  cf4,    // (i, w) => f(i, w)
        ///    N n, W w)
        /// {
        ///    if n != null && w != null => cf2(n, w)
        ///    if n != null && w == null => cf1(n)
        ///    if n == null && w != null => cf3(i_nameIsNull, w)
        ///    if n == null && w == null => Error
        ///    ...
        /// } 
        /// 
        /// CF(F/I/     cf1,    // i => f(i)
        ///    F/I, W/  cf2,    // f(i, w) => f(i, w)),
        ///    I i, W w)
        /// {
        ///    if i is def && w != null  => cf3(i, w)
        ///    if i is def && w == null  => cf1(i)
        ///    if i is !def && w != null => cf2(i_ndef, w)
        ///    if i is !def && w == null => Error        
        /// } 
        /// 
        /// CF_f(N n, W w) => CF(n => f(n), (n, w) => f(n,w), i => f(i), (i, w) => f(i, w), n, w)
        /// CF_f(I i, W w) => CF(i => f(i), (i, w) => f(i,w), n, w)
        /// 
        /// Durch Parameterbindung spezielle Klassenfabriken ableiten
        /// </summary>
        /// <typeparam name="TName"></typeparam>
        /// <typeparam name="IRet"></typeparam>
        /// <typeparam name="IParam"></typeparam>
        /// <param name="nidOfDocuTerm"></param>
        /// <param name="factory"></param>        
        /// <param name="_pn"></param>
        /// <returns></returns>


        //private IRet CreateDocuTermWithMemberList<IParam, IRet>(
        //    long nidOfDocuTerm,
        //    Func<long, IParam[], IRet> factory,
        //    Func<long, IRet> factoryIfParamIsNull,
        //    Func<IParam[], IParam[]> KillIfNotParamFilter,
        //    IParam[] _pn)
        //    where IRet : class, IDocuEntity
        //    where IParam : class, IListMember
        //{
        //    IRet ret = null;

        //    var nisIsDefined = NC.ContainsKey(nidOfDocuTerm);

        //    // mko, 5.7.2021
        //    // Absichern gegen Null- Werte im Namen
        //    if (nisIsDefined && _pn != null)
        //    {
        //        // Member müssen stets in einer Liste verpackt werden
        //        // Achtung: beim Erstellen der Liste werden bedingt aufzunehmende Elemente und Einbettungslisten evaluiert. Die Liste 
        //        // kann dadurch schrumpfen oder wachsen.

        //        _pn = KillIfNotParamFilter(_pn);
        //        ret = factory(nidOfDocuTerm, _pn);
        //    }
        //    else if (nisIsDefined && _pn == null)
        //    {
        //        ret = factoryIfParamIsNull(nidOfDocuTerm);
        //    }
        //    else if (!nisIsDefined && _pn != null)
        //    {
        //        _pn = KillIfNotParamFilter(_pn);
        //        ret = factory(TTD.Composer.Errors.NidIsUnknown.UID, _pn);
        //    }
        //    else if (!nisIsDefined && _pn == null)
        //    {
        //        ret = factoryIfParamIsNull(TTD.Composer.Errors.NidIsUnknownAndValueIsNull.UID);
        //    }

        //    return ret;
        //}

        //private IRet CreateDocuTermWithMemberList<IRet, IParam>(
        //    string nameOfDocuTerm,
        //    Func<String, IParam[], IRet> factory,
        //    Func<String, IRet> factoryIfParamIsNull,
        //    Func<long, IParam[], IRet> factoryNid,
        //    Func<long, IRet> factoryNidIfParamIsNull,
        //    Func<IParam[], IParam[]> KillIfNotParamFilter,
        //    IParam[] _pn)
        //    where IRet : class, IDocuEntity
        //    where IParam : class, IListMember
        //{
        //    IRet ret = null;

        //    var nameIsDefined = !string.IsNullOrWhiteSpace(nameOfDocuTerm);

        //    // mko, 5.7.2021
        //    // Absichern gegen Null- Werte im Namen
        //    if (nameIsDefined && _pn != null)
        //    {
        //        // Member müssen stets in einer Liste verpackt werden
        //        // Achtung: beim Erstellen der Liste werden bedingt aufzunehmende Elemente und Einbettungslisten evaluiert. Die Liste 
        //        // kann dadurch schrumpfen oder wachsen.
        //        _pn = KillIfNotParamFilter(_pn);
        //        ret = factory(new String(nameOfDocuTerm), _pn);
        //    }
        //    else if (nameIsDefined && _pn == null)
        //    {
        //        ret = factoryIfParamIsNull(new String(nameOfDocuTerm));
        //    }
        //    else if (!nameIsDefined && _pn != null)
        //    {
        //        _pn = KillIfNotParamFilter(_pn);
        //        ret = factoryNid(TTD.Composer.Errors.NameIsNull.UID, _pn);
        //    }
        //    else if (!nameIsDefined && _pn == null)
        //    {
        //        ret = factoryNidIfParamIsNull(TTD.Composer.Errors.NameIsNullAndValueIsNull.UID);
        //    }

        //    return ret;
        //}

        //private IRet CreateDocuTermWithMemberList<IRet, IParam>(
        //    IWildCard wc,
        //    Func<IWildCard, IParam[], IRet> factory,
        //    Func<IWildCard, IRet> factoryIfParamIsNull,
        //    Func<long, IRet> factoryNid,
        //    Func<IParam[], IParam[]> KillIfNotParamFilter,
        //    IParam[] _pn)
        //    where IRet : class, IDocuEntity
        //    where IParam : class, IListMember
        //{
        //    IRet ret = null;

        //    var wcIsDefined = wc != null;

        //    // mko, 5.7.2021
        //    // Absichern gegen Null- Werte im Namen
        //    if (wcIsDefined && _pn != null)
        //    {
        //        // Member müssen stets in einer Liste verpackt werden
        //        // Achtung: beim Erstellen der Liste werden bedingt aufzunehmende Elemente und Einbettungslisten evaluiert. Die Liste 
        //        // kann dadurch schrumpfen oder wachsen.
        //        _pn = KillIfNotParamFilter(_pn);
        //        ret = factory(wc, _pn);
        //    }
        //    else if (wcIsDefined && _pn == null)
        //    {
        //        ret = factoryIfParamIsNull(wc);
        //    }
        //    else if (!wcIsDefined && _pn != null)
        //    {
        //        _pn = KillIfNotParamFilter(_pn);
        //        ret = factoryNid(TTD.Composer.Errors.WildCardInstanceIsNull.UID);
        //    }
        //    else if (!wcIsDefined && _pn == null)
        //    {
        //        ret = factoryNid(TTD.Composer.Errors.WildCardInstanceIsNullAndValueIsNull.UID);
        //    }

        //    return ret;
        //}

        /// <summary>
        /// mko, 28.2.2019
        /// Creates list elements
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        //private IListMember[] CreateListElements(IListMember[] _pn)
        //{
        //    IListMember[] res;


        //    // Hier werden alle Listenelemente entfernt, welche die `IKillIfNot` Schnittstelle implementiern,
        //    // und bei denen die Bedingung nicht erfüllt ist. Im Extremfall kann ein leeres Array
        //    // zurückgegeben werden
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
        //        res = new IListMember[] { };
        //    }

        //    return res;
        //}

        /// <summary>
        /// mko, 2.7.2019
        /// Erstellt tiefe Kopie des Dokuentities
        /// 
        /// mko, 9.8.2021
        /// Entfernt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //public IDocuEntity CreateCopyOfEntity(IDocuEntity entity)
        //{
        //    if (entity is String str)
        //    {
        //        return new String(str.Value);
        //    }
        //    else
        //    {
        //        return new DocuEntity(fmt, entity.EntityType, entity.Childs.Select(r => CreateCopyOfEntity(r)).ToArray());
        //    }
        //}

    }
}
