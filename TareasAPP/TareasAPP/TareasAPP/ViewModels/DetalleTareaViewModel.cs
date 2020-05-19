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
    public class DetalleTareaViewModel : ViewModelBase, INavigationAware
    {
        private Tarea _tarea;
        private INavigationService _navegacion;
        private IPageDialogService _dialog;

        // Propiedad de Binding a la página de detalle
        public Tarea Tarea
        {
            get { return _tarea; }
            set { SetProperty(ref _tarea, value); }
        }

        // Commando para ejecutar acciones de edición
        public DelegateCommand btnActualizar { get; set; }
        public DelegateCommand btnEliminar { get; set; }
        
        
        public DetalleTareaViewModel(INavigationService navigationService, IPageDialogService dialogs)
            : base(navigationService)
        {
            this._navegacion = navigationService;
            this._dialog = dialogs;
            this.btnActualizar = new DelegateCommand(btnActualizar_Command);
            this.btnEliminar = new DelegateCommand(btnEliminar_Command);
            
        }       

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.Tarea = parameters.GetValue<Tarea>("Tarea");
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        /// <summary>
        /// Método a ejecutar el proceso de eliminación
        /// </summary>
        private async void btnEliminar_Command()
        {
            ITarea tareaProcesos = new TareaProcesos();
            var opcion = await this._dialog.DisplayAlertAsync(
                                            DialogConst.tituloPrecausion,
                                            DialogConst.precausionEliminar,
                                            DialogConst.okOpcion,
                                            DialogConst.cancelOpcion);
            if (opcion)
            {
                bool resultado = tareaProcesos.EliminarTarea(Tarea).Result;
                if (resultado)
                {
                    await this._dialog.DisplayAlertAsync(
                                               DialogConst.tituloExito,
                                               DialogConst.exitoEliminar,
                                               DialogConst.okOpcion);

                    btnCancelar_Command();
                }
                else {
                    await this._dialog.DisplayAlertAsync(
                                               DialogConst.tituloError,
                                               DialogConst.errorEliminar,
                                               DialogConst.okOpcion);
                }
            }
            
        }        

        /// <summary>
        /// Método a ejecutar el proceso de actualización
        /// </summary>
        private async void btnActualizar_Command()
        {
            ITarea tareaProcesos = new TareaProcesos();
            var opcion = await this._dialog.DisplayAlertAsync(
                                            DialogConst.tituloPrecausion,
                                            DialogConst.precausionActualizar,
                                            DialogConst.okOpcion,
                                            DialogConst.cancelOpcion);
            if (opcion)
            {
                bool resultado = tareaProcesos.ActualizarTarea(Tarea).Result;
                if (resultado)
                {
                    await this._dialog.DisplayAlertAsync(
                                               DialogConst.tituloExito,
                                               DialogConst.exitoActualizar,
                                               DialogConst.okOpcion);
                }
                else
                {
                    await this._dialog.DisplayAlertAsync(
                                               DialogConst.tituloError,
                                               DialogConst.errorActualizar,
                                               DialogConst.okOpcion);
                }
            }
        }
    }
}
