using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using DeudorApp.Models;
using DeudorApp.ViewModels;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace DeudorApp.Views
{
    public partial class InfoPerfil : ContentPage
    {
        SM sM = new SM();
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
            clickActualiza.Tapped += async (e, s) =>
            {
                bool aux = await DisplayAlert("Alerta", "¿Seguro que desea actualizar?", "SI", "NO");
                if (aux)
                {
                    try
                    {
                        loader.IsVisible = true;
                        Contenido.IsVisible = false;
                        
                        Perfil p = new Perfil();
                        p.IdCuenta = Application.Current.Properties["IdCuenta"].ToString();
                        p.Nombre = txtNombre.Text;
                        p.Apellidos = txtApellidos.Text;
                        p.Clave = txtContra.Text;
                        p.NIT = txtTelefono.Text;

                        string cons = await sM.ActualizaPerfilAsync(p);
                        

                        if (cons == "nada")
                        {
                            await DisplayAlert("Error", "No se pudieron guardar los cambios", "OK");
                            loader.IsVisible = false;
                            Contenido.IsVisible = true;
                        }
                        else
                        {
                            Application.Current.Properties["Nombre"] = p.Nombre;
                            Application.Current.Properties["Apellidos"] = p.Apellidos;
                            Application.Current.Properties["Clave"] = p.Clave;
                            Application.Current.Properties["NIT"] = p.NIT;
                            await Application.Current.SavePropertiesAsync();
                            await Task.Delay(3000);
                            loader.IsVisible = false;
                            Contenido.IsVisible = true;
                        }

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("ERROR", ex.ToString(), "OK");
                    }
                }
            };
            btnActualizar.GestureRecognizers.Add(clickActualiza);
        }
    }
}