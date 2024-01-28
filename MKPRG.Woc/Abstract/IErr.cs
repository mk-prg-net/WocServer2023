using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Woc
{
    /// <summary>
    /// mko, 27.1.2024
    /// Diese Schnittstelle implementieren alle Fehlerbeschreibungen.
    /// </summary>
    public interface IErr
        : IStEx
    {
        long ErrorDescriptionNamingId { get; }
    }
}
