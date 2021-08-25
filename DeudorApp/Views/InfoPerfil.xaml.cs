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
        string claveOficial = "";
        public InfoPerfil()
        {
            InitializeComponent();
            Title = "Mi Perfil";

            var actualizaContra = new TapGestureRecognizer();
            actualizaContra.Tapped += async (s, e) =>
            {
                await Navigation.PushModalAsync(new ActualizaContraseña());
            };
            btnActualizarContra.GestureRecognizers.Add(actualizaContra);


            ICommand refreshCommand = new Command(() =>
            {
                scrollV.IsEnabled = false;
                _ = TraerCreditos();
                _ = TraerDatosPerfil();

                refreshV.IsRefreshing = false;
            });
            refreshV.Command = refreshCommand;
            scrollV.IsEnabled = true;
            //refreshV.Content = scrollV;
            _ = TraerCreditos();
            _ = TraerDatosPerfil();

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
                        NavigationPage.SetHasBackButton(this, false);

                        Perfil p = new Perfil();
                        p.IdCuenta = Application.Current.Properties["IdCuenta"].ToString();
                        p.Nombre = txtNombre.Text;
                        p.Apellidos = txtApellidos.Text;
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
                            await Task.Delay(2000);
                            loader.IsVisible = false;
                            Contenido.IsVisible = true;
                            NavigationPage.SetHasBackButton(this, true);
                        }

                    }
                    catch (Exception ex)
                    {
                        await DisplayAlert("ERROR", ex.ToString(), "OK");
                    }
                }
                else
                {
                    string clave = Application.Current.Properties["Clave"].ToString();
                    txtNombre.Text = Application.Current.Properties["Nombre"].ToString();
                    txtApellidos.Text = Application.Current.Properties["Apellidos"].ToString();
                    txtTelefono.Text = Application.Current.Properties["NIT"].ToString();
                }
            };
            btnActualizar.GestureRecognizers.Add(clickActualiza);
        }
        public async Task TraerCreditos()
        {
            try
            {
                var resp = await sM.TraerCreditos();
                if (resp == "")
                {
                    Totales.Text = "0";
                    Usados.Text = "0";
                    Restantes.Text = "0";
                }
                else
                {
                    JArray obj = JArray.Parse(resp);
                    Totales.Text = obj[0]["Totales"].ToString();
                    Usados.Text = obj[0]["Usados"].ToString();
                    Restantes.Text = obj[0]["Restantes"].ToString();
                }   
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error",ex.ToString(),"Ok");
            }
        }

        public async Task TraerDatosPerfil()
        {
            try
            {
                var resp = await sM.TraerDatosPerfil();
                if (resp == "")
                {
                    
                }
                else
                {
                    JArray obj = JArray.Parse(resp);

                    string contra = obj[0]["Clave"].ToString();
                    claveOficial = obj[0]["Clave"].ToString();

                    txtCurp.Text = obj[0]["CURP"].ToString();
                    txtNombre.Text = obj[0]["Nombre"].ToString();
                    txtApellidos.Text = obj[0]["Apellidos"].ToString();
                    txtTelefono.Text = obj[0]["telefono"].ToString();
                    txtCorreo.Text = obj[0]["Correo"].ToString();

                    Application.Current.Properties["Nombre"] = obj[0]["Nombre"].ToString();
                    Application.Current.Properties["Apellidos"] = obj[0]["Apellidos"].ToString();
                    Application.Current.Properties["Clave"] = obj[0]["Clave"].ToString();
                    Application.Current.Properties["NIT"] = obj[0]["telefono"].ToString();
                    Application.Current.Properties["Correo"] = obj[0]["Correo"].ToString();
                    await Application.Current.SavePropertiesAsync();

                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Error", ex.ToString(), "Ok");
            }
        }

        public void Refresh()
        {
            txtCorreo.Text = Application.Current.Properties["Correo"].ToString();
            txtCurp.Text = Application.Current.Properties["CURP"].ToString();
            txtNombre.Text = Application.Current.Properties["Nombre"].ToString();
            txtApellidos.Text = Application.Current.Properties["Apellidos"].ToString();
            txtTelefono.Text = Application.Current.Properties["NIT"].ToString();
        }
    }
}