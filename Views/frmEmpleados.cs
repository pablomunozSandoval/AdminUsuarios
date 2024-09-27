using AdminUsuarios.BLL;
using AdminUsuarios.Controller;
using AdminUsuarios.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AdminUsuarios.PL
{
    public partial class frmEmpleados : Form
    {
        private EmpleadoController EmpleadosController;
        private Empleados empleados;

        public frmEmpleados()
        {
            InitializeComponent();
            empleados = new Empleados();
            EmpleadosController = new EmpleadoController();
            GetEmpleados();
        }


        //Seleccionar la informacion y enviarla al controlador
        //Muestra la informacion devuelta por el controlador
        private async void AddEmpleado(Empleado empleado)
        {
            var empleadoResultJson = await EmpleadosController.AddEmpleado(empleado);
            EmpleadoResult empleadoResult = JsonConvert.DeserializeObject<EmpleadoResult>(empleadoResultJson);
            string message = $"Empleado creado:\n" +
                $"ID: {empleadoResult.Id}\n" +
                $"Nombre: {empleadoResult.First_Name} {empleadoResult.Last_Name}\n" +
                $"Email: {empleadoResult.Email}\n" +
                $"Creado el: {empleadoResult.CreatedAt.ToString("g")}";

            MessageBox.Show(message);
        }
        private async void UpdateEmpleado(Empleado empleado)
        {
            var empleadoResultJason = await EmpleadosController.UpdateEmpleado(empleado);
            EmpleadoUpdate empleadoUpdate = JsonConvert.DeserializeObject<EmpleadoUpdate>(empleadoResultJason);
            string message = $"Empleado creado:\n" +
                $"ID: {empleadoUpdate.Id}\n" +
                $"Nombre: {empleadoUpdate.First_Name} {empleadoUpdate.Last_Name}\n" +
                $"Email: {empleadoUpdate.Email}\n" +
                $"Actualizado el: {empleadoUpdate.UpdatedAt.ToString("g")}";

            MessageBox.Show(message);
        }
        private async void DeleteEmpleado(int id)
        {
            var empleadoDelete = await EmpleadosController.DeleteEmpleado(id);

            MessageBox.Show(empleadoDelete);
        }


        //Obtiene todos los datos de los empleados desde la Api
        //por medio del controlador|
        private async void GetEmpleados()
        {
            // Limpiar el DataGridView antes de agregar nuevas filas
            dgvEmpleados.Rows.Clear();

            empleados = await EmpleadosController.GetAllEmpleados();

            if (empleados != null)
            {
                foreach (var empleado in empleados?.data)
                {
                    DataGridViewRow row = new DataGridViewRow();
                    row.CreateCells(dgvEmpleados);

                    row.Cells[0].Value = empleado.Id;
                    row.Cells[1].Value = empleado.First_Name;
                    row.Cells[2].Value = empleado.Last_Name;
                    row.Cells[3].Value = empleado.Email;
                    row.Cells[4].Value = empleado.Avatar;

                    dgvEmpleados.Rows.Add(row);
                }
            }
            else
            {
                MessageBox.Show("No se pudo obtener la petición.", "Error",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void limpiarForm()
        {
            txtApellido.Clear();
            txtNombre.Clear();
            txtId.Clear();
            txtCorreo.Clear();
        }
        private Empleado RecuperarInformacion()
        {
            Empleado oEmpleado = new Empleado();
            int id = 0; int.TryParse(txtId.Text, out id);
            oEmpleado.Id = id;

            oEmpleado.First_Name = txtNombre.Text;
            oEmpleado.Last_Name = txtApellido.Text;
            oEmpleado.Email = txtCorreo.Text;    
            
            return oEmpleado;
        }

        //PictureBox
        private async void CargarImagen(string url)
        {
            try
            {
                // Crear un HttpClient para descargar la imagen
                using (HttpClient client = new HttpClient())
                {
                    // Descargar la imagen como un flujo de bytes
                    var imageBytes = await client.GetByteArrayAsync(url);

                    // Crear un MemoryStream a partir del flujo de bytes
                    using (var stream = new MemoryStream(imageBytes))
                    {
                        // Establecer la imagen en el PictureBox
                        pBoxAvatar.Image = Image.FromStream(stream);
                    }
                }
            }
            catch (Exception ex)
            {
                // Manejo de errores
                MessageBox.Show($"Error al cargar la imagen: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //Botones
        private void btnAgregar_Click(object sender, EventArgs e)
        {
            Empleado empleado = new Empleado();
            empleado=RecuperarInformacion();
            AddEmpleado(empleado);
            GetEmpleados();
            limpiarForm();           
        }        
        private void btnModificar_Click(object sender, EventArgs e)
        {
            Empleado empleado = new Empleado();
            empleado = RecuperarInformacion();
            UpdateEmpleado(empleado);
            GetEmpleados();
            limpiarForm();
        }
        private void btnEliminar_Click(object sender, EventArgs e)
        {
            int id = 0; int.TryParse( txtId.Text, out id);
            DeleteEmpleado(id);
            GetEmpleados();
            limpiarForm();

        }


        //CellClick DataGridView
        private void dgvEmpleados_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            // Asegúrate de que el índice de la fila seleccionada sea válido
            if (e.RowIndex >= 0)
            {
                // Obtén la fila seleccionada
                DataGridViewRow row = dgvEmpleados.Rows[e.RowIndex];

                // Asigna los valores de las celdas a los Label
                txtId.Text = row.Cells["Id"].Value.ToString();
                txtNombre.Text = row.Cells["Nombre"].Value.ToString();
                txtApellido.Text = row.Cells["Apellido"].Value.ToString();
                txtCorreo.Text = row.Cells["Correo"].Value.ToString();
                string url = row.Cells["Avatar"].Value.ToString();
                CargarImagen(url);


            }
        }
    }
}
