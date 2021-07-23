using DeudorApp.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class PlusCreditos : ContentPage
    {
        public PlusCreditos()
        {
            InitializeComponent();
            GetCreditosPlus();
        }

        private async void GetCreditosPlus()
        {
            var client = new HttpClient();
            StringContent str = new StringContent("op=GetCreditosPlus&IdCuenta=" + App.Current.Properties["IdCuenta"].ToString(), Encoding.UTF8, "application/x-www-form-urlencoded");
            var consulta = await client.PostAsync(new Uri(Constantes.url + "Usuario/App.php"), str);
            var json = consulta.Content.ReadAsStringAsync().Result.Trim();
            var creditos = JsonConvert.DeserializeObject<RootCreditos>(json);
            listCreditos.ItemsSource = creditos.CreditosPlus;

        }

        private void listCreditos_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {

        }

        private void ViewCell_Tapped(object sender, EventArgs e)
        {

        }
    }
}