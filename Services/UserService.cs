using AdminUsuarios.DAL.Datos;
using AdminUsuarios.Models;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace AdminUsuarios.Services
{
    public class UserService
    {

        public UserService() { }

        public List<CampoDTO> getUrlService()
        {
            try
            {
                // Solicitud de datos a Oracle
                ButtonDataAccess<CampoDTO> data = new ButtonDataAccess<CampoDTO>();
                List<CampoDTO> dataList = data.GetUrl();
                return dataList; // Retornamos los datos obtenidos
            }
            catch (Exception ex)
            {
                // Mostrar el error en un MessageBox
                MessageBox.Show($"Error al buscar los datos: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return new List<CampoDTO>();

            }
        }
    }
}


