using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NaLisp = mko.NaLisp;

namespace MKPRG.CSSQL
{
    /// <summary>
    /// mko, 22.1.2018
    /// </summary>
    public interface IColXpr : NaLisp.Core.INaLisp
    {
        /// <summary>
        /// Evaluates expression and returns value as String
        /// </summary>
        string Value { get; }
    }
}
