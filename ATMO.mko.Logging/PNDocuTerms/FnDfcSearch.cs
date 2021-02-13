using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms
{
    /// <summary>
    /// mko, 25.6.2019
    /// 
    /// mko, 8.6.2020
    /// Prefixe für Boolean, Int, Double und MKPRG.Naming NID's hinzugefügt
    /// </summary>
    public class FnDfcSearch : IFn
    {
        public string Instance => $"{NamePrefix}i";

        public string Method => $"{NamePrefix}m";

        public string Function => $"{NamePrefix}f";

        public string Return => $"{NamePrefix}r";

        public string Property => $"{NamePrefix}p";

        public string PropertySet => $"{NamePrefix}pset";

        public string Version => $"{NamePrefix}v";

        public string Event => $"{NamePrefix}e";

        public string Date => $"{NamePrefix}d";

        public string Time => $"{NamePrefix}t";

        public string List => "(";

        public string Txt => "$(";

        /// <summary>
        /// mko.RPN Tokenize bool
        /// </summary>
        public string constBool => "";

        /// <summary>
        /// mko.RPN Tokenize int
        /// </summary>
        public string constInt => "";

        /// <summary>
        /// mko.RPN Tokenize int
        /// </summary>
        public string constDbl => "";

        /// <summary>
        /// mko.RPN Tokenize int
        /// </summary>
        public string constStr => "";

        public string ListEnd => ")";

        public string NamePrefix => "#";

        public string ParamNamePrefix => "";

        public string DerivedTokenPrefix => "";

        /// <summary>
        /// Präfix für boolsche Werte
        /// </summary>
        public string Bool => ""; // $"{NamePrefix}B";

        /// <summary>
        /// Präfix für Integer- Werte
        /// </summary>
        public string Int => ""; // $"{NamePrefix}I";

        /// <summary>
        /// Präfix für doppelt genaue Gleitkommawerte
        /// </summary>
        public string Dbl => "";// $"{NamePrefix}D";

        /// <summary>
        /// Präfix für Naming-Ids
        /// </summary>
        public string Nid => $"{NamePrefix}nid";

        public string PropertyWildCard => $"{NamePrefix}x";

        public bool IsSemanticDescriptor(string FunctionName)
        {
            throw new NotImplementedException();
        }
    }
}
