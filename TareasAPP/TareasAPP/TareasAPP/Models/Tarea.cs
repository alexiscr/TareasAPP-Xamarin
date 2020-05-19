using System;
using System.Collections.Generic;
using System.Text;
using SQLite;

namespace TareasAPP.Models
{
    
    public class Tarea
    {
        [PrimaryKey, AutoIncrement]
        public int IdTarea { get; set; }
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public DateTime Fecha { get; set; }
    }
}
