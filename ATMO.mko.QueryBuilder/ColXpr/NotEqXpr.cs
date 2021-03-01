﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;

using Trc = mko.TraceHlp;

namespace ATMO.mko.QueryBuilder
{
    /// <summary>
    /// mko, 23.1.2018
    /// </summary>
    public class NotEqXpr : ColXprBase, IColXpr
    {
        public NotEqXpr(IColXpr a, IColXpr b) : base(2)
        {
            Elements = new IColXpr[] { a, b };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" {el[0]} != {el[1]} ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new NotEqXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }
}
