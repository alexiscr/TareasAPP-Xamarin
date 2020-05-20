using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace TareasAPP.Service
{
    public interface ICalendarService
    {
        /// <summary>
        /// Creación de tarea en el calendario del dispositivo
        /// </summary>
        /// <param name="titulo"></param>
        /// <param name="descripcion"></param>
        /// <param name="inicioEvento"></param>
        /// <param name="finEvento"></param>
        /// <returns></returns>
        Task<string> CreateEventCalendar(string titulo, string descripcion, DateTime inicioEvento, DateTime finEvento);
    }
}
