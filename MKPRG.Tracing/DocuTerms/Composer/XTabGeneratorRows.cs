using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 22.7.2020
    /// </summary>
    public class XTabGeneratorRows
        : IXTabGeneratorDefRows
    {
        IComposer pnL;
        IDTList Cols;

        List<IProperty> rows = new List<IProperty>();

        public XTabGeneratorRows(IComposer pnL, IDTList Cols)
        {
            this.pnL = pnL;
            this.Cols = Cols;
        }

        public IXTabGeneratorDefRows defRow(string rowId, string rowDescription)
        {
            rows.Add(pnL.p(rowId, rowDescription));
            return this;
        }

        public IXTabGeneratorDefRows defRow(string rowId, long nidRowDescription)
        {
            rows.Add(pnL.p(rowId, pnL.NID(nidRowDescription)));
            return this;
        }

        public IXTabGeneratorDefRows defRow(long nidRowId, string RowDescription)
        {
            rows.Add(pnL.p(nidRowId, RowDescription));
            return this;
        }

        public IXTabGeneratorDefRows defRow(long nidRowId, long nidRowDescription)
        {
            rows.Add(pnL.p_NID(nidRowId, nidRowDescription));
            return this;
        }

        public IXTabGeneratorDefValues Values()
        {
            return new XTabGenerator(pnL, Cols, pnL.List(pnL.EmbedListMembers(rows.ToArray())));
        }
    }
}
