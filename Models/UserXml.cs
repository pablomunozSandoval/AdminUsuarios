using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[XmlRoot("user")]
public class UserXml
{
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

    [XmlElement("gender")]
    public string Gender { get; set; }

    [XmlElement("user_group")]
    public string UserGroup { get; set; }

    [XmlElement("preferred_language")]
    public string PreferredLanguage { get; set; }

    [XmlElement("status")]
    public string Status { get; set; }

    [XmlElement("contact_info")]
    public ContactInfo ContactInfo { get; set; }

    [XmlElement("expiry_date")]
    public string ExpiryDate { get; set; }

    [XmlArray("user_statistics")]
    [XmlArrayItem("user_statistic")]
    public List<UserStatistic> UserStatistics { get; set; }
}

public class ContactInfo
{
    [XmlArray("addresses")]
    [XmlArrayItem("address")]
    public List<Address> Addresses { get; set; }

    [XmlArray("emails")]
    [XmlArrayItem("email")]
    public List<Email> Emails { get; set; }

    [XmlArray("phones")]
    [XmlArrayItem("phone")]
    public List<Phone> Phones { get; set; }
}

public class Address
{
    [XmlElement("line1")]
    public string Line1 { get; set; }

    [XmlElement("line2")]
    public string Line2 { get; set; }

    [XmlElement("line3")]
    public string Line3 { get; set; }

    [XmlElement("line4")]
    public string Line4 { get; set; }

    [XmlElement("city")]
    public string City { get; set; }

    [XmlElement("state_province")]
    public string StateProvince { get; set; }

    [XmlElement("postal_code")]
    public string PostalCode { get; set; }

    [XmlElement("country")]
    public string Country { get; set; }
}

public class Email
{
    [XmlElement("email_address")]
    public string EmailAddress { get; set; }

    [XmlArray("email_types")]
    [XmlArrayItem("email_type")]
    public List<string> EmailTypes { get; set; }
}

public class Phone
{
    [XmlElement("phone_number")]
    public string PhoneNumber { get; set; }

    [XmlArray("phone_types")]
    [XmlArrayItem("phone_type")]
    public List<string> PhoneTypes { get; set; }
}

public class UserStatistic
{
    [XmlElement("statistic_category")]
    public string StatisticCategory { get; set; }

    [XmlElement("category_type")]
    public string CategoryType { get; set; }
}

