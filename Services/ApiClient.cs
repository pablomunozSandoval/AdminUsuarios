using AdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace AdminUsuarios.Infrastructure
{

    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        // Método para actualizar el usuario en la API, sin uso
        public async Task<bool> UpdateUserWithCampoDTOAsync(CampoDTO data)
        {
            try
            {
                var url = "https://api-na.hosted.exlibrisgroup.com/almaws/v1/users/" + data.Url;
                var apiKey = data.Apikey;

                // Crear el objeto UserXml con los datos de CampoDTO
                UserXml userXml = new UserXml
                {
                    PrimaryId = "1659812-0",
                    FirstName = "Moises",
                    LastName = "Muñoz",
                    FullName = "Moises Mi Muñoz",
                    RecordType = new RecordType { Description = "Public", Value = "PUBLIC" },
                    PreferredLanguage = new PreferredLanguage { Description = "Español", Value = "es" },
                    AccountType = new AccountType { Description = "Internal", Value = "INTERNAL" },
                    ContactInfo = new ContactInfo
                    {
                        Emails = new Emails
                        {
                            EmailList = new List<Email>
                {
                    new Email { EmailAddress = "m_munozn@inacap.cl" }
                }
                        }
                    }
                };

                // Serializar a XML

                string xmlData = SerializeUserToXml(userXml);

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


        // Método que serializa el UserDTO a XML
        private string SerializeUserToXml(UserXml user)
        {
            // Lógica de serialización a XML
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(UserXml));

            using (StringWriter textWriter = new StringWriter())
            {
                xmlSerializer.Serialize(textWriter, user);
                return textWriter.ToString();
            }
        }



        //Metodo utilizado actualmente para actualizar datos

        public async Task<bool> UpdateUserWithTestXmlAsync(List<CampoDTO> data)
        {
            try
            {
                var url = "https://api-na.hosted.exlibrisgroup.com/almaws/v1/users/" + data[0];
                var apiKey = data[0].Apikey;


                // XML de prueba como string
                var xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <user>
                    <primary_id>{1}</primary_id>
                    <first_name>{3}</first_name>
                    <middle_name></middle_name>
                    <last_name>{2}</last_name>
                    <full_name>{3} {2}</full_name>
                    <gender>{7}</gender>
                    <user_group></user_group>
                    <preferred_language>es</preferred_language>
                    <expiry_date>{25}</expiry_date>
                    <status>{26}</status>
                    <contact_info>
                        <addresses>
                            <address>
                                <line1>{8}</line1>
                                <line2>{9}</line2>
                                <line3>{10}</line3>
                                <line4>Región {11}</line4>
                                <city></city>
                                <state_province></state_province>
                                <postal_code></postal_code>
                                <country></country>
                                <address_note></address_note>
                                <address_types>
                                    <address_type>home</address_type>
                                </address_types>
                            </address>
                        </addresses>
                        <emails>
                            <email>
                                <email_address>{27}</email_address>
                                <email_types>
                                    <email_type>personal</email_type>
                                </email_types>
                            </email>
                        </emails>
                        <phones>
                            <phone>
                                <phone_number>{12}</phone_number>
                                <phone_types>
                                    <phone_type>home</phone_type>
                                </phone_types>
                            </phone>
                            <phone>
                                <phone_number>{13}</phone_number>
                                <phone_types>
                                    <phone_type>mobile</phone_type>
                                </phone_types>
                            </phone>
                        </phones>
                    </contact_info>
                    <user_statistics>
                        <user_statistic>
                            <statistic_category>{25}</statistic_category>
                            <category_type>BIBLIOTECA</category_type>
                        </user_statistic>
                    </user_statistics>
                    <proxy_for_users></proxy_for_users>
                 </user>";

                var content = new StringContent(xmlData, Encoding.UTF8, "application/xml");

                // Realizar la solicitud PUT a la API
                var response = await _httpClient.PutAsync(url + "?apikey=" + apiKey, content);

                return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }
    }

}


