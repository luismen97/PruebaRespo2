using DeudorApp.Views;
//using Sharpnado.Tabs;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace DeudorApp.ViewModels
{
    public class PlusViewModel
    {

        
        public Command TapCommando { get; private set; }
         public Command TapCommand => new Command(OnTap);

        public ObservableCollection<Employee> employees { get; set; }

        public class Employee
        {
            public string DisplayName { get; set; }
        }

        void OnTap(object val)
         {
             Application.Current.MainPage.DisplayAlert("Alert", "Hello", "Cancel", "ok");

         }

         public PlusViewModel()
         {
             TapCommando = new Command(get => {
                 _ = AlertaAsync();
             });
            employees = new ObservableCollection<Employee>();
            employees.Add(new Employee { DisplayName = "Rob Finnerty" });
            employees.Add(new Employee { DisplayName = "Bill Wrestler" });
            employees.Add(new Employee { DisplayName = "Dr. Geri-Beth Hooper" });
            employees.Add(new Employee { DisplayName = "Dr. Keith Joyce-Purdy" });
            employees.Add(new Employee { DisplayName = "Sheri Spruce" });
            employees.Add(new Employee { DisplayName = "Burt Indybrick" });
        }

        public async Task AlertaAsync()
         {
            await  Application.Current.MainPage.DisplayAlert("Alert", "Hello", "Cancel", "ok");
         }


    }
    
}
