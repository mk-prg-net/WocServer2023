using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static mko.RPN.UrlSaveStringEncoder;


using ANC = MKPRG.Naming;
using TT = MKPRG.Naming.TechTerms;
using TTD = MKPRG.Naming.DocuTerms;


namespace MKPRG.Tracing.DocuTerms.Formatter
{
    public class IndentedTextFormatter : IFormater
    {
        /// <summary>
        /// Function Name Prefixes (Table of keywords)
        /// </summary>
        Parser.IFn fn = new Parser.Fn();

        readonly int IndentSpc;

        ANC.Language lng = ANC.Language.CNT;

        /// <summary>
        /// Ordnet einer long UID einen EventName- Naming Objekt zu.
        /// </summary>
        IReadOnlyDictionary<long, ANC.INaming> NC;
        ANC.NamingHelper NH;

        /// <summary>
        /// mko, 19.11.2019
        /// Erzeugt eine Liste von Tabs
        /// </summary>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        void Tabs(StringBuilder bld, int Indentation)
        {
            //var tabsBld = new StringBuilder();
            //tabsBld.Append("");

            for (int i = 0; i < Indentation; i++)
            {
                bld.Append(" ");
            }
            //return tabsBld.ToString();
        }

        bool RPNUrlSaveEncode = false;

        global::mko.RPN.Composer basicComp;

        string delimitIfneeded(string txt)
        {
            // mko, 10.9.2021
            // Leere Zeichenketten, wie sie z.B. als Werte von Eigenschaften auftreten können
            // werden explizit delimitiert, damit sie auch in der Ausgabe erscheinen
            if (txt == "")
            {
                txt = @"''";
            }

            var str = basicComp.Str(txt);
            return str.RPNUrlSaveStringEncodeIf(RPNUrlSaveEncode);
        }

        string nl = "\n";

        /// <summary>
        /// mko, 12.3.2020
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="pnL"></param>
        /// <param name="NamingContainers"></param>
        /// <param name="lng"></param>
        /// <param name="Indentation"></param>
        /// <param name="RPNUrlSaveEncode"></param>
        public IndentedTextFormatter(
            Parser.IFn fn,
            IReadOnlyDictionary<long, ANC.INaming> NamingContainers,
            ANC.Language lng = ANC.Language.NID,
            int Indentation = 1,
            bool RPNUrlSaveEncode = false,
            string newLine = "\n")
        {
            this.fn = fn;
            this.nl = newLine;
            this.RPNUrlSaveEncode = RPNUrlSaveEncode;
            this.lng = lng;
            IndentSpc = Indentation;
            basicComp = new global::mko.RPN.Composer(fn, RPNUrlSaveEncode);
            this.NC = NamingContainers;
            NH = new ANC.NamingHelper(NC, lng);
        }

        public string Print(IDocuEntity entity)
        {
            var bld = new StringBuilder();
            Print(entity, IndentSpc, bld);
            return bld.ToString();
        }


        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(INID nid, int Indentation)
        {
            var bld = new StringBuilder();
            Print(nid, Indentation, bld);
            return bld.ToString();
        }

        /// <summary>
        /// mko, 1.10.2021
        /// Ausgabe  einer NID als Namen
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        private void Print(INID nid, StringBuilder bld)
        {
            // mko, 9.6.2020
            // Abrufen des Namens in der Wunschsprache


            if (lng == ANC.Language.NID)
            {
                bld.Append($"{fn.Nid} {nid.NamingId}");
            }
            else if (lng == ANC.Language.CNT)
            {
                // Culture neutral names sind immer ein regulärer Name (bestehen aus einem Wort)
                bld.Append($"{delimitIfneeded(NH._(nid.NamingId))} ");
            }
            else
            {
                bld.Append($"{fn.Txt} {delimitIfneeded(NH._(nid.NamingId))} {fn.ListEnd} ");
            }
        }

