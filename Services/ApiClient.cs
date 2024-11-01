using AdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
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

        public async Task<UserXml> GetUserAsync(string userId, List<CampoDTO> data)
        {
            try
            {
                // Construye la URL usando los campos de data
                string baseUrl = data[0].pagen_tcampolibre2; // URL base de la API
                string apiKey = data[0].pagen_tcampolibre3;  // API Key
                string fullUrl = $"{baseUrl}/{userId}{apiKey}";

                // Enviar solicitud GET a la API
                HttpResponseMessage response = await _httpClient.GetAsync(fullUrl);

                if (response.IsSuccessStatusCode)
                {
                    string xmlContent = await response.Content.ReadAsStringAsync();

                    try
                    {
                        XmlSerializer serializer = new XmlSerializer(typeof(UserXml));
                        using (StringReader reader = new StringReader(xmlContent))
                        {
                            return (UserXml)serializer.Deserialize(reader);
                        }
                    }
                    catch (InvalidOperationException ex)
                    {
                        Console.WriteLine($"Error en la deserialización: {ex.Message}");
                        throw;
                    }
                }
                else
                {
                    throw new Exception($"Error en la solicitud: {response.StatusCode}");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ocurrió un error al obtener el usuario: {ex.Message}");
            }
        }

        public async Task<bool> UpdateUserAsync(UserXml user, List<CampoDTO> data)
        {
            try
            {
                string baseUrl = data[0].pagen_tcampolibre2; // API base URL
                string apiKey = data[0].pagen_tcampolibre3;  // API Key
                string userId = user.PrimaryId;
                string fullUrl = $"{baseUrl}/{userId}{apiKey}&";


                HttpResponseMessage response = await _httpClient.GetAsync(fullUrl);
                if (!response.IsSuccessStatusCode)
                {
                    Console.WriteLine($"Error obteniendo usuario: {response.StatusCode}");
                    return false;
                }

                string xmlData = await response.Content.ReadAsStringAsync();
                XDocument xmlDoc = XDocument.Parse(xmlData);

                //Actualizar xml con detalles de usuario
                xmlDoc = UpdateUserXml(xmlDoc, user);

                //string updatedXml = xmlDoc.ToString();
                //Convierte en string y adjunta cabecera
                string updatedXml = "<?xml version=\"1.0\" encoding=\"UTF-8\" standalone=\"yes\"?>\n" + xmlDoc.ToString();

                Console.WriteLine($"Actualizado XML: {updatedXml}");

                //Envia el XML actualizado
                var content = new StringContent(updatedXml, Encoding.UTF8, "application/xml");
                HttpResponseMessage putResponse = await _httpClient.PutAsync(fullUrl, content);

                if (putResponse.IsSuccessStatusCode)
                {
                    Console.WriteLine("Usuario Actualizado.");
                    return true;
                }
                else
                {
                    string errorResponse = await putResponse.Content.ReadAsStringAsync();
                    Console.WriteLine($"Error actualizando usuario: {putResponse.StatusCode}, {errorResponse}");
                    return false;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
                return false;
            }
        }

        private XDocument UpdateUserXml(XDocument xmlDoc, UserXml user)
        {
            // Actualiza el elemento "first_name" si existe en el XML con el valor del usuario
            var firstNameElement = xmlDoc.Descendants("first_name").FirstOrDefault();
            if (firstNameElement != null) firstNameElement.Value = user.FirstName;

            // Actualiza el elemento "last_name" si existe en el XML con el valor del usuario
            var lastNameElement = xmlDoc.Descendants("last_name").FirstOrDefault();
            if (lastNameElement != null) lastNameElement.Value = user.LastName;

            // Actualiza el elemento "middle_name" solo si existe y el usuario tiene un valor en MiddleName
            var middleNameElement = xmlDoc.Descendants("middle_name").FirstOrDefault();
            if (middleNameElement != null && user.MiddleName != null)
                middleNameElement.Value = user.MiddleName;

            // Actualiza los elementos de dirección en "address" si existen en el XML con los valores del usuario
            var addressElement = xmlDoc.Descendants("address").FirstOrDefault();
            if (addressElement != null)
            {
                addressElement.Element("line1")?.SetValue(user.ContactInfo.Addresses[0].Line1);
                addressElement.Element("line2")?.SetValue(user.ContactInfo.Addresses[0].Line2);
                addressElement.Element("line3")?.SetValue(user.ContactInfo.Addresses[0].Line3);
                addressElement.Element("line4")?.SetValue(user.ContactInfo.Addresses[0].Line4);
                addressElement.Element("city")?.SetValue(user.ContactInfo.Addresses[0].City);
                addressElement.Element("state_province")?.SetValue(user.ContactInfo.Addresses[0].StateProvince);
                addressElement.Element("postal_code")?.SetValue(user.ContactInfo.Addresses[0].PostalCode);
                addressElement.Element("country")?.SetValue(user.ContactInfo.Addresses[0].Country);
            }

            // Verifica si cada teléfono en "phone" contiene el tipo de teléfono, y si no, lo agrega con un valor predeterminado "home"
            var phones = xmlDoc.Descendants("phone").FirstOrDefault();
            foreach (var phone in phones.Elements("phone"))
            {
                if (!phone.Elements("phone_type").Any())
                {
                    phone.Add(new XElement("phone_types",
                        new XElement("phone_type", new XAttribute("desc", "Home"), "home")));
                }
            }

            // Verifica si cada email en "emails" contiene el tipo de email, y si no, lo agrega con un valor predeterminado "personal"
            var emails = xmlDoc.Descendants("emails").FirstOrDefault();
            foreach (var email in emails.Elements("email"))
            {
                if (!email.Elements("email_types").Any())
                {
                    email.Add(new XElement("email_types",
                        new XElement("email_type", new XAttribute("desc", "Personal"), "personal")));
                }
            }

            // Actualiza el elemento "status" si existe en el XML con el valor del usuario
            var statusElement = xmlDoc.Descendants("status").FirstOrDefault();
            if (statusElement != null)
                statusElement.Value = user.Status;

            // Devuelve el XML modificado
            return xmlDoc;
        }
    }
}
