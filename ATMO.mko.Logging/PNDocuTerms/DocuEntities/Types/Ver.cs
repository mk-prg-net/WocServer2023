using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms.DocuEntities
{
    /// <summary>
    /// mko, 16.6.2020
    /// </summary>
    public class Ver
        : DocuEntity,
        IVer
    {
        public Ver(IFormater fmt, String verString)
            : base(fmt, DocuEntityTypes.Version, verString)
        {

        }

        public string VersionString => ((String)Childs.First()).Value;
    }
}
