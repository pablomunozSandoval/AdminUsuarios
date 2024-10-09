using AdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using AdminUsuarios.Services;
using AdminUsuarios.Infrastructure;

namespace AdminUsuarios.Views
{
    public partial class frmUpdateUsuario : Form
    {
        public frmUpdateUsuario()
        {
            InitializeComponent();
        }

        // Cambia el método a asincrónico añadiendo async
        private async void btnClickBuscar(object sender, EventArgs e)
        {
            try
            {
                // Haces la solicitud de datos select a Oracle 
                UserService userService = new UserService();

                // Obtiene los datos de la API
                List<CampoDTO> dataList = userService.getUrl();

                // Pasa los datos a ApiClient
                ApiClient apiClient = new ApiClient();

                //Datos de UserXML a modificar

                UserXml user = new UserXml
                {
                    // Datos actuales, ya precargados o definidos
                    PrimaryId = "1659812-0",
                    FirstName = "Moises",
                    LastName = "Muñoz",
                    Gender = "Male",
                    ContactInfo = new ContactInfo
                    {
                        Address = new Address
                        {
                            Line1 = "Calle Principal 123",
                            Line2 = "Edificio B",
                            Line3 = "Depto. 5",
                            Line4 = "Región Metropolitana",
                            City = "Santiago", // Valor que vamos a cambiar
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
                    },
                    ExpiryDate = "2025-12-31",
                    Status = "Active",
                    UserStatistics = new List<UserStatistic>
                        {
                            new UserStatistic { StatisticCategory = "General", CategoryType = "BIBLIOTECA" }
                        }
                };
                // Modificar solo la ciudad
                user.ContactInfo.Address.City = "Nuevo Santiago";

                // Luego puedes pasar el objeto 'user' actualizado al método de actualización de la API
               bool success = await apiClient.UpdateUserAsync(user, dataList);

                // Usa await para esperar el resultado del método asíncrono
                if (success)
                {
                    MessageBox.Show("Usuario actualizado exitosamente.", "Éxito", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    MessageBox.Show("Error al actualizar el usuario.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                // Manejo de excepciones no controladas
                MessageBox.Show($"Ocurrió un error inesperado: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
