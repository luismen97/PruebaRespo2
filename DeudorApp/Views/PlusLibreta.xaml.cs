using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class PlusLibreta : ContentPage
    {
        public PlusLibreta()
        {
            InitializeComponent();

            /*var clickGasto = new TapGestureRecognizer();
            clickGasto.Tapped += async (s, e) =>
            {
                await Navigation.PushModalAsync(new ViewMovimiento(1));
            };
            btnGasto.GestureRecognizers.Add(clickGasto);

            var clickIng = new TapGestureRecognizer();
            clickIng.Tapped += async (s, e) =>
            {
                await Navigation.PushModalAsync(new ViewMovimiento(2));
            };
            btnIngreso.GestureRecognizers.Add(clickIng);*/
        }
    }
}