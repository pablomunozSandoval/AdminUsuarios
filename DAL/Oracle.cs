using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Net.NetworkInformation;
using System.Web;

namespace Backend.DAL
{
    public class Oracle<T> : DataGenericReader<T> where T : new()
    {
        private string _stringAccess { get; set; }

        public Oracle() { }

        public Oracle(string key)
        {
            _stringAccess = key;
        }

        public void ExecuteStoredProcedure(string NameStoredProcedure)
        {
            try
            {
                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(NameStoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void ExecuteStoredProcedure(string NameStoredProcedure, ref IDataParameter[] Params)
        {
            try
            {
                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(NameStoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    TransferParameters(cmd, Params);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public List<T> _ExecuteStoredProcedure(string NameStoredProcedure, ref IDataParameter[] Params)
        {
            try
            {
                List<T> lstObjeto;
                using (OracleConnection conn = GetConexion())
                {
                    var cmd = new OracleCommand(NameStoredProcedure, conn)
                    {
                        CommandType = CommandType.StoredProcedure
                    };

                    // Log the stored procedure command
                    Console.WriteLine($"Executing stored procedure: {NameStoredProcedure}");

                    // Iterate over parameters and log them
                    foreach (IDataParameter p in Params)
                    {
                        Console.WriteLine($"Parameter Name: {p.ParameterName}, Value: {p.Value}");
                    }

                    // Add parameters to the command
                    TransferParameters(cmd, Params);

                    conn.Open();

                    // Execute the stored procedure and retrieve the data
                    OracleDataReader DataResult = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                    // Parse the data into a list of objects
                    lstObjeto = ListaObjeto(DataResult);
                }

                return lstObjeto;
            }
            catch (Exception ex)
            {
                // Throw a more descriptive exception with the error message
                throw new Exception($"Error executing stored procedure: {ex.Message}", ex);
            }
        }



        public void ExecuteStoredProcedure(string NameStoredProcedure, ref IDataParameter[] Params, ref DataTable DataResult)
        {
            try
            {
                DataResult = new DataTable();

                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(NameStoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    TransferParameters(cmd, Params);
                    var DataAdap = new OracleDataAdapter(cmd);

                    conn.Open();
                    DataAdap.Fill(DataResult);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void ExecuteStoredProcedure1(string NameStoredProcedure, ref IDataParameter[] Params, ref DataTable DataResult)
        {
            try
            {
                DataResult = new DataTable();

                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(NameStoredProcedure, conn);
                    cmd.CommandType = CommandType.StoredProcedure;

                    TransferParameters(cmd, Params);
                    var DataAdap = new OracleDataAdapter(cmd);

                    conn.Open();
                    DataAdap.Fill(DataResult);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        public void ExecuteSQL(string SQLString)
        {
            try
            {
                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(SQLString, conn);
                    cmd.CommandType = CommandType.Text;

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void ExecuteSQL(string SQLString, ref DataTable DataResult)
        {
            try
            {
                DataResult = new DataTable();

                using (OracleConnection conn = GetConexion())
                {
                    var DataAdap = new OracleDataAdapter(SQLString, conn);

                    conn.Open();
                    DataAdap.Fill(DataResult);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }
        public void ExecuteSQL(string SQLString, ref IDataParameter[] Params)
        {
            try
            {
                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(SQLString, conn);
                    cmd.CommandType = CommandType.Text;
                    TransferParameters(cmd, Params);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }


        public void ExecuteSQL(string SQLString, ref IDataParameter[] Params, ref DataTable DataResult)
        {
            try
            {
                DataResult = new DataTable();

                using (OracleConnection conn = GetConexion())
                {
                    OracleCommand cmd = new OracleCommand(SQLString, conn);
                    TransferParameters(cmd, Params);
                    var DataAdap = new OracleDataAdapter(cmd);

                    conn.Open();
                    DataAdap.Fill(DataResult);
                    conn.Close();
                }
            }
            catch (Exception ex)
            {
                WriteToEventLog(ex.Message);
                throw new Exception(ex.Message);
            }
        }

        #region Privados
        private string StringConexion
        {
            get
            {
                if (string.IsNullOrEmpty(_stringAccess)) 
                    return ConfigurationManager.AppSettings["AKORACLE"];            
                else
                    return ConfigurationManager.AppSettings[_stringAccess];
            }
        }
        private string GetStringConexion()
        {
            string NameKey = StringConexion;
            //TODO: Cambiar mensaje de error 1

            if (NameKey == null) throw new Exception("Namekey null, no encontrada (NULL).");

            string StrConn = ConfigurationManager.AppSettings[NameKey];

            if (StrConn == null) throw new Exception("String de conexion no encontrado (NULL), revisar ubicación de string de conexión.");
            return StrConn;
        }
        private OracleConnection GetConexion()
        {
            string str = GetStringConexion();
            OracleConnection conn = new OracleConnection(str);

            return conn;
        }
        private void TransferParameters(OracleCommand cm, IDataParameter[] Params)
        {
            for (int i = 0; i < Params.Length; i++)
            {
                if (Params[i] != null) // Validar que no es null
                {
                    if (((OracleParameter)Params[i]).OracleDbType == OracleDbType.RefCursor)
                    {
                        Params[i].Direction = ParameterDirection.Output;
                    }

                    cm.Parameters.Add(Params[i]);
                }
                else
                {
                    throw new NullReferenceException($"El parámetro en la posición {i} es nulo.");
                }
            }
        }

        private void WriteToEventLog(string message)
        {
            string cs = ConfigurationManager.AppSettings["KEYLOG"];
            if (!string.IsNullOrEmpty(cs))
            {
                try
                {
                    EventLog elog = new EventLog();

                    if (!EventLog.SourceExists(cs))
                    {
                        EventLog.CreateEventSource(cs, cs);
                    }

                    elog.Source = cs;
                    elog.EnableRaisingEvents = true;
                    elog.WriteEntry(message, EventLogEntryType.Error, 7637);
                }
                catch { }
            }
        }
        #endregion
    }
}