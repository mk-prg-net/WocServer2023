using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NaLisp = mko.NaLisp;
using Trc = mko.TraceHlp;
using mko.NaLisp.Core;


namespace MKPRG.CSSQL
{
    /// <summary>
    /// mko, 23.1.2018
    /// </summary>
    public class WhereBuilder<TBo>
    {

        Evaluator _Evaluator;
        Inspector _Inspector;

        /// <summary>
        /// mko, 19.6.2018
        /// Creates where clauses for Update queries
        /// </summary>
        /// <param name="SelectFrom"></param>
        public WhereBuilder(string SelectFrom, Evaluator _Evaluator, Inspector _Inspector)
        {
            SelectFromTerm = SelectFrom;
            this._Evaluator = _Evaluator;
            this._Inspector = _Inspector;
        }

        public WhereBuilder(string SelectFrom, RecordToBoMapper<TBo> Mapper, Evaluator _Evaluator, Inspector _Inspector)
        {
            SelectFromTerm = SelectFrom;
            this.Mapper = Mapper;
            this._Evaluator = _Evaluator;
            this._Inspector = _Inspector;
        }

        string SelectFromTerm { get; }
        RecordToBoMapper<TBo> Mapper { get; }


        public QueryBuilderResult<TBo> done()
        {
            return new QueryBuilderResult<TBo>(SelectFromTerm, Mapper);
        }


        public OrderByBuilder<TBo> Where(IColXpr colXpr)
        {

            var whereExpr = new Where(colXpr);
            var pe = _Inspector.Validate(whereExpr);

            Trc.ThrowArgExIfNot(pe.IsCurrentValid && pe.IsTreeValid, $"Invalid where expression: {pe.Description}");

            var res = (NaLisp.Data.IConstValue<string>)_Evaluator.Eval(whereExpr);

            if (string.IsNullOrWhiteSpace(res.Value))
            {
                return new OrderByBuilder<TBo>($"{SelectFromTerm}", Mapper);
            }
            else
            {
                return new OrderByBuilder<TBo>($"{SelectFromTerm} where {res.Value}", Mapper);
            }
        }

        /// <summary>
        /// mko, 29.10.2018
        /// Für eine Abfrage können alternative Where- Clauseln definiert werden, die über Schalter
        /// an oder abgeschaltet werden.
        /// Sind mehrere Where- Clauseln aktiv, dann werden sie in einem Und- Ausdruck zusammengefasst.
        /// </summary>
        /// <param name="switchedWhereClauses"></param>
        /// <returns></returns>
        public OrderByBuilder<TBo> SwitchedWhere(params (bool, IColXpr)[] switchedWhereClauses)
        {
            // Alle nicht aktivierten Where- Clauseln herausfiltern
            var switchedOn = switchedWhereClauses.Where(r => r.Item1).Select(r => r.Item2);

            Where where = null;
            if (switchedOn.Any())
            {
                // Wenn nach dem Filtern noch where- clauseln übrig bleiben, dann :
                if (switchedOn.Count() > 1)
                {
                    // Mehr als eine Where- Klausel ist eingeschaltet:
                    // Alle eingeschalteten Where- Klausen und- verknüpfen
                    var and = new AndXpr(switchedOn.ToArray());
                    where = new Where(and);
                }
                else
                {
                    // nur eine Where- Klausel ist eingeschaltet: bilden der where- Klausel aus 
                    // dieser einen.
                    where = new Where(switchedOn.First());
                }

                var pe = _Inspector.Validate(where);
                Trc.ThrowArgExIfNot(pe.IsCurrentValid && pe.IsTreeValid, $"Invalid where expression: {pe.Description}");

                var res = (NaLisp.Data.IConstValue<string>)_Evaluator.Eval(where);
                return new OrderByBuilder<TBo>($"{SelectFromTerm} WHERE {res.Value}", Mapper);
            }
            else
            {
                return new OrderByBuilder<TBo>($"{SelectFromTerm} ", Mapper);
            }
        }

    }
}
