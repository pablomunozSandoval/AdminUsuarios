using AdminUsuarios.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUsuarios.Models
{
    public class EmpleadoResult : Empleado
    {
        public DateTime CreatedAt { get; set; }
    }

}
