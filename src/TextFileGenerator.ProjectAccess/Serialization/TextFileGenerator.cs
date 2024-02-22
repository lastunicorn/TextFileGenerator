﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

// 
// This source code was auto-generated by xsd, Version=4.8.3928.0.
// 
namespace DustInTheWind.TextFileGenerator.Serialization {
    using System.Xml.Serialization;
    
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://alez.ro/TextFileGenerator", IsNullable=false)]
    public partial class TextFileGenerator {
        
        private Section[] sectionField;
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Section")]
        public Section[] Section {
            get {
                return this.sectionField;
            }
            set {
                this.sectionField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://alez.ro/TextFileGenerator", IsNullable=false)]
    public partial class Section {
        
        private object[] itemsField;
        
        private Parameter[] parameterField;
        
        private string nameField;
        
        private string repeatField;
        
        private string separatorField;
        
        private SeparatorLocation separatorLocationField;
        
        public Section() {
            this.repeatField = "1";
            this.separatorField = "";
            this.separatorLocationField = SeparatorLocation.Infix;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Section", typeof(Section))]
        [System.Xml.Serialization.XmlElementAttribute("Text", typeof(string))]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Parameter")]
        public Parameter[] Parameter {
            get {
                return this.parameterField;
            }
            set {
                this.parameterField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        public string Name {
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
        public string Repeat {
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
        public string Separator {
            get {
                return this.separatorField;
            }
            set {
                this.separatorField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(SeparatorLocation.Infix)]
        public SeparatorLocation SeparatorLocation {
            get {
                return this.separatorLocationField;
            }
            set {
                this.separatorLocationField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    [System.Xml.Serialization.XmlRootAttribute(Namespace="http://alez.ro/TextFileGenerator", IsNullable=false)]
    public partial class Parameter {
        
        private object itemField;
        
        private string nameField;
        
        private ParameterValuePersistence valuePersistenceField;
        
        public Parameter() {
            this.valuePersistenceField = ParameterValuePersistence.PerRequest;
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlElementAttribute("Constant", typeof(ParameterConstant))]
        [System.Xml.Serialization.XmlElementAttribute("Counter", typeof(ParameterCounter))]
        [System.Xml.Serialization.XmlElementAttribute("RandomNumber", typeof(ParameterRandomNumber))]
        [System.Xml.Serialization.XmlElementAttribute("RandomText", typeof(ParameterRandomText))]
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
        public string Name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute(ParameterValuePersistence.PerRequest)]
        public ParameterValuePersistence ValuePersistence {
            get {
                return this.valuePersistenceField;
            }
            set {
                this.valuePersistenceField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    public partial class ParameterConstant {
        
        private string valueField;
        
        public ParameterConstant() {
            this.valueField = "";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string Value {
            get {
                return this.valueField;
            }
            set {
                this.valueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    public partial class ParameterCounter {
        
        private string formatField;
        
        private string startValueField;
        
        private string stepField;
        
        public ParameterCounter() {
            this.formatField = "";
            this.startValueField = "1";
            this.stepField = "1";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string Format {
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
        public string StartValue {
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
        public string Step {
            get {
                return this.stepField;
            }
            set {
                this.stepField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    public partial class ParameterRandomNumber {
        
        private string formatField;
        
        private string minValueField;
        
        private string maxValueField;
        
        public ParameterRandomNumber() {
            this.formatField = "";
            this.minValueField = "0";
            this.maxValueField = "99";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute()]
        [System.ComponentModel.DefaultValueAttribute("")]
        public string Format {
            get {
                return this.formatField;
            }
            set {
                this.formatField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("0")]
        public string MinValue {
            get {
                return this.minValueField;
            }
            set {
                this.minValueField = value;
            }
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("99")]
        public string MaxValue {
            get {
                return this.maxValueField;
            }
            set {
                this.maxValueField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.ComponentModel.DesignerCategoryAttribute("code")]
    [System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://alez.ro/TextFileGenerator")]
    public partial class ParameterRandomText {
        
        private string minLengthField;
        
        private string maxLengthField;
        
        public ParameterRandomText() {
            this.minLengthField = "1";
            this.maxLengthField = "100";
        }
        
        /// <remarks/>
        [System.Xml.Serialization.XmlAttributeAttribute(DataType="integer")]
        [System.ComponentModel.DefaultValueAttribute("1")]
        public string MinLength {
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
        public string MaxLength {
            get {
                return this.maxLengthField;
            }
            set {
                this.maxLengthField = value;
            }
        }
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://alez.ro/TextFileGenerator")]
    public enum ParameterValuePersistence {
        
        /// <remarks/>
        PerRequest,
        
        /// <remarks/>
        PerSectionStep,
    }
    
    /// <remarks/>
    [System.CodeDom.Compiler.GeneratedCodeAttribute("xsd", "4.8.3928.0")]
    [System.SerializableAttribute()]
    [System.Xml.Serialization.XmlTypeAttribute(Namespace="http://alez.ro/TextFileGenerator")]
    public enum SeparatorLocation {
        
        /// <remarks/>
        Prefix,
        
        /// <remarks/>
        Infix,
        
        /// <remarks/>
        Postfix,
    }
}
