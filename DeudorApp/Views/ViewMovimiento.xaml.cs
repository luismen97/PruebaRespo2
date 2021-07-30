using DeudorApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ViewMovimiento : ContentPage
    {
        TipoViewModel TipoViewM = new TipoViewModel();
        public ViewMovimiento(int TipoMovimiento)
        {
            InitializeComponent();

            BindingContext = TipoViewM = new TipoViewModel();

            if (TipoMovimiento != 1)
            {
                VistaIngreso.IsVisible = true;
            }
            else
            {
                VistaGasto.IsVisible = true;
            }

            var clickClose = new TapGestureRecognizer();
            clickClose.Tapped += async (s, e) =>
            {
                await btnCerrar.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(10);
                await btnCerrar.ScaleTo(1, length: 50, Easing.Linear);

                await Navigation.PopModalAsync();
            };
            btnCerrar.GestureRecognizers.Add(clickClose);

            var clickRegistroIngreso = new TapGestureRecognizer();
            clickRegistroIngreso.Tapped += async(s, e) =>
            {
                await btnRegistrIng.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(10);
                await btnRegistrIng.ScaleTo(1, length: 50, Easing.Linear);

                string Codigo = txtCodigo.Text;
                decimal Monto = Convert.ToDecimal(txtMontoIng.Text);
                

            };
            btnRegistrIng.GestureRecognizers.Add(clickRegistroIngreso);
        }

        private void Frame_Focused(object sender, FocusEventArgs e)
        {
            
            this.BackgroundColor = Color.FromHex("000");
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (TipoViewM.TiposMovimiento.Count == 0)
            {
                TipoViewM.LoadTipoCommand.Execute(null);
            }

        }
    }
}