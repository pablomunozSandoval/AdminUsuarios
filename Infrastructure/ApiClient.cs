using AdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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
        public async Task UpdateUserAsync(UserModel user)
        {
            var url = "https://api-na.hosted.exlibrisgroup.com/almaws/v1/users/" + user.PrimaryId;
            var apiKey = "tu_api_key";  // Esto puede venir de la base de datos o configuración

            var xmlData = SerializeUserToXml(user); // Método que convierte el DTO a XML

            var content = new StringContent(xmlData, Encoding.UTF8, "application/xml");

            // Realizar la solicitud PUT a la API
            var response = await _httpClient.PutAsync(url + "?apikey=" + apiKey, content);
            response.EnsureSuccessStatusCode();
        }

        // Método que serializa el UserDTO a XML
        private string SerializeUserToXml(UserModel user)
        {
            // Lógica de serialización a XML
            // Podrías usar clases como XmlSerializer para esto
            return string.Empty;
        }
    }

}
