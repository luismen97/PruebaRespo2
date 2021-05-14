using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class RegistrarCliente : ContentPage
    {
        private string vCuenta = "";
        public RegistrarCliente()
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

            webViewElement.Source = "https://deudorapp.com/app/registroCliente.php?IdCuenta=" + vCuenta + "";

            webViewElement.RegisterAction(DisplayDataFromJavascript);

        }

        private void DisplayDataFromJavascript(string data)
        {
            //
        }
    }
}
