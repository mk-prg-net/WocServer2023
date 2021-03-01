using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing;
using MKPRG.Tracing.DocuTerms;

using ANC = MKPRG.Naming;


namespace MKPRG.CSSQL.Results
{
    /// <summary>
    /// mko, 25.10.2018
    /// Einheitliche Beschreibung von Abfrageergebnissen
    /// </summary>
    public class PlxQueryResultDescriptionFactory
    {
        //public const string iQuery = "query";
        //public const string mQueryExec = "exec";
        //public const string iQueryResult = "result";
        //public const string pQueryResultCount = "count";

        IComposer pnL;

        public PlxQueryResultDescriptionFactory(IComposer pnL)
        {
            this.pnL = pnL;
        }

        /// <summary>
        /// mko, 25.10.2018
        /// Beschreibt Fehler, die eine Abfrage scheitern ließen
        /// 
        /// mko, 31.1.2019
        /// Als Parameter können jetzt auch KillIf- Ausdrücke übergeben werden. Diese 
        /// werden, falls vorhanden, evaluiert
        /// </summary>
        /// <param name="description"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryExecutionFailed(IDocuEntity description)
            => pnL.ReturnSearchExecutionFails(pnL.EncapsulateAsPropertyValue(description));

        /// <summary>
        /// mko, 7.12.2020
        /// </summary>
        /// <param name="reason"></param>
        /// <param name="filterTerms"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryExecutionFailed(IPropertyValue reason, params IPropertyValue[] filterTerms)
            => pnL.ReturnSearchExecutionFails(reason, filterTerms);



        /// <summary>
        /// 3.12.2020
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="reason"></param>
        /// <param name="filterTerms"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryExecutionFailed(Table tab, IPropertyValue reason, params IPropertyValue[] filterTerms)
            => pnL.ReturnSearchExecutionFails(tab.TableName, reason, filterTerms);

        public IDocuEntity CreateQueryExecutionFailed(Table tab1, Table JoinWithTab2, IPropertyValue reason, params IPropertyValue[] filterTerms)
            => pnL.ReturnSearchExecutionFails($"{tab1.TableName} Join {JoinWithTab2.TableName}", reason, filterTerms);

        public IDocuEntity CreateQueryExecutionFailed(Table tab1, Table JoinWithTab2, Table JoinWithTab3, IPropertyValue reason, params IPropertyValue[] filterTerms)
            => pnL.ReturnSearchExecutionFails($"{tab1.TableName} Join {JoinWithTab2.TableName} Join {JoinWithTab3.TableName}", reason, filterTerms);


        /// <summary>
        /// mko, 25.10.2018
        /// Eine Abfrage verlief erfolgreich, jedoch lieferte sie ein leeres Resultset.
        /// </summary>
        /// <returns></returns>
        public IDocuEntity CreateQueryResultEmpty()
            => pnL.ReturnSearchFailsEmptyResult();

        /// <summary>
        /// mko, 31.1.2019
        /// Zusätzliche Dokumentation von leeren Resultsets möglich.
        /// Als Parameter können jetzt auch KillIf- Ausdrücke übergeben werden. Diese 
        /// werden, falls vorhanden, evaluiert.
        /// </summary>
        /// <param name="FilterConditions"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryResultEmpty(params IPropertyValue[] FilterConditions)
            => pnL.ReturnSearchFailsEmptyResult(FilterConditions);

        /// <summary>
        /// mko, 3.12.2020
        /// </summary>
        /// <param name="TabName"></param>
        /// <param name="FilterConditions"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryResultEmpty(Table Tab, params IPropertyValue[] FilterConditions)
            => pnL.ReturnWarnEmptyResult(Tab.TableName, FilterConditions);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Tab1"></param>
        /// <param name="JoinedWithTab2"></param>
        /// <param name="FilterConditions"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryResultEmpty(Table Tab1, Table JoinedWithTab2, params IPropertyValue[] FilterConditions)
            => pnL.ReturnWarnEmptyResult($"{Tab1.TableName} Join {JoinedWithTab2.TableName}", FilterConditions);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Tab1"></param>
        /// <param name="JoinedWithTab2"></param>
        /// <param name="JoinedWithTab3"></param>
        /// <param name="FilterConditions"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryResultEmpty(Table Tab1, Table JoinedWithTab2, Table JoinedWithTab3, params IPropertyValue[] FilterConditions)
            => pnL.ReturnWarnEmptyResult($"{Tab1.TableName} Join {JoinedWithTab2.TableName} Join {JoinedWithTab3.TableName}", FilterConditions);

        /// <summary>
        /// mko, 7.12.2020
        /// </summary>
        /// <param name="descriptionOfInconsistencies"></param>
        /// <param name="FilterConditions"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryFailsDueInconsistencies(IPropertyValue descriptionOfInconsistencies, params IPropertyValue[] FilterConditions)
            => pnL.ReturnSearchFailsDueInconsistenciesResult(descriptionOfInconsistencies, FilterConditions);


        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Tab1"></param>
        /// <param name="descriptionOfInconsistencies"></param>
        /// <param name="FilterConditions"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryFailsDueInconsistencies(Table Tab1, IPropertyValue descriptionOfInconsistencies, params IPropertyValue[] FilterConditions)
            => pnL.ReturnSearchFailsDueInconsistenciesResult(Tab1.TableName, descriptionOfInconsistencies, FilterConditions);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Tab1"></param>
        /// <param name="JoinedWithTab2"></param>
        /// <param name="descriptionOfInconsistencies"></param>
        /// <param name="FilterConditions"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryFailsDueInconsistencies(Table Tab1, Table JoinedWithTab2, IPropertyValue descriptionOfInconsistencies, params IPropertyValue[] FilterConditions)
            => pnL.ReturnSearchFailsDueInconsistenciesResult($"{Tab1.TableName} Join {JoinedWithTab2.TableName}", descriptionOfInconsistencies, FilterConditions);

        /// <summary>
        /// mko, 4.12.2020
        /// </summary>
        /// <param name="Tab1"></param>
        /// <param name="JoinedWithTab2"></param>
        /// <param name="JoinedWithTab3"></param>
        /// <param name="descriptionOfInconsistencies"></param>
        /// <param name="FilterConditions"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryFailsDueInconsistencies(Table Tab1, Table JoinedWithTab2, Table JoinedWithTab3, IPropertyValue descriptionOfInconsistencies, params IPropertyValue[] FilterConditions)
            => pnL.ReturnSearchFailsDueInconsistenciesResult($"{Tab1.TableName} Join {JoinedWithTab2.TableName} Join {JoinedWithTab3.TableName}", descriptionOfInconsistencies, FilterConditions);


        /// <summary>
        /// mko, 25.10.2018
        /// Eine Abfrage verlief erfolgreich. Es werden statistische Informationen geliefert.
        /// 
        /// mko, 31.1.2018
        /// Achtung: Dieser Generator kann auch genutzt werden, um Abfragen, welche die leere Menge liefern, zu dokumentieren.
        /// Als Parameter können jetzt auch KillIf- Ausdrücke übergeben werden. Diese 
        /// werden, falls vorhanden, evaluiert.
        /// 
        /// </summary>
        /// <param name="countResultsetRows"></param>
        /// <returns></returns>
        public IDocuEntity CreateQueryResultOk(long countResultsetRows, IPropertyValue details = null)
            => pnL.ReturnSearchOk(countResultsetRows, details);


    }
}
