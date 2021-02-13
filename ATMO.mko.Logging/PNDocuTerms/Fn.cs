using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ATMO.mko.Logging.PNDocuTerms
{
    /// <summary>
    /// mko, 2018
    /// 
    /// mko, 8.6.2020
    /// Prefixe für Boolean, Int, Double und MKPRG.Naming NID's hinzugefügt
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

        public string ListEnd => NamePrefix + ".";

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
        public string Instance => NamePrefix + "i";

        /// <summary>
        /// Method
        /// A method documents a method- or function call an the results of them.
        /// It contains instances, properties and events
        /// </summary>
        public string Method => NamePrefix + "m";


        public string Function => NamePrefix + "f";


        /// <summary>
        /// Return 
        /// A return block describes the result of a function- or method call. 
        /// </summary>
        public string Return => NamePrefix + "r";        


        /// <summary>
        /// Property
        /// Assignes a name to a portion of information.
        /// A portion of information can be a text, a list or a instance.
        /// </summary>
        public string Property => NamePrefix + "p";

        public string PropertySet => NamePrefix + "p_set";
        

        /// <summary>
        /// Version
        /// Defines a version numeber for a business object like instances.
        /// The version number consists of thre parts: main, sub and build- number.
        /// The parts are separated with points (i.e. 1.2.3).
        /// </summary>
        public string Version => NamePrefix + "v";


        /// <summary>
        /// Event
        /// An Event can indicate the success of an operation on an business object. 
        /// The structure of an event is equivalent to the structure of a property: #e name value.
        /// The name is often an indicator for success: succeded, failed, warn, ... se DocuEntityHlp.MapStringToEventType
        /// </summary>
        public string Event => NamePrefix + "e";


        /// <summary>
        /// Date
        /// Prefix for date literal
        /// </summary>
        public string Date => NamePrefix + "d";

        /// <summary>
        /// Time
        /// Prefix for time literal
        /// </summary>
        public string Time => NamePrefix + "t";
        
        /// <summary>
        /// Prefix for list literal
        /// </summary>
        public string List => NamePrefix + "_";

        /// <summary>
        /// Prefix for text literal
        /// </summary>
        public string Txt => $"{NamePrefix}$";

        /// <summary>
        /// Präfix für boolsche Werte
        /// </summary>
        public string Bool => "";

        /// <summary>
        /// Präfix für Integer- Werte
        /// </summary>
        public string Int => "";

        /// <summary>
        /// Präfix für doppelt genaue Gleitkommawerte
        /// </summary>
        public string Dbl => $"{NamePrefix}D";

        /// <summary>
        /// Präfix für Naming-Ids
        /// </summary>
        public string Nid => $"{NamePrefix}NID";

        public string PropertyWildCard => $"{NamePrefix}*";
    }
}
