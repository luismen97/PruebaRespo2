using DeudorApp.Models;
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
    public partial class ViewDetalleMovimiento : ContentPage
    {
        public ViewDetalleMovimiento(Movimiento model)
        {
            InitializeComponent();
            lblCantidad.Text = model.Cantidad;
            lblFecha.Text = model.FechaFormato;
            lblTipo.Text = model.Tipo2;
            lblNota.Text = model.Nota;

            var clickClose = new TapGestureRecognizer();
            clickClose.Tapped += async (s, e) =>
            {
                await btnCerrar.ScaleTo(0.8, length: 50, Easing.Linear);
                await Task.Delay(10);
                await btnCerrar.ScaleTo(1, length: 50, Easing.Linear);

                await Navigation.PopModalAsync();
            };
            btnCerrar.GestureRecognizers.Add(clickClose);

        }
    }
}