
using DeudorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class FormularioRegistro : ContentPage
    {
        FormularioRegistroViewModel Vmodel;
        SM sM = new SM();
        public FormularioRegistro()
        {
            InitializeComponent();
            
            Title = "Completa Registro";
            this.BindingContext = Vmodel = new FormularioRegistroViewModel();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Cargar();
        }
        private async void Cargar()
        {
            contenido.IsVisible = false;
            loader.IsVisible = true;
            Vmodel.LoadFormulariosCommand.Execute(null);
            Vmodel.FlujoCommand.Execute(null);
            await Task.Delay(1500);
            contenido.IsVisible = true;
            loader.IsVisible = false;

        }

        private async void btnLlenarDatos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormularioAcreedor());
        }

        private async void btnDocumentos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentosAcreedor());
        }

        private async void Button_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentosAcreedorValidacion());
        }

        private async void btnEnviarSolicitud_Clicked_1(object sender, EventArgs e)
        {
            this.IsEnabled = false;
            string resp = await sM.EnviarSolicitud();
            if (resp == "1")
            {
                await DisplayAlert("Solicitud enviada correctamente", "Éxito", "OK");
                Cargar();
            }
            else
            {
                await DisplayAlert("Ups! Hubo un problema, intentalo más tarde", "Alerta", "OK");

            }
            this.IsEnabled = true;
        }
    }
}