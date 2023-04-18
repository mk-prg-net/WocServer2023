using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming
{
    public interface IGetNameSpaceOfNamingContainer
    {
        /// <summary>
        /// Liefert den Namen des Namenscontainers
        /// </summary>
        string MyNamingContainerName { get; }

        /// <summary>
        /// Liefert den Namespace, in dem der Namenscontainer abgelegt ist
        /// </summary>
        string MyNamespace { get; }


        /// <summary>
        /// Liefert den Level/die Tiefe des Namespaces
        /// </summary>
        int MyNameSpaceLevel { get; }


    }
}
