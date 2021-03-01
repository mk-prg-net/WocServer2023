using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NaLisp = mko.NaLisp;
using Trc = mko.TraceHlp;
using mko.NaLisp.Core;

namespace ATMO.mko.QueryBuilder
{
    /// <summary>
    /// mko, 23.1.2018
    /// 
    /// mko, 31.10.2018
    /// Injection von NaLisp Evaluator
    /// </summary>
    public class FromBuilder<TBo>
    {

        Evaluator _Evaluator;
        Inspector _Inspector;

        public FromBuilder(string select, RecordToBoMapper<TBo> Mapper, Evaluator _Evaluator, Inspector _Inspector)
        {
            SelectTerm = select;
            this.Mapper = Mapper;
            this._Evaluator = _Evaluator;
            this._Inspector = _Inspector;
        }

        string SelectTerm { get; }
        RecordToBoMapper<TBo> Mapper { get;}

        QueryBuilderResult<TBo> done()
        {
            return new QueryBuilderResult<TBo>(SelectTerm, Mapper);
        }


        /// <summary>
        /// mko, 23.1.2018
        /// 
        /// mko, 19.2.2020
        /// Alias Definition für Tabellennamen implementiert 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public WhereBuilder<TBo> From(params ITable[] tab)
        {
            var bld = new StringBuilder($"SELECT {SelectTerm} FROM {tab[0].TableName} ");
            if (tab[0].HasAlias)
            {
                bld.Append(tab[0].Alias);
            }

            if (tab.Length > 1)
            {
                foreach(var tb in tab.Skip(1))
                {
                    bld.Append($",{tb.TableName} ");
                    if (tb.HasAlias)
                    {
                        bld.Append(tb.Alias);
                    }
                }                
            }

            return new WhereBuilder<TBo>(bld.ToString(), Mapper, _Evaluator, _Inspector);
        }

        /// <summary>
        /// mko, 19.2.2020
        /// 
        /// Definition von Joins in der Modernen SQL Join- Syntax. Es sind nur die Paare von 
        /// Tabellenfeldern zu definieren, die jeweils eine Equi- Join Bedingung darstellen.
        /// Aus den Metadaten dieser Spaltenpaare werden die notwendigen Zusatzinformationen 
        /// wie Namen der beteiligten Tabellen und eventuelle Aliases abgeleitet.
        /// 
        /// </summary>
        /// <param name="colToJoin"></param>
        /// <returns></returns>
        public WhereBuilder<TBo> EqJoinFrom(params (ColName left, ColName right)[] colToJoin)
        {
            var bld = new StringBuilder($"SELECT {SelectTerm} FROM {colToJoin[0].left.TableName} ");
            if (colToJoin[0].left.HasTableAlias)
            {
                bld.Append($"{colToJoin[0].left.TableAlias} ");
            }

            bld.Append($"JOIN {colToJoin[0].right.TableName} ");
            if (colToJoin[0].right.HasTableAlias)
            {
                bld.Append($"{colToJoin[0].right.TableAlias} ");
            }

            bld.Append($"ON {colToJoin[0].left.FQN.N} = {colToJoin[0].right.FQN.N} ");

            // weitere Joins?
            for(int i = 1; i < colToJoin.Length; i++)
            {
                // im nächsten Paar von Spaltennamen muss einer zu einer Tabelle gehören, 
                // die bereits im vorausgegangen Join eingesetzt wurde
                
                var leftIsMaster = colToJoin[i - 1].left.TableName == colToJoin[i].left.TableName
                                || colToJoin[i - 1].right.TableName == colToJoin[i].left.TableName;

                var rightIsMaster = colToJoin[i - 1].left.TableName == colToJoin[i].right.TableName
                                || colToJoin[i - 1].right.TableName == colToJoin[i].right.TableName;

                if (leftIsMaster)
                {
                    // zusätzlicher Join mit der rechten Tabelle
                    bld.Append($"JOIN {colToJoin[i].right.TableName} ");
                    if (colToJoin[i].right.HasTableAlias)
                    {
                        bld.Append($"{colToJoin[i].right.TableAlias} ");
                    }

                }
                else
                {
                    // zusätzlicher Join mit der linken Tabelle
                    bld.Append($"JOIN {colToJoin[i].left.TableName} ");
                    if (colToJoin[i].left.HasTableAlias)
                    {
                        bld.Append($"{colToJoin[i].left.TableAlias} ");
                    }
                }

                bld.Append($"ON {colToJoin[i].left.FQN.N} = {colToJoin[i].right.FQN.N} ");
            }

            return new WhereBuilder<TBo>(bld.ToString(), Mapper, _Evaluator, _Inspector);
        }

