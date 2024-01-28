using System;
using System.Collections.Generic;
using System.Text;

namespace MKPRG.Woc
{
    /// <summary>
    /// mko, 27.1.2024
    /// Status after execution Base interface
    /// </summary>
    public interface IStEx
    {
        /// <summary>
        /// True, if Stage/Function/Method has succesfully completed
        /// </summary>
        bool SuccessfullyCompleted { get; }  
    }
}
