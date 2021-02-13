using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing
{
    /// <summary>
    /// mko, 25.7.2018
    /// Defines a Value Property. For purpose of standardisation    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IValue<out T>
    {        
        T Value { get; }
    }
}
