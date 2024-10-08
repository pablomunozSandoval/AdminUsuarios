using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("user")]
public class UserXml
{
    [XmlElement("record_type")]
    public RecordType RecordType { get; set; }

    [XmlElement("primary_id")]
    public string PrimaryId { get; set; }

    [XmlElement("first_name")]
    public string FirstName { get; set; }

    [XmlElement("middle_name")]
    public string MiddleName { get; set; }

    [XmlElement("last_name")]
    public string LastName { get; set; }

    [XmlElement("full_name")]
    public string FullName { get; set; }

    [XmlElement("preferred_language")]
    public PreferredLanguage PreferredLanguage { get; set; }

    [XmlElement("account_type")]
    public AccountType AccountType { get; set; }

    [XmlElement("force_password_change")]
    public bool ForcePasswordChange { get; set; } 

    [XmlElement("status")]
    public Status UserStatus { get; set; }

    [XmlElement("contact_info")]
    public ContactInfo ContactInfo { get; set; }
}

public class RecordType
{
    [XmlAttribute("desc")]
    public string Description { get; set; }

    [XmlText]
    public string Value { get; set; }
}

public class PreferredLanguage
{
    [XmlAttribute("desc")]
    public string Description { get; set; }

    [XmlText]
    public string Value { get; set; }
}

public class AccountType
{
    [XmlAttribute("desc")]
    public string Description { get; set; }

    [XmlText]
    public string Value { get; set; }
}

public class Status
{
    [XmlAttribute("desc")]
    public string Description { get; set; }

    [XmlText]
    public string Value { get; set; }
}

public class ContactInfo
{
    [XmlElement("emails")]
    public Emails Emails { get; set; }

    [XmlElement("addresses")]
    public Addresses Addresses { get; set; }
}

public class Emails
{
    [XmlElement("email")]
    public List<Email> EmailList { get; set; }
}

public class Email
{
    [XmlElement("email_address")]
    public string EmailAddress { get; set; }

    [XmlElement("preferred")]
    public bool Preferred { get; set; } 

    [XmlElement("segment_type")]
    public string SegmentType { get; set; } 
}

public class Addresses
{
    [XmlElement("address")]
    public List<Address> AddressList { get; set; }
}

public class Address
{
    [XmlElement("line1")]
    public string Line1 { get; set; }

    [XmlElement("city")]
    public string City { get; set; }

    [XmlElement("postal_code")]
    public string PostalCode { get; set; }

    [XmlElement("preferred")]
    public bool Preferred { get; set; } 

    [XmlElement("segment_type")]
    public string SegmentType { get; set; } 
}
