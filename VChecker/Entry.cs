//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace VChecker
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;
    [XmlRoot(ElementName = "entry", Namespace = "http://scap.nist.gov/schema/feed/vulnerability/2.0")]
    public partial class Entry
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Entry()
        {
            this.References = new List<References>();
            this.VulnerableConfiguration = new List<VulnerableConfiguration>();
            this.VulnerableSoftwareList = new List<VulnerableSoftwareList>();
        }
        [XmlAttribute(AttributeName = "id")]
        public string EntryId { get; set; }
        [XmlElement(ElementName = "summary", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        public string Summary { get; set; }
        [XmlElement(ElementName = "last-modified-datetime", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        public string LastmodifiedDateTime { get; set; }
        [XmlElement(ElementName = "published-datetime", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        public string PublishedDateTime { get; set; }
        [XmlAttribute(AttributeName = "pub_date")]
        public string NvdPubDate { get; set; }

        [XmlElement(ElementName = "nvd", Namespace = "http://scap.nist.gov/schema/feed/vulnerability/2.0")]
        public virtual Nvd Nvd { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [XmlElement(ElementName = "references", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        public virtual List<References> References { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        [XmlElement(ElementName = "vulnerable-configuration", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        public virtual List<VulnerableConfiguration> VulnerableConfiguration { get; set; }
        [XmlElement(ElementName = "vulnerable-software-list", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<VulnerableSoftwareList> VulnerableSoftwareList { get; set; }
    }
}
