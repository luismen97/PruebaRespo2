using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeudorApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class CuentaRegistro : ContentPage
    {
        int TipoCuenta;
        public CuentaRegistro()
        {
            InitializeComponent();
            Title = "Escoge tu cuenta";
        }

        private void btnFisica_Clicked(object sender, EventArgs e)
        {
            TipoCuenta = 1;
            Navigation.PushAsync(new Registro(TipoCuenta));
        }

        private void btnAcreedor_Clicked(object sender, EventArgs e)
        {
            TipoCuenta = 2;
            Navigation.PushAsync(new Registro(TipoCuenta));
        }
    }
}