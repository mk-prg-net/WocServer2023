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
    /// </summary>
    public interface IComposer   
        : IXTabGenerator
    {
        IDTList List(params IListMember[] entities);


        Boolean boolean(bool b);

        Integer integer(long i);

        NN NN(ulong u);

        Double dbl(double d);

        String str(string s);


        IInstance i(string name, params IInstanceMember[] pn);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="pn"></param>
        /// <returns></returns>
        IInstance i(long NID, params IInstanceMember[] pn);

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
        /// mko, 10.4.2018
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
        /// mko, 28.02.2021
        /// Eigenschaft mit ulong- Wert
        /// </summary>
        /// <param name="Name"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(string Name, ulong Value);

        /// <summary>
        /// mko, 21.2.2021
        /// eigenschaft mit ulong Wert
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="Value"></param>
        /// <returns></returns>
        IProperty p(long NID, ulong Value);


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
        /// mko, 15.6.2020
        /// DocuTerm, der einen Platzhalter für den Wert eines Eigenschaftsausdruckes darstellt.
        /// Wird beim Pattern- Matching berücksichtigt.
        /// 
        /// Bsp.:
        /// pnL.m("Query", pnL.p("MatNr", pnL._()))
        /// </summary>
        /// <returns></returns>
        IWildCard _();

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
        ///      pnL.p("MatNr", pnL._()),
        ///      pnL.eFails(pnL._(pnL.eSucceded(_pnL._())))).IsSubTreeOf(X);
        /// </summary>
        /// <param name="subTreePattern"></param>
        /// <returns></returns>
        IWildCard _(IDocuEntity subTreePattern);


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

        IEvent e(String name, IEventParameter value);

        IEvent e(String name);

        /// <summary>
        /// mko, 3.3.2020
        /// </summary>
        /// <param name="NID"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        IEvent e(long NID, IEventParameter value);

        IEvent e(long NID);

        IEvent e(NID nid, IEventParameter value);

        IEvent e(NID nid);

        /// <summary>
        /// mko, 17.6.2020
        /// Erzeugt einen EventParameter nur dann, wenn eine Bedingung erfüllt ist.
        /// Sonst wird nur ein einfach benanntes Event ohne Parameter erzeugt.
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="killIfNot"></param>
        /// <returns></returns>
        IEvent e(long nid, IKillEventParamIfNot killIfNot);

        IEvent e(string name, IKillEventParamIfNot killIfNot);

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
        IEvent eStart(IKillEventParamIfNot value);

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
        IEvent eEnd(IKillEventParamIfNot value);

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
        IEvent eNotCompleted(IKillEventParamIfNot value);

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
        IEvent eFails(IKillEventParamIfNot value);

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
        IEvent eWarn(IKillEventParamIfNot value);

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
        /// <param name="NID">MKPRG.Naming- ID</param>
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
        IEvent eInfo(IKillEventParamIfNot value);

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
        IEvent eSucceeded(IKillEventParamIfNot value);

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
        NID NID(long nid);


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

        IKillIfNot KillIfNot(bool Condition, Func<IListMember> docuEntityFactory);

        IKillIfNot KillIf(bool Condition, Func<IListMember> docuEntityFactory);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        IKillEventParamIfNot KillIfNot(bool Condition, Func<IEventParameter> docuEntityFactory);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="docuEntityFactory"></param>
        /// <returns></returns>
        IKillEventParamIfNot KillIf(bool Condition, Func<IEventParameter> docuEntityFactory);

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
        IReturnValue IfElse(bool Condition, Func<IReturnValue> memberIfTrue, Func<IReturnValue> memberIfFalse);

        /// <summary>
        /// mko, 17.6.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IPropertyValue IfElse(bool Condition, Func<IPropertyValue> valueIfTrue, Func<IPropertyValue> valueIfFalse);

        /// <summary>
        /// mko, 8.7.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IInstanceMember IfElse(bool Condition, Func<IInstanceMember> valueIfTrue, Func<IInstanceMember> valueIfFalse);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IInstance IfElse(bool Condition, Func<IInstance> valueIfTrue, Func<IInstance> valueIfFalse);

        /// <summary>
        /// mko, 4.12.2020 
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IMethod IfElse(bool Condition, Func<IMethod> valueIfTrue, Func<IMethod> valueIfFalse);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IEvent IfElse(bool Condition, Func<IEvent> valueIfTrue, Func<IEvent> valueIfFalse);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Condition"></param>
        /// <param name="valueIfTrue"></param>
        /// <param name="valueIfFalse"></param>
        /// <returns></returns>
        IProperty IfElse(bool Condition, Func<IProperty> valueIfTrue, Func<IProperty> valueIfFalse);

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
        /// </summary>
        /// <param name="entities"></param>
        /// <returns></returns>
        IListToEmbed EmbedMembers(params IListMember[] entities);

        /// <summary>
        /// mko, 31.1.2019
        /// Führt ein KillIfNot- Kommando aus, wenn der übergebene Parameter ein solches ist.
        /// Als Ergebnis wird dann das eingekapselte IDocuEntity, falls die Bedingung nicht zutraf,
        /// oder null zurückgegeben. 
        /// Liegt kein KillIfNot- Kommando vor, dann wird dieses zurückgegeben
        /// </summary>
        /// <param name="docuEntity"></param>
        /// <returns></returns>
        IDocuEntity ExecuteKillCommand(IDocuEntity docuEntity);


        /// <summary>
        /// mko, 2.7.2019
        /// Erzeugt eine Kopie eines DocuEntity.
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        IDocuEntity CreateCopyOfEntity(IDocuEntity entity);
    }
}
