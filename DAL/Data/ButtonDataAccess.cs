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

                // Initialize the parameter array
                IDataParameter[] param = new IDataParameter[1];

                // Set up the outCur parameter as a RefCursor with Output direction
                param[0] = new OracleParameter
                {
                    ParameterName = "outCur",
                    OracleDbType = OracleDbType.RefCursor,
                    Direction = ParameterDirection.Output
                };

                // Execute the stored procedure
                try
                {
                    List<T> lstObjeto = _ExecuteStoredProcedure("PKG_INTEGRACION_ALMA.getUrl", ref param);
                    return lstObjeto;
                }
                catch (OracleException oex)
                {
                    Console.WriteLine($"OracleException: {oex.Message}");
                    throw new Exception("Oracle error occurred during stored procedure execution.");
                }
                catch (NullReferenceException nex)
                {
                    Console.WriteLine($"NullReferenceException: {nex.Message}");
                    throw new Exception("A null reference was encountered during execution.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"General Exception: {ex.Message}");
                    throw new Exception("General error occurred while executing stored procedure.");
                }
            }            
    }

}
