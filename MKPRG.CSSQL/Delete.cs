using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;


namespace MKPRG.CSSQL
{
    /// <summary>
    /// mko, 11.2.2020
    /// Im Rahmen der Entwicklung des Datentransferjobs DFC.DB nach DFC.Olap.SFC implementiert
    /// </summary>
    public class Delete<TBo> 
    {
        /// <summary>
        /// Erzeugt einen SQL- Delete Befehl
        /// </summary>
        /// <param name="_Evaluator">NaLisp- Baum Evaluierer</param>
        /// <param name="_Inspector">NaLisp- Baum Konsitenzprüfer</param>
        public Delete(Evaluator _Evaluator, Inspector _Inspector)
        {
            this._Evaluator = _Evaluator;
            this._Inspector = _Inspector;
        }

        Evaluator _Evaluator;
        Inspector _Inspector;

        public WhereBuilder<TBo> From(Table tab)
        {

            return new WhereBuilder<TBo>($"DELETE FROM  {tab.TableName} ", _Evaluator, _Inspector);

        }
    }
}
