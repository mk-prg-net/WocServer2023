using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    /// <summary>
    /// mko, 5.5.2021
    /// 
    /// Wird von Naming- Containern implementiert, die eine Pluralform darstellen
    /// </summary>
    public interface IPluralForm
        : INaming
    {
        /// <summary>
        /// Wenn der Name in der Pluralform steht (PluralForm == true), dann wird hier die NID
        /// geliefert der Singularform
        /// </summary>
        long PluralFormOfNameInSingluarNID { get; }

    }
}
