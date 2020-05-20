using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TareasAPP.Interfaces;
using TareasAPP.Models;

namespace TareasAPP.DataAccess
{
    public class TareaProcesos : ITareaService
    {
        public Task<bool> ActualizarTarea(Tarea pTarea)
        {
            int retorno = BaseDatos.Instancia.Conexion.UpdateAsync(pTarea).Result;
            bool resultado = retorno == 1 ? true : false;
            return Task.FromResult(resultado);
        }

        public void Dispose()
        {
            BaseDatos.Instancia.Dispose();
        }

        public Task<bool> EliminarTarea(Tarea pTarea)
        {
            int retorno = BaseDatos.Instancia.Conexion.DeleteAsync(pTarea).Result;
            bool resultado = retorno == 1 ? true : false;
            return Task.FromResult(resultado);
        }

        public Task<bool> GuardarTarea(Tarea pTarea)
        {
            int retorno = BaseDatos.Instancia.Conexion.InsertAsync(pTarea).Result;
            bool resultado = retorno == 1 ? true : false;
            return Task.FromResult(resultado);
        }

        public Task<List<Tarea>> ObtenerTareas()
        {
            return BaseDatos.Instancia.Conexion.QueryAsync<Tarea>("SELECT * FROM [Tarea]");
        }
    }
}
