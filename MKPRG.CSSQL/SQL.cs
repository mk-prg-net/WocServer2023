using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;
using Trc = mko.TraceHlp;


namespace MKPRG.CSSQL
{

    public static class SQL
    {
        public enum Dialect
        {
            Oracle,
            MSSql
        }
    }

    /// <summary>
    /// mko, 22.1.2018
    /// Class factories for SQL- Expressions
    /// 
    /// mko, 31.10.2018
    /// Statischen Evaluator Evaluator._ ersetzt durch private Instanz
    /// </summary>
    public class SQL<TBo>
    {        

        readonly SQL.Dialect dialect = SQL.Dialect.Oracle;

        /// <summary>
        /// mko, 31.1.2020
        /// Mittels dialect- Schalter kann jetzt die Generierung für bestimmte 
        /// Sql- Dialekte gewählt werden (aktuell Oracle oder Microsoft Sql Server)
        /// </summary>
        /// <param name="dialect"></param>
        public SQL(SQL.Dialect dialect = SQL.Dialect.Oracle)
        {
            this.dialect = dialect;
        }

        Evaluator _Evaluator = new Evaluator();
        Inspector _Inspector = new Inspector();

        public CMap<TBo> Map(IColXpr colXpr, Action<TBo, object> ProxyPropSetter)
        {
            return new CMap<TBo>(colXpr, ProxyPropSetter);
        }

        ///// <summary>
        ///// mko, 28.11.2018
        ///// Bedingtes Mapping. 
        ///// </summary>
        ///// <param name="condition"></param>
        ///// <param name="colXpr"></param>
        ///// <param name="ProxyPropSetter"></param>
        ///// <returns></returns>
        //public CMap<TBo> MapIf(bool condition, IColXpr colXpr, Action<TBo, object> ProxyPropSetter)
        //{
        //    if (condition)
        //    {
        //        return new CMap<TBo>(colXpr, ProxyPropSetter);
        //    } else
        //    {
        //        return new CMap<TBo>(Nop(), (bo, v) => { });
        //    }            
        //}

        /// <summary>
        /// mko, 28.11.2018
        /// Bedingtes Mapping. 
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="colXpr"></param>
        /// <param name="ProxyPropSetter"></param>
        /// <returns></returns>
        public CMap<TBo> MapIf(bool condition, IColXpr colXpr, Action<TBo, object> ProxyPropSetter, Action<TBo> DefaultSetter)
        {
            return new CMap<TBo>(condition ? colXpr : Nop(), ProxyPropSetter, DefaultSetter);

        }


        /// <summary>
        /// SELECT ...
        /// 
        /// Creates a Select NaLispExpression
        /// </summary>
        /// <param name="cmap"></param>
        /// <returns></returns>
        public FromBuilder<TBo> Select(params CMap<TBo>[] cmap)
        {
            // Exctract column expressions from mappings
            var ColumnExpressions = cmap.Select(r => r.ColXpr).ToArray();
            var selectExpr = new Select(ColumnExpressions);
            var pe = _Inspector.Validate(selectExpr);

            Trc.ThrowArgExIfNot(pe.IsCurrentValid && pe.IsTreeValid, $"Invalid select expression: {pe.Description}");

            var res = (NaLisp.Data.IConstValue<string>)_Evaluator.Eval(selectExpr);

            // Build record to proxy mapper
            var mapper = new RecordToBoMapper<TBo>(cmap);

            return new FromBuilder<TBo>(res.Value, mapper, _Evaluator, _Inspector);
        }

        /// <summary>
        /// UPDATE tab SET ...
        /// 
        /// mko, 19.6.2018
        /// Creates a Update NaLisp Expression
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="setXprs"></param>
        /// <returns></returns>
        public WhereBuilder<TBo> Update(Table tab, params SetXpr[] setXprs)
        {
            // Exctract column expressions from mappings            
            var updateExpr = new Update(tab, setXprs);
            var pe = _Inspector.Validate(updateExpr);

            Trc.ThrowArgExIfNot(pe.IsCurrentValid && pe.IsTreeValid, $"Invalid update expression: {pe.Description}");

            var res = (NaLisp.Data.IConstValue<string>)_Evaluator.Eval(updateExpr);

            return new WhereBuilder<TBo>(res.Value, _Evaluator, _Inspector);
        }

