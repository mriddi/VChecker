using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace XmlD
{
    [XmlRoot(ElementName = "fact-ref", Namespace = "http://cpe.mitre.org/language/2.0")]
    public partial class FactRef2
    {
        public int FactRef2Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        public int LogicalTest2Id { get; set; }

        public virtual LogicalTest2 LogicalTest2 { get; set; }
    }

    [XmlRoot(ElementName = "fact-ref", Namespace = "http://cpe.mitre.org/language/2.0")]
    public partial class FactRef1
    {
        public int FactRed1Id { get; set; }
        [XmlAttribute(AttributeName = "name")]
        public string Name { get; set; }
        public int LogicalTest1Id { get; set; }

        public virtual LogicalTest1 LogicalTest1 { get; set; }
    }

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
        public int NvdId { get; set; }

        [XmlElement(ElementName = "references", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<References> References { get; set; }
        public virtual Nvd Nvd { get; set; }
        [XmlElement(ElementName = "vulnerable-configuration", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<VulnerableConfiguration> VulnerableConfiguration { get; set; }
        [XmlElement(ElementName = "vulnerable-software-list", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<VulnerableSoftwareList> VulnerableSoftwareList { get; set; }
    }

    [XmlRoot(ElementName = "vulnerable-software-list", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
    public partial class VulnerableSoftwareList
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VulnerableSoftwareList()
        {
            this.Product = new List<Product>();
        }

        public int VulnerableSoftwareListId { get; set; }
        public string EntryId { get; set; }

        [XmlElement(ElementName = "product", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Product> Product { get; set; }
        public virtual Entry Entry { get; set; }
    }

    [XmlRoot(ElementName = "vulnerable-configuration", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]

    public partial class VulnerableConfiguration
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public VulnerableConfiguration()
        {
            this.LogicalTest1 = new List<LogicalTest1>();
        }

        public int VulnerableConfigurationId { get; set; }
        public string EntryId { get; set; }

        public virtual Entry Entry { get; set; }
        [XmlElement(ElementName = "logical-test", Namespace = "http://cpe.mitre.org/language/2.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<LogicalTest1> LogicalTest1 { get; set; }
    }

    [XmlRoot(ElementName = "references", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]

    public partial class References
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public References()
        {
            this.Reference = new List<Reference>();
        }

        public int ReferencesId { get; set; }
        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }
        [XmlAttribute(AttributeName = "reference_type")]
        public string ReferenceType { get; set; }
        public string EntryId { get; set; }
        [XmlElement(ElementName = "source", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        public string Source { get; set; }

        public virtual Entry Entry { get; set; }
        [XmlElement(ElementName = "reference", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Reference> Reference { get; set; }
    }

    [XmlRoot(ElementName = "reference", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]
    public partial class Reference
    {
        public int ReferenceId { get; set; }
        [XmlAttribute(AttributeName = "href")]
        public string Href { get; set; }
        [XmlAttribute(AttributeName = "lang", Namespace = "http://www.w3.org/XML/1998/namespace")]
        public string Lang { get; set; }
        [XmlText]
        public string Text { get; set; }
        public int ReferencesId { get; set; }

        public virtual References References { get; set; }
    }

    [XmlRoot(ElementName = "nvd", Namespace = "http://scap.nist.gov/schema/feed/vulnerability/2.0")]
    public partial class Nvd
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Nvd()
        {
            this.Entry = new List<Entry>();
        }

        public int NvdId { get; set; }
        [XmlAttribute(AttributeName = "pub_date")]
        public string PubDate { get; set; }

        [XmlElement(ElementName = "entry", Namespace = "http://scap.nist.gov/schema/feed/vulnerability/2.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<Entry> Entry { get; set; }
    }

    [XmlRoot(ElementName = "product", Namespace = "http://scap.nist.gov/schema/vulnerability/0.4")]

    public partial class Product
    {
        public int ProductId { get; set; }
        [XmlText]
        public string ProductN { get; set; }
        public int VulnerableSoftwareListId { get; set; }

        public virtual VulnerableSoftwareList VulnerableSoftwareList { get; set; }
    }

    [XmlRoot(ElementName = "logical-test", Namespace = "http://cpe.mitre.org/language/2.0")]

    public partial class LogicalTest1
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LogicalTest1()
        {
            this.FactRef1 = new List<FactRef1>();
            this.LogicalTest2 = new List<LogicalTest2>();
        }

        public int LogicalTest1Id { get; set; }
        [XmlAttribute(AttributeName = "negate")]
        public string Negate1 { get; set; }
        [XmlAttribute(AttributeName = "operator")]
        public string Operator1 { get; set; }
        public int VulnerableConfigurationId { get; set; }

        [XmlElement(ElementName = "fact-ref", Namespace = "http://cpe.mitre.org/language/2.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<FactRef1> FactRef1 { get; set; }
        [XmlElement(ElementName = "logical-test", Namespace = "http://cpe.mitre.org/language/2.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<LogicalTest2> LogicalTest2 { get; set; }
        public virtual VulnerableConfiguration VulnerableConfiguration { get; set; }
    }

    [XmlRoot(ElementName = "logical-test", Namespace = "http://cpe.mitre.org/language/2.0")]

    public partial class LogicalTest2
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LogicalTest2()
        {
            this.FactRef2 = new List<FactRef2>();
        }

        public int LogicalTest2Id { get; set; }
        [XmlAttribute(AttributeName = "negate")]
        public string Negate2 { get; set; }
        [XmlAttribute(AttributeName = "operator")]
        public string Operator2 { get; set; }
        public int LogicalTest1Id { get; set; }

        public virtual LogicalTest1 LogicalTest1 { get; set; }
        [XmlElement(ElementName = "fact-ref", Namespace = "http://cpe.mitre.org/language/2.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual List<FactRef2> FactRef2 { get; set; }
    }
}
