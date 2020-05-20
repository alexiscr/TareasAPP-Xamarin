using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EventKit;
using Foundation;
using GameKit;
using TareasAPP.iOS.Service;
using TareasAPP.Service;
using UIKit;
using Xamarin.Forms.Platform.iOS;

[assembly: Xamarin.Forms.Dependency(typeof(CalendarService))]
namespace TareasAPP.iOS.Service
{
    public class CalendarService : ICalendarService
    {
        // Variables locales a utilizar
        protected EKEventStore eventStore;
        protected bool permisoOtorgado;


        public CalendarService()
        {
            this.eventStore = new EKEventStore();            
            ChequearPermiso();

        }

       
        // Implementación de la interfaz
        public async Task<string> CreateEventCalendar(string titulo, string descripcion, DateTime inicioEvento, DateTime finEvento)
        {                                  
            if (permisoOtorgado)
            {
                EKEvent newEvent = EKEvent.FromStore(eventStore);
                newEvent.StartDate = inicioEvento.ToNSDate();
                newEvent.EndDate = finEvento.ToNSDate();
                newEvent.Title = titulo;
                newEvent.Notes = descripcion;
                newEvent.Calendar = eventStore.DefaultCalendarForNewEvents;
                NSError e;
                eventStore.SaveEvent(newEvent, EKSpan.ThisEvent, out e);
                return "true";
            }
            else
            {
                errorMesage();
                return string.Empty;
            }
        }
        /// <summary>
        /// Método generico para el mensaje de error de permisos
        /// </summary>
        private void errorMesage()
        {
            var alert = UIAlertController.Create("Acceso denegado", "Acceso al calendario denegado por el usuario", UIAlertControllerStyle.Alert);
            UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
        }

        // Chequeo de permisos para acceder a calendario en iOS 
        private void ChequearPermiso()
        {
            eventStore.RequestAccess(EKEntityType.Event, (bool granted, NSError error) =>
            {
                if (!granted)
                {
                    errorMesage();
                    permisoOtorgado = false;
                }
                else
                {
                    permisoOtorgado = true;
                }
            });
        }
    }
}