using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ANC = MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 22.10.2020;
    /// </summary>
    public class XTabGenerator
        : IXTabGeneratorDefValues
    {
        IComposer pnL;
        IDTList Cols;
        IDTList Rows;

        List<IInstance> allVals = new List<IInstance>();        

        public XTabGenerator(IComposer pnL, IDTList Cols, IDTList Rows)
        {
            this.pnL = pnL;
            this.Cols = Cols;
            this.Rows = Rows;
        }

        public IXTabGeneratorDefValues defVal(string ColId, string RowId, IPropertyValue value)
        {
            allVals.Add(pnL.i(ColId, pnL.p(RowId, value)));
            return this;
        }

        public IXTabGeneratorDefValues defVal(string ColId, long nidRowId, IPropertyValue value)
        {
            allVals.Add(pnL.i(ColId, pnL.p(nidRowId, value)));
            return this;
        }

        public IXTabGeneratorDefValues defVal(long nidColId, string RowId, IPropertyValue value)
        {
            allVals.Add(pnL.i(nidColId, pnL.p(RowId, value)));
            return this;
        }

        public IXTabGeneratorDefValues defVal(long nidColId, long nidRowId, IPropertyValue value)
        {
            allVals.Add(pnL.i(nidColId, pnL.p(nidRowId, value)));
            return this;
        }

        public IInstance create()
        {
            var cols = allVals.GroupBy(i => i.Name());

            var colRows = new List<IInstance>();

            foreach(var colGrp in cols)
            {
                if(colGrp.First().Childs.First() is NID nid)
                {
                    colRows.Add(pnL.i(nid.NamingId, pnL.EmbedMembers(colGrp.Select(i => i.InstanceMembers.First()).ToArray())));
                }
                else
                {                    
                    colRows.Add(pnL.i(colGrp.Key, pnL.EmbedMembers(colGrp.Select(i => i.InstanceMembers.First()).ToArray())));
                }                
            }

            return pnL.i(ANC.DocuTerms.Formatting.XTab.XTab.UID,
                            pnL.p(ANC.DocuTerms.Formatting.XTab.Dim1.UID, Cols),
                            pnL.p(ANC.DocuTerms.Formatting.XTab.Dim2.UID, Rows),
                            pnL.p(ANC.DocuTerms.Formatting.XTab.Values.UID,
                                pnL.List(
                                        pnL.EmbedMembers(colRows.ToArray())
                                    )));
        }
    }
}
