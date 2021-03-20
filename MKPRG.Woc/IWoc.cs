using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Woc
{
    public interface IWoc
        : Naming.INaming
    {

        IEnumerable<long> Refs { get; }

    }
}
