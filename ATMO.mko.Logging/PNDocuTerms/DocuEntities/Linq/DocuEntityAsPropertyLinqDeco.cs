using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static ATMO.mko.Logging.PNDocuTerms.DocuEntities.ComposerSubTrees;
using ANC = MKPRG.Naming;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 22.11.2018
    /// Enhanced a DocuEntity with EntityType Property with special accessors to property value
    /// </summary>
    public class DocuEntityAsPropertyLinqDeco : DocuEntityWithNameLinqDeco
    {
        public DocuEntityAsPropertyLinqDeco(IDocuEntity entity)
            : base(entity)
        {
            Debug.Assert(entity.EntityType == DocuEntityTypes.Property);
        }


        public string PropValueAsString
        {
            get
            {
                return DocuEntityHlp.EntityValue(this).GetText();
            }
        }

        public int PropValueAsInt
        {
            get
            {
                TraceHlp.ThrowArgExIfNot(DocuEntityHlp.EntityValue(this) is Integer, "Integer erwartet");
                return ((Integer)DocuEntityHlp.EntityValue(this)).ValueAsInteger;
                //return  int.Parse(DocuEntityHlp.EntityValue(this).GetText());
            }
        }

        public long PropValueAsLong
        {
            get
            {
                // mko, 25.3.2019
                // Das Suffix L entfernt, da sonst eine Format- Exception geworfen wird
                return ((Integer)DocuEntityHlp.EntityValue(this)).ValueAsLong;
            }
        }

        public double PropValueAsDouble
        {
            get
            {
                TraceHlp.ThrowArgExIfNot(DocuEntityHlp.EntityValue(this) is Double, "Integer erwartet");
                return ((Double)DocuEntityHlp.EntityValue(this)).Value;

                //return double.Parse(DocuEntityHlp.EntityValue(this).GetText());
            }
        }

        public DateTime PropValueAsDateTime
        {
            get
            {
                TraceHlp.ThrowArgExIfNot(DocuEntityHlp.EntityValue(this) is IDate, "Date erwartet");
                var dat = (IDate)DocuEntityHlp.EntityValue(this);

                return new DateTime(dat.Year, dat.Month, dat.Day);

                //return DateTime.Parse(DocuEntityHlp.EntityValue(this).GetText());
            }
        }

    }
}
