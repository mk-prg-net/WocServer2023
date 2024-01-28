using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.StackFlow
{
    /// <summary>
    /// mko, 27.1.2024
    /// Gleitpunktzahl
    /// </summary>
    public interface IFloatingPointNumber
        :ITerminal
    {
        double FloatingPointValue { get; }
    }
}
