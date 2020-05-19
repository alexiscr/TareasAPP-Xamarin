using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TareasAPP.Models;

namespace TareasAPP.Interfaces
{
    interface ITarea : IDisposable
    {
        /// <summary>
        /// Método para almacenar una nueva tarea
        /// </summary>
        /// <param name="pTarea">Recibe una objeto tarea como parametro</param>
        /// <returns></returns>
        Task<bool> GuardarTarea(Tarea pTarea);

        /// <summary>
        /// Método asignado para eliminación de una tarea
        /// </summary>
        /// <param name="pTarea">Recibe un objeto del tipo tarea a ser eliminado</param>
        /// <returns></returns>
        Task<bool> EliminarTarea(Tarea pTarea);

        /// <summary>
        /// Método asignado para actualizar una tarea
        /// </summary>
        /// <param name="pTarea">Recibe el objeto del tipo tarea a ser actuaizado</param>
        /// <returns></returns>
        Task<bool> ActualizarTarea(Tarea pTarea);

        /// <summary>
        /// Método para obtener la lista de tareas agregadas a la DB
        /// </summary>
        /// <returns></returns>
        Task<List<Tarea>> ObtenerTareas();
    }
}