        /// <summary>
        /// INSERT INTO tab ( col1, ..., colN) VALUES(...)
        /// 
        /// mko, 19.6.2018
        /// Creates a insert NaLisp Expression
        /// </summary>
        /// <param name="tab"></param>
        /// <param name="newValuesXprs"></param>
        /// <returns></returns>
        public QueryBuilderResult<TBo> Insert(Table tab, params NewValueXpr[] newValuesXprs)
        {
            var insertExpr = new Insert(tab, newValuesXprs);
            var pe = _Inspector.Validate(insertExpr);

            Trc.ThrowArgExIfNot(pe.IsCurrentValid && pe.IsTreeValid, $"Invalid insert expression: {pe.Description}");

            var res = (NaLisp.Data.IConstValue<string>)_Evaluator.Eval(insertExpr);

            return new QueryBuilderResult<TBo>(res.Value);
        }

        /// <summary>
        /// DELETE FROM tab WHERE ...
        /// 
        /// mko, 11.2.2020
        /// Löscht Datensätze in der mittels From zu definierenden Tabelle.
        /// Dei zu löschenden Datensätze können mittels where eingeschränkt werden.
        /// </summary>
        /// <returns></returns>
        public Delete<TBo> Delete()
        {
            return new Delete<TBo>(this._Evaluator, this._Inspector);
        }

        /// <summary>
        /// TRUNCATE FROM tab
        /// 
        /// mko, 11.2.2020
        /// Bitte mit äußerster Vorsicht einsetzen!!!
        /// Löscht alle Datensätze unwiderruflich, ohne dabei Integritätsregeln zu beachten. Ein Rollback eines
        /// Truncate- Befehls ist nicht möglich.
        /// </summary>
        /// <returns></returns>
        public TruncateTable<TBo> Truncate()
        {
            return new TruncateTable<TBo>();
        }

        /// <summary>
        /// mko, 29.10.2018
        /// Creates a No Operation Function(). Expressiontree will not be modified by this function.
        /// </summary>
        /// <returns></returns>
        public Nop Nop()
        {
            return new Nop();
        }

        // Classfactories for constants
        public IColXpr Int(int i)
        {
            if(dialect == SQL.Dialect.Oracle)
            {
                return new Constant(i);
            }
            else
            {
                return new ConstantMSSql(i);
            }            
        }

        public IColXpr Long(long i)
        {
            if(dialect == SQL.Dialect.Oracle)
            {
                return new Constant(i);
            }
            else
            {
                return new ConstantMSSql(i);
            }            
        }

        public IColXpr Dbl(double d)
        {
            if(dialect == SQL.Dialect.Oracle)
            {
                return new Constant(d);
            }
            else
            {
                return new ConstantMSSql(d);
            }            
        }

        public IColXpr Bool(bool b)
        {
            if (dialect == SQL.Dialect.Oracle)
            {
                return new Constant(b);
            }
            else
            {
                return new ConstantMSSql(b);
            }
        }

        public IColXpr Txt(string txt)
        {
            if (dialect == SQL.Dialect.Oracle)
            {
                return new Constant(txt);
            }
            else
            {
                return new ConstantMSSql(txt);
            }
        }

        /// <summary>
        /// mko, 19.6.2018
        /// Creates a Oracle conform DateTime string
        /// </summary>
        /// <param name="date"></param>
        /// <returns></returns>
        public IColXpr Date(DateTime date)
        {
            if (dialect == SQL.Dialect.Oracle)
            {
                return new Constant(date);
            }
            else
            {
                return new ConstantMSSql(date);
            }

        }


        // Class factories for expressions

        /// <summary>
        /// Embraces a term with parentheses
        /// a -: (a)
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public PXpr P(IColXpr a)
        {
            return new PXpr(a);
        }

