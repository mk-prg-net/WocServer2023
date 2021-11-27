using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.11.2021
    /// Diese Schnittstelle haben alle DocuTerme, welche ein Wertefeld besitzen wie 
    /// IEvent, IProperty und IReturn
    /// </summary>
    /// <typeparam name="TValue"></typeparam>
    public interface IDocuTermWithValue<TValue>
    {
        /// <summary>
        /// Liefert den Default- Wert, auf den das Wertefeld gesetzt wird, falls 
        /// es bei der Initialisierung nicht explizit gesetzt wird
        /// </summary>
        TValue DocuTermDefaultValue { get; }

        /// <summary>
        /// Liefert true, wenn das Wertefeld mit dem Default- Wert initialisiert wurde
        /// </summary>
        bool IsSetToDefaultValue { get; }


    }
}
