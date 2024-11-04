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

        private List<T> PopulateObjectsFromReader(T obj, OracleDataReader rdr)
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

        private void PopulateObjectFromReader(ref T obj, OracleDataReader rdr)
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
                            // Intenta obtener la propiedad del objeto por nombre
                            PropertyInfo propiedad = obj.GetType().GetProperty(_property.Name);
                            // Intenta asignar el valor de la columna correspondiente a la propiedad
                            propiedad.SetValue(obj, rdr.GetValue(rdr.GetOrdinal(_property.Name)), null);
                        }
                        catch (Exception ex)
                        {
                            // Si ocurre un error, lanza una excepción indicando el nombre de la propiedad y la descripción del error
                            throw new Exception("Error en la propiedad: " + _property.Name + " - " + ex.Message, ex);
                        }
                    }
                }
            }
        }

        public bool ColumnaExiste(OracleDataReader odr, string nombrecolumna)
        {
            for (var i = 0; i < odr.FieldCount; i++)
            {
                if (string.Equals(odr.GetName(i), nombrecolumna, StringComparison.CurrentCultureIgnoreCase))
                    return true;
            }

            return false;
        }
    }
}
