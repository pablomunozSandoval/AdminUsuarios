using AdminUsuarios.BLL;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace AdminUsuarios.Controller
{

    //ESTO SE USA SOLO PARA EMPLEADO ES BORRABLE
    public class EmpleadoController
    {
        private HttpClient _httpClient;

        public EmpleadoController()
        {
            _httpClient = new HttpClient();
        }

        public async Task<string> AddEmpleado(Empleado empleado)
        {
            try
            {
                // Serializamos el objeto empleado a JSON
                var empleadoJson = JsonConvert.SerializeObject(empleado);
                var content = new StringContent(empleadoJson, Encoding.UTF8, "application/json");

                // Hacemos la solicitud POST
                HttpResponseMessage response = await _httpClient.PostAsync("https://reqres.in/api/users", content);

                response.EnsureSuccessStatusCode();

                // Leemos la respuesta del servidor
                string result = await response.Content.ReadAsStringAsync();
                return result; // Puedes devolver el resultado para verificar si se creó correctamente
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return $"Error al agregar empleado: {ex.Message}";
            }
        }
        public async Task<string> DeleteEmpleado(int empleadoId)
        {
            try
            {
                // Crear una solicitud DELETE
                var request = new HttpRequestMessage(HttpMethod.Delete, $"https://reqres.in/api/users/{empleadoId}");

                // Enviar la solicitud DELETE
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                // Verificar si el código de estado es 204 No Content
                if (response.StatusCode == HttpStatusCode.NoContent)
                {
                    return $"Empleado {empleadoId}, eliminado exitosamente.";
                }
                else
                {
                    // Leer el contenido de la respuesta si no es 204
                    string result = await response.Content.ReadAsStringAsync();
                    return $"Error al eliminar empleado: {result}";
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return $"Error al eliminar empleado: {ex.Message}";
            }
        }
        public async Task<string> UpdateEmpleado(Empleado empleado)
        {
            try
            {
                // Serializamos el objeto empleado a JSON
                var empleadoJson = JsonConvert.SerializeObject(empleado);
                var content = new StringContent(empleadoJson, Encoding.UTF8, "application/json");

                // Crear un HttpRequestMessage para especificar el método PATCH
                var request = new HttpRequestMessage(new HttpMethod("PATCH"), "https://reqres.in/api/users/2");
                request.Content = content;

                // Enviar la solicitud PATCH
                HttpResponseMessage response = await _httpClient.SendAsync(request);

                response.EnsureSuccessStatusCode();

                // Leer la respuesta del servidor
                string result = await response.Content.ReadAsStringAsync();
                return result; // Puedes devolver el resultado para verificar si se actualizó correctamente
            }
            catch (Exception ex)
            {
                // Manejo de errores
                return $"Error al actualizar empleado: {ex.Message}";
            }
        }



        public async Task<Empleados> GetAllEmpleados()
        {
            try
            {
                Empleados empleados = new Empleados();
                HttpResponseMessage response = await _httpClient.GetAsync("https://reqres.in/api/users");
                response.EnsureSuccessStatusCode();

                string responseJson = await
                    response.Content.ReadAsStringAsync();

                //Instalar paquete, para usar JsonCover, Newtonsoft.Json que se encuentra en los paquetes Nuget
                empleados = JsonConvert.DeserializeObject<Empleados>(responseJson);
                return empleados;

            }
            catch (Exception)
            {
                throw null;

            }
        }
    }
}
