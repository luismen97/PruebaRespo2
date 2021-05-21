using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class InfoPerfil : ContentPage
    {
        public InfoPerfil()
        {
            InitializeComponent();
            Title = "Mi Perfil";
            txtCorreo.Text = Application.Current.Properties["Correo"].ToString();
            txtCurp.Text = Application.Current.Properties["CURP"].ToString();
            txtNombre.Text = Application.Current.Properties["Nombre"].ToString();
            txtApellidos.Text = Application.Current.Properties["Apellidos"].ToString();
            txtContra.Text = Application.Current.Properties["Clave"].ToString();
            txtTelefono.Text = Application.Current.Properties["NIT"].ToString();

            var clickActualiza = new TapGestureRecognizer();
            clickActualiza.Tapped += (e, s) =>
            {
                DisplayAlert("Alerta","¿Seguro que desea actualizar?","SI","NO");
            };
            btnActualizar.GestureRecognizers.Add(clickActualiza);
        }
    }
}