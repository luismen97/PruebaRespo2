using DeudorApp.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class Planes : ContentPage
    {
        private string vCuenta;
        SM sM = new SM();
        public Planes()
        {
            InitializeComponent();
            Aut();

            if (Application.Current.Properties.ContainsKey("IdCuenta"))
            {
              vCuenta = Convert.ToString(Application.Current.Properties["IdCuenta"]);
            }
            else
            {
              vCuenta = "";
            }
        }

        private async void Aut()
        {
            string aut = await sM.GetAutorizado();
            if (aut != "1")
            {
                webViewElement.IsVisible = false;
                NoDis.IsVisible = true;
            }
            else
            {
                webViewElement.IsVisible = true;
                NoDis.IsVisible = false;
            }
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            webViewElement.Source = "https://deudorapp.com/app/paquetes.php?IdCuenta=" + vCuenta + "";

            webViewElement.RegisterAction(DisplayDataFromJavascript);

        }

        private void DisplayDataFromJavascript(string data)
        {
            //
        }
    }
}
