using Backend.DAL;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AdminUsuarios.DAL.Datos
{
    public class ButtonDataAccess<T> : Oracle<T> where T : new()
    {
        
        public List<T> GetUrl()
        {
            try
            {
                // Define the output parameter for the cursor
                IDataParameter[] param = new IDataParameter[1];
                param[0] = new OracleParameter("outCur", OracleDbType.RefCursor);
                param[0].Direction = ParameterDirection.Output;

                // Call the stored procedure with the output parameter
                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_INTEGRACION_ALMA.getUrl", ref param);              

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
