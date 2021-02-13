using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 26.6.2020
    /// Von System.Argument Exception abgeleitete Klasse, in welcher die Ursachen einer ausnahme mittels 
    /// DocuTerms beschrieben werden können.
    /// </summary>
    public class ArgumentExceptionWithDocuTermDescription
        : ArgumentException,
        IExceptionWithDocuTermDescription
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageAsDocuTerm">Beschreibung der Ursache der Ausnahme mittels eines DocuTerms</param>
        public ArgumentExceptionWithDocuTermDescription(IDocuEntity MessageAsDocuTerm)
        {
            this.MessageAsDocuTerm = MessageAsDocuTerm;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageAsDocuTerm"></param>
        /// <param name="innerException"></param>
        public ArgumentExceptionWithDocuTermDescription(IDocuEntity MessageAsDocuTerm, Exception innerException)
            : base("", innerException)
        {
            this.MessageAsDocuTerm = MessageAsDocuTerm;
        }


        /// <summary>
        /// Abruf der Meldung als DocuTerm- Ausdruck, formatiert mit dem Standard- Formatierer.
        /// </summary>
        public override string Message => RCV3.fmtPN.Print(MessageAsDocuTerm);

        /// <summary>
        /// Beschreibung der Fehlerursache durch einen DocuTerm
        /// </summary>
        public IDocuEntity MessageAsDocuTerm { get; }

    }
}
