using AdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using System.Linq;

namespace AdminUsuarios.Infrastructure
{

    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        //Metodo utilizado actualmente para actualizar datos
        public async Task<bool> UpdateUserAsync(UserXml user, List <CampoDTO> data)
        {
            try
            {
                var url = "https://api-na.hosted.exlibrisgroup.com/almaws/v1/users/" + data[0].Url; // Url desde la base de datos
                var apiKey = data[0].Apikey; // ApiKey de la base de datos

                // Generar el XML dinámicamente
                string xmlData = GenerateUserXml(user);

                var content = new StringContent(xmlData, Encoding.UTF8, "application/xml");

                // Enviar la solicitud PUT a la API
                var response = await _httpClient.PutAsync(url + "?apikey=" + apiKey, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {
                return false;
            }
        }



        private string GenerateUserXml(UserXml user)
        {
            return 
        $@"<?xml version=""1.0"" encoding=""UTF-8""?>
            <user>
                <primary_id>{user.PrimaryId}</primary_id>
                <first_name>{user.FirstName}</first_name>
                <middle_name>{user.MiddleName}</middle_name>
                <last_name>{user.LastName}</last_name>
                <full_name>{user.FullName}</full_name>
                <gender>{user.Gender}</gender>
                <user_group>{user.UserGroup}</user_group>
                <preferred_language>{user.PreferredLanguage}</preferred_language>
                <expiry_date>{user.ExpiryDate}</expiry_date>
                <status>{user.Status}</status>
                <contact_info>
                    <addresses>
                        <address>
                            <line1>{user.ContactInfo.Address.Line1}</line1>
                            <line2>{user.ContactInfo.Address.Line2}</line2>
                            <line3>{user.ContactInfo.Address.Line3}</line3>
                            <line4>{user.ContactInfo.Address.Line4}</line4>
                            <city>{user.ContactInfo.Address.City}</city>
                            <state_province>{user.ContactInfo.Address.StateProvince}</state_province>
                            <postal_code>{user.ContactInfo.Address.PostalCode}</postal_code>
                            <country>{user.ContactInfo.Address.Country}</country>
                        </address>
                    </addresses>
                    <emails>
                        <email>
                            <email_address>{user.ContactInfo.Email.EmailAddress}</email_address>
                        </email>
                    </emails>
                    <phones>
                        {string.Join("\n", user.ContactInfo.Phones.Select(p => $@"
                        <phone>
                            <phone_number>{p.PhoneNumber}</phone_number>
                            <phone_type>{p.PhoneType}</phone_type>
                        </phone>"))}
                    </phones>
                </contact_info>
                <user_statistics>
                    {string.Join("\n", user.UserStatistics.Select(s => $@"
                    <user_statistic>
                        <statistic_category>{s.StatisticCategory}</statistic_category>
                        <category_type>{s.CategoryType}</category_type>
                    </user_statistic>"))}
                </user_statistics>
                <proxy_for_users></proxy_for_users>
            </user>";
        }


    }

}


