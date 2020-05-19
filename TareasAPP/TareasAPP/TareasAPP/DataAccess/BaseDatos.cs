using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using SQLite;
using TareasAPP.Models;

namespace TareasAPP.DataAccess
{
    public class BaseDatos : IDisposable        
    {
        // Propiedad que instancia la conexión a la DB
        private static BaseDatos _instancia;
        public static BaseDatos Instancia { 
            get {
                if (_instancia == null)
                {
                    _instancia = new BaseDatos(GetDBpath());
                }
                return _instancia;
            }            
        }

        public SQLiteAsyncConnection Conexion { get; set; }
        private const string nombreBD = "dbTareas.db3"; 
        
        // Inicialización de la conexión a la DB SQlite
        protected BaseDatos(string pDBPath) {
            try
            {
                this.Conexion = new SQLiteAsyncConnection(pDBPath);
                this.Conexion.CreateTableAsync<Tarea>().Wait();
            }
            catch (Exception)
            {

                throw new Exception("Ocurrio un problema al crear la base de datos");
            }
        }

        /// <summary>
        /// Método para obtener la ruta de almacenamiento de la base de datos
        /// </summary>
        /// <returns></returns>
        private static string GetDBpath()
        {
            try
            {
                return Path.Combine(
                    Environment.GetFolderPath(
                        Environment.SpecialFolder.LocalApplicationData), nombreBD);
            }
            catch (Exception)
            {

                throw new Exception("Ocurrio un problema al acceder a la ruta especifica");
            }
        }

        /// <summary>
        /// Método para liberar los recursos instanciados de la conexión
        /// </summary>
        public void Dispose()
        {
            _instancia.Dispose();
        }
    }
}
