using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using mko.RPN;
using pnL = MKPRG.Tracing.DocuTerms.Composer;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 6.3.2018
    /// Parser for log- messages in polish notation
    /// mko, 17.5.2018
    /// fix: dict- Key's where generated from pnL.fn- this is static Fn. So a definition of IFn fn where without meaning.
    ///      Now keys's will be generated with fn, defined in constructor.
    /// </summary>
    public class FunctionEvalMapperFunctor : IFnameEvalMapper
    {
        IFn fn = Fn._;

        public FunctionEvalMapperFunctor() { }


        IComposer pnL;

        public FunctionEvalMapperFunctor(IFn fn, IComposer pnL)
        {
            this.fn = fn;
            this.pnL = pnL;
        }

        public void MapFnameToEvalIn(Dictionary<string, IEval> dict)
        {
            dict[fn.Instance] = new InstanceEval(pnL);
            dict[fn.Property] = new PropertyEval(pnL);
            // Deaktiviert am 25.6.2020: aktuell wird das Konzept eines Property- Setters nicht weiterverfolgt
            //dict[fn.PropertySet] = new PropertySetEval(pnL);
            dict[fn.Method] = new MethodEval(pnL);
            dict[fn.Event] = new EventEval(pnL);
            dict[fn.Version] = new VersionEval(pnL);
            dict[fn.Txt] = new TextEval(fn, pnL);
            dict[fn.Date] = new DateEval(pnL);
            dict[fn.Time] = new TimeEval(pnL);
            dict[fn.ListEnd] = new ListEndEval(fn);
            dict[fn.List] = new ListEval(fn, pnL);
            dict[fn.Return] = new ReturnEval(pnL);
            dict[fn.Nid] = new NidEval(pnL);
            dict[fn.PropertyWildCard] = new PropertyWildCardEval(pnL);
        }
    }
}
