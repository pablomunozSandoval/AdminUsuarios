using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Backend.DAL;
using Backend.DTO;
using Backend.DTO.Alma;
using Newtonsoft.Json.Linq;
using Oracle.DataAccess.Client;

namespace Backend.Datos
{
    public class da_combobox<T> : Oracle<T> where T : new()
    {
        public List<T> getCboSedes(int i_pers_nrut)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[2];

                param[0] = new OracleParameter("i_pers_nrut", OracleDbType.Int32);
                param[1] = new OracleParameter("o_cursor", OracleDbType.RefCursor);

                param[0].Value = i_pers_nrut;

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getCboSedes", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> getCboPeriodo()
        {
            try
            {
                IDataParameter[] param = new IDataParameter[1];

                param[0] = new OracleParameter("o_cursor", OracleDbType.RefCursor);

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getPeriodoActual", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        //No devuelve ningun dato
        public List<T> getPeriodoActual()
        {
            try
            {
                IDataParameter[] param = new IDataParameter[1];

                param[0] = new OracleParameter("o_cursor", OracleDbType.RefCursor);
                param[0].Direction = ParameterDirection.Output;

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getPeriodoActual", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> getCboOtrosPeriodos()
        {
            try
            {
                IDataParameter[] param = new IDataParameter[1];

                param[0] = new OracleParameter("o_cursor", OracleDbType.RefCursor);

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getCboOtrosPeriodos", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> getHorarioSedePeriodo(int i_sede_ccod, int i_peri_ccod, int i_inicio, int i_limite, string i_sort, string i_dir, string i_filtro)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[8];

                param[0] = new OracleParameter("i_sede_ccod", OracleDbType.Int32) { Value = i_sede_ccod };
                param[1] = new OracleParameter("i_peri_ccod", OracleDbType.Int32) { Value = i_peri_ccod };
                param[2] = new OracleParameter("i_inicio", OracleDbType.Int32) { Value = i_inicio };
                param[3] = new OracleParameter("i_limite", OracleDbType.Int32) { Value = i_limite };
                param[4] = new OracleParameter("i_sort", OracleDbType.Varchar2) { Value = i_sort };
                param[5] = new OracleParameter("i_dir", OracleDbType.Varchar2) { Value = i_dir };
                param[6] = new OracleParameter("i_filtro", OracleDbType.Varchar2) { Value = i_filtro };
                param[7] = new OracleParameter("o_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getHorario_Sede_Periodo", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<T> getHorariosBySede(int i_sede_ccod, int i_inicio, int i_limite, string i_sort, string i_dir, string i_filtro)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[7];

                param[0] = new OracleParameter("i_sede_ccod", OracleDbType.Int32) { Value = i_sede_ccod };
                param[1] = new OracleParameter("i_inicio", OracleDbType.Int32) { Value = i_inicio };
                param[2] = new OracleParameter("i_limite", OracleDbType.Int32) { Value = i_limite };
                param[3] = new OracleParameter("i_sort", OracleDbType.Varchar2) { Value = i_sort };
                param[4] = new OracleParameter("i_dir", OracleDbType.Varchar2) { Value = i_dir };
                param[5] = new OracleParameter("i_filtro", OracleDbType.Varchar2) { Value = i_filtro };
                param[6] = new OracleParameter("o_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getHorariosBySede", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> getComboTurno()
        {
            try
            {
                IDataParameter[] param = new IDataParameter[1];

                param[0] = new OracleParameter("o_cursor", OracleDbType.RefCursor);

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getComboTurno", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
        public List<T> insHorario(int i_sede_ccod, int i_peri_ccod, DateTime i_hinicio, DateTime i_htermino, int i_turn_ccod, string i_audi_tusuario, int i_origen)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[8];
                param[0] = new OracleParameter("i_sede_ccod", OracleDbType.Int32) { Value = i_sede_ccod };
                param[1] = new OracleParameter("i_peri_ccod", OracleDbType.Int32) { Value = i_peri_ccod };
                param[2] = new OracleParameter("i_hinicio", OracleDbType.Date) { Value = i_hinicio };
                param[3] = new OracleParameter("i_htermino", OracleDbType.Date) { Value = i_htermino };
                param[4] = new OracleParameter("i_turn_ccod", OracleDbType.Int32) { Value = i_turn_ccod };
                param[5] = new OracleParameter("i_audi_tusuario", OracleDbType.Varchar2) { Value = i_audi_tusuario };
                param[6] = new OracleParameter("i_origen", OracleDbType.Int32) { Value = i_origen };
                param[7] = new OracleParameter("o_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.insHorario", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        public List<T> updateHorario(int i_hora_ccod, int i_sede_ccod, int i_peri_ccod, DateTime i_hinicio, DateTime i_htermino, int i_turn_ccod, string i_audi_tusuario, int i_origen)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[9];

                param[0] = new OracleParameter("i_hora_ccod", OracleDbType.Int32) { Value = i_hora_ccod };
                param[1] = new OracleParameter("i_sede_ccod", OracleDbType.Int32) { Value = i_sede_ccod };
                param[2] = new OracleParameter("i_peri_ccod", OracleDbType.Int32) { Value = i_peri_ccod };
                param[3] = new OracleParameter("i_hinicio", OracleDbType.Date) { Value = i_hinicio };
                param[4] = new OracleParameter("i_htermino", OracleDbType.Date) { Value = i_htermino };
                param[5] = new OracleParameter("i_turn_ccod", OracleDbType.Int32) { Value = i_turn_ccod };
                param[6] = new OracleParameter("i_audi_tusuario", OracleDbType.Varchar2) { Value = i_audi_tusuario };
                param[7] = new OracleParameter("i_origen", OracleDbType.Int32) { Value = i_origen };
                param[8] = new OracleParameter("o_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.updHorario", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


        public List<T> delHorario(int i_hora_ccod, int i_sede_ccod, int i_peri_ccod, int i_origen)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[5];

                param[0] = new OracleParameter("i_hora_ccod", OracleDbType.Int32) { Value = i_hora_ccod };
                param[1] = new OracleParameter("i_sede_ccod", OracleDbType.Int32) { Value = i_sede_ccod };
                param[2] = new OracleParameter("i_peri_ccod", OracleDbType.Int32) { Value = i_peri_ccod };
                param[3] = new OracleParameter("i_origen", OracleDbType.Int32) { Value = i_origen };
                param[4] = new OracleParameter("o_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.delHorario", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> getCountHorariosSedesPeriodos(int i_peri_ccod, int i_sede_ccod)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[3];

                param[0] = new OracleParameter("i_peri_ccod", OracleDbType.Int32) { Value = i_peri_ccod };
                param[1] = new OracleParameter("i_sede_ccod", OracleDbType.Int32) { Value = i_sede_ccod };
                param[2] = new OracleParameter("o_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getCountHorariosSedesPeriodos", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public List<T> importarHorario(int i_peri_ccod, int i_sede_ccod, int i_audi_tusuario)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[4];

                param[0] = new OracleParameter("i_peri_ccod", OracleDbType.Int32) { Value = i_peri_ccod };
                param[1] = new OracleParameter("i_sede_ccod", OracleDbType.Int32) { Value = i_sede_ccod };
                param[2] = new OracleParameter("i_audi_tusuario", OracleDbType.Int32) { Value = i_audi_tusuario };
                param[3] = new OracleParameter("o_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.importarHorario", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<T> getMensajeReplica(int i_sede_ccod)
        {
            try
            {
                IDataParameter[] param = new IDataParameter[2];

                param[0] = new OracleParameter("i_sede_ccod", OracleDbType.Int32) { Value = i_sede_ccod };
                param[1] = new OracleParameter("o_cursor", OracleDbType.RefCursor) { Direction = ParameterDirection.Output };

                List<T> lstObjeto = _ExecuteStoredProcedure("PKG_MANT_HORARIOS.getMensajeReplica", ref param);

                return lstObjeto;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

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
