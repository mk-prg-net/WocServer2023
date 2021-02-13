using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ATMO.mko.Logging.PNDocuTerms.DocuEntities;

namespace ATMO.mko.Logging
{
    /// <summary>
    /// mko, 2.7.2019
    /// Ausnahme, die geworfen wird, wenn auf eien Wert in RCV3WithValue zugegriffen werden soll, das
    /// Rückgabeobjekt jedoch einen Fehler anzeigt und kein Wert existiert.
    /// 
    /// mko, 26.6.2020
    /// Schnittstele IExceptionWithDocuTermDescription hinzugefügt
    /// </summary>
    public class RCV3GetValueException 
        : Exception,
        IExceptionWithDocuTermDescription
    {
        public IDocuEntity MessageEntity { get; }

        public IDocuEntity MessageAsDocuTerm => MessageEntity;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="MessageEntity"></param>
        public RCV3GetValueException(IDocuEntity MessageEntity)
            :base(RCV3.fmtPN.Print(MessageEntity))
        {            
            this.MessageEntity = MessageEntity;            
        }
    }
}
