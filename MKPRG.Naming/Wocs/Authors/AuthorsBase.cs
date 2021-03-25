using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Naming.Wocs.Authors
{
    /// <summary>
    /// mko, 25.3.2021
    /// Basisklasse der Autoren
    /// </summary>
    public abstract class AuthorsBase
        : NamingBase
    {
        public AuthorsBase(long AuthorUID, long WocAuthorUID, long NodeUID)
            : base(AuthorUID, _TypeAuthor.UID, WocAuthorUID, NodeUID,
                   new (long WocType, long Ref)[]
                    {
                        (_WocTypeNamespace.UID, _TypeAuthor.UID)
                    })
        { }    
    }
}
