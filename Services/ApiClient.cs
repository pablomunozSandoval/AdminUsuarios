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
    //TODO : MODIFICAR PARA SUBIR DATOS EN FORMATO XML A LA API ALMA
    public class ApiClient
    {
        private readonly HttpClient _httpClient;

        public ApiClient()
        {
            _httpClient = new HttpClient();
        }

        // Método para actualizar el usuario en la API
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



        //Metodo utilizado actualmente

        public async Task<bool> UpdateUserWithTestXmlAsync(List<CampoDTO> data)
        {
            try
            {
                var url = "https://api-na.hosted.exlibrisgroup.com/almaws/v1/users/" + data[0];
                var apiKey = data[0].Apikey;

                // XML de prueba como string
                var xmlData = @"<?xml version=""1.0"" encoding=""UTF-8""?>
                <user>
                    <record_type desc=""Public"">PUBLIC</record_type>
                    <primary_id>1659812-0</primary_id>
                    <first_name>Moises</first_name>
                    <middle_name>Mi</middle_name>
                    <last_name>Muñoz</last_name>
                    <full_name>Moises Mi Muñoz</full_name>
                    <preferred_language desc=""Español"">es</preferred_language>
                    <account_type desc=""Internal"">INTERNAL</account_type>
                    <force_password_change>false</force_password_change>
                    <status desc=""Active"">ACTIVE</status>
                    <status_date>2024-09-05Z</status_date>
                    <contact_info>
                        <addresses>
                            <address preferred=""true"" segment_type=""Internal"">
                                <line1>3598 N. Buckingham Road</line1>
                                <line2>asd</line2>
                                <line3>qwe</line3>
                                <line4>zxc</line4>
                                <city>Scottsdale</city>
                                <state_province>AZ</state_province>
                                <postal_code>85054</postal_code>
                                <country>
                                    <xml_value>USA</xml_value>
                                </country>
                                <start_date>2024-05-30Z</start_date>
                                <end_date>2024-05-30Z</end_date>
                                <address_types>
                                    <address_type>
                                        <xml_value>home</xml_value>
                                    </address_type>
                                </address_types>
                            </address>
                        </addresses>
                        <emails>
                            <email preferred=""true"" segment_type=""Internal"">
                                <email_address>m_munozn@inacap.cl</email_address>
                                <email_types>
                                    <email_type desc=""Work"">work</email_type>
                                </email_types>
                            </email>
                        </emails>
                    </contact_info>
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


