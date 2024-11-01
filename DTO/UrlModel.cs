using AdminUsuarios.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUsuarios.Models
{
    public class UrlModel : Empleado
    {
        
        public string CampoUno {get;
        set;
        }
        public string CampoDos { get; set;}
        
    }
}
