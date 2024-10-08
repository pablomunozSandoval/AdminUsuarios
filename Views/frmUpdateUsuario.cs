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

                // Usa await para esperar el resultado del método asíncrono
                bool success = await apiClient.UpdateUserWithTestXmlAsync(dataList);
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
