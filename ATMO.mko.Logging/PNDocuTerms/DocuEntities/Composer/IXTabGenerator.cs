using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// mko, 22.7.2020
/// Hilfmittel zur Erzeugung von xTabs
/// </summary>
namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{

    public interface IXTabGenerator
    {
        IXTabGeneratorDefCols XTab();
    }


    public interface IXTabGeneratorDefCols
    {
        IXTabGeneratorDefCols defCol(string colId, string ColDescription);
        IXTabGeneratorDefCols defCol(string ColId, long nidColDescription);
        IXTabGeneratorDefCols defCol(long  nidColId, string ColDescription);
        IXTabGeneratorDefCols defCol(long nidColId, long nidColDescription);

        IXTabGeneratorDefRows Rows();
    }

    public interface IXTabGeneratorDefRows
    {
        IXTabGeneratorDefRows defRow(string rowId, string rowDescription);
        IXTabGeneratorDefRows defRow(string rowId, long nidRowDescription);
        IXTabGeneratorDefRows defRow(long nidRowId, string RowDescription);
        IXTabGeneratorDefRows defRow(long nidRowId, long nidRowDescription);

        IXTabGeneratorDefValues Values();

    }

    public interface IXTabGeneratorDefValues
    {
        IXTabGeneratorDefValues defVal(string ColId, string RowId, IPropertyValue value);
        IXTabGeneratorDefValues defVal(string ColId, long nidRowId, IPropertyValue value);
        IXTabGeneratorDefValues defVal(long nidColId, string RowId, IPropertyValue value);
        IXTabGeneratorDefValues defVal(long nidColId, long nidRowId, IPropertyValue value);
        IInstance create();
    }
}
