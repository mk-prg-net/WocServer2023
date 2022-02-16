using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms
{
    public class XTabGeneratorCols
        : IXTabGeneratorDefCols
    {
        IComposer pnL;

        List<IProperty> cols = new List<IProperty>();

        public XTabGeneratorCols(IComposer pnL)
        {
            this.pnL = pnL;
        }

        public IXTabGeneratorDefCols defCol(string colId, string ColDescription)
        {
            cols.Add(pnL.p(colId, ColDescription));
            return this;
        }

        public IXTabGeneratorDefCols defCol(string ColId, long nidColDescription)
        {
            cols.Add(pnL.p(ColId, pnL.NID(nidColDescription)));
            return this;
        }

        public IXTabGeneratorDefCols defCol(long nidColId, string ColDescription)
        {
            cols.Add(pnL.p(nidColId, ColDescription));
            return this;
        }

        public IXTabGeneratorDefCols defCol(long nidColId, long nidColDescription)
        {
            cols.Add(pnL.p_NID(nidColId, nidColDescription));
            return this;
        }

        public IXTabGeneratorDefRows Rows()
        {
            return new XTabGeneratorRows(pnL, pnL.List(pnL.EmbedListMembers(cols.ToArray())));
        }
    }
}
