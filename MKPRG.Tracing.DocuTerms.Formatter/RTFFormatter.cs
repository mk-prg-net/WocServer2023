using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static mko.RPN.UrlSaveStringEncoder;

using MKPRG.Tracing.DocuTerms.Parser;

using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.Tracing.DocuTerms.Formater
{
    /// <summary>
    /// mko, 3.11.2020
    /// Formatierte Ausgabe von Docuterms als RTF- Text (http://www.biblioscape.com/rtf15_spec.htm)
    /// </summary>
    public class RTFFormatter
    {
        /// <summary>
        /// Function Name Prefixes (Table of keywords)
        /// </summary>
        IFn fn = new Fn();

        readonly int IndentSpc;

        Naming.Language lng = Naming.Language.CNT;

        /// <summary>
        /// Ordnet einer long UID einen EventName- Naming Objekt zu.
        /// </summary>
        IReadOnlyDictionary<long, Naming.INaming> NC;

        /// <summary>
        /// mko, 19.11.2019
        /// Erzeugt eine Liste von Tabs
        /// </summary>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        string Tabs(int Indentation)
        {
            var tabsBld = new StringBuilder();
            tabsBld.Append("");

            for (int i = 0; i < Indentation; i++)
            {
                tabsBld.Append(" ");
            }
            return tabsBld.ToString();
        }

        bool RPNUrlSaveEncode = false;

        global::mko.RPN.Composer basicComp;

        string delimitIfneeded(string txt)
        {
            var str = basicComp.Str(txt);
            return str.RPNUrlSaveStringEncodeIf(RPNUrlSaveEncode);
        }

        /// <summary>
        /// mko, 12.3.2020
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="pnL"></param>
        /// <param name="NamingContainers"></param>
        /// <param name="lng"></param>
        /// <param name="Indentation"></param>
        /// <param name="RPNUrlSaveEncode"></param>
        public RTFFormatter(
            IFn fn,
            IReadOnlyDictionary<long, MKPRG.Naming.INaming> NamingContainers,
            MKPRG.Naming.Language lng = MKPRG.Naming.Language.NID,
            int Indentation = 1,
            bool RPNUrlSaveEncode = false)
        {
            this.fn = fn;
            this.RPNUrlSaveEncode = RPNUrlSaveEncode;
            this.lng = lng;
            IndentSpc = Indentation;
            basicComp = new global::mko.RPN.Composer(fn, RPNUrlSaveEncode);
            this.NC = NamingContainers;
        }

        public string Print(IDocuEntity entity)
        {
            return Print(entity, 0);
        }


        public string Print(IDocuEntity entity, int Indentation)
        {
            var bld = new StringBuilder();
            switch (entity.EntityType)
            {
                case DocuEntityTypes.Event:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Event, bld, Indentation);
                    }
                    break;
                case DocuEntityTypes.Instance:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Instance, bld, Indentation);
                    }
                    break;
                case DocuEntityTypes.Method:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Method, bld, Indentation);
                    }
                    break;
                case DocuEntityTypes.List:
                    {
                        if (entity.Childs.Any())
                        {
                            bld.Append($"{Tabs(Indentation)}{fn.List}\n")
                               .Append(System.String.Join("\n", entity.Childs.Select(c => $"{Print(c, Indentation + IndentSpc)}")))
                               .Append($"\n{Tabs(Indentation)}{fn.ListEnd}");
                        }
                        else
                        {
                            // mko, 26.6.2020: Leere Listen werden weggelassen
                        }
                    }
                    break;
                case DocuEntityTypes.Property:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Property, bld, Indentation);
                    }
                    break;
                case DocuEntityTypes.PropertySet:
                    {

                    }
                    break;
                case DocuEntityTypes.NID:
                    {
                        // mko, 9.6.2020
                        // Abrufen des Namens in der Wunschsprache
                        var nid = (NID)entity;

                        if (lng == MKPRG.Naming.Language.NID)
                        {
                            bld.Append($"{fn.Nid} {nid.NamingId}");
                        }
                        else if (lng == MKPRG.Naming.Language.CNT)
                        {
                            // Culture neutral names sind immer ein regulärer Name (bestehen aus einem Wort)
                            bld.Append($"{delimitIfneeded(NC[nid.NamingId].NameIn(lng))}");
                        }
                        else
                        {
                            bld.Append($"{fn.Txt} {delimitIfneeded(NC[nid.NamingId].NameIn(lng))} {fn.ListEnd}");
                        }
                    }
                    break;
                case DocuEntityTypes.Bool:
                    {
                        var boolVal = (Boolean)entity;

                        var bVal = boolVal.ValueAsBool
                                    ? NC[MKPRG.Naming.DocuTerms.Boolean.True.UID].NameIn(lng)
                                    : NC[MKPRG.Naming.DocuTerms.Boolean.False.UID].NameIn(lng);
                        bld.Append($"{bVal}");
                    }
                    break;
                case DocuEntityTypes.Int:
                    {
                        var intVal = (Integer)entity;

                        bld.Append($"{intVal.ValueAsLong}");
                    }
                    break;
                case DocuEntityTypes.Float:
                    {
                        var floatVal = (Double)entity;

                        bld.Append($"{floatVal.Value}");
                    }
                    break;
                case DocuEntityTypes.String:
                    {
                        // mko, 2.3.2020
                        // Wenn der String ein DocuTermUID definiert ist,
                        // dann wird seine sprachspezifische Repräsentation 
                        // nachgeschlagen und angezeigt.

                        var str = ((String)entity).Value;

                        bld.Append($"{delimitIfneeded(str)}");
                    }
                    break;
                case DocuEntityTypes.Text:
                    {
                        bld.Append($"{Tabs(Indentation)}{fn.Txt}")
                           .Append(System.String.Join(" ", entity.Childs.Select(c => Print(c))))
                           .Append($"{fn.ListEnd}");
                    }
                    break;
                case DocuEntityTypes.Version:
                    {
                        bld.Append($"{Tabs(Indentation)}{fn.Version} {Print(entity.Childs.First())}");
                    }
                    break;
                case DocuEntityTypes.ReturnValue:
                    {
                        bld.Append($"{Tabs(Indentation)}{fn.Return}\n{Print(entity.Childs.First(), Indentation + IndentSpc)}");
                    }
                    break;
                // mko, 5.3.2019
                // Ausgabe von Time
                case DocuEntityTypes.Time:
                    {
                        ITime t = (ITime)entity;

                        bld.Append($"{fn.Time} {t.Hour} {t.Minutes} {t.Seconds} {t.Milliseconds}");
                    }
                    break;
                // mko, 5.3.2019
                // Ausgabe von Date
                case DocuEntityTypes.Date:
                    {
                        IDate d = (IDate)entity;
                        bld.Append($"{fn.Date} {d.Year} {d.Month} {d.Day}");
                    }
                    break;
                default:
                    ;
                    break;
            }

            return bld.ToString();
        }

        /// <summary>
        /// mko
        /// 
        /// mko, 2.3.2020
        /// Auflösung von docuTermIds in nationalsprachliche Namen
        /// 
        /// mko, 8.10.2020
        /// Liste der nicht umzubrechenden Werte erweitert auf die Typen bool, float, Int und Wildecard
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="entity"></param>
        /// <param name="TypeName"></param>
        /// <param name="bld"></param>
        /// <param name="Indentation"></param>
        private void PrintTypeNameAndValue(IFn fn, IDocuEntity entity, string TypeName, StringBuilder bld, int Indentation)
        {
            TraceHlp.ThrowArgExIf(entity.Childs.Count() < 1, RC.pnL.eFails(TTD.Parser.Errors.NameValuePairExpected.UID));
            bld.Append($"{Tabs(Indentation)}\b{TypeName} {Print(entity.Childs.First(), Indentation + IndentSpc)}");

            foreach (var c in entity.Childs.Skip(1))
            {
                bld.Append(
                    c.EntityType == DocuEntityTypes.String
                    || c.EntityType == DocuEntityTypes.Text
                    || c.EntityType == DocuEntityTypes.Version
                    || c.EntityType == DocuEntityTypes.Date
                    || c.EntityType == DocuEntityTypes.Time
                    || c.EntityType == DocuEntityTypes.Bool
                    || c.EntityType == DocuEntityTypes.Float
                    || c.EntityType == DocuEntityTypes.Int
                    || c.EntityType == DocuEntityTypes.WildCard
                    ? $" {Print(c, IndentSpc)}"
                    : $"\n{Print(c, Indentation + IndentSpc)}");

            }
        }
    }
}
