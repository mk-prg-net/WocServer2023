using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mko.NaLisp.Core;
using NaLisp = mko.NaLisp;

using Trc = mko.TraceHlp;

namespace ATMO.mko.QueryBuilder
{
    public class LikeXpr : ColXprBase, IColXpr
    {
        public LikeXpr(IColXpr a, IColXpr b) : base(2)
        {
            Elements = new IColXpr[] { a, b };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" {el[0]} LIKE {el[1]} ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new LikeXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }


    /// <summary>
    /// mko, 27.9.2018
    /// 
    /// mko, 6.4.2020
    /// Wert, mit dem verglichen wird (el[1]) wird jetzt automatisch in Großschreibung konvertiert (ToUpper)
    /// </summary>
    public class LikeLowerCaseXpr : ColXprBase, IColXpr
    {
        public LikeLowerCaseXpr(IColXpr a, IColXpr b) : base(2)
        {
            Elements = new IColXpr[] { a, b };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" LOWER({el[0]}) LIKE {el[1].ToLower()} ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new LikeLowerCaseXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }

    /// <summary>
    /// mko, 6.4.2020
    /// </summary>
    public class LikeUpperCaseXpr : ColXprBase, IColXpr
    {
        public LikeUpperCaseXpr(IColXpr a, IColXpr b) : base(2)
        {
            Elements = new IColXpr[] { a, b };
        }

        public override INaLisp Eval(INaLisp[] EvaluatedElements, NaLispStack StackInstance, bool DebugOn)
        {
            var el = new EvaluatedElemsWrapper(EvaluatedElements);
            return NaLisp.Factories.Txt._.Create($" UPPER({el[0]}) LIKE {el[1].ToUpper()} ");
        }

        protected override INaLisp Create(INaLisp[] Elements)
        {
            return new LikeUpperCaseXpr((IColXpr)Elements[0], (IColXpr)Elements[1]);
        }
    }


}