        /// <summary>
        /// 4.10.2021
        /// Ausgabe eines NID als Wert
        /// </summary>
        /// <param name="nid"></param>
        /// <param name="Indentation"></param>
        /// <param name="bld"></param>
        private void Print(INID nid, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            if (lng == ANC.Language.NID)
            {
                bld.Append($"{fn.Nid} {nid.NamingId}{nl}");
            }
            else if (lng == ANC.Language.CNT)
            {
                // Culture neutral names sind immer ein regulärer Name (bestehen aus einem Wort)
                bld.Append($"{delimitIfneeded(NH._(nid.NamingId))}{nl}");
            }
            else
            {
                // mko, 9.6.2020
                // Abrufen des Namens in der Wunschsprache
                bld.Append($"{fn.Txt} {delimitIfneeded(NH._(nid.NamingId))} {fn.ListEnd}{nl}");
            }
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="t"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(ITime t, int Indentation)
        {
            var bld = new StringBuilder();
            Print(t, Indentation, bld);
            return bld.ToString();
        }

        public void Print(ITime t, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            bld.Append($"{fn.Time} {t.Hour} {t.Minutes} {t.Seconds} {t.Milliseconds}{nl}");
        }


        public string Print(IDate d, int Indentation)
        {
            var bld = new StringBuilder();
            Print(d, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IDate d, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            bld.Append($"{fn.Date} {d.Year} {d.Month} {d.Day}{nl}");
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="e"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IEvent e, int Indentation)
        {
            var bld = new StringBuilder();
            Print(e, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IEvent e, int Indentation, StringBuilder bld)
        {
            PrintNameAndValue(
                e,
                bld,
                Indentation,
                fn.Event,
                () =>
                {
                    if (!(e.EventParameter is IDocuEntityWithNameAsNid iNid && iNid.DocuTermNid.NamingId == TTD.Types.UndefinedEventParameter.UID))
                    {
                        Print(e.EventParameter, Indentation + IndentSpc, bld);
                    }
                });
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="i"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IInstance i, int Indentation)
        {
            var bld = new StringBuilder();
            Print(i, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IInstance i, int Indentation, StringBuilder bld)
        {
            PrintNameAndValue(
                i,
                bld,
                Indentation,
                fn.Instance,
                () =>
                {
                    if (i.InstanceMembers.Any())
                    {
                        Tabs(bld, Indentation + IndentSpc);
                        bld.Append($"{fn.List}{nl}");
                        foreach (var member in i.InstanceMembers)
                        {
                            Print(member, Indentation + 2 * IndentSpc, bld);
                        }
                        Tabs(bld, Indentation + IndentSpc);
                        bld.Append($"{fn.ListEnd}{nl}");
                    }
                });
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="m"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IMethod m, int Indentation)
        {
            var bld = new StringBuilder();
            Print(m, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IMethod m, int Indentation, StringBuilder bld)
        {
            PrintNameAndValue(
                m,
                bld,
                Indentation,
                fn.Method,
                () =>
                {
                    if (m.Parameters.Any())
                    {
                        Tabs(bld, Indentation + IndentSpc);
                        bld.Append($"{fn.List}{nl}");
                        foreach (var parameter in m.Parameters)
                        {
                            Print(parameter, Indentation + 2 * IndentSpc, bld);
                        }
                        Tabs(bld, Indentation + IndentSpc);
                        bld.Append($"{fn.ListEnd}{nl}");
                    }
                });
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="lst"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IDTList lst, int Indentation)
        {
            var bld = new StringBuilder();
            Print(lst, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IDTList lst, int Indentation, StringBuilder bld)
        {
            if (lst.ListMembers.Any())
            {
                Tabs(bld, Indentation);
                bld.Append($"{fn.List}{nl}");

                foreach (var member in lst.ListMembers)
                {
                    Print(member, Indentation + IndentSpc, bld);
                }

                Tabs(bld, Indentation);
                bld.Append($"{fn.ListEnd}{nl}");
            }
            else
            {
                // mko, 26.6.2020: Leere Listen werden weggelassen
                //bld.Append(" ");
            }
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="p"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IProperty p, int Indentation)
        {
            var bld = new StringBuilder();
            Print(p, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IProperty p, int Indentation, StringBuilder bld)
        {
            PrintNameAndValue(
                p,
                bld,
                Indentation,
                fn.Property,
                () =>
                {
                    Print(p.PropertyValue, Indentation + IndentSpc, bld);

                });
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="b"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IBoolean b, int Indentation)
        {
            var bld = new StringBuilder();
            Print(b, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IBoolean boolVal, int Indentation, StringBuilder bld)
        {
            var bVal = "";

            if (lng == ANC.Language.NID)
            {
                bVal = boolVal.ValueAsBool
                        ? $"{fn.Nid} {TTD.Boolean.True.UID}"
                        : $"{fn.Nid} {TTD.Boolean.False.UID}";
            }
            else
            {
                bVal = boolVal.ValueAsBool
                        ? $"{NH._(TTD.Boolean.True.UID)}"
                        : $"{NH._(TTD.Boolean.False.UID)}";
            }

            bld.Append($"{bVal}");
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="i"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IInteger i, int Indentation)
        {
            var bld = new StringBuilder();
            Print(i, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IInteger intVal, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            bld.Append($"{intVal.ValueAsLong}{nl}");
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="d"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IDouble d, int Indentation)
        {
            var bld = new StringBuilder();
            Print(d, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IDouble dblVal, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            bld.Append($"{dblVal.ValueAsDouble}{nl}");
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="s"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IString s, int Indentation)
        {
            var bld = new StringBuilder();
            Print(s, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IString s, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            bld.Append($"{delimitIfneeded(s.ValueAsString)}{nl}");
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(ITxt txt, int Indentation)
        {
            var bld = new StringBuilder();
            Print(txt, Indentation, bld);
            return bld.ToString();
        }

        public void Print(ITxt txt, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            bld.Append($"{fn.Txt} ");
            foreach (var w in txt.Words)
            {
                bld.Append($"{delimitIfneeded(w.ValueAsString)} ");
            }

            bld.Append($"{fn.ListEnd}{nl}");
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="ver"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IVer ver, int Indentation)
        {
            var bld = new StringBuilder();
            Print(ver, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IVer ver, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            bld.Append($"{fn.Version} {ver.VersionString}{nl}");
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="ret"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IReturn ret, int Indentation)
        {
            var bld = new StringBuilder();
            Print(ret, Indentation, bld);
            return bld.ToString();
        }

        public void Print(IReturn ret, int Indentation, StringBuilder bld)
        {
            if (!(ret.ReturnValue is IDocuEntityWithNameAsNid nid && nid.DocuTermNid.NamingId == TTD.Types.UndefinedReturnValue.UID))
            {
                Tabs(bld, Indentation);
                bld.Append($"{fn.Return}{nl}");
                Print(ret.ReturnValue, Indentation + IndentSpc, bld);
            }
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="Indentation"></param>
        /// <returns></returns>
        public string Print(IWildCard wc, int Indentation)
        {
            var bld = new StringBuilder();
            Print(wc, Indentation, bld);
            return bld.ToString();
        }

        /// <summary>
        /// mko, 1.10.2021
        /// </summary>
        /// <param name="wc"></param>
        /// <param name="Indentation"></param>
        /// <param name="bld"></param>
        public void Print(IWildCard wc, int Indentation, StringBuilder bld)
        {
            Tabs(bld, Indentation);
            bld.Append($"{fn.PropertyWildCard}{nl}");
            if (wc.HasSubTreeConstraint)
            {
                Print(wc.SubTreeConstraint, Indentation + IndentSpc, bld);
            }
        }

        /// <summary>
        /// mko
        /// </summary>
        /// <param name="entity"></param>
        /// <param name="Indentation"></param>
        /// <param name="bld"></param>
        /// <returns></returns>
        public void Print(IDocuEntity entity, int Indentation, StringBuilder bld)
        {
            try
            {
                //var bld = new StringBuilder();
                switch (entity.EntityType)
                {
                    case DocuEntityTypes.Event:
                        {
                            var e = (IEvent)entity;
                            Print(e, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.Instance:
                        {
                            var i = (IInstance)entity;
                            Print(i, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.Method:
                        {
                            var m = (IMethod)entity;
                            Print(m, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.List:
                        {
                            var lst = (IDTList)entity;
                            Print(lst, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.Property:
                        {
                            var p = (IProperty)entity;
                            Print(p, Indentation, bld);
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
                            Print(nid, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.Bool:
                        {
                            var boolVal = (Boolean)entity;
                            Print(boolVal, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.Int:
                        {
                            var intVal = (Integer)entity;
                            Print(intVal, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.Float:
                        {
                            var floatVal = (Double)entity;
                            Print(floatVal, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.String:
                        {
                            var str = (IString)entity;
                            Print(str, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.Text:
                        {
                            var txt = (ITxt)entity;
                            Print(txt, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.Version:
                        {
                            var ver = (IVer)entity;
                            Print(ver, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.ReturnValue:
                        {
                            var ret = (IReturn)entity;
                            Print(ret, Indentation, bld);
                        }
                        break;
                    // mko, 5.3.2019
                    // Ausgabe von Time
                    case DocuEntityTypes.Time:
                        {
                            ITime t = (ITime)entity;
                            Print(t, Indentation, bld);
                        }
                        break;
                    // mko, 5.3.2019
                    // Ausgabe von Date
                    case DocuEntityTypes.Date:
                        {
                            IDate d = (IDate)entity;
                            Print(d, Indentation, bld);
                        }
                        break;
                    case DocuEntityTypes.WildCard:
                        {
                            var wc = (IWildCard)entity;
                            Print(wc, Indentation, bld);
                        }
                        break;
                    default:
                        ;
                        break;
                }


            }
            catch (Exception ex)
            {
                bld.Append($"{fn.Method} IndentedTextFormatter_Print {fn.List} {fn.Return} {fn.Event} fails {ex} {fn.ListEnd}");
            }
        }


        private void PrintNameAndValue(IDocuEntity entity, StringBuilder bld, int Indentation, string TypeName, Action PrintValue)
        {
            if (entity is IDocuEntityWithNameAsNid Nid)
            {
                // mko, 6.9.2021
                // NID's als Name werden jetzt explizit als NID- Terme ausgegeben
                if (lng == ANC.Language.NID)
                {
                    Tabs(bld, Indentation);
                    bld.Append($"{TypeName} ");
                    Print(Nid.DocuTermNid, bld);
                    bld.Append($" {nl}");
                }
                else
                {
                    Tabs(bld, Indentation);
                    bld.Append($"{TypeName} {NH._(Nid.DocuTermNid.NamingId)}{nl}");
                }
                PrintValue();

            }
            else if (entity is IDocuTermWithNameAsString Str)
            {
                Tabs(bld, Indentation);
                bld.Append($"{TypeName} {Str.DocuTermName}{nl}");
                PrintValue();
            }
        }
    }
}
