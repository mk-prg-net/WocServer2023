using MKPRG.StackFlow.Abstract;
using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.StackFlow
{
    /// <summary>
    /// mko, 27.1..2024
    /// Liste aus Namenszuweisungen
    /// </summary>
    public interface IListOfNameAssignments
    {
        IEnumerable<INameAssignment> NameAssignments { get; }
    }
}
