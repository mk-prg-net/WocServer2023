﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 27.7.2021
    /// Instanzen, deren Namen als freier String definiert sind
    /// </summary>
    public interface IInstanceWithNameAsString
        : IInstance,
        IDocuTermWithNameAsString
    {
    }
}