        /// <summary>
        /// mko, 19.6.2018
        /// Creates a Update Set- Expression
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public SetXpr Set(IColXpr a, IColXpr b)
        {
            return new SetXpr(a, b);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a Update Set- Expression
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public SetXpr Set(IColXpr a, string b)
        {
            return new SetXpr(a, Txt(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a Update Set- Expression
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public SetXpr Set(IColXpr a, int b)
        {
            return new SetXpr(a, Int(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a Update Set- Expression
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public SetXpr Set(IColXpr a, long b)
        {
            return new SetXpr(a, Long(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a Update Set- Expression
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public SetXpr Set(IColXpr a, double b)
        {
            return new SetXpr(a, Dbl(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a Update Set- Expression
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public SetXpr Set(IColXpr a, bool b)
        {
            return new SetXpr(a, Bool(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a Update Set- Expression
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public SetXpr Set(IColXpr a, DateTime b)
        {
            return new SetXpr(a, Date(b));
        }



        /// <summary>
        /// mko, 19.6.2018
        /// Creates a new Value Expression for sql Insert commands
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NewValueXpr NewVal(IColXpr a, IColXpr b)
        {
            return new NewValueXpr(a, b);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a new Value Expression for sql Insert commands
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NewValueXpr NewVal(IColXpr a, string b)
        {
            return new NewValueXpr(a, Txt(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a new Value Expression for sql Insert commands
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NewValueXpr NewVal(IColXpr a, int b)
        {
            return new NewValueXpr(a, Int(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a new Value Expression for sql Insert commands
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NewValueXpr NewVal(IColXpr a, long b)
        {
            return new NewValueXpr(a, Long(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a new Value Expression for sql Insert commands
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NewValueXpr NewVal(IColXpr a, bool b)
        {
            return new NewValueXpr(a, Bool(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Creates a new Value Expression for sql Insert commands
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NewValueXpr NewVal(IColXpr a, DateTime b)
        {
            return new NewValueXpr(a, Date(b));
        }


        /// <summary>
        /// Oracle sql equality operator
        /// a = b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public EqXpr Eq(IColXpr a, IColXpr b)
        {
            return new EqXpr(a, b);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql equality operator
        /// a = b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public EqXpr Eq(IColXpr a, string b)
        {
            return new EqXpr(a, Txt(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql equality operator
        /// a = b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public EqXpr Eq(IColXpr a, int b)
        {
            return new EqXpr(a, Int(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql equality operator
        /// a = b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public EqXpr Eq(IColXpr a, long b)
        {
            return new EqXpr(a, Long(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql equality operator
        /// a = b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public EqXpr Eq(IColXpr a, bool b)
        {
            return new EqXpr(a, Bool(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql equality operator
        /// a = b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public EqXpr Eq(IColXpr a, DateTime b)
        {
            return new EqXpr(a, Date(b));
        }


        /// <summary>
        /// Oracle sql unequality operator
        /// a != b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NotEqXpr NotEq(IColXpr a, IColXpr b)
        {
            return new NotEqXpr(a, b);
        }

        /// <summary>
        /// 2.12.2020
        /// Oracle sql unequality operator
        /// a != b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NotEqXpr NotEq(IColXpr a, string b)
        {
            return new NotEqXpr(a, Txt(b));
        }

        /// <summary>
        /// 2.12.2020
        /// Oracle sql unequality operator
        /// a != b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NotEqXpr NotEq(IColXpr a, int b)
        {
            return new NotEqXpr(a, Int(b));
        }

        /// <summary>
        /// 2.12.2020
        /// Oracle sql unequality operator
        /// a != b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NotEqXpr NotEq(IColXpr a, long b)
        {
            return new NotEqXpr(a, Long(b));
        }

        /// <summary>
        /// 2.12.2020
        /// Oracle sql unequality operator
        /// a != b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NotEqXpr NotEq(IColXpr a, bool b)
        {
            return new NotEqXpr(a, Bool(b));
        }

        /// <summary>
        /// 2.12.2020
        /// Oracle sql unequality operator
        /// a != b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public NotEqXpr NotEq(IColXpr a, DateTime b)
        {
            return new NotEqXpr(a, Date(b));
        }



        /// <summary>
        /// Oracle sql and operator
        /// a and b
        /// 
        /// mko, 2.8.2018
        /// parameterlist extended into a variadic one
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public AndXpr And(params IColXpr[] a)
        {
            return new AndXpr(a);
        }


        /// <summary>
        /// Oracel sql or operator 
        /// a or b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public OrXpr Or(params IColXpr[] a)
        {
            return new OrXpr(a);
        }

        /// <summary>
        /// Oracle sql not operator
        /// NOT a
        /// </summary>
        /// <param name="a"></param>
        /// <returns></returns>
        public NotXpr Not(IColXpr a)
        {
            return new NotXpr(a);
        }

        /// <summary>
        /// Oracle sql lower then operator
        /// a &lt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LtXpr Lt(IColXpr a, IColXpr b)
        {
            return new LtXpr(a, b);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql lower then operator
        /// a &lt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LtXpr Lt(IColXpr a, int b)
        {
            return new LtXpr(a, Int(b));
        }


        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql lower then operator
        /// a &lt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LtXpr Lt(IColXpr a, long b)
        {
            return new LtXpr(a, Long(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql lower then operator
        /// a &lt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LtXpr Lt(IColXpr a, double b)
        {
            return new LtXpr(a, Dbl(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql lower then operator
        /// a &lt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LtXpr Lt(IColXpr a, DateTime b)
        {
            return new LtXpr(a, Date(b));
        }



        /// <summary>
        /// Oracel sql lower equal operator 
        /// a &lt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LeXpr Le(IColXpr a, IColXpr b)
        {
            return new LeXpr(a, b);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracel sql lower equal operator 
        /// a &lt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LeXpr Le(IColXpr a, int b)
        {
            return new LeXpr(a, Int(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracel sql lower equal operator 
        /// a &lt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LeXpr Le(IColXpr a, long b)
        {
            return new LeXpr(a, Long(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracel sql lower equal operator 
        /// a &lt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LeXpr Le(IColXpr a, double b)
        {
            return new LeXpr(a, Dbl(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracel sql lower equal operator 
        /// a &lt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public LeXpr Le(IColXpr a, DateTime b)
        {
            return new LeXpr(a, Date(b));
        }


        /// <summary>
        /// Oracle sql greater oprerator
        /// a &gt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GtXpr Gt(IColXpr a, IColXpr b)
        {
            return new GtXpr(a, b);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql greater oprerator
        /// a &gt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GtXpr Gt(IColXpr a, int b)
        {
            return new GtXpr(a, Int(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql greater oprerator
        /// a &gt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GtXpr Gt(IColXpr a, long b)
        {
            return new GtXpr(a, Long(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql greater oprerator
        /// a &gt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GtXpr Gt(IColXpr a, double b)
        {
            return new GtXpr(a, Dbl(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql greater oprerator
        /// a &gt; b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GtXpr Gt(IColXpr a, DateTime b)
        {
            return new GtXpr(a, Date(b));
        }


        /// <summary>
        /// Oracle sql greater equal operator 
        /// a &gt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GeXpr Ge(IColXpr a, IColXpr b)
        {
            return new GeXpr(a, b);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql greater equal operator 
        /// a &gt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GeXpr Ge(IColXpr a, int b)
        {
            return new GeXpr(a, Int(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql greater equal operator 
        /// a &gt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GeXpr Ge(IColXpr a, long b)
        {
            return new GeXpr(a, Long(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql greater equal operator 
        /// a &gt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GeXpr Ge(IColXpr a, double b)
        {
            return new GeXpr(a, Dbl(b));
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Oracle sql greater equal operator 
        /// a &gt;= b
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public GeXpr Ge(IColXpr a, DateTime b)
        {
            return new GeXpr(a, Date(b));
        }


        public InXpr In(IColXpr v, params IColXpr[] set)
        {
            return new InXpr(v, set);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// </summary>
        /// <param name="v"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public InXpr In(IColXpr v, params string[] set)
        {
            return new InXpr(v, set.Select(r => Txt(r)).ToArray());
        }


        /// <summary>
        /// mko, 2.12.2020
        /// </summary>
        /// <param name="v"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public InXpr In(IColXpr v, params int[] set)
        {
            return new InXpr(v, set.Select(r => Int(r)).ToArray());
        }

        /// <summary>
        /// mko, 2.12.2020
        /// </summary>
        /// <param name="v"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public InXpr In(IColXpr v, params long[] set)
        {
            return new InXpr(v, set.Select(r => Long(r)).ToArray());
        }


        public IsNotNullNorEmptyXpr IsNotNullNorEmpty(IColXpr col)
        {
            return new IsNotNullNorEmptyXpr(col);
        }


        public IsNotNullXpr IsNotNull(IColXpr col)
        {
            return new IsNotNullXpr(col);
        }


        public IsNullOrEmptyXpr IsNullOrEmpty(IColXpr col)
        {
            return new IsNullOrEmptyXpr(col);
        }

        public IsNullXpr IsNull(IColXpr col)
        {
            return new IsNullXpr(col);
        }

        /// <summary>
        /// Determines weather two strings are identical
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public StrEqXpr StrEq(IColXpr a, IColXpr b)
        {
            return new StrEqXpr(a, b);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Determines weather two strings are identical
        /// </summary>
        /// <param name="a"></param>
        /// <param name="b"></param>
        /// <returns></returns>
        public StrEqXpr StrEq(IColXpr a, string b)
        {
            return new StrEqXpr(a, Txt(b));
        }


        public NotStrEqXpr NotStrEq(IColXpr a, IColXpr b)
        {
            return new NotStrEqXpr(a, b);
        }

        public NotStrEqXpr NotStrEq(IColXpr a, string b)
        {
            return new NotStrEqXpr(a, Txt(b));
        }

        /// <summary>
        /// Identifies words whose structure follows a simple pattern
        /// </summary>
        /// <param name="a"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public LikeXpr Like(IColXpr a, IColXpr pattern)
        {
            return new LikeXpr(a, pattern);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Identifies words whose structure follows a simple pattern
        /// </summary>
        /// <param name="a"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public LikeXpr Like(IColXpr a, string pattern)
        {
            return new LikeXpr(a, Txt(pattern));
        }


        /// <summary>
        /// mko, 27.9.2018
        /// Identifies words whose structure follows a simple pattern. 
        /// Case insensitive version.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public LikeLowerCaseXpr LikeLowerCase(IColXpr a, IColXpr pattern)
        {
            return new LikeLowerCaseXpr(a, pattern);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// Identifies words whose structure follows a simple pattern. 
        /// Case insensitive version.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public LikeLowerCaseXpr LikeLowerCase(IColXpr a, string pattern)
        {
            return new LikeLowerCaseXpr(a, Txt(pattern));
        }

        /// <summary>
        /// mko, 6.4.2020
        /// Vergleicht die Werte einer Spalte mit einem String. Spaltenwerte und String werden 
        /// zuvor in Großschreibung konvertiert. Damit ist der Vergleich Case- insensitive.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public LikeUpperCaseXpr LikeUpperCase(IColXpr a, IColXpr pattern)
        {
            return new LikeUpperCaseXpr(a, pattern);
        }

        /// <summary>
        /// mko, 2.12.2020
        /// 
        /// Vergleicht die Werte einer Spalte mit einem String. Spaltenwerte und String werden 
        /// zuvor in Großschreibung konvertiert. Damit ist der Vergleich Case- insensitive.
        /// </summary>
        /// <param name="a"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public LikeUpperCaseXpr LikeUpperCase(IColXpr a, string pattern)
        {
            return new LikeUpperCaseXpr(a, Txt(pattern));
        }

        /// <summary>
        /// Identifies words whose structure follows a complex regular expression
        /// </summary>
        /// <param name="col"></param>
        /// <param name="regPattern"></param>
        /// <returns></returns>
        public RegExLikeXpr RegLike(IColXpr col, IColXpr regPattern)
        {
            return new RegExLikeXpr(col, regPattern);
        }

        /// <summary>
        /// Identifies words whose structure follows a complex regular expression
        /// </summary>
        /// <param name="col"></param>
        /// <param name="regPattern"></param>
        /// <returns></returns>
        public RegExLikeXpr RegLike(IColXpr col, string regPattern)
        {
            return new RegExLikeXpr(col, Txt(regPattern));
        }


        /// <summary>
        /// Defines a aggregat sum function for col
        /// </summary>
        /// <param name="col"></param>
        /// <returns></returns>
        public SumXpr Sum(IColXpr col)
        {
            return new SumXpr(col);
        }

        public AvgXpr Avg(IColXpr col)
        {
            return new AvgXpr(col);
        }

        public CountAllXpr CountAll()
        {
            return new CountAllXpr();
        }

        public CountXpr Count(IColXpr col)
        {
            return new CountXpr(col);
        }

        public MaxXpr Max(IColXpr col)
        {
            return new MaxXpr(col);
        }

        public MinXpr Min(IColXpr col)
        {
            return new MinXpr(col);
        }

    }
}
