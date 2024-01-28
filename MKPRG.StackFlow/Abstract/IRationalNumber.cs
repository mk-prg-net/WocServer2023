using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.StackFlow
{
    /// <summary>
    /// mko, 27.1.2024
    /// Rationaler Bruch
    /// </summary>
    public interface IRationalFraction
        : ITerminal
    {
        /// <summary>
        /// Zähler eines Bruches
        /// </summary>
        long RationalFractionNominator {  get; }

        /// <summary>
        /// Nenner eines Bruches
        /// </summary>
        long RationalFractionDenominator { get; }
    }
}
