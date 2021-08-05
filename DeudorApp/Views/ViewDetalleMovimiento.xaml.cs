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

        }
    }
}