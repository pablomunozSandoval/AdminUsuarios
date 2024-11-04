using Backend.DAL;
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;


namespace AdminUsuarios.DAL.Datos
{
    public class ButtonDataAccess<T> : Oracle<T> where T : new()
    {
        public List<T> GetUrl()
        {
            // Inicializa el array de parámetros
            IDataParameter[] param = new IDataParameter[1];

            // Configura el parámetro outCur como RefCursor con dirección Output
            param[0] = new OracleParameter
            {
                ParameterName = "outCur",
                OracleDbType = OracleDbType.RefCursor,
                Direction = ParameterDirection.Output
            };

            // Ejecuta el procedimiento almacenado
            try
            {
                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_INTEGRACION_ALMA.getUrl", ref param);
                return lstObjeto;
            }
            catch (OracleException oex)
            {
                Console.WriteLine($"OracleException: {oex.Message}");
                throw new Exception("Ocurrió un error de Oracle durante la ejecución del procedimiento almacenado.");
            }
            catch (NullReferenceException nex)
            {
                Console.WriteLine($"NullReferenceException: {nex.Message}");
                throw new Exception("Se encontró una referencia nula durante la ejecución.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"General Exception: {ex.Message}");
                throw new Exception("Ocurrió un error general mientras se ejecutaba el procedimiento almacenado.");
            }
        }
    }
}
