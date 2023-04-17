using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using NM = MKPRG.Naming;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// 
    /// mko, 8.6.2020
    /// Prefixe für Boolean, Int, Double und NID's hinzugefügt
    /// </summary>
    public class FnRunen : IFn
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static FnRunen _ {
            get
            {
                if(_instance == null)
                {
                    _instance = new FnRunen();
                }
                return _instance;
            }
        }
        static FnRunen _instance;

        /// <summary>
        /// mko.RPN Tokenizer- Bool
        /// </summary>
        public string constBool => "";

        /// <summary>
        /// mko.RPN Tokenizer- Int
        /// </summary>
        public string constInt => "";

        /// <summary>
        /// mko.RPN Tokenizer- Dbl
        /// </summary>
        public string constDbl => "";

        /// <summary>
        /// mko.RPN Tokenizer- String
        /// </summary>
        public string constStr => "";

        public string ListEnd => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.ListEnd);

        public string NamePrefix => "";

        public string ParamNamePrefix => "";

        public string DerivedTokenPrefix => "";

        public bool IsSemanticDescriptor(string FunctionName)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Instance
        /// A instance defines a block, that decribes a business object.
        /// It has a name and contains a list with properties, methods and events or a version number.
        /// </summary>
        public string Instance => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Instance);

        /// <summary>
        /// Method
        /// A method documents a method- or function call an the results of them.
        /// It contains instances, properties and events
        /// </summary>
        public string Method => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Method);


        public string Function => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Function);


        /// <summary>
        /// Return 
        /// A return block describes the result of a function- or method call. 
        /// </summary>
        public string Return => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Return);


        /// <summary>
        /// Property
        /// Assignes a name to a portion of information.
        /// A portion of information can be a text, a list or a instance.
        /// </summary>
        public string Property => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Property);

        public string PropertySet => NamePrefix + "p_set";


        /// <summary>
        /// Version
        /// Defines a version numeber for a business object like instances.
        /// The version number consists of thre parts: main, sub and build- number.
        /// The parts are separated with points (i.e. 1.2.3).
        /// </summary>
        public string Version => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Version);


        /// <summary>
        /// Event
        /// An Event can indicate the success of an operation on an business object. 
        /// The structure of an event is equivalent to the structure of a property: #e name value.
        /// The name is often an indicator for success: succeded, failed, warn, ... se DocuEntityHlp.MapStringToEventType
        /// </summary>
        public string Event => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Event);


        /// <summary>
        /// Date
        /// Prefix for date literal
        /// </summary>
        public string Date => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Date);

        /// <summary>
        /// Time
        /// Prefix for time literal
        /// </summary>
        public string Time => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.Time);

        /// <summary>
        /// Prefix for list literal
        /// </summary>
        public string List => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.ListBegin);

        /// <summary>
        /// Prefix for text literal
        /// </summary>
        public string Txt => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.TextBegin);

        /// <summary>
        /// Präfix für boolsche Werte
        /// </summary>
        public string Bool => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.BoolPrefix);

        /// <summary>
        /// Präfix für Integer- Werte
        /// </summary>
        public string Int => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.IntPrefix);

        /// <summary>
        /// Präfix für doppelt genaue Gleitkommawerte
        /// </summary>
        public string Dbl => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.BoolPrefix);

        /// <summary>
        /// Präfix für Naming-Ids
        /// </summary>
        public string Nid => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.NID_Prefix);

        public string PropertyWildCard => NM.Glyphs.toStr(NM.Glyphs.DocuTerms.ValueWildcard);
    }
}
