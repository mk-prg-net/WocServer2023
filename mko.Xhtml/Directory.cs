﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:2.0.50727.4200
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// Dieser Quellcode wurde automatisch generiert von xsd, Version=2.0.50727.3038.
// 
namespace mkoIt.Xhtml.Directory {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.mkoIt.de/Directory.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.mkoIt.de/Directory.xsd", IsNullable=false)]
    public partial class dir {
        
        private string descrField;
        
        private e[] eField;
        
        private string idField;
        
        private System.DateTime createdField;
        
        private System.DateTime modifiedField;
        
        /// <remarks/>
        public string descr {
            get {
                return this.descrField;
            }
            set {
                this.descrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("e")]
        public e[] e {
            get {
                return this.eField;
            }
            set {
                this.eField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime created {
            get {
                return this.createdField;
            }
            set {
                this.createdField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime modified {
            get {
                return this.modifiedField;
            }
            set {
                this.modifiedField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.mkoIt.de/Directory.xsd")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://www.mkoIt.de/Directory.xsd", IsNullable=false)]
    public partial class e {
        
        private string descrField;
        
        private object[] itemsField;
        
        private string idField;
        
        private string tField;
        
        private int ixField;
        
        private int verField;
        
        private System.DateTime dField;
        
        public e() {
            this.verField = 1;
        }
        
        /// <remarks/>
        public string descr {
            get {
                return this.descrField;
            }
            set {
                this.descrField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("e", typeof(e))]
        [System.Xml.Serialization.XmlElementAttribute("val", typeof(eVal))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string t {
            get {
                return this.tField;
            }
            set {
                this.tField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public int ix {
            get {
                return this.ixField;
            }
            set {
                this.ixField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(1)]
        public int ver {
            get {
                return this.verField;
            }
            set {
                this.verField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public System.DateTime d {
            get {
                return this.dField;
            }
            set {
                this.dField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "2.0.50727.3038")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://www.mkoIt.de/Directory.xsd")]
    public partial class eVal {
        
        private string parserField;
        
        private string viewerField;
        
        private string valueField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string parser {
            get {
                return this.parserField;
            }
            set {
                this.parserField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string viewer {
            get {
                return this.viewerField;
            }
            set {
                this.viewerField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlTextAttribute()]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
}