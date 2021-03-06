using Prism;
using Prism.Ioc;
using TareasAPP.ViewModels;
using TareasAPP.Views;
using Xamarin.Essentials.Interfaces;
using Xamarin.Essentials.Implementation;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using TareasAPP.Service;
using TareasAPP.Interfaces;
using TareasAPP.DataAccess;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace TareasAPP
{
    public partial class App
    {
        /* 
         * The Xamarin Forms XAML Previewer in Visual Studio uses System.Activator.CreateInstance.
         * This imposes a limitation in which the App class must have a default constructor. 
         * App(IPlatformInitializer initializer = null) cannot be handled by the Activator.
         */
        public App() : this(null) { }

        public App(IPlatformInitializer initializer) : base(initializer) { }

        protected override async void OnInitialized()
        {
            InitializeComponent();

            await NavigationService.NavigateAsync("NavigationPage/MainPage");
        }

        protected override void RegisterTypes(IContainerRegistry containerRegistry)
        {
            containerRegistry.RegisterSingleton<IAppInfo, AppInfoImplementation>();

            containerRegistry.RegisterForNavigation<NavigationPage>();
            containerRegistry.RegisterForNavigation<MainPage, MainPageViewModel>();
            containerRegistry.RegisterForNavigation<NuevaTarea, NuevaTareaViewModel>();
            containerRegistry.RegisterForNavigation<DetalleTarea, DetalleTareaViewModel>();

            var calendario = DependencyService.Get<ICalendarService>();
            containerRegistry.RegisterInstance<ICalendarService>(calendario);

            containerRegistry.Register<ITareaService, TareaProcesos>();            
            
        }
    }
}
