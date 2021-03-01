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
    /// mko, 28.11.2018
    /// Zum einfachen Parametrieren von From- Klauseln in Abfragen geschaffen.
    /// </summary>
    public interface ITable : INaLisp
    {
        string TableName { get; }

        bool HasAlias { get; }
        string Alias { get; }
    }


    /// <summary>
    /// mko, 23.1.2018
    /// 
    /// mko, 19.2.2020
    /// Konstruktor ohne Aliasname gelöscht. Konstruktor mit Aliasname hat jetzt 
    /// für Aliasname den Default- Wert null
    /// </summary>
    public class Table : NaLisp.Core.NaLispTerminal, ITable
    {

        public Table(string TableName, string AliasName = null)
        {
            if (!string.IsNullOrWhiteSpace(AliasName))
            {
                this.TableName = TableName;
                this.Alias = AliasName;
                HasAlias = true;
            }else
            {
                this.TableName = TableName;
                HasAlias = false;
            }
        }

        public string TableName { get; }

        public bool HasAlias { get; }
        public string Alias { get; }

        public override INaLisp Clone(bool deep = true)
        {
            if (HasAlias)
            {
                return new Table(TableName, Alias);
            } else
            {
                return new Table(TableName);
            }
        }

        public override INaLisp Eval(NaLispStack StackInstance, bool DebugOn)
        {
            if (HasAlias)
            {
                return NaLisp.Factories.Txt._.Create($"{TableName} as {Alias}");
            } else
            {
                return NaLisp.Factories.Txt._.Create(TableName);
            }
        }

        public override Inspector.ProtocolEntry Validate(NaLispStack Stack)
        {
            return new Inspector.ProtocolEntry(this, true, true, typeof(NaLisp.Data.IConstValue<string>));
        }
    }
}
