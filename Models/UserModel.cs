using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUsuarios.Models
{
    //ESTE ES EL UNICO QUE USO PARA LA API ALMA LO DEMAS ES "BORRABLE"

    //TODO : MODIFICAR PARA RECIBIR DATOS DE LA BASE DE DATOS
    public class UserModel
    {
        public string PrimaryId { get; set; }
        public string ContactInfo { get; set; }
        public string Address { get; set; }
    }
}
