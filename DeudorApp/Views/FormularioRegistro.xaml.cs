
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
        public FormularioRegistro()
        {
            InitializeComponent();
            Title = "Completa Registro";
        }

        private async void btnLlenarDatos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new FormularioAcreedor());
        }

        private async void btnDocumentos_Clicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new DocumentosAcreedor());
        }
    }
}