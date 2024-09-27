
using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Reflection;


namespace Backend.DAL
{
    public class DataGenericReader<T> where T : new()
    {
        internal List<T> ListaObjeto(OracleDataReader odr)
        {
            T objeto = new T();
            var lstObjeto = PopulateObjectsFromReader(objeto, odr);

            return lstObjeto;
        }

        public List<T> PopulateObjectsFromReader(T obj, OracleDataReader rdr)
        {
            List<T> list = new List<T>();
            while (rdr.Read())
            {
                T objeto = Activator.CreateInstance<T>();
                PopulateObjectFromReader(ref objeto, rdr);
                list.Add(objeto);
            }
            rdr.Close();
            rdr.Dispose();

            return list;
        }

        internal void PopulateObjectFromReader(ref T obj, OracleDataReader rdr)
        {
            Type type = obj.GetType();
            PropertyInfo[] properties = type.GetProperties();
            foreach (PropertyInfo _property in properties)
            {
                if (ColumnaExiste(rdr, _property.Name))
                {
                    if (!rdr.IsDBNull(rdr.GetOrdinal(_property.Name)))
                    {
                        try
                        {
                            PropertyInfo propiedad = obj.GetType().GetProperty(_property.Name);
                            propiedad.SetValue(obj, rdr.GetValue(rdr.GetOrdinal(_property.Name)), null);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Error en el tipo de Variable: " + _property.Name + "<br/> Descripción: " + ex.Message, ex);
                        }
                    }
                }
            }
        }

        public bool ColumnaExiste(OracleDataReader odr, string nombrecolumna)
        {
            for (var i = 0; i < odr.FieldCount; i++)
            {
                if (string.Equals(odr.GetName(i), nombrecolumna, StringComparison.CurrentCultureIgnoreCase)) return true;
            }

            return false;
        }
    }
}