using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MKPRG.Tracing.DocuTerms.Parser
{
    /// <summary>
    /// mko, 2018
    /// 
    /// mko, 8.6.2020
    /// Prefixe für Boolean, Int, Double und ATMO.DFC.Naming NID's hinzugefügt
    /// </summary>
    public class Fn : IFn
    {
        /// <summary>
        /// Singleton
        /// </summary>
        public static Fn _ {
            get
            {
                if(_instance == null)
                {
                    _instance = new Fn();
                }
                return _instance;
            }
        }
        static Fn _instance;

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

        public string ListEnd => "⟩";

        public string NamePrefix => "#";

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
        public string Instance => "⌸";

        /// <summary>
        /// Method
        /// A method documents a method- or function call an the results of them.
        /// It contains instances, properties and events
        /// </summary>
        public string Method => "↴";


        public string Function => "𝑓";


        /// <summary>
        /// Return 
        /// A return block describes the result of a function- or method call. 
        /// </summary>
        public string Return => "⤣";        


        /// <summary>
        /// Property
        /// Assignes a name to a portion of information.
        /// A portion of information can be a text, a list or a instance.
        /// </summary>
        public string Property => "⦾";

        public string PropertySet => NamePrefix + "p_set";
        

        /// <summary>
        /// Version
        /// Defines a version numeber for a business object like instances.
        /// The version number consists of thre parts: main, sub and build- number.
        /// The parts are separated with points (i.e. 1.2.3).
        /// </summary>
        public string Version => "𝑣";


        /// <summary>
        /// Event
        /// An Event can indicate the success of an operation on an business object. 
        /// The structure of an event is equivalent to the structure of a property: #e name value.
        /// The name is often an indicator for success: succeded, failed, warn, ... se DocuEntityHlp.MapStringToEventType
        /// </summary>
        public string Event => "🚨";


        /// <summary>
        /// Date
        /// Prefix for date literal
        /// </summary>
        public string Date => "📅";

        /// <summary>
        /// Time
        /// Prefix for time literal
        /// </summary>
        public string Time => "⏱";
        
        /// <summary>
        /// Prefix for list literal
        /// </summary>
        public string List => "⟨";

        /// <summary>
        /// Prefix for text literal
        /// </summary>
        public string Txt => $"$⟨";

        /// <summary>
        /// Präfix für boolsche Werte
        /// </summary>
        public string Bool => $"𝔹";

        /// <summary>
        /// Präfix für Integer- Werte
        /// </summary>
        public string Int => "";

        /// <summary>
        /// Präfix für doppelt genaue Gleitkommawerte
        /// </summary>
        public string Dbl => $"";

        /// <summary>
        /// Präfix für Naming-Ids
        /// </summary>
        public string Nid => $"𝔑";

        public string PropertyWildCard => $"{NamePrefix}*";
    }
}
