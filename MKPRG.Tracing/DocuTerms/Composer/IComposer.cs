using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 26.3.2018
    /// Create Document entity trees
    /// 
    /// mko, 22.7.2020
    /// Schnittstelle IXTabGenerator hinzugefügt
    /// 
    /// mko, 15.3.2021
    /// Die Namen von Methoden und Instanzen können jetzt duch einen 
    /// Wildcard definiert werden. Hierdurch wird die Mächtigkeit von 
    /// Musterausdrücken für SubTree- Analysen vergrößert.
    /// </summary>
    public interface IComposer
        : IXTabGenerator
    {


        IDTList List(params IListMember[] entities);


        IBoolean boolean(bool b);

        IInteger integer(long i);

        IDouble dbl(double d);

        IString str(string s);


        IInstance i(string name, params IInstanceMember[] pn);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        IInstance i(long NID, params IInstanceMember[] pn);

        /// <summary>
        /// mko, 15.3.2021
        /// 
        /// Für Mustervergleiche kann jetzt eine Instanz mit beliebigen Namen (Wildcard) erzeugt werden
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        //IInstance i(IWildCard wc, params IInstanceMember[] pn);


        /// <summary>
        /// mko, 15.3.2021
        /// 
        /// Für Mustervergleiche kann jetzt eine Instanz mit beliebigen Namen (Wildcard) erzeugt werden
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        //IInstance i(IWildCard wc, params IInstanceMember[] pn);


        /// <summary>
        /// Defines a vrsion number of an object or method
        /// </summary>
        /// <param name="versionStr"></param>
        /// <returns></returns>
        IVer ver(string versionStr);


        /// <summary>
        /// Decribes a method/action call
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IMethod m(string name, params IMethodParameter[] pn);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        IMethod m(long NID, params IMethodParameter[] pn);

        /// <summary>
        /// mko, 15.3.2021
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        //IMethod m(IWildCard wc, params IMethodParameter[] pn);


        /// <summary>
        /// mko, 10.4.2018
        /// 
        /// Für Mustervergleiche kann jetzt eine Methode mit beliebigen Namen (Wildcard) erzeugt werden
        /// Returnvalue
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        //IReturn ret(params IReturnValue[] pn);
        IReturn ret(IReturnValue pn);

        /// <summary>
        /// mko, 27.02.2019
        /// Rückgabe einer Entscheidung
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        IReturn ret(bool res);

        /// <summary>
        /// mko, 27.2.2019
        /// Rückgabe eines Integers
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        IReturn ret(int res);

        /// <summary>
        /// mko, 27.2.2019
        /// Rückgabe eines Longs
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        IReturn ret(long res);

        /// <summary>
        /// mko, 27.2.2019
        /// Rückgabe einer Textmeldung
        /// </summary>
        /// <param name="res"></param>
        /// <returns></returns>
        IReturn ret(string res);

        /// <summary>
        /// mko, 9.6.2020
        /// Rückgabe einer Meldung, die mehrsprachig abgerufen werden kann.
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        IReturn ret_NID(long nid);

        // == Eigenschaften ====== ====== ======

        /// <summary>
        /// mko, 18.6.2020
        /// Häufig sind Rückgabewerte von Unterprogrammen zu dokumentieren, die als allgemeine IDocuEntity vorliegen. Diese dann z.B. als
        /// Property- Value einer Eigenschaft zuzuweisen.
        /// Ist der IDocuEntity eine Mehtode, Eigenschaft oder Return- Value, dann müssen diese zunächst in eine
        /// Instanz gekapselt werden, die einen IPropertyValue darstellt. Diese Funktion führt dies aus
        /// </summary>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        IPropertyValue EncapsulateAsPropertyValue(IDocuEntity docuTerm);


        /// <summary>
        /// Reports the value of a property 
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IProperty p(string Name, IPropertyValue Value);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(long NID, IPropertyValue Value);

        /// <summary>
        /// Reports the value of a property 
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IProperty p(String Name, IPropertyValue Value);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(NID nid, IPropertyValue Value);

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaften mit Zeichenkettenwert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(string Name, string Value);

        /// <summary>
        /// 3.3.2020
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(long NID, string Value);

        /// <summary>
        /// mko, 9.6.2020
        /// Property, deren Wert durch mehrsprachig abrufbare NID's definiert ist.
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="NID"></param>
        /// <returns></returns>
        IProperty p_NID(string name, long NID_Value);

        /// <summary>
        /// mko, 9.6.2020
        /// Property, deren Namen als auch Wert durch mehrsprachig abrufbare NID's definiert sind
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="NID"></param>
        /// <returns></returns>
        IProperty p_NID(long NID, long NID_Value);


        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit int- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(string Name, int Value);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(long NID, int Value);

        /// <summary>
        /// mko, 22.02.2019
        /// Eigenschaft mit long- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(string Name, long Value);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(long NID, long Value);

        /// <summary>
        /// mko, 27.2.2019
        /// Eigenschaft mit bool- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(string Name, bool Value);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(long NID, bool Value);

        /// <summary>
        /// mko, 22.2.2019
        /// Eigenschaft mit float- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(string Name, double Value);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(long NID, double Value);

        // == Wildcards/Paltzhalter ====== ====== ======

        /// <summary>
        /// mko, 28.7.2021
        /// Neu: Spezieller Wildcard für Namen
        /// </summary>
        long _n { get; }

        /// <summary>
        /// mko, 15.6.2020
        /// DocuTerm, der einen Platzhalter für den Wert eines Eigenschaftsausdruckes darstellt.
        /// Wird beim Pattern- Matching berücksichtigt.
        /// 
        /// Bsp.:
        /// pnL.m("Query", pnL.p("ID", pnL._()))
        /// 
        /// mko, 28.7.2021
        /// Umbenannt von _() in _v()
        /// </summary>
        /// <returns></returns>
        IWildCard _v();

        /// <summary>
        /// mko, 16.6.2020
        /// DocuTerm, der einen Platzhalter für Werte eines Eigenschaftsausdruckes darstellt. Die 
        /// Werte müssen dabei den angegebenen SubTree enthalten.
        /// Entspricht allen Nachfolgern eines Knotens im Baum, die den Subtree als Teilstruktur enhalten
        /// 
        /// Bsp.:
        /// 
        /// 
        /// pnL.m("Query", 
        ///      pnL.p("ID", pnL._()),
        ///      pnL.eFails(pnL._(pnL.eSucceded(_pnL._())))).IsSubTreeOf(X);
        ///      
        /// mko, 28.7.2021
        /// Umbenannt von _(...) in _v(...)
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <returns></returns>
        IWildCard _v(IDocuEntity subTreePattern);


        /// <summary>
        /// Reports the value, property was set
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IProperty pSet(string Name, IPropertyValue Value);

        /// <summary>
        /// mko, 3.3.2020
        /// 
        /// mko, 25.6.2020
        /// Rückgabetyp auf IProperty geändert
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty pSet(long NID, IPropertyValue Value);

        // == Ergeignisse ====== ====== ======

        /// <summary>
        /// mko, 18.6.2020
        /// Häufig sind Rückgabewerte von Unterprogrammen zu dokumentieren, die als allgemeine IDocuEntity vorliegen. Diese sind dann in eine 
        /// eFails oder eSucceeded zu kapseln. Ist der IDocuEntity eine Mehtode oder ein Eigenschaftswert, dann müssen diese zunächst in eine
        /// Instanz gekapselt werden, die einen IEventParameter darstellt. Diese Funktion führt dies aus
        /// </summary>
        /// <param name="docuTerm"></param>
        /// <returns></returns>
        IEventParameter EncapsulateAsEventParameter(IDocuEntity docuTerm);


        /// <summary>
        /// Defines a fired event with parameters
        /// </summary>
        /// <param name="pn"></param>
        /// <returns></returns>
        IEvent e(string name, IEventParameter value);

        IEvent e(string name);

        IEvent e(IString name, IEventParameter value);

        IEvent e(IString name);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent e(long NID, IEventParameter value);

        IEvent e(long NID);

        IEvent e(INID nid, IEventParameter value);

        IEvent e(INID nid);

        /// <summary>
        /// mko, 17.6.2020
        /// Erzeugt einen EventParameter nur dann, wenn eine Bedingung erfüllt ist.
        /// Sonst wird nur ein einfach benanntes Event ohne Parameter erzeugt.
        /// 
        /// mko, 10.8.2021
        /// Wieder entfernt. `IKillEventParam` ist ein `IEventParam` und wird von den        
        /// Funktionen, die diese Parametertypen haben, abgedeckt.
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="killIfNot"></param>
        /// <returns></returns>
        //IEvent e(long nid, IKillEventParamIfNot killIfNot);

        //IEvent e(string name, IKillEventParamIfNot killIfNot);

        // == Meldung des Beginns einer Transkaktion ====== ====== ======

        /// <summary>
        /// Describes an event, that fires, when a process starts
        /// </summary>        
        /// <param name="pn"></param>
        /// <returns></returns>
        IEvent eStart(IEventParameter value);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        // IEvent eStart(IKillEventParamIfNot value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        IEvent eStart(string info);

        /// <summary>
        /// mko, 28.5.2020
        /// Event durch eine mehrsprachig abrufbare NID beschreiben
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        IEvent eStart(long NID);

        IEvent eStart();

        // == Meldung Ende einer Transaktion ====== ====== ======

        /// <summary>
        /// Describes an event, that fires, when a process ends
        /// </summary>
        /// <param name="nameOfProcess"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        IEvent eEnd(IEventParameter value);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //IEvent eEnd(IKillEventParamIfNot value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent eEnd(string value);

        /// <summary>
        /// mko, 28.5.2020
        /// /// Event durch eine mehrsprachig abrufbare NID beschreiben
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        IEvent eEnd(long NID);

        IEvent eEnd();

        // == Meldung unvollständiger Ausführung einer Transaktion ====== ====== ======

        /// <summary>
        /// Signals interrupted or aborted functions etc.
        /// </summary>
        /// <returns></returns>
        IEvent eNotCompleted();

        IEvent eNotCompleted(IEventParameter value);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //IEvent eNotCompleted(IKillEventParamIfNot value);

        /// <summary>
        /// mko, 28.5.2020
        /// Event durch eine mehrsprachig abrufbare NID beschreiben
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        IEvent eNotCompleted(long NID);

        // == Fehlermeldungen ====== ====== ======

        /// <summary>
        /// Describes an event, that fires, when a process fails
        /// </summary>
        /// <param name="nameOfProcess"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        IEvent eFails(IEventParameter value);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //IEvent eFails(IKillEventParamIfNot value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent eFails(string value);

        /// <summary>
        /// mko, 19.9.2018
        /// </summary>
        /// <returns></returns>
        IEvent eFails();

        /// <summary>
        /// mko, 28.5.2020
        /// Event durch eine mehrsprachig abrufbare NID beschreiben
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        IEvent eFails(long NID);

        // == Warnmeldungen ====== ====== ======

        IEvent eWarn(IEventParameter value);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //IEvent eWarn(IKillEventParamIfNot value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent eWarn(string value);

        /// <summary>
        /// mko, 28.5.2020
        /// Event durch eine mehrsprachig abrufbare NID beschreiben
        /// </summary>
        /// <param name="NID">Naming- ID</param>
        /// <returns></returns>
        IEvent eWarn(long NID);

        /// <summary>
        /// mko, 19.9.2018
        /// </summary>
        /// <returns></returns>
        IEvent eWarn();

        // == Inforamtionsmeldungen ====== ====== ======

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent eInfo(IEventParameter value);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //IEvent eInfo(IKillEventParamIfNot value);

        /// <summary>
        /// mko, 28.5.2020
        /// Event durch eine mehrsprachig abrufbare NID beschreiben
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        IEvent eInfo(long NID);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent eInfo(string value);

        /// <summary>
        /// mko, 19.9.2018
        /// </summary>
        /// <returns></returns>
        IEvent eInfo();

        // === Erfolgsmeldungen ====== ======= =======

        /// <summary>
        /// 
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent eSucceeded(IEventParameter value);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        //IEvent eSucceeded(IKillEventParamIfNot value);

        /// <summary>
        /// mko, 22.2.2019
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent eSucceeded(string value);

        /// <summary>
        /// mko, 28.5.2020
        /// Event durch eine mehrsprachig abrufbare NID beschreiben
        /// </summary>
        /// <param name="NID"></param>
        /// <returns></returns>
        IEvent eSucceeded(long NID);

        IEvent eSucceeded();

        IEvent ePrms(string name, params IEventParameter[] pn);

        /// <summary>
        /// Defines a textvalue
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        ITxt txt(string text);

        /// <summary>
        /// mko, 24.6.2020
        /// Erzeugt eine Naming- Id
        /// </summary>
        /// <param name="nid"></param>
        /// <returns></returns>
        INID NID(long nid);


        /// <summary>
        /// Defines a date constant
        /// </summary>
        /// <param name="dat"></param>
        /// <returns></returns>
        IDate date(DateTime dat);

        IDate date(int year, int month, int day);

        /// <summary>
        /// Defines a time constant
        /// </summary>
        /// <param name="dat"></param>
        /// <param name="showMilliseconds"></param>
        /// <returns></returns>
        ITime time(TimeSpan dat, bool showMilliseconds = false);

        ITime time(int hour, int minutes, int sec, int milliseconds = 0);

        // == KillIf ====== ====== ======

        IKillListElementIfNot KillListMemberIfNot(bool Condition, Func<IListMember> docuEntityFactory);

        IKillListElementIfNot KillListMemberIf(bool Condition, Func<IListMember> docuEntityFactory);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        IKillEventParamIfNot KillEventParamIfNot(bool Condition, Func<IEventParameter> docuEntityFactory);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        IKillEventParamIfNot KillEventParamIf(bool Condition, Func<IEventParameter> docuEntityFactory);


        /// <summary>
        /// mko, 6.8.2021
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        IKillMethodPrarmeterIfNot KillMethodParamIfNot(bool Condition, Func<IMethodParameter> docuEntityFactory);
        
        /// <summary>
        /// mko, 6.8.2021
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        IKillMethodPrarmeterIfNot KillMethodParamIf(bool Condition, Func<IMethodParameter> docuEntityFactory);

        /// <summary>
        /// mko, 6.8.2021
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        IKillInstanceMemberIfNot KillInstanceMemberIfNot(bool Condition, Func<IInstanceMember> docuEntityFactory);

        /// <summary>
        /// mko, 6.8.2021
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        IKillInstanceMemberIfNot KillInstanceMemberIf(bool Condition, Func<IInstanceMember> docuEntityFactory);    








        // == IfElse ====== ====== ======

        /// <summary>
        /// mko, 17.6.2020
        /// Wählen zwischen zwei möglichen Listenelementen. Z.B.
        /// pnL.m("Query", 
        ///         pnL.("Id", 99), 
        ///         pnL.IfElse(succeeded, 
        ///             () => pnL.ret(pnL.eSucceeded(pnL.i("DS") ...)), 
        ///             () => pnL.ret(pnL.eFailed(...))));
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        //IListMember IfElse(bool Condition, Func<IListMember> memberIfTrue, Func<IListMember> memberIfFalse);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="memberIfTrue"></param>
        /// <param name="memberIfFalse"></param>
        /// <returns></returns>
        IReturnValue IfElseRet(bool Condition, Func<IReturnValue> memberIfTrue, Func<IReturnValue> memberIfFalse);


        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IProperty IfElseProp(bool Condition, Func<IProperty> valueIfTrue, Func<IProperty> valueIfFalse);


        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IPropertyValue IfElsePropVal(bool Condition, Func<IPropertyValue> valueIfTrue, Func<IPropertyValue> valueIfFalse);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IInstance IfElseInstance(bool Condition, Func<IInstance> valueIfTrue, Func<IInstance> valueIfFalse);


        /// <summary>
        /// mko, 8.7.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IInstanceMember IfElseInstMember(bool Condition, Func<IInstanceMember> valueIfTrue, Func<IInstanceMember> valueIfFalse);


        /// <summary>
        /// mko, 4.12.2020 
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IMethod IfElseMethod(bool Condition, Func<IMethod> valueIfTrue, Func<IMethod> valueIfFalse);

        /// <summary>
        /// mko, 10.8.2021
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IMethodParameter IfElseMethodParam(bool Condition, Func<IMethodParameter> valueIfTrue, Func<IMethodParameter> valueIfFalse);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IEvent IfElseEvent(bool Condition, Func<IEvent> valueIfTrue, Func<IEvent> valueIfFalse);

        /// <summary>
        /// mko, 10.8.2021
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IEventParameter IfElseEventParam(bool Condition, Func<IEventParameter> valueIfTrue, Func<IEventParameter> valueIfFalse);


        /// <summary>
        /// Embeds entities as Child in current Entity.
        /// Note that thisEntity(.., embed(entities), ..) it is not the same like thisEntity(.., List(entities), ..).
        /// List then will be a child of this entity, and entities are childs of list:
        /// thisEntity             
        ///    +--> List
        ///           +-->> entities
        /// Instead of, after calling thisEntity(.., embed(entities), ..), entities are childs of thisEntity.
        /// thisEntity             
        ///     +-->> entities
        ///     
        /// mko, 10.8.2021
        /// Eingeschränkt für das Einbetten auf Listenmember.
        /// 
        /// Das Einbetten von Methodenparametern oder Instanzmembern erfolgt jetzt mit spezialisierten
        /// Methoden.
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IListMembersToEmbed EmbedListMembers(params IListMember[] members);


        /// <summary>
        /// mko, 10.8.2021
        /// Streng typisierte Embed- Funktion für Methodenparameter
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IMethodParametersToEmbed EmbedMethodParameters(params IMethodParameter[] members);

        /// <summary>
        /// mko, 10.8.2021
        /// Streng typisierte Embed- Funktion für Instanz- Member.
        /// </summary>
        /// <param name="members"></param>
        /// <returns></returns>
        IInstanceMembersToEmbed EmbedInstanceMembers(params IInstanceMember[] members);

        /// <summary>
        /// mko, 31.1.2019
        /// Führt ein KillIfNot- Kommando aus, wenn der übergebene Parameter ein solches ist.
        /// Als Ergebnis wird dann das eingekapselte IDocuEntity, falls die Bedingung nicht zutraf,
        /// oder null zurückgegeben. 
        /// Liegt kein KillIfNot- Kommando vor, dann wird dieses zurückgegeben
        /// 
        /// mko, 9.8.2021
        /// Aus der Schnittstelle entfernt, da Implementierungsdetail.
        /// Wird jetzt in den Konstruktoren von IInstance, IProperty etc. implementiert.
        /// 
        /// </summary>
        /// <param name="docuEntity"></param>
        /// <returns></returns>
        //IDocuEntity ExecuteKillCommand(IDocuEntity docuEntity);


        /// <summary>
        /// mko, 2.7.2019
        /// Erzeugt eine Kopie eines DocuEntity.
        /// 
        /// mko, 9.8.2021
        /// Entfernt
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        //IDocuEntity CreateCopyOfEntity(IDocuEntity entity);
    }
}
