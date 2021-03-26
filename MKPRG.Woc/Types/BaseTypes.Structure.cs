using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc.Types.BaseTypes.Structure
{
    /// <summary>
    /// mko, 25.3.2021
    /// Ablage der Information als unstrukturierten Text, bzw. strukturiert in einer Markup- Sprache
    /// wie HTML oder **Markdown**
    /// </summary>
    public interface IPlainText
        : IWoc,
        Naming.INaming        
    {
    }
}
