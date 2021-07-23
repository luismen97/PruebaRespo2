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
        public ViewMovimiento(int TipoMovimiento)
        {
            InitializeComponent();

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
                await Navigation.PopModalAsync();
            };
            btnCerrar.GestureRecognizers.Add(clickClose);
        }

        private void Frame_Focused(object sender, FocusEventArgs e)
        {
            
            this.BackgroundColor = Color.FromHex("000");
        }
    }
}