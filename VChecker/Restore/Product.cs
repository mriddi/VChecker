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
    
    [XmlRoot(ElementName = "product", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
    
    public partial class Product
    {
        public int ProductId { get; set; }
        [XmlText]
        public string ProductN { get; set; }
        public int VulnerableSoftwareListId { get; set; }
    
        public virtual VulnerableSoftwareList VulnerableSoftwareList { get; set; }
    }
}
