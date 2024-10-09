using System.Collections.Generic;

public class UserXml
{
    public string PrimaryId { get; set; }
    public string FirstName { get; set; }
    public string MiddleName { get; set; }
    public string LastName { get; set; }
    public string FullName { get; set; }
    public string Gender { get; set; }
    public string UserGroup { get; set; }
    public string PreferredLanguage { get; set; }
    public string ExpiryDate { get; set; }
    public string Status { get; set; }
    public ContactInfo ContactInfo { get; set; }
    public List<UserStatistic> UserStatistics { get; set; }

    // Constructor por defecto
    public UserXml()
    {
        // Valores por defecto o de ejemplo
        PrimaryId = "1659812-0";
        FirstName = "Moises";
        MiddleName = "Ignacio";
        LastName = "Muñoz";
        FullName = "Moises Ignacio Muñoz";
        Gender = "Male";
        UserGroup = "Faculty";
        PreferredLanguage = "es";
        ExpiryDate = "2025-12-31";
        Status = "Active";
        ContactInfo = new ContactInfo
        {
            Address = new Address
            {
                Line1 = "Calle Principal 123",
                Line2 = "Edificio B",
                Line3 = "Depto. 5",
                Line4 = "Región Metropolitana",
                City = "Santiago",
                StateProvince = "RM",
                PostalCode = "8320000",
                Country = "Chile"
            },
            Email = new Email
            {
                EmailAddress = "m_munozn@inacap.cl"
            },
            Phones = new List<Phone>
            {
                new Phone { PhoneNumber = "+56 9 8765 4321", PhoneType = "home" },
                new Phone { PhoneNumber = "+56 9 1234 5678", PhoneType = "mobile" }
            }
        };
        UserStatistics = new List<UserStatistic>
        {
            new UserStatistic { StatisticCategory = "General", CategoryType = "BIBLIOTECA" }
        };
    }
}

public class ContactInfo
{
    public Address Address { get; set; }
    public Email Email { get; set; }
    public List<Phone> Phones { get; set; }
}

public class Address
{
    public string Line1 { get; set; }
    public string Line2 { get; set; }
    public string Line3 { get; set; }
    public string Line4 { get; set; }
    public string City { get; set; }
    public string StateProvince { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
}

public class Email
{
    public string EmailAddress { get; set; }
}

public class Phone
{
    public string PhoneNumber { get; set; }
    public string PhoneType { get; set; }
}

public class UserStatistic
{
    public string StatisticCategory { get; set; }
    public string CategoryType { get; set; }
}
