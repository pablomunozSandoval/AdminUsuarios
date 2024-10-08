using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Backend.DAL
{
    // Clase genérica que permite leer datos de una base de datos Oracle
    // y convertirlos en una lista de objetos del tipo genérico T
    public class DataGenericReader<T> where T : new()
    {
        
        // Método que recibe un OracleDataReader y devuelve una lista de objetos del tipo T
        internal List<T> ListaObjeto(OracleDataReader odr)
        {
            // Crear una instancia del objeto genérico T
            T objeto = new T(); 
            
            // Llama a PopulateObjectsFromReader para poblar la lista de objetos
            var lstObjeto = PopulateObjectsFromReader(objeto, odr);

            return lstObjeto; // Retorna la lista de objetos T poblada
        }



        // Método que recibe un objeto genérico y un OracleDataReader,
        // devuelve una lista de objetos T rellenados con los datos del lector
        private List<T> PopulateObjectsFromReader(T obj, OracleDataReader rdr)
        {
            // Inicializa una nueva lista de objetos T
            List<T> list = new List<T>(); 
            
            // Mientras haya filas en el lector de datos
            while (rdr.Read())
            {
                // Crea una nueva instancia de T
                T objeto = Activator.CreateInstance<T>();
                // Poblamos la instancia con los valores del lector de datos
                PopulateObjectFromReader(ref objeto, rdr);
                // Añade el objeto a la lista
                list.Add(objeto);
            }
            // Cierra el OracleDataReader
            rdr.Close();
            // Libera los recursos del OracleDataReader
            rdr.Dispose();

            return list; // Retorna la lista de objetos T
        }

        // Método que rellena un objeto T con los datos del OracleDataReader
        private void PopulateObjectFromReader(ref T obj, OracleDataReader rdr)
        {
            Type type = obj.GetType(); // Obtiene el tipo del objeto T
            // Obtiene todas las propiedades del objeto T
            PropertyInfo[] properties = type.GetProperties();
            // Itera sobre las propiedades del objeto
            foreach (PropertyInfo _property in properties)
            {
                // Verifica si la columna correspondiente a la propiedad existe en el lector de datos
                if (ColumnaExiste(rdr, _property.Name))
                {
                    // Si la columna no tiene un valor nulo en la fila actual del lector
                    if (!rdr.IsDBNull(rdr.GetOrdinal(_property.Name)))
                    {
                        try
                        {
                            // Obtiene la propiedad del objeto por nombre
                            PropertyInfo propiedad = obj.GetType().GetProperty(_property.Name);
                            // Asigna el valor de la columna correspondiente a la propiedad
                            propiedad.SetValue(obj, rdr.GetValue(rdr.GetOrdinal(_property.Name)), null);
                        }
                        catch (Exception ex)
                        {
                            // Si ocurre un error, lanza una excepción indicando el nombre de la propiedad
                            // y la descripción del error
                            throw new Exception("Error en el tipo de Variable: " + _property.Name +
                                                "<br/> Descripción: " + ex.Message, ex);
                        }
                    }
                }
            }
        }

        // Método que verifica si una columna existe en el OracleDataReader
        public bool ColumnaExiste(OracleDataReader odr, string nombrecolumna)
        {
            // Itera sobre todas las columnas del OracleDataReader
            for (var i = 0; i < odr.FieldCount; i++)
            {
                // Compara el nombre de la columna actual con el nombre proporcionado
                if (string.Equals(odr.GetName(i), nombrecolumna, StringComparison.CurrentCultureIgnoreCase))
                    return true; // Si el nombre coincide, la columna existe
            }

            return false; // Si no se encuentra, retorna false
        }
    }
}
