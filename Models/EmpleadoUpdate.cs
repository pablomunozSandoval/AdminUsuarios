using AdminUsuarios.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUsuarios.Models
{
    public class EmpleadoUpdate : Empleado
    {
        public DateTime UpdatedAt { get; set; }
    }
}
