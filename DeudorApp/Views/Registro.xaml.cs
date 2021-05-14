using DeudorApp.Models;
using DeudorApp.ViewModels;
using System;
using System.Collections.Generic;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace DeudorApp.Views
{
    public partial class Registro : ContentPage
    {
        int TipoDeCuenta;
        SM sM = new SM();
        public Registro(int TipoCuenta)
        {
            InitializeComponent();
            Title = "Crear cuenta";
            TipoDeCuenta = TipoCuenta;


            var clickTos = new TapGestureRecognizer();
            clickTos.Tapped += async (s, e) =>
            {
                await Launcher.OpenAsync("https://deudorapp.com/condiciones.php");
            };
            tos.GestureRecognizers.Add(clickTos);

            var clickRegistrar = new TapGestureRecognizer();
            clickRegistrar.Tapped += async (s, e) =>
            {
                if (mail.Text.Equals("") || pass1.Text.Equals("") || pass2.Text.Equals("") || nombre.Text.Equals("") || apellidos.Text.Equals("") || /*telefono.Text.Equals("") ||*/ !switchT.IsToggled)
                {
                    await DisplayAlert("Error", "Tienes que llenar todos los campos o aceptar terminos y condiciones", "Ok");
                }
                else
                {
                    try
                    {

                        Perfil p = new Perfil();
                        p.Nombre = nombre.Text;
                        p.Apellidos = apellidos.Text;
                        p.Correo = mail.Text;
                        p.Clave = pass1.Text;

                        await sM.RegistrarUsuario(p);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine(ex);
                        await DisplayAlert("Error", "Ocurrió un error, inténtalo de nuevo mas tarde.", "Ok");
                    }
                }
            };
            btnRegistrarse.GestureRecognizers.Add(clickRegistrar);


            var clickRegresaLogin = new TapGestureRecognizer();
            clickRegresaLogin.Tapped += (s, e) =>
            {
                Navigation.PopAsync();
            };
            RegresaLogin.GestureRecognizers.Add(clickRegresaLogin);
        }

        public void cerrar()
        {
            Navigation.PopModalAsync();
        }
    }
}
