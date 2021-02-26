using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

//using TechTerms = MKPRG.Tracing.DocuTerms.Composer.TechTerms;

using ANC = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.Tracing.DocuTerms
{


    /// <summary>
    /// mko, 27.2.2019
    /// Komplexe Strukturen zur Dokumentation von Programmzuständen wie Fehlern oder Statuswechseln in einer standardisierten Form
    /// erzeugen. Dadurch wird eine automatisierte Auswertung dieser Strukturen ermöglicht.
    /// </summary>
    public static class ComposerSubTrees
    {
        //-------------------------------------------------------------------------------------------------------------------------
        // Common

        /// <summary>
        /// mko, 29.8.2019
        /// Allgemeine Anzeige, das eine Methode zwar aufgerufen, in dieser aber noch keine wesentlichen Berechnungen stattgefunden 
        /// haben.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="MethodName"></param>
        /// <param name="MethodParameters"></param>
        /// <returns></returns>
        public static IInstance ReturnNotCompleted(
            this IComposer dct, 
            string MethodName, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(MethodName,
                        // MethodParameters == null ist in EmbedMembers berücksichtigt
                        dct.EmbedMembers(MethodParameters),
                        dct.eNotCompleted()));

        /// <summary>
        /// mko, 3.7.2020
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="MethodName"></param>
        /// <param name="Details"></param>
        /// <param name="MethodParameters"></param>
        /// <returns></returns>
        public static IInstance ReturnAfterSuccess(
            this IComposer dct, 
            string MethodName, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(MethodName,
                        dct.EmbedMembers(MethodParameters),
                        dct.ret(dct.eSucceeded())));

        public static IInstance ReturnAfterSuccess(
            this IComposer dct, 
            long MethodNameNID, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(MethodNameNID,
                        dct.EmbedMembers(MethodParameters),
                        dct.ret(dct.eSucceeded())));


        public static IInstance ReturnAfterSuccessWithDetails(
            this IComposer dct, 
            string MethodName, 
            IEventParameter Details, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                dct.m(MethodName,
                    dct.EmbedMembers(MethodParameters),
                    dct.ret(dct.eSucceeded(Details))));

        public static IInstance ReturnAfterSuccessWithDetails(
            this IComposer dct, 
            long MethodNameNID, 
            IEventParameter Details, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(MethodNameNID,
                        dct.EmbedMembers(MethodParameters),
                        dct.ret(dct.eSucceeded(Details))));


        // After Failure

        public static IInstance ReturnAfterFailure(
            this IComposer dct, 
            string MethodName, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                dct.m(MethodName,
                    dct.EmbedMembers(MethodParameters),
                    dct.ret(dct.eFails())));

        public static IInstance ReturnAfterFailure(
            this IComposer dct, 
            long MethodNameNID, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(MethodNameNID,
                        dct.EmbedMembers(MethodParameters),
                        dct.ret(dct.eFails())));


        public static IInstance ReturnAfterFailureWithDetails(
            this IComposer dct, 
            string MethodName, 
            IEventParameter Details, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                dct.m(MethodName,
                    dct.EmbedMembers(MethodParameters),
                    dct.ret(dct.eFails(Details))));

        public static IInstance ReturnAfterFailureWithDetails(
            this IComposer dct, 
            long MethodNameNID, 
            IEventParameter Details, 
            params IMethodParameter[] MethodParameters)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(MethodNameNID,
                        dct.EmbedMembers(MethodParameters),
                        dct.ret(dct.eFails(Details))));

        //-------------------------------------------------------------------------------------------------------------------------
        // Access

        /// <summary>
        /// mko, 4.3.2019
        /// Dokumentiert den erfolgreichen/fehlgeschlagenen Zugriff auf ein Objekt, dessen Existenz vorausgesetzt wurde.
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="DataSource">Datenquelle</param>
        /// <param name="CompositeKeyParts">Schlüsselattribute, die beim Abruf verwendet wurden</param>
        /// <returns></returns>
        public static IInstance ReturnFetch(
            this IComposer dct, 
            bool Succeeded, 
            IPropertyValue DataSource, 
            params IPropertyValue[] CompositeKeyParts)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Access.Fetch.UID,
                                dct.p(TT.Access.Datasources.DataSource.UID, DataSource),
                                // alle Schlüsselkomponenten als Key- Properties auflisten
                                dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                dct.ret(dct.IfElse(Succeeded, () => (IReturnValue)dct.eSucceeded(), () => dct.eFails()))));

        /// <summary>
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="Succeeded"></param>
        /// <param name="UID_of_DataSourceName"></param>
        /// <param name="CompositeKeyParts"></param>
        /// <returns></returns>
        public static IInstance ReturnFetch(
            this IComposer dct, 
            bool Succeeded, 
            long UID_of_DataSourceName, 
            params IPropertyValue[] CompositeKeyParts)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Access.Fetch.UID,
                                dct.p_NID(TT.Access.Datasources.DataSource.UID, UID_of_DataSourceName),
                                // alle Schlüsselkomponenten als Key- Properties auflisten
                                dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                dct.ret(dct.IfElse(Succeeded, () => (IReturnValue)dct.eSucceeded(), () => dct.eFails()))));

        /// <summary>
        /// mko, 2.7.2020
        /// Predikate, die bim Filtern auf Daensätze angewendet werden.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="UID_Operation"></param>
        /// <param name="mParams"></param>
        /// <returns></returns>
        public static IMethod Predicate(
            this IComposer dct, 
            long UID_Operation, 
            params IMethodParameter[] mParams)
            => dct.m(UID_Operation, mParams);


        /// <summary>
        /// mko, 2.7.2020
        /// Beschreibt Erfolg einer Fetch- Operation
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="Succeeded"></param>
        /// <param name="UID_of_DataSourceName"></param>
        /// <param name="UID_of_Datatype"></param>
        /// <param name="CompositeKeyParts"></param>
        /// <returns></returns>
        public static IInstance ReturnFetch(
            this IComposer dct,
            bool Succeeded,
            long UID_of_DataSource,
            long UID_of_DataType,
            params IPropertyValue[] CompositeKeyParts)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Access.Fetch.UID,
                        dct.p_NID(TT.Access.Datasources.DataSource.UID, UID_of_DataSource),
                        dct.p_NID(TT.Access.DataType.UID, UID_of_DataType),
                        // alle Schlüsselkomponenten als Key- Properties auflisten
                        dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                        dct.ret(dct.IfElse(Succeeded, () => (IReturnValue)dct.eSucceeded(), () => dct.eFails()))));




        /// <summary>
        /// mko, 4.3.2019
        /// Dokumentiert den erfolgreichen/fehlgeschlagenen Zugriff auf ein Objekt, dessen Existenz vorausgesetzt wurde.
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="Succeeded">Zugriff erfolgreich ja/nein</param>
        /// <param name="DataSource">Datenquelle, aus der das Objekt geholt werden sollte</param>
        /// <param name="Details">Dateils zum erfolgreichen/fehlgeschlagenen Zugriff</param>
        /// <param name="CompositeKeyParts">Details zum Schlüssel</param>
        /// <returns></returns>
        public static IInstance ReturnFetchWithDetails(
            this IComposer dct, 
            bool Succeeded, 
            IPropertyValue DataSource, 
            IEventParameter Details, 
            params IPropertyValue[] CompositeKeyParts)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Access.Fetch.UID,
                                dct.p(TT.Access.Datasources.DataSource.UID, DataSource),
                                // alle Schlüsselkomponenten als Key- Properties auflisten
                                dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                dct.ret(dct.IfElse(Succeeded, () => (IReturnValue)dct.eSucceeded(Details), () => dct.eFails(Details)))));

        /// <summary>
        /// mko, 18.12.2020
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="Succeeded"></param>
        /// <param name="DataSourceNID"></param>
        /// <param name="Details"></param>
        /// <param name="CompositeKeyParts"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchWithDetails(
            this IComposer dct,
            bool Succeeded,
            long DataSourceNID,
            IEventParameter Details,
            params IPropertyValue[] CompositeKeyParts)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Access.Fetch.UID,
                                dct.p_NID(TT.Access.Datasources.DataSource.UID, DataSourceNID),
                                // alle Schlüsselkomponenten als Key- Properties auflisten
                                dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                dct.ret(dct.IfElse(Succeeded, () => (IReturnValue)dct.eSucceeded(Details), () => dct.eFails(Details)))));


        /// <summary>
        /// mko, 2.7.2020
        /// Beschreibt Erfolg einer Fetch- Operation
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        public static IInstance ReturnFetchWithDetails(
            this IComposer dct,
            bool Succeeded,
            long UID_of_DataSource,
            long UID_of_DataType,
            IEventParameter Details,
            params IPropertyValue[] CompositeKeyParts)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Access.Fetch.UID,
                                dct.p_NID(TT.Access.Datasources.DataSource.UID, UID_of_DataSource),
                                dct.p_NID(TT.Access.DataType.UID, UID_of_DataType),
                                // alle Schlüsselkomponenten als Key- Properties auflisten
                                dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                dct.ret(dct.IfElse(Details != null,
                                    () => dct.IfElse(Succeeded,
                                                () => (IReturnValue)dct.eSucceeded(),
                                                () => dct.eFails()),
                                    () => dct.IfElse(Succeeded,
                                                () => (IReturnValue)dct.eSucceeded(Details),
                                                () => dct.eFails(Details))))));

        // dct.ret(dct.IfElse(Succeeded, () =>  Details != null ? dct.eSucceeded(Details) : dct.eSucceeded(), () => dct.eFails(Details)))));

        /// <summary>
        /// mko, 3.7.2020
        /// Ein abzurufendes Objekt wurde nicht gefunden, was einen Fehler darstellt
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="UID_of_DataSource"></param>
        /// <param name="UID_of_DataType"></param>
        /// <param name="CompositeKeyParts"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchNotFound(
            this IComposer dct, 
            long UID_of_DataSource, 
            long UID_of_DataType, 
            params IPropertyValue[] CompositeKeyParts)
             => dct.i(TTD.StateDescription.FinStateDescr.UID,
                     dct.m(TT.Access.Fetch.UID,
                                 dct.p_NID(TT.Access.Datasources.DataSource.UID, UID_of_DataSource),
                                 dct.p_NID(TT.Access.DataType.UID, UID_of_DataType),
                                 // alle Schlüsselkomponenten als Key- Properties auflisten
                                 dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                 dct.ret(
                                     dct.eFails(
                                         dct.List(
                                             dct.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Search.NotFound.UID))))));

        /// <summary>
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="UID_of_DataSource"></param>
        /// <param name="UID_of_DataType"></param>
        /// <param name="Details"></param>
        /// <param name="CompositeKeyParts"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchNotFoundWithDetails(
            this IComposer dct,
            long UID_of_DataSource,
            long UID_of_DataType,
            IPropertyValue Details,
            params IPropertyValue[] CompositeKeyParts)
             => dct.i(TTD.StateDescription.FinStateDescr.UID,
                     dct.m(TT.Access.Fetch.UID,
                                 dct.p_NID(TT.Access.Datasources.DataSource.UID, UID_of_DataSource),
                                 dct.p_NID(TT.Access.DataType.UID, UID_of_DataType),
                                 // alle Schlüsselkomponenten als Key- Properties auflisten
                                 dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                 dct.ret(
                                     dct.eFails(
                                         dct.List(
                                             dct.p_NID(TTD.StateDescription.WhatsUp.UID, ANC.TechTerms.Search.NotFound.UID),
                                             dct.p(TTD.StateDescription.Why.UID, Details))))));




        /// <summary>
        /// mko, 3.7.2020
        /// Der Versuch, ein Objekt abzurufen, war erfolglos, und es wird eine leere Menge zurückgeliefert. Davor wird gewarnt.
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="UID_of_DataSource"></param>
        /// <param name="UID_of_DataType"></param>
        /// <param name="CompositeKeyParts"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchWarnEmptySet(
            this IComposer dct, 
            long UID_of_DataSource, 
            long UID_of_DataType, 
            params IPropertyValue[] CompositeKeyParts)
             => dct.i(TTD.StateDescription.FinStateDescr.UID,
                     dct.m(TT.Access.Fetch.UID,
                                 dct.p_NID(TT.Access.Datasources.DataSource.UID, UID_of_DataSource),
                                 dct.p_NID(TT.Access.DataType.UID, UID_of_DataType),
                                 // alle Schlüsselkomponenten als Key- Properties auflisten
                                 dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                   dct.ret(
                                     dct.eWarn(
                                         dct.List(
                                             dct.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Sets.EmptySet.UID))))));

        /// <summary>
        /// mko, 3.7.2020
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="UID_of_DataSource"></param>
        /// <param name="UID_of_DataType"></param>
        /// <param name="Details"></param>
        /// <param name="CompositeKeyParts"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchWarnEmptySetWithDetails(
            this IComposer dct,
            long UID_of_DataSource,
            long UID_of_DataType,
            IPropertyValue Details,
            params IPropertyValue[] CompositeKeyParts)
             => dct.i(TTD.StateDescription.FinStateDescr.UID,
                     dct.m(TT.Access.Fetch.UID,
                                 dct.p_NID(TT.Access.Datasources.DataSource.UID, UID_of_DataSource),
                                 dct.p_NID(TT.Access.DataType.UID, UID_of_DataType),
                                 // alle Schlüsselkomponenten als Key- Properties auflisten
                                 dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                   dct.ret(
                                     dct.eWarn(
                                         dct.List(
                                             dct.p_NID(TTD.StateDescription.WhatsUp.UID, ANC.TechTerms.Sets.EmptySet.UID),
                                             dct.p(TTD.StateDescription.Why.UID, Details))))));



        /// <summary>
        /// mko, 21.5.2019
        /// Dokumentiert den erfolgreichen Zugriff auf ein Objekt. Jedoch kam es dabei zu Komplikationen, die durch Wanrnungen dokumentiert sind
        /// 
        /// mko, 4.12.2020
        /// CompositeKeyParts.Select -: CompositeKeyParts?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="DataSource"></param>
        /// <param name="Warnings"></param>
        /// <param name="CompositeKeyParts"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchWithWarnings(
            this IComposer dct, 
            IPropertyValue DataSource, 
            IEventParameter Warnings, 
            params IPropertyValue[] CompositeKeyParts)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Access.Fetch.UID,
                                dct.p(TT.Access.Datasources.DataSource.UID, DataSource),
                                // alle Schlüsselkomponenten als Key- Properties auflisten
                                dct.EmbedMembers(CompositeKeyParts?.Select(kp => dct.p(TT.Search.Key.UID, kp)).ToArray()),
                                dct.ret(dct.eWarn(Warnings))));

        //-------------------------------------------------------------------------------------------------------------------------
        // Search




        /// <summary>
        /// mko, 6.7.2020
        /// 
        /// mko, 3.12.2020
        /// Überarbeiten
        /// 
        /// mko, 4.12.2020
        /// Results.Select -: Results?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="countResults"></param>
        /// <param name="Results"></param>
        /// <param name="details"></param>
        /// <returns></returns>
        public static IInstance CreateSearchResult(
            this IComposer dct,
            long countResults, 
            IEnumerable<IPropertyValue> Results, 
            IPropertyValue details)
            => dct.i(TTD.MetaData.Result.UID,
                    dct.p_NID(TT.Grammar.Subject.UID, TT.Search.Search.UID),
                    dct.p(TT.Metrology.Counter.UID, Results?.Count() ?? 0),
                    dct.KillIf(details == null, () => dct.p(TTD.MetaData.Details.UID, details)),
                    dct.IfElse(countResults == 0L,
                         () => dct.p_NID(TTD.MetaData.Val.UID, TT.Sets.EmptySet.UID),
                         () => dct.EmbedMembers(Results?.Select(res => dct.p(TTD.MetaData.Val.UID, res)).ToArray())));

        /// <summary>
        /// mko, 6.7.2020
        /// Suchergebnis als allgemeines Muster zum Suchen mittels SubTree- Operatoren.
        /// </summary>
        /// <param name="dct"></param>
        /// <returns></returns>
        public static IInstance SearchResult(this IComposer dct)
            => dct.i(TTD.MetaData.Result.UID,
                    dct.p_NID(TT.Grammar.Subject.UID, TT.Search.Search.UID),
                    dct.p(TT.Metrology.Counter.UID, dct._()));

        /// <summary>
        /// mko, 6.7.2020
        /// Allgemeiner Kopf eines Suchergebnisses
        /// </summary>
        /// <param name="dct"></param>
        /// <returns></returns>
        public static IInstance ReturnSearch(this IComposer dct)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID));

        /// <summary>
        /// mko, 6.7.2020
        /// 
        /// mko, 4.12.2020
        /// FilterTerms.Select -: FilterTerms?.Select
        /// 
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="countResults"></param>
        /// <param name="Results"></param>
        /// <param name="details"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchOk(
            this IComposer dct, 
            long countResults, 
            IEnumerable<IPropertyValue> Results, 
            IPropertyValue details, 
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation des leeren Menge als Ergebnis (Warnung)
                            dct.ret(
                                dct.IfElse(countResults == 0,
                                    () => (IReturnValue)dct.eWarn(dct.CreateSearchResult(0L, new IPropertyValue[] { }, details)),
                                    () => dct.eSucceeded(dct.CreateSearchResult(countResults, Results, details))))));

        /// <summary>
        /// mko, 3.12.2020
        /// 
        /// mko, 4.12.2020
        /// FilterTerms.Select -: FilterTerms?.Select
        /// 
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="countResults"></param>
        /// <param name="Results"></param>
        /// <param name="details"></param>
        /// <param name="TableName"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchOk(
            this IComposer dct,
            long countResults,
            IEnumerable<IPropertyValue> Results,
            IPropertyValue details,
            string TableName,
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,

                            // Tabellenname
                            dct.p(TT.Access.Datasources.WellKnown.DataBaseTable.UID, TableName),

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation des leeren Menge als Ergebnis (Warnung)
                            dct.ret(
                                dct.IfElse(countResults == 0,
                                    () => (IReturnValue)dct.eWarn(dct.CreateSearchResult(0L, null, details)),
                                    () => dct.eSucceeded(dct.CreateSearchResult(countResults, Results, details))))));


        /// <summary>
        /// mko, 3.12.2020
        /// 
        /// 
        /// mko, 4.12.2020
        /// FilterTerms.Select -: FilterTerms?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="countResults"></param>
        /// <param name="Results"></param>
        /// <param name="details"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnWarnEmptyResult(
            this IComposer dct, 
            string TableName, 
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,

                            dct.KillIf(string.IsNullOrWhiteSpace(TableName), () => dct.p(TT.Access.Datasources.WellKnown.DataBaseTable.UID, TableName)),

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation des leeren Menge als Ergebnis (Warnung)
                            dct.ret(dct.eWarn(dct.CreateSearchResult(0L, new IPropertyValue[] { }, null)))));

        /// <summary>
        /// mko, 3.12.2020
        /// 
        /// mko, 4.12.2020
        /// FilterTerms.Select -: FilterTerms?.Select
        /// 
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="countResults"></param>
        /// <param name="Results"></param>
        /// <param name="details"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnFailEmptyResult(
            this IComposer dct, 
            string TableName, 
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,

                            dct.KillIf(string.IsNullOrWhiteSpace(TableName), () => dct.p(TT.Access.Datasources.WellKnown.DataBaseTable.UID, TableName)),

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation des leeren Menge als Ergebnis (Warnung)
                            dct.ret(dct.eFails(dct.CreateSearchResult(0L, null, null)))));




        /// <summary>
        /// mko, 22.6.2020
        /// Dokumentiert die Ergebnisse einer Suche
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="countResults"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchOk(
            this IComposer dct, 
            long countResults, 
            IPropertyValue details, 
            params IPropertyValue[] FilterTerms)
            => dct.ReturnSearchOk(countResults, null, details, FilterTerms);

        /// <summary>
        /// mko, 22.6.2020
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="Results"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchOk(
            this IComposer dct, 
            IEnumerable<IPropertyValue> Results, 
            params IPropertyValue[] FilterTerms)
            => dct.ReturnSearchOk(Results.Count(), Results, null, FilterTerms);


        /// <summary>
        /// mko, 27.2.2019
        /// Signalisiert für eine Search- Methode, das die Suche zwar fehlerfrei ausgeführt wurde, jedoch zu keinem Ergebnis führte
        /// </summary>
        /// <param name="dct"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchWarnEmptyResult(
            this IComposer dct, 
            params IPropertyValue[] FilterTerms)
            => dct.ReturnWarnEmptyResult(null, FilterTerms);


        /// <summary>
        /// mko, 
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="TableName"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchWarnEmptyResult(
            this IComposer dct, 
            string TableName, 
            params IPropertyValue[] FilterTerms)
            => dct.ReturnWarnEmptyResult(TableName, FilterTerms);



        /// <summary>
        /// Signalisiert für eine Search- Methode, dass die Suche kein Ergebnis geliefert hat und dies als Fehler zu werten ist.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchFailsEmptyResult(
            this IComposer dct, 
            params IPropertyValue[] FilterTerms)
            => dct.ReturnFailEmptyResult(null, FilterTerms);


        /// <summary>
        /// Signalisiert für eine Search- Methode, dass die Suche kein Ergebnis geliefert hat und dies als Fehler zu werten ist.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchFailsEmptyResult(
            this IComposer dct, 
            string TableName, 
            params IPropertyValue[] FilterTerms)
            => dct.ReturnFailEmptyResult(TableName, FilterTerms);


        /// <summary>
        /// mko, 3.12.2020
        /// Signalisiert für eine Search- Methode, dass eine Suche wg. Inkonsitenzen in den Daten gescheitert ist.
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchFailsDueInconsistenciesResult(
            this IComposer dct, 
            string TableName, 
            IPropertyValue descriptionOfInconsistencies, 
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,

                            dct.KillIf(string.IsNullOrWhiteSpace(TableName), () =>  dct.p(TT.Access.Datasources.WellKnown.DataBaseTable.UID, TableName)),

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation des leeren Menge als Ergebnis
                            dct.ret(dct.eFails(                                
                                dct.i(TTD.StateDescription.Details.UID,
                                    dct.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Access.Datasources.DataInconsistency.UID),
                                    dct.KillIf(descriptionOfInconsistencies == null, () =>  dct.p(TTD.StateDescription.Why.UID, descriptionOfInconsistencies)))))));


        /// <summary>
        /// mko, 7.12.2020
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="descriptionOfInconsistencies"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchFailsDueInconsistenciesResult(
            this IComposer dct,            
            IPropertyValue descriptionOfInconsistencies,
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,                            

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation der Aufgetretenen inkonsitenzen und Wertung als Fehler
                            dct.ret(dct.eFails(
                                dct.i(TTD.StateDescription.Details.UID,
                                    dct.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Access.Datasources.DataInconsistency.UID),
                                    dct.KillIf(descriptionOfInconsistencies == null, () => dct.p(TTD.StateDescription.Why.UID, descriptionOfInconsistencies)))))));


        /// <summary>
        /// mko, 3.12.2020
        /// Rumpf der Meldung einer gescheiterten Suche wg. Inkonsistenzen. Sinnvoll für IsSubTreeOf(...) Auswertungen.
        /// </summary>
        /// <param name="dct"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchFailsDueInconsistenciesResult(this IComposer dct)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,
                            // Dokumentation des leeren Menge als Ergebnis
                            dct.ret(dct.eFails(
                                dct.i(TTD.StateDescription.Details.UID,
                                    dct.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Access.Datasources.DataInconsistency.UID))))));






        public static IInstance ReturnSearchFails(
            this IComposer dct, 
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation des leeren Menge als Ergebnis
                            dct.ret(dct.eFails())));

        /// <summary>
        /// mko, 22.6.2020
        /// Signalisiert, daß eine Abfrage bereits bei der Ausführung gescheitert ist.
        /// 
        /// mko, 4.12.2020
        /// reasons.Select -: reasons?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="reasons"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchExecutionFails(
            this IComposer dct, 
            params IPropertyValue[] reasons)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,

                            // Dokumentation des leeren Menge als Ergebnis
                            dct.ret(dct.eFails(
                                dct.i(TTD.MetaData.Details.UID,

                                    dct.p_NID(TTD.StateDescription.CurrentState.UID, ANC.TechTerms.Runtime.Execute.UID),

                                    // alle angewendeten Filter als Filter- Properties auflisten
                                    dct.EmbedMembers(reasons?.Select(err => dct.p(TTD.MetaData.Error.UID, err)).ToArray()))))));

        /// <summary>
        /// mko, 7.12.2020
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="reason"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchExecutionFails(
            this IComposer dct,            
            IPropertyValue reason,
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,                            

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation des leeren Menge als Ergebnis
                            dct.ret(dct.eFails(
                                dct.i(TTD.MetaData.Details.UID,

                                    dct.p_NID(TTD.StateDescription.CurrentState.UID, TT.Runtime.Execute.UID),

                                    dct.KillIf(reason == null, () => dct.p(TTD.StateDescription.WhatsUp.UID, reason)))))));




        /// <summary>
        /// mko, 3.12.2020
        /// 
        /// mko, 4.12.2020
        /// FilterTerms.Select -: FilterTerms?.Select
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="TableName"></param>
        /// <param name="reason"></param>
        /// <param name="FilterTerms"></param>
        /// <returns></returns>
        public static IInstance ReturnSearchExecutionFails(
            this IComposer dct, 
            string TableName, 
            IPropertyValue reason, 
            params IPropertyValue[] FilterTerms)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Search.Search.UID,

                            dct.KillIf(string.IsNullOrWhiteSpace(TableName), () => dct.p(TT.Access.Datasources.WellKnown.DataBaseTable.UID, TableName)),

                            // alle angewendeten Filter als Filter- Properties auflisten
                            dct.EmbedMembers(FilterTerms?.Select(flt => dct.p(TT.Search.Filter.UID, flt)).ToArray()),

                            // Dokumentation des leeren Menge als Ergebnis
                            dct.ret(dct.eFails(
                                dct.i(TTD.MetaData.Details.UID,

                                    dct.p_NID(TTD.StateDescription.CurrentState.UID, TT.Runtime.Execute.UID),

                                    dct.KillIf(reason == null, () => dct.p(TTD.StateDescription.WhatsUp.UID, reason)))))));


        //--------------------------------------------------------------------------------------------------------------------------
        // Preconditions

        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt die fehlgeschlagene Validierung einer Vorbedingung: Argument liegt außerhalb eines gültigen Bereiches
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="ArgumentName">Argument, das der Vorbedingung nicht genügte</param>
        /// <param name="Range">Optional: Definition des Bereiches</param>
        /// <returns></returns>
        public static IInstance ReturnValidatePreconditionFailedArgumentOutOfRange(
            this IComposer dct, 
            IMethodParameter Argument, 
            IPropertyValue Range = null)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Validation.Validate.UID,

                            // Anzeige, das Validierung gescheitert ist
                            dct.ret(dct.eFails(

                                // Vorbedingung, welche vom Parameter nicht erfüllt wurde 
                                dct.i(TT.Validation.PreCondition.UID,
                                    dct.m(TT.Validation.CheckIfValueInRange.UID,
                                            //Parameter, der out of range ist
                                            Argument,

                                            // Bereich, auf den geprüft wurde
                                            dct.p(TT.Sets.Range.UID, Range),

                                            dct.ret(false)))))));

        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt einen Bereich aus Long- Werten
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IInstance Range(
            this IComposer dct, 
            long begin, 
            long end)
            => dct.i(TT.Sets.Range.UID,
                    dct.p(TT.Sets.Begin.UID, begin),
                    dct.p(TT.Sets.End.UID, end));

        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt einen Bereich aus Long- Werten
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IInstance Range(
            this IComposer dct, 
            int begin, 
            int end)
            => dct.i(TT.Sets.Range.UID,
                    dct.p(TT.Sets.Begin.UID, begin),
                    dct.p(TT.Sets.End.UID, end));


        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt einen Bereich aus Long- Werten
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="begin"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public static IInstance Range(
            this IComposer dct, 
            double begin, 
            double end)
            => dct.i(TT.Sets.Range.UID,
                    dct.p(TT.Sets.Begin.UID, begin),
                    dct.p(TT.Sets.End.UID, end));


        /// <summary>
        /// mko, 27.2.2019
        /// Beschreibt eine Fehlgeschlagene Validierung einer allgemeine Vorbedingung. Die Vorbedingung sollte durch eine boolsche Funktion,
        /// die false zurückgibt, beschrieben werden wie:
        /// dct.m(TechTerms.RelationalOperators.mGtEq,
        ///    dct.p(TechTerms.MetaData.Arg, "arg1"),
        ///    dct.p(TechTerms.MetaData.Val, 100),
        ///    dct.ret(false))
        ///    
        /// mko, 4.12.2020
        /// PreconditionAsPredicate.Select -: PreconditionAsPredicate?.Select
        ///    
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="PreconditionAsPredicate"></param>
        /// <returns></returns>
        public static IInstance ReturnValidatePreconditionFailed(
            this IComposer dct, 
            params IMethod[] PreconditionAsPredicate)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Validation.Validate.UID,

                            dct.EmbedMembers(PreconditionAsPredicate?.Select(p => dct.p(TT.Validation.PreCondition.UID, p)).ToArray()),

                            // Anzeige, das Validierung gescheitert ist
                            dct.ret(dct.eFails())));

        /// <summary>
        /// mko, 3.7.2020
        /// </summary>
        /// <param name="dct"></param>
        /// <param name="Details"></param>
        /// <param name="PreconditionAsPredicate"></param>
        /// <returns></returns>
        public static IInstance ReturnValidatePreconditionFailedWithDetails(
            this IComposer dct,
            IPropertyValue Details,
            params IMethod[] PreconditionAsPredicate)
            => dct.i(TTD.StateDescription.FinStateDescr.UID,
                    dct.m(TT.Validation.Validate.UID,

                            dct.EmbedMembers(PreconditionAsPredicate?.Select(p => dct.p(TT.Validation.PreCondition.UID, p)).ToArray()),

                            // Anzeige, das Validierung gescheitert ist
                            dct.ret(dct.eFails(
                                dct.List(
                                    dct.p(TTD.StateDescription.WhatsUp.UID, Details))))));




        public static IInstance ReturnValidatePreconditionNotNullFailed(
            this IComposer dct, 
            IMethodParameter PropertyArgToBeNotNull)
            => dct.ReturnValidatePreconditionFailed(
                    dct.m(TT.Operators.Sets.NotIsNullValue.UID,
                        dct.EmbedMembers(PropertyArgToBeNotNull),
                        dct.ret(false)));

        //-----------------------------------------------------------------------------------------------
        // Authentifizierung
        // Achtung: Parameter wie UserId werden als optionale Parameter übergeben, um so subTree- Vergleiche
        //          der allgemeinen Struktur einer Fehlermeldung (ohne konkrete UserId) mit den detaillierten 
        //          Fehlerbeschreibungen aus den Rückgabewerten durchführen zu können.

        /// <summary>
        /// mko, 11.3.2019
        /// Beschreibt einen allgemeinen Fehler bei der Authentifizierung
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="UserId"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static IInstance ReturnAuthenticationGeneralError(
            this IComposer pnL,
            IPropertyValue UserId = null,
            IPropertyValue LoginStep = null,
            IEventParameter ErrorDescription = null)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                pnL.m(TT.Authentication.Login.UID,
                        pnL.KillIf(UserId == null, () => pnL.p(TT.Authentication.UserId.UID, UserId)),
                        pnL.KillIf(LoginStep == null, () => pnL.p(TTD.StateDescription.CurrentState.UID, LoginStep)),
                        pnL.ret(pnL.eFails(ErrorDescription))));

        /// <summary>
        /// mko, 6.7.2020
        /// Vereinfachter Aufruf
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="UserId"></param>
        /// <param name="LoginStep"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static IInstance ReturnAuthenticationGeneralError(
            this IComposer pnL,
            string UserId = null,
            string LoginStep = null,
            IEventParameter ErrorDescription = null)
            => ReturnAuthenticationGeneralError(
                pnL,
                string.IsNullOrWhiteSpace(UserId) ? null : pnL.txt(UserId),
                string.IsNullOrWhiteSpace(LoginStep) ? null : pnL.txt(LoginStep),
                ErrorDescription);

        /// <summary>
        /// mko, 6.7.2020
        /// Vereinfachter Aufruf.
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="UserId"></param>
        /// <param name="UID_LoginStep"></param>
        /// <param name="ErrorDescription"></param>
        /// <returns></returns>
        public static IInstance ReturnAuthenticationGeneralError(
            this IComposer pnL,
            string UserId,
            long UID_LoginStep,
            IEventParameter ErrorDescription = null)
            => ReturnAuthenticationGeneralError(
                pnL,
                pnL.txt(UserId),
                pnL.NID(UID_LoginStep),
                ErrorDescription);


        //-----------------------------------------------------------------------------------------------
        // Authorisierung

        /// <summary>
        /// mko, 18.12.2020
        /// Grundstruktur einer Meldung für Zugriffsverweigerung für IsSubTreeOf- Prüfungen
        /// </summary>
        /// <param name="pnL"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchAccessDenied(this IComposer pnL)        
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Fetch.UID,
                            pnL.ret(
                                pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID))))));

        /// <summary>
        /// mko, 18.12.2020
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="DataSourceNid"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchAccessDenied(this IComposer pnL, long DataSourceNid)
        => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                pnL.m(TT.Access.Fetch.UID,
                    pnL.p_NID(TT.Access.Datasources.DataSource.UID, DataSourceNid),
                    pnL.ret(
                        pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                            pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID))))));

        /// <summary>
        /// mko, 18.12.2020
        /// Dokumentiert verweigerten Zugriff auf eine Datenquelle
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="DataSourceNid"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchAccessDenied(this IComposer pnL, long DataSourceNid, IPropertyValue Reason)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,            
                    pnL.m(TT.Access.Fetch.UID,
                        pnL.p_NID(TT.Access.Datasources.DataSource.UID, DataSourceNid),
                        pnL.ret(
                            pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                pnL.KillIf(Reason == null, () => pnL.p(TTD.StateDescription.Why.UID, Reason)))))));

        /// <summary>
        /// mko, 18.12.2020
        /// Dokumentiert verweigerten Zugriff auf eine Datenquelle
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="DataSource"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchAccessDenied(this IComposer pnL, IPropertyValue DataSource, IPropertyValue Reason)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Fetch.UID,
                        pnL.p(TT.Access.Datasources.DataSource.UID, DataSource),
                        pnL.ret(
                            pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                pnL.KillIf(Reason == null, () => pnL.p(TTD.StateDescription.Why.UID, Reason)))))));

        /// <summary>
        /// mko, 18.12.2020
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="DataSource"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchAccessDenied(this IComposer pnL, string DataSource, IPropertyValue Reason)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Fetch.UID,
                        pnL.p(TT.Access.Datasources.DataSource.UID, DataSource),
                        pnL.ret(
                            pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                pnL.KillIf(Reason == null, () => pnL.p(TTD.StateDescription.Why.UID, Reason)))))));

        /// <summary>
        /// mko, 18.12.2020
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IInstance ReturnFetchAccessDenied(this IComposer pnL, IPropertyValue Reason)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Fetch.UID,                            
                        pnL.ret(
                            pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                    pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                    pnL.KillIf(Reason == null, () => pnL.p(TTD.StateDescription.Why.UID, Reason)))))));

        /// <summary>
        /// mko, 18.12.2020
        /// Grundstruktur einer Meldung für Zugriffsverweigerung für IsSubTreeOf- Prüfungen
        /// </summary>
        /// <param name="pnL"></param>
        /// <returns></returns>
        public static IInstance ReturnWriteAccessDenied(this IComposer pnL)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Write.UID,
                            pnL.ret(
                                pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                        pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID))))));

        /// <summary>
        /// mko, 18.12.2020
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="DataSourceNid"></param>
        /// <returns></returns>
        public static IInstance ReturnWriteAccessDenied(this IComposer pnL, long DataSourceNid)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Write.UID,
                        pnL.p_NID(TT.Access.Datasources.DataSource.UID, DataSourceNid),
                        pnL.ret(
                            pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID))))));


        /// <summary>
        /// mko, 18.1.2020
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IInstance ReturnWriteAccessDenied(this IComposer pnL, IPropertyValue Reason)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                pnL.m(TT.Access.Write.UID,
                    pnL.ret(
                        pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                pnL.KillIf(Reason == null, () => pnL.p(TTD.StateDescription.Why.UID, Reason)))))));


        /// <summary>
        /// mko, 18.12.2020
        /// Dokumentiert verweigerten Zugriff auf eine Datenquelle
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="DataSourceNid"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IInstance ReturnWriteAccessDenied(this IComposer pnL, long DataSourceNid, IPropertyValue Reason)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Write.UID,
                        pnL.p_NID(TT.Access.Datasources.DataSource.UID, DataSourceNid),
                        pnL.ret(
                            pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                pnL.KillIf(Reason == null, () => pnL.p(TTD.StateDescription.Why.UID, Reason)))))));

        /// <summary>
        /// mko, 18.12.2020
        /// Dokumentiert verweigerten Schreibzugriff auf eine Datenquelle
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="DataSource"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IInstance ReturnWriteAccessDenied(this IComposer pnL, IPropertyValue DataSource, IPropertyValue Reason)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Write.UID,
                        pnL.p(TT.Access.Datasources.DataSource.UID, DataSource),
                        pnL.ret(
                            pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                pnL.KillIf(Reason == null, () => pnL.p(TTD.StateDescription.Why.UID, Reason)))))));

        /// <summary>
        /// mko, 18.12.2020
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="DataSource"></param>
        /// <param name="Reason"></param>
        /// <returns></returns>
        public static IInstance ReturnWriteAccessDenied(this IComposer pnL, string DataSource, IPropertyValue Reason)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Access.Write.UID,
                        pnL.p(TT.Access.Datasources.DataSource.UID, DataSource),
                        pnL.ret(
                            pnL.eFails(pnL.i(TTD.MetaData.Details.UID,
                                pnL.p_NID(TTD.StateDescription.WhatsUp.UID, TT.Authorization.AccessDenied.UID),
                                pnL.KillIf(Reason == null, () => pnL.p(TTD.StateDescription.Why.UID, Reason)))))));


        //-----------------------------------------------------------------------------------------------
        // DocuTerm Parser
        /// <summary>
        /// mko, 24.6.2020
        /// Beschreibt Fehler beim Parsen von DocuTerms
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="NID_DocTermTypeName"></param>
        /// <param name="NID_SyntaxErrorDescription"></param>
        /// <returns></returns>
        public static IInstance ReturnDocuTermSyntaxError(
            this IComposer pnL,
            long NID_DocTermTypeName,
            long NID_SyntaxErrorDescription)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Parser.Parse.UID,
                        pnL.p_NID(TTD.MetaData.Arg.UID, NID_DocTermTypeName),
                        pnL.ret(pnL.eFails(pnL.i(TT.Parser.SyntaxError.UID,
                                    pnL.p_NID(TTD.StateDescription.WhatsUp.UID, NID_SyntaxErrorDescription))))));

        //-----------------------------------------------------------------------------------------------
        // DocuTerm Parser
        /// <summary>
        /// mko, 25.6.2020
        /// Beschreibt Fehler beim Parsen von DocuTerms. Details können angefügt werden
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="NID_DocTermTypeName"></param>
        /// <param name="NID_SyntaxErrorDescription"></param>
        /// <returns></returns>
        public static IInstance ReturnDocuTermSyntaxErrorWithDetails(
            this IComposer pnL,
            long NID_DocTermTypeName,
            long NID_SyntaxErrorDescription,
            long NID_Details)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Parser.Parse.UID,
                        pnL.p_NID(TTD.MetaData.Arg.UID, NID_DocTermTypeName),
                        pnL.ret(pnL.eFails(pnL.i(TT.Parser.SyntaxError.UID,
                                    pnL.p_NID(TTD.StateDescription.WhatsUp.UID, NID_SyntaxErrorDescription),
                                    pnL.p_NID(TTD.StateDescription.Why.UID, NID_Details))))));

        /// <summary>
        /// mko, 26.6.2020
        /// </summary>
        /// <param name="pnL"></param>
        /// <param name="NID_DocTermTypeName"></param>
        /// <param name="NID_SyntaxErrorDescription"></param>
        /// <param name="Details"></param>
        /// <returns></returns>
        public static IInstance ReturnDocuTermSyntaxErrorWithDetails(
            this IComposer pnL,
            long NID_DocTermTypeName,
            long NID_SyntaxErrorDescription,
            IPropertyValue Details)
            => pnL.i(TTD.StateDescription.FinStateDescr.UID,
                    pnL.m(TT.Parser.Parse.UID,
                        pnL.p_NID(TTD.MetaData.Arg.UID, NID_DocTermTypeName),
                        pnL.ret(pnL.eFails(pnL.i(TT.Parser.SyntaxError.UID,
                                    pnL.p_NID(TTD.StateDescription.WhatsUp.UID, NID_SyntaxErrorDescription),
                                    pnL.p(TTD.StateDescription.Why.UID, Details))))));




    }
}

