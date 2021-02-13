using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// mko, 11.2.2020
/// Bennenungen und Textuelle Beschreibungen in DFC. Diese werden mittels Namespaces und partieller Klassen organisiert.
/// </summary>
namespace DFCTerms
{
    /// <summary>
    /// Auflistung der unterstützen Sprachen in den hier definierten Sprachtabellen
    /// </summary>
    public enum Languages
    {
        // deutsch 
        DE,

        // englisch
        EN
    }

    public partial class Global
    {
        public string ProductName => "DFC2";
    }
}
