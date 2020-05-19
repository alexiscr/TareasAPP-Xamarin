using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace TareasAPP.ViewModels
{
    public class ViewModelBase : BindableBase, IInitialize, INavigationAware, IDestructible
    {
        protected INavigationService NavigationService { get; private set; }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        // Commando cancelar Personalizado
        public DelegateCommand btnCancelar { get; set; }

        public ViewModelBase(INavigationService navigationService)
        {
            NavigationService = navigationService;
            btnCancelar = new DelegateCommand(btnCancelar_Command);
        }

        public virtual void Initialize(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedFrom(INavigationParameters parameters)
        {

        }

        public virtual void OnNavigatedTo(INavigationParameters parameters)
        {

        }

        public virtual void Destroy()
        {

        }

     
        // Método Cancelar
        protected async void btnCancelar_Command()
        {
            await this.NavigationService.GoBackToRootAsync();
        }
    }
}
