using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static mko.RPN.UrlSaveStringEncoder;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms.Formatter
{
    /// <summary>
    /// mko, 23.3.2018
    /// Formats DocuEntities in polish notation
    /// 
    /// mko, 6.12.2018
    /// Beginning from mko.RPN 18.12.1 the tokenizer automaticly decodes from RPNUrlSaveString.
    /// This will exploited here: 
    /// 
    /// mko, 10.8.2021
    /// Angepasst an die neu implementierte, streng typisierte DocuEntity- Lib
    /// 
    /// mko, 8.10.2021
    /// Pflege von zwei nahezu identischen PN- Formattern aufgegeben
    /// 
    /// </summary>
    public class PNFormater : IFormater
    {
        IFormater fmt;

        public PNFormater(Parser.IFn fn, IReadOnlyDictionary<long, Naming.INaming> NC, Naming.Language lng = Naming.Language.NID, bool RPNUrlSaveEncode = false)
        {
            fmt = new IndentedTextFormatter(fn, NC, lng, 0, RPNUrlSaveEncode, " ");
        }



        public string Print(IDocuEntity entity)
        {
            return fmt.Print(entity);
        }
    }
}
