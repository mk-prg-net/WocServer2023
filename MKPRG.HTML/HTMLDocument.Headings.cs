using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using MKPRG.Tracing;
using static MKPRG.Tracing.DocuTerms.ComposerSubTrees;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;

namespace ATMO.mko.Logging.HTML
{
    /// <summary>
    /// mko, 11.10.2020
    /// </summary>
    partial class HTMLDocument
    {
        /// <summary>
        /// mko, 4.1.2021
        /// Parametrischer Version des Überschriften- Generators
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public HTMLDocument hAtLevel(int level)
        {
            TraceHlp.ThrowArgExIf(level < 1 || level > 6,
                pnL.ReturnValidatePreconditionFailedArgumentOutOfRange(
                    pnL.p("HTMLHeadingLevel", level),
                    pnL.i(TT.Sets.Range.UID,
                        pnL.p(TT.Sets.Begin.UID, 1),
                        pnL.p(TT.Sets.End.UID, 6))));
                    
            t($"h{level}");
            return this;
        }

        /// <summary>
        /// mko, 4.1.2021
        /// Parametrischer Version des Überschriften- Generators.
        /// </summary>
        /// <param name="level"></param>
        /// <param name="cssClass"></param>
        /// <returns></returns>
        public HTMLDocument hAtLevelWithClass(int level, string cssClass)
        {
            TraceHlp.ThrowArgExIf(level < 1 || level > 6,
                pnL.ReturnValidatePreconditionFailedArgumentOutOfRange(
                    pnL.p("HTMLHeadingLevel", level),
                    pnL.i(TT.Sets.Range.UID,
                        pnL.p(TT.Sets.Begin.UID, 1),
                        pnL.p(TT.Sets.End.UID, 6))));

            tags.Push($"h{level}");
            bldDoc.Append($"<h{level} class='{cssClass}'>");
            return this;

        }

        public HTMLDocument h1
        {
            get
            {
                t("h1");
                return this;
            }
        }

        public HTMLDocument h1_class(string cssClass)
        {
            tWithClass("h1", cssClass);
            return this;
        }

        public string H1(string content)
        {
            return $"<h1>{content}</h1>";            
        }

        public HTMLDocument h2
        {
            get
            {
                t("h2");
                return this;
            }
        }

        public HTMLDocument h2_class(string cssClass)
        {
            tWithClass("h2", cssClass);
            return this;
        }

        public string H2(string content)
        {
            return $"<h2>{content}</h2>";
        }

        public HTMLDocument h3
        {
            get
            {
                t("h3");
                return this;
            }
        }

        public HTMLDocument h3_class(string cssClass)
        {
            tWithClass("h3", cssClass);
            return this;
        }

        public string H3(string content)
        {
            return $"<h3>{content}</h3>";
        }
    }
}
