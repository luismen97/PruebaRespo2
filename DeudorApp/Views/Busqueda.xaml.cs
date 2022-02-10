using DeudorApp.ViewModels;
using System;
using System.Collections.Generic;

using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class Busqueda : ContentPage
    {
        string TipoBusqueda = "";
        private string vCuenta = "";
        SM sM = new SM();
        public Busqueda()
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

        async void OnCriterioCheckedChanged(object sender, EventArgs args)
        {

            try
            {
                RadioButton button = sender as RadioButton;
                //SelBusqueda.Text = $"Tu forma de busqueda es : {button.Content}";

                if (button.Content.ToString() == "Nombre o Razón Social")
                {                    
                    TipoBusqueda = "Nombre";                    
                }
                if (button.Content.ToString() == "Registro Federal [RFC]")
                {
                    TipoBusqueda = "RFC";                    
                }
                if (button.Content.ToString() == "Registro de población [CURP]")
                {                    
                    TipoBusqueda = "CURP";                 
                }

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await DisplayAlert("Error", "Intentalo de nuevo mas tarde", "Ok");

            }

        }

        async void OnBtnBuscarClicked(object sender, EventArgs args)
        {

            try
            {

                await DisplayAlert("Mensaje", "Es necesario estar registrado para poder realizar busquedas", "Ok");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine(ex.Message);
                await DisplayAlert("Error", "Intentalo de nuevo mas tarde", "Ok");

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

            webViewElement.Source = "https://deudorapp.com/app/buscarCliente.php?IdCuenta=" + vCuenta + "";

            webViewElement.RegisterAction(DisplayDataFromJavascript);

        }

        private void DisplayDataFromJavascript(string data)
        {
            //
        }
    }
}
