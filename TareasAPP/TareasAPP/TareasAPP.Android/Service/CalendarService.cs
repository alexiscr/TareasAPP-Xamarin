using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Provider;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using AndroidX.Core.Content;
using TareasAPP.Droid.Services;
using Android.Content.PM;
using Android.Database;
using Android.Support.V4.App;
using Java.Util;
using System.Threading.Tasks;
using AndroidX.Core.App;
using TareasAPP.Service;

[assembly: Xamarin.Forms.Dependency(typeof(CalendarService))]
namespace TareasAPP.Droid.Services
{
    public class CalendarService : ICalendarService
    {
        // Variable para almacenar el calendario activo 
        private int calendarioId;

        // Proyección del calendario
        private string[] calendarsProjection =
        {
            CalendarContract.Calendars.InterfaceConsts.Id,
            CalendarContract.Calendars.InterfaceConsts.CalendarDisplayName,
            CalendarContract.Calendars.InterfaceConsts.AccountName
        };

        // Implementación del Servicio
        public Task<string> CreateEventCalendar(string titulo, string descripcion, DateTime inicioEvento, DateTime finEvento)
        {
            TaskCompletionSource<string> tcs = new TaskCompletionSource<string>();
            GetSystemCalendar();
            if (calendarioId == 0)
            {
                tcs.SetResult(string.Empty);
                return tcs.Task;
            }

            ContentValues eventValues = new ContentValues();

            eventValues.Put(CalendarContract.Events.InterfaceConsts.CalendarId, calendarioId);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Title, titulo);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Description, descripcion);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtstart, GetDateTimeMS(inicioEvento));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.Dtend, GetDateTimeMS(finEvento));
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventTimezone, System.TimeZoneInfo.Local.StandardName);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.EventEndTimezone, System.TimeZoneInfo.Local.StandardName);
            eventValues.Put(CalendarContract.Events.InterfaceConsts.AllDay, true);

            var uri = Application.Context.ContentResolver.Insert(CalendarContract.Events.ContentUri, eventValues);

            if (!long.TryParse(uri.LastPathSegment, out long eventID))
                tcs.SetResult(string.Empty);
            else
            {
                  tcs.SetResult(eventID.ToString());
            }

            return tcs.Task;
        }

        //
        private void GetSystemCalendar()
        {
            var calendarsUri = CalendarContract.Calendars.ContentUri;
            
            var loader = new AndroidX.Loader.Content.CursorLoader(Application.Context, calendarsUri, calendarsProjection, null, null, null);
            //var loader = new Android.Content.CursorLoader(Application.Context, calendarsUri, calendarsProjection, null, null, null);
            var cursor = (ICursor)loader.LoadInBackground();
            if (cursor.Count > 0)
            {
                cursor.MoveToFirst();
                calendarioId = cursor.GetInt(cursor.GetColumnIndex(calendarsProjection[0]));
            }
            else {
                calendarioId = 0;
            }
            
        }
        
        public CalendarService()
        {
            
        }

        // Conversor para los tiempos a almacenar
        long GetDateTimeMS(DateTime date)
        {
            Calendar calendar = Calendar.GetInstance(Java.Util.TimeZone.GetTimeZone(System.TimeZoneInfo.Local.StandardName));
            calendar.Set(CalendarField.DayOfMonth, date.Day);
            calendar.Set(CalendarField.Month, date.Month - 1);
            calendar.Set(CalendarField.Year, date.Year);
            calendar.Set(CalendarField.HourOfDay, date.Hour);
            calendar.Set(CalendarField.Minute, date.Minute);

            return calendar.TimeInMillis;
        }        
    }
}