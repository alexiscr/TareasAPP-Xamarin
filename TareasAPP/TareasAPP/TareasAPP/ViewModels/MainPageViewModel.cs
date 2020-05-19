using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using TareasAPP.DataAccess;
using TareasAPP.Interfaces;
using TareasAPP.Models;

namespace TareasAPP.ViewModels
{
    public class MainPageViewModel : ViewModelBase, INavigationAware
    {
        private ObservableCollection<Tarea> _tareas;        
        private INavigationService _navegacion;
        private Tarea _tareaSeleccionada;

        // Propiedad que realizara el binding hacia la vista 
        public ObservableCollection<Tarea> Tareas
        {
            get { return _tareas; }
            set { SetProperty(ref _tareas, value); }
        }

        // Propiedad que enviara los datos seleccionados
        public Tarea TareaSeleccionada
        {
            get { return _tareaSeleccionada; }
            set { SetProperty(ref _tareaSeleccionada, value);

                // Parametro de navegacion a registrar
                var navigationParam = new NavigationParameters();
                navigationParam.Add("Tarea", value);

                // Navegación a la página donde se mostrar la información
                _navegacion.NavigateAsync("DetalleTarea", navigationParam);
            }
        }

        // Definción de bones para la navegacion de las acciones
        public DelegateCommand btnAgregar { get; set; }

        public MainPageViewModel(INavigationService navigationService)
            : base(navigationService)
        {            
            this._navegacion = navigationService;            
            this.btnAgregar = new DelegateCommand(btnAgregar_Command);
            this.CargarListaTareas();
        }

        /// <summary>
        /// Método para cargargar la observable collection encargada de mostrar la lista de tareas
        /// </summary>
        private void CargarListaTareas()
        {
            Tareas = new ObservableCollection<Tarea>();
            ITarea tareaProcesos = new TareaProcesos();
            var listaProcesos = tareaProcesos.ObtenerTareas().Result;

            if (listaProcesos != null)
            {
                Tareas = new ObservableCollection<Tarea>(listaProcesos);
            }           
        }

        private void btnAgregar_Command() {
            this._navegacion.NavigateAsync("NuevaTarea");
        }

        // Forzamos a la actualización de lista al navegar nuevamente a la principal
        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            this.CargarListaTareas();
        }

        public override void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

    }
}
