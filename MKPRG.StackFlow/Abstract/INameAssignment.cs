using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.StackFlow.Abstract
{
    /// <summary>
    /// Markiert Objekte, die an eienn Namen zugewiesen werden können
    /// </summary>
    public interface IAssignableToName { }


    /// <summary>
    /// mko, 24.1.2024
    /// Bindet einen 
    /// </summary>
    public interface INameAssignment
    {
        /// <summary>
        /// Namens- ID
        /// Für vordefinierte Namen ist die NID stets positiv (z.B. von Naming Containern)
        /// Für ad Hoc definierte Namen im Quelltext ist diese stets negativ.
        /// </summary>
        long NidForValue { get; }

        /// <summary>
        /// Wert oder Liste, die an den Namen gebunden wurde
        /// </summary>
        IAssignableToName AssignedValue { get; }

    }
}
