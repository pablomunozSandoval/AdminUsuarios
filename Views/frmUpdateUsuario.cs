using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using


using AdminUsuarios.DAL.Datos;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.Window;

namespace AdminUsuarios.PL
{
    public partial class btnBuscarDatos : Form
    {
        public btnBuscarDatos()
        {
            InitializeComponent();
        }

        private void btnClickBuscar(object sender, EventArgs e)
        {
            try
            {
                //haces la solicitud de datos select a oracle 
                da_Button<> buscarOracle = new da_Button();
                //obtiene los datos 
                //los pasa a ApiClient
                //apiClient hace el update



            }
            

        }
    }
}
