//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VChecker
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlRoot(ElementName = "fact-ref", Namespace = "http://cpe.mitre.org/language/2.0")]
    
    public partial class FactRef1
    {
        public int FactRed1Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        public int LogicalTest1Id { get; set; }
    
        public virtual LogicalTest1 LogicalTest1 { get; set; }
    }
}
