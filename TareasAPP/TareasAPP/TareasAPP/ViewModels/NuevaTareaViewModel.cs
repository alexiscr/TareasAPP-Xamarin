using Prism.Commands;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Text;
using TareasAPP.DataAccess;
using TareasAPP.Helpers;
using TareasAPP.Interfaces;
using TareasAPP.Models;

namespace TareasAPP.ViewModels
{
    public class NuevaTareaViewModel : ViewModelBase
    {
      
        private string _tituloTarea;
        private string _descripcionTarea;
        private DateTime _fechaTarea;

        // Servicios de Prism
        private INavigationService _navegacion;
        private IPageDialogService _dialogs;


        // Propiedades a realizar Binding
        public string TituloTarea
        {
            get { return _tituloTarea; }
            set { SetProperty(ref _tituloTarea, value);}
           
        }

        public string DescripcionTarea
        {
            get { return _descripcionTarea; }
            set { SetProperty(ref _descripcionTarea, value); }
        }

        public DateTime FechaTarea
        {
            get { return _fechaTarea; }
            set { SetProperty(ref _fechaTarea, value);}
        }

        
        // Commandos para la accion de guardar y cancelar
        public DelegateCommand btnGuardar { get; set; }        

        public NuevaTareaViewModel(INavigationService navigationService, IPageDialogService dialogs) : base(navigationService)
        {
            this._navegacion = navigationService;
            this._dialogs = dialogs;

            // Configuración de la fecha inicial en el DatePicker
            this._fechaTarea = DateTime.Now.Date;

            btnGuardar = new DelegateCommand(btnGuardar_Command);
            
        }       

        /// <summary>
        /// Comando para almacenar una nueva tarea
        /// </summary>
        private async void btnGuardar_Command()
        {
            Tarea tarea = new Tarea();
            tarea.Titulo = this._tituloTarea;
            tarea.Descripcion = this._descripcionTarea;
            tarea.Fecha = this._fechaTarea.Date;

            ITarea tareas = new TareaProcesos();
            if (!string.IsNullOrEmpty(this._tituloTarea) || !string.IsNullOrWhiteSpace(this._tituloTarea))
            {
                bool resultado = await tareas.GuardarTarea(tarea);

                if (resultado)
                {
                    await this._dialogs.DisplayAlertAsync(DialogConst.tituloExito,
                                                          DialogConst.exitoAgregar,
                                                          DialogConst.okOpcion);

                    // Reset de las propiedades
                    TituloTarea = string.Empty;
                    DescripcionTarea = string.Empty;
                    FechaTarea = DateTime.Now;
                }
                else
                {
                    await this._dialogs.DisplayAlertAsync(DialogConst.tituloError,
                                                           DialogConst.errorAgregar,
                                                           DialogConst.okOpcion); ;
                }
            }
            else {
                await this._dialogs.DisplayAlertAsync(DialogConst.tituloError,
                                                           DialogConst.errorEntry,
                                                           DialogConst.okOpcion); ;
            }
            

           
            
        }

        
    }
}
