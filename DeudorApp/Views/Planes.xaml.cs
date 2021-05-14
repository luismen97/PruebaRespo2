using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class Planes : ContentPage
    {
        private string vCuenta;
        public Planes()
        {
            InitializeComponent();
            if (Application.Current.Properties.ContainsKey("IdCuenta"))
            {
                vCuenta = Convert.ToString(Application.Current.Properties["IdCuenta"]);
            }
            else
            {
                vCuenta = "";
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