        /// <summary>
        /// mko, 17.12.2020
        /// 
        /// Komplexerere Join- Defininition. Die Join- Expression kann nun beliebig sein. Achtung: 
        /// die On- Expression wird nur auf Syntax geprüft, jedoch nicht darauf, dass die Tabellenspalten
        /// zu den Tabellen hinter dem Join gehören!
        /// </summary>
        /// <param name="Join"></param>
        /// <returns></returns>
        public WhereBuilder<TBo> JoinFrom(params (ITable left, ITable right, IColXpr JoinXpr)[] Join)
        {
            var bld = new StringBuilder($"SELECT {SelectTerm} FROM {Join[0].left.TableName} ");
            if (Join[0].left.HasAlias)
            {
                bld.Append($"{Join[0].left.Alias} ");
            }

            bld.Append($"JOIN {Join[0].right.TableName} ");
            if (Join[0].right.HasAlias)
            {
                bld.Append($"{Join[0].right.Alias} ");
            }

            var pe = _Inspector.Validate(Join[0].JoinXpr);
            Trc.ThrowArgExIfNot(pe.IsCurrentValid && pe.IsTreeValid, $"Invalid Join expression No. 0: {pe.Description}");

            var res = (NaLisp.Data.IConstValue<string>)_Evaluator.Eval(Join[0].JoinXpr);

            if (!string.IsNullOrWhiteSpace(res.Value))
            {
                bld.Append($"ON {res.Value} ");
            }
            else
            {
                Trc.ThrowArgEx($"Empty Join expression for Join No. 0");
            }

            // weitere Joins?
            for (int i = 1; i < Join.Length; i++)
            {
                // im nächsten Paar von Spaltennamen muss einer zu einer Tabelle gehören, 
                // die bereits im vorausgegangen Join eingesetzt wurde

                var leftIsMaster = Join[i - 1].left.TableName == Join[i].left.TableName
                                || Join[i - 1].right.TableName == Join[i].left.TableName;

                var rightIsMaster = Join[i - 1].left.TableName == Join[i].right.TableName
                                || Join[i - 1].right.TableName == Join[i].right.TableName;

                if (leftIsMaster)
                {
                    // zusätzlicher Join mit der rechten Tabelle
                    bld.Append($"JOIN {Join[i].right.TableName} ");
                    if (Join[i].right.HasAlias)
                    {
                        bld.Append($"{Join[i].right.Alias} ");
                    }

                }
                else
                {
                    // zusätzlicher Join mit der linken Tabelle
                    bld.Append($"JOIN {Join[i].left.TableName} ");
                    if (Join[i].left.HasAlias)
                    {
                        bld.Append($"{Join[i].left.Alias} ");
                    }
                }

                pe = _Inspector.Validate(Join[i].JoinXpr);
                Trc.ThrowArgExIfNot(pe.IsCurrentValid && pe.IsTreeValid, $"Invalid Join expression No. {i}: {pe.Description}");

                res = (NaLisp.Data.IConstValue<string>)_Evaluator.Eval(Join[i].JoinXpr);

                if (!string.IsNullOrWhiteSpace(res.Value))
                {
                    bld.Append($"ON {res.Value} ");
                }
                else
                {
                    Trc.ThrowArgEx($"Empty Join expression for Join No. {i}");
                }
            }

            return new WhereBuilder<TBo>(bld.ToString(), Mapper, _Evaluator, _Inspector);
        }


        /// <summary>
        /// mko, 19.11.2018
        /// Liefert alle Datensätze ohne Einschränkung
        /// 
        /// mko, 19.2.2020
        /// Alias Definition für Tabellennamen implementiert 
        /// </summary>
        /// <param name="tab"></param>
        /// <returns></returns>
        public OrderByBuilder<TBo> AllSortedFrom(params ITable[] tab)
        {
            var bld = new StringBuilder($"SELECT {SelectTerm} FROM {tab[0].TableName} ");
            if (tab[0].HasAlias)
            {
                bld.Append(tab[0].Alias);
            }


            if (tab.Length > 1)
            {
                foreach (var tb in tab.Skip(1))
                {
                    bld.Append($",{tb.TableName} ");
                    if (tb.HasAlias)
                    {
                        bld.Append(tb.Alias);
                    }
                }
            }

            return new OrderByBuilder <TBo>(bld.ToString(), Mapper);
        }



        /// <summary>
        /// mko, 2.10.2018
        /// Left outer join implementiert, um beim Abruf von DocInfos zu einem vorhandenen
        /// Dokument aus der Path- Tabelle einen nicht leeren Join mit der Projektliste2 zu
        /// garantieren.
        /// </summary>
        /// <param name="LeftTab"></param>
        /// <param name="LeftTabKey"></param>
        /// <param name="RightTab"></param>
        /// <param name="RightTabKey"></param>
        /// <returns></returns>
        public WhereBuilder<TBo> LeftOuterJoin(ITable LeftTab, IColXpr LeftTabKey, ITable RightTab, IColXpr RightTabKey)
        {
            var from = $"SELECT {SelectTerm} FROM {LeftTab.TableName}  LEFT JOIN {RightTab.TableName} ON {LeftTabKey.Value} = {RightTabKey.Value} ";

            return new WhereBuilder<TBo>(from, Mapper, _Evaluator, _Inspector);
        }

    }
}
