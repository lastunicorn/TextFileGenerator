﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.17929
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.0.30319.17929.
// 
namespace DustInTheWind.TextFileGenerator.Serialization {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://alez.ro/TextFileGenerator", IsNullable=false)]
    public partial class textFileGenerator {
        
        private section[] sectionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("section")]
        public section[] section {
            get {
                return this.sectionField;
            }
            set {
                this.sectionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://alez.ro/TextFileGenerator", IsNullable=false)]
    public partial class section {
        
        private object[] itemsField;
        
        private parameter[] parameterField;
        
        private string nameField;
        
        private string repeatField;
        
        private string separatorField;
        
        private separatorLocation separatorLocationField;
        
        public section() {
            this.repeatField = "1";
            this.separatorField = "";
            this.separatorLocationField = separatorLocation.Infix;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("section", typeof(section))]
        [System.Xml.Serialization.XmlElementAttribute("text", typeof(string))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("parameter")]
        public parameter[] parameter {
            get {
                return this.parameterField;
            }
            set {
                this.parameterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        [System.ComponentModel.DefaultValueAttribute("1")]
        public string repeat {
            get {
                return this.repeatField;
            }
            set {
                this.repeatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string separator {
            get {
                return this.separatorField;
            }
            set {
                this.separatorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(separatorLocation.Infix)]
        public separatorLocation separatorLocation {
            get {
                return this.separatorLocationField;
            }
            set {
                this.separatorLocationField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://alez.ro/TextFileGenerator", IsNullable=false)]
    public partial class parameter {
        
        private object itemField;
        
        private string nameField;
        
        private parameterValuePersistence valuePersistenceField;
        
        public parameter() {
            this.valuePersistenceField = parameterValuePersistence.PerRequest;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("constant", typeof(parameterConstant))]
        [System.Xml.Serialization.XmlElementAttribute("counter", typeof(parameterCounter))]
        [System.Xml.Serialization.XmlElementAttribute("randomNumber", typeof(parameterRandomNumber))]
        [System.Xml.Serialization.XmlElementAttribute("randomText", typeof(parameterRandomText))]
        public object Item {
            get {
                return this.itemField;
            }
            set {
                this.itemField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(parameterValuePersistence.PerRequest)]
        public parameterValuePersistence valuePersistence {
            get {
                return this.valuePersistenceField;
            }
            set {
                this.valuePersistenceField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    public partial class parameterConstant {
        
        private string valueField;
        
        public parameterConstant() {
            this.valueField = "";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    public partial class parameterCounter {
        
        private string formatField;
        
        private string startValueField;
        
        private string stepField;
        
        public parameterCounter() {
            this.formatField = "";
            this.startValueField = "1";
            this.stepField = "1";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string format {
            get {
                return this.formatField;
            }
            set {
                this.formatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("1")]
        public string startValue {
            get {
                return this.startValueField;
            }
            set {
                this.startValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="positiveInteger")]
        [System.ComponentModel.DefaultValueAttribute("1")]
        public string step {
            get {
                return this.stepField;
            }
            set {
                this.stepField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    public partial class parameterRandomNumber {
        
        private string formatField;
        
        private string minValueField;
        
        private string maxValueField;
        
        public parameterRandomNumber() {
            this.formatField = "";
            this.minValueField = "1";
            this.maxValueField = "100";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string format {
            get {
                return this.formatField;
            }
            set {
                this.formatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("1")]
        public string minValue {
            get {
                return this.minValueField;
            }
            set {
                this.minValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("100")]
        public string maxValue {
            get {
                return this.maxValueField;
            }
            set {
                this.maxValueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    public partial class parameterRandomText {
        
        private string minLengthField;
        
        private string maxLengthField;
        
        public parameterRandomText() {
            this.minLengthField = "1";
            this.maxLengthField = "100";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("1")]
        public string minLength {
            get {
                return this.minLengthField;
            }
            set {
                this.minLengthField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("100")]
        public string maxLength {
            get {
                return this.maxLengthField;
            }
            set {
                this.maxLengthField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://alez.ro/TextFileGenerator")]
    public enum parameterValuePersistence {
        
        /// <remarks/>
        PerRequest,
        
        /// <remarks/>
        PerSectionStep,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.0.30319.17929")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://alez.ro/TextFileGenerator")]
    public enum separatorLocation {
        
        /// <remarks/>
        Prefix,
        
        /// <remarks/>
        Infix,
        
        /// <remarks/>
        Postfix,
    }
}
