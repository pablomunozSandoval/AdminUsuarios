using AdminUsuarios.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using AdminUsuarios.Models

namespace AdminUsuarios.Services
{
    public class UserService
    {
        private readonly ApiClient _apiClient;

        public UserService(ApiClient apiClient)
        {
            _apiClient = apiClient;
        }

        public async Task UpdateUser(UserModel user)
        {
            string apiUrl = "https://api-na.hosted.exlibrisgroup.com/almaws/v1/users/" + user.PrimaryId;
            string apiKey = "apikey"; // Recuperar la API key desde la base de datos.

            // Convierte el UserDTO en XML.
            string userXml = ConvertUserToXml(user);

            // Llama al método en ApiClient para actualizar el usuario.
            HttpResponseMessage response = await _apiClient.UpdateUserAsync(apiUrl, apiKey, userXml);

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al actualizar el usuario.");
            }
        }

        private string ConvertUserToXml(UserModel user)
        {
            // Aquí iría la conversión de UserDTO a XML.
            // Usualmente usando un serializador o plantilla XML.
            return string.Empty;

        }
    }

}
