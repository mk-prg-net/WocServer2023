using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using static mko.RPN.UrlSaveStringEncoder;
using MKPRG.Tracing.DocuTerms.Parser;

using TTD = MKPRG.Naming.DocuTerms;

namespace MKPRG.Tracing.DocuTerms
{
    /// <summary>
    /// mko, 23.3.2018
    /// Formats DocuTerms in polish notation
    /// 
    /// mko, 6.12.2018
    /// Beginning from mko.RPN 18.12.1 the tokenizer automaticly decodes from RPNUrlSaveString.
    /// This will exploited here: 
    /// </summary>
    public class PNFormater : IFormater
    {
        /// <summary>
        /// Function Name Prefixes (Table of keywords)
        /// </summary>
        Parser.IFn fn = new Fn();

        bool RPNUrlSaveEncode = false;        
        

        global::mko.RPN.Composer basicComp;

        /// <summary>
        /// mko, 2.3.2020
        /// Aus Gründen der Abwärtscompatibilität wird immer in der kulturneutralen Sparache ausgegeben
        /// </summary>
        Naming.Language lng = Naming.Language.NID;

        /// <summary>
        /// Ordnet einer long UID einen EventName- Naming Objekt zu.
        /// 
        /// mko, 12.3.2020
        /// Namensraum MKPRG.Naming.DocuTerms.Event gekürzt auf MKPRG.Naming. Damit werden jetzt alle 
        /// Namenscontainer eingelesen.
        /// 
        /// mko, 9.6.2020
        /// Automatische Erzeugung eines NC, falls noch keiner definiert wurde (Singleton- Pattern) abgeschaltet.
        /// NC muss jetzt durch einen Konstruktor definiert werden.
        /// </summary>
        IReadOnlyDictionary<long, MKPRG.Naming.INaming> NC
        {
            get;
        }          


        string delimitIfneeded(string txt)
        {
            var str = basicComp.Str(txt);
            return str.RPNUrlSaveStringEncodeIf(RPNUrlSaveEncode);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="fn"></param>
        /// <param name="pnL"></param>
        /// <param name="NC">nur lesbares Dictionary, welches zu einerm NID den zugehörigen Naming- Container liefert</param>
        /// <param name="lng">Sprache, in der DocuTerm- Ausgabe erfolgen soll</param>
        /// <param name="RPNUrlSaveEncode"></param>
        public PNFormater(Parser.IFn fn, IReadOnlyDictionary<long, Naming.INaming> NC, Naming.Language lng = Naming.Language.NID, bool RPNUrlSaveEncode = false)
        {            
            this.fn = fn;
            this.RPNUrlSaveEncode = RPNUrlSaveEncode;
            basicComp = new global::mko.RPN.Composer(fn, RPNUrlSaveEncode);
            this.NC = NC;
            this.lng = lng;
        }


        public string Print(IDocuEntity entity)
        {
            var bld = new StringBuilder();
            switch (entity.EntityType)
            {
                case DocuEntityTypes.Event:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Event, bld);
                    }
                    break;
                case DocuEntityTypes.Instance:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Instance, bld);
                    }
                    break;
                case DocuEntityTypes.Method:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Method, bld);
                    }
                    break;
                case DocuEntityTypes.List:
                    {
                        if (entity.Childs.Any())
                        {
                            bld.Append($"{fn.List} ")
                               .Append(System.String.Join(" ", entity.Childs.Select(c => Print(c))))
                               .Append($" {fn.ListEnd}");
                        }
                        else
                        {
                            // mko, 26.6.2020: Leere Listen werden weggelassen
                            //bld.Append(" ");
                        }
                    }
                    break;
                case DocuEntityTypes.Property:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Property, bld);
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
                        
                        if(lng == MKPRG.Naming.Language.NID)
                        {
                            bld.Append($"{fn.Nid} {nid.NamingId}");
                        }
                        else if (lng == MKPRG.Naming.Language.CNT)
                        {
                            // Culture neutral names sind immer ein regulärer Name (bestehen aus einem Wort)
                            bld.Append($"{NC[nid.NamingId].NameIn(lng)}");
                        }
                        else
                        {
                            try
                            {
                                bld.Append($"{delimitIfneeded(NC[nid.NamingId].NameIn(lng))}");
                            }catch(Exception ex)
                            {
                                Debug.WriteLine($"Meldung: {ex.Message}");
                            }
                        }                        
                    }
                    break;
                case DocuEntityTypes.Bool:
                    {
                        var boolVal = (Boolean)entity;

                        var bVal =  boolVal.ValueAsBool
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
                        var str = ((String)entity).Value;
                        bld.Append($"{delimitIfneeded(str)}");
                    }
                    break;
                case DocuEntityTypes.Text:
                    {
                        bld.Append($"{fn.Txt} ")
                           .Append(System.String.Join(" ", entity.Childs.Select(c => Print(c))))
                           .Append($" {fn.ListEnd}");
                    }
                    break;
                case DocuEntityTypes.Version:
                    {
                        bld.Append($"{fn.Version} {Print(entity.Childs.First())}");
                    }
                    break;
                case DocuEntityTypes.ReturnValue:
                    {
                        PrintTypeNameAndValue(fn, entity, fn.Return, bld);
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
                case DocuEntityTypes.WildCard:
                    {
                        bld.Append($"{fn.PropertyWildCard}");
                    }
                    break;
                default:
                    ;
                    break;
            }

            return bld.ToString();
        }


        private void PrintTypeNameAndValue(Parser.IFn fn, IDocuEntity entity, string TypeName, StringBuilder bld)
        {
            TraceHlp.ThrowArgExIf(entity.Childs.Count() < 1, RC.pnL.NID(TTD.Parser.Errors.NameValuePairExpected.UID));
            bld.Append($"{TypeName} {Print(entity.Childs.First())}");

            foreach (var c in entity.Childs.Skip(1))
            {
                bld.Append($" {Print(c)}");
            }
        }
    }
}
