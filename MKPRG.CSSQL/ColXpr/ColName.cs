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
    /// mko, 22.1.2018
    /// </summary>
    public class ColName : NaLisp.Core.NaLispTerminal, IColXpr
    {
        /// <summary>
        /// mko, 22.1.2018
        /// 
        /// mko, 19.2.2020
        /// HasTableAlias hinzugefügt. Achtung: Im Zusammenhang mit dem neuen Konstruktor ist das Verhalten jetzt deutlich komplexer!!!
        /// </summary>
        /// <param name="TableName"></param>
        /// <param name="ColumnName"></param>
        public ColName(string TableName, string ColumnName)
        {
            Value = N = ColumnName;
            this.TableName = TableName;
            this.HasTableAlias = false;
        }

        /// <summary>
        /// mko, 19.2.2020
        /// </summary>
        /// <param name="table"></param>
        /// <param name="ColumnName"></param>
        public ColName(Table table, string ColumnName)
        {
            Value = N = ColumnName;
            this.TableName = table.TableName;
            this.HasTableAlias = table.HasAlias;
            this.TableAlias = table.Alias;
        }

        /// <summary>
        /// mko, 19.2.2020
        /// </summary>
        /// <param name="Tablename"></param>
        /// <param name="HasAlias"></param>
        /// <param name="TableAlias"></param>
        /// <param name="ColumnName"></param>
        public ColName(string TableName, bool HasAlias, string TableAlias, string ColumnName)
        {
            Value = N = ColumnName;
            this.TableName = TableName;
            this.HasTableAlias = HasAlias;
            this.TableAlias = TableAlias;            
        }

        /// <summary>
        /// mko, 19.2.2020
        /// </summary>
        public bool HasTableAlias { get; }

        /// <summary>
        /// mko, 19.2.2020
        /// </summary>
        public string TableAlias { get; }

        /// <summary>
        /// Column name
        /// </summary>
        public string N { get; }

        /// <summary>
        /// Full qualified column name
        /// </summary>
        public ColName FQN
        {
            get
            {
                return new ColName(TableName, HasTableAlias, TableAlias, $"{(HasTableAlias ? TableAlias : TableName)}.{N}");
            }
        }

        public string TableName { get; }

        public string Value { get; }


        public ColName Alias(string Aliasname)
        {
            return new ColName(TableName, HasTableAlias, TableAlias, $"{N} as {Aliasname}");
        }

        public override INaLisp Clone(bool deep = true)
        {
            return new ColName(TableName, HasTableAlias, TableAlias, N);
        }

        public override INaLisp Eval(NaLispStack StackInstance, bool DebugOn)
        {
            return NaLisp.Factories.Txt._.Create(Value);
        }

        public override Inspector.ProtocolEntry Validate(NaLispStack Stack)
        {
            return new Inspector.ProtocolEntry(this, true, true, typeof(ColName));
        }

        public override string ToString()
        {
            return Value;
        }
    }
}
